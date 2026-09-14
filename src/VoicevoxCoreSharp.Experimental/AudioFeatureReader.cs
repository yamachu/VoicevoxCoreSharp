using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Experimental.Exception;

namespace VoicevoxCoreSharp.Experimental
{
    public sealed class AudioFeatureReader : IDisposable
    {
        private readonly Synthesizer _synthesizer;
        private readonly AudioFeature _audioFeature;
        private readonly bool _ownsAudioFeature;
        private readonly object _gate = new object();
        private readonly SemaphoreSlim _readLock = new SemaphoreSlim(1, 1);
        private bool _disposed;
        private nuint _position;

        public AudioFeatureReader(Synthesizer synthesizer, AudioFeature audioFeature)
            : this(synthesizer, audioFeature, false)
        {
        }

        internal AudioFeatureReader(Synthesizer synthesizer, AudioFeature audioFeature, bool ownsAudioFeature)
        {
            _synthesizer = synthesizer ?? throw new ArgumentNullException(nameof(synthesizer));
            _audioFeature = audioFeature ?? throw new ArgumentNullException(nameof(audioFeature));
            _ownsAudioFeature = ownsAudioFeature;
        }

        public nuint Position
        {
            get
            {
                lock (_gate)
                {
                    ThrowIfDisposed();
                    return _position;
                }
            }
        }

        public nuint Length
        {
            get
            {
                lock (_gate)
                {
                    ThrowIfDisposed();
                    return _audioFeature.FrameLength;
                }
            }
        }

        public bool EndOfStream
        {
            get
            {
                lock (_gate)
                {
                    ThrowIfDisposed();
                    return _position >= _audioFeature.FrameLength;
                }
            }
        }

        public void Seek(nuint position)
        {
            _readLock.Wait();
            try
            {
                lock (_gate)
                {
                    ThrowIfDisposed();
                    if (position > _audioFeature.FrameLength)
                    {
                        throw new ArgumentOutOfRangeException(nameof(position));
                    }

                    _position = position;
                }
            }
            finally
            {
                _readLock.Release();
            }
        }

        public AudioChunk Read(nuint frameCount)
        {
            if (frameCount == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frameCount));
            }

            _readLock.Wait();
            try
            {
                nuint startInclusive;
                nuint endExclusive;
                nuint frameLength;

                lock (_gate)
                {
                    ThrowIfDisposed();
                    startInclusive = _position;
                    frameLength = _audioFeature.FrameLength;
                    if (startInclusive >= frameLength)
                    {
                        return new AudioChunk(ReadOnlyMemory<byte>.Empty, frameLength, frameLength, true);
                    }

                    var remainingFrames = frameLength - startInclusive;
                    var framesToRead = frameCount > remainingFrames ? remainingFrames : frameCount;
                    endExclusive = startInclusive + framesToRead;
                }

                var result = _synthesizer.Render(_audioFeature, startInclusive, endExclusive, out _, out var outputPcm);
                if (result != ResultCode.RESULT_OK)
                {
                    throw new VoicevoxCoreResultException(result);
                }

                lock (_gate)
                {
                    ThrowIfDisposed();
                    _position = endExclusive;
                    return new AudioChunk(outputPcm ?? Array.Empty<byte>(), startInclusive, endExclusive, _position >= frameLength);
                }
            }
            finally
            {
                _readLock.Release();
            }
        }

        public Task<AudioChunk> ReadAsync(nuint frameCount, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Read(frameCount));
        }

        public async IAsyncEnumerable<AudioChunk> ReadAllAsync(
            nuint frameCount,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (frameCount == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frameCount), "frameCount must be greater than zero.");
            }

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var chunk = await ReadAsync(frameCount, cancellationToken).ConfigureAwait(false);
                if (chunk.IsEndOfStream && chunk.Pcm.IsEmpty)
                {
                    yield break;
                }

                yield return chunk;
            }
        }

        public async IAsyncEnumerable<AudioChunk> ReadAllAsync(
            Func<AudioFeatureReader, nuint> frameCountProvider,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (frameCountProvider == null)
            {
                throw new ArgumentNullException(nameof(frameCountProvider));
            }

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var frameCount = frameCountProvider(this);
                if (frameCount == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(frameCountProvider), "frameCountProvider must return a value greater than zero.");
                }

                var chunk = await ReadAsync(frameCount, cancellationToken).ConfigureAwait(false);
                if (chunk.IsEndOfStream && chunk.Pcm.IsEmpty)
                {
                    yield break;
                }

                yield return chunk;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            _readLock.Wait();
            try
            {
                lock (_gate)
                {
                    if (!_disposed)
                    {
                        if (disposing && _ownsAudioFeature)
                        {
                            _audioFeature.Dispose();
                        }

                        _disposed = true;
                    }
                }
            }
            finally
            {
                _readLock.Release();
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(AudioFeatureReader));
            }
        }
    }
}
