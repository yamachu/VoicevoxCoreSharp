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
        private bool _disposed;

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

        public nuint Position { get; private set; }
        public nuint Length
        {
            get
            {
                ThrowIfDisposed();
                return _audioFeature.FrameLength;
            }
        }

        public bool EndOfStream => Position >= Length;

        public void Seek(nuint position)
        {
            ThrowIfDisposed();
            if (position > Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            Position = position;
        }

        public AudioChunk Read(nuint frameCount)
        {
            ThrowIfDisposed();
            if (frameCount == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frameCount));
            }

            var startInclusive = Position;
            if (startInclusive >= Length)
            {
                return new AudioChunk(ReadOnlyMemory<byte>.Empty, Length, Length, true);
            }

            var remainingFrames = Length - startInclusive;
            var framesToRead = frameCount > remainingFrames ? remainingFrames : frameCount;
            var endExclusive = startInclusive + framesToRead;

            var result = _synthesizer.Render(_audioFeature, startInclusive, endExclusive, out _, out var outputPcm);
            if (result != ResultCode.RESULT_OK)
            {
                throw new VoicevoxCoreResultException(result);
            }

            Position = endExclusive;
            return new AudioChunk(outputPcm ?? Array.Empty<byte>(), startInclusive, endExclusive, Position >= Length);
        }

        public Task<AudioChunk> ReadAsync(nuint frameCount, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return Read(frameCount);
            }, cancellationToken);
        }

        public async IAsyncEnumerable<AudioChunk> ReadAllAsync(
            nuint frameCount,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (frameCount == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frameCount));
            }

            while (!EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await ReadAsync(frameCount, cancellationToken).ConfigureAwait(false);
            }
        }

        public async IAsyncEnumerable<AudioChunk> ReadAllAsync(
            Func<AudioFeatureReader, nuint> frameCountProvider,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            if (frameCountProvider == null)
            {
                throw new ArgumentNullException(nameof(frameCountProvider));
            }

            while (!EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var frameCount = frameCountProvider(this);
                if (frameCount == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(frameCountProvider));
                }

                yield return await ReadAsync(frameCount, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
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

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(AudioFeatureReader));
            }
        }
    }
}
