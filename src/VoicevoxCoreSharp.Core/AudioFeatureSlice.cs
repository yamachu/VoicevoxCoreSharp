using System;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core
{
    public sealed class AudioFeatureSlice
    {
        private readonly AudioFeature _audioFeature;
        private readonly VoicevoxAudioFeatureSlice _nativeSlice;

        internal AudioFeatureSlice(AudioFeature audioFeature, VoicevoxAudioFeatureSlice nativeSlice)
        {
            _audioFeature = audioFeature;
            _nativeSlice = nativeSlice;
        }

        public AudioFeature AudioFeature
        {
            get
            {
                AcquireNative();
                try
                {
                    return _audioFeature;
                }
                finally
                {
                    ReleaseNative();
                }
            }
        }

        public nuint StartOffset
        {
            get
            {
                AcquireNative();
                try
                {
                    return _nativeSlice.start_offset;
                }
                finally
                {
                    ReleaseNative();
                }
            }
        }

        public nuint FrameLength
        {
            get
            {
                unsafe
                {
                    var nativeSlice = AcquireNative();
                    try
                    {
                        nuint frameLength;
                        var result = CoreUnsafe.voicevox_audio_feature_slice_len(nativeSlice, &frameLength);
                        if (result != VoicevoxResultCode.VOICEVOX_RESULT_OK)
                        {
                            throw new InvalidOperationException($"Failed to get audio feature slice length: {result}.");
                        }

                        return frameLength;
                    }
                    finally
                    {
                        ReleaseNative();
                    }
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                unsafe
                {
                    var nativeSlice = AcquireNative();
                    try
                    {
                        bool isEmpty;
                        var result = CoreUnsafe.voicevox_audio_feature_slice_is_empty(nativeSlice, &isEmpty);
                        if (result != VoicevoxResultCode.VOICEVOX_RESULT_OK)
                        {
                            throw new InvalidOperationException($"Failed to check whether the audio feature slice is empty: {result}.");
                        }

                        return isEmpty;
                    }
                    finally
                    {
                        ReleaseNative();
                    }
                }
            }
        }

        public ResultCode Slice(
            nuint startInclusive,
            nuint end,
            bool endIsInclusive,
            bool clamp,
            bool allowStartExceedsEnd,
            out AudioFeatureSlice? slice)
        {
            unsafe
            {
                var nativeSlice = AcquireNative();
                try
                {
                    VoicevoxAudioFeatureSlice output;
                    var result = CoreUnsafe.voicevox_audio_feature_slice_slice(
                        nativeSlice,
                        startInclusive,
                        end,
                        endIsInclusive,
                        clamp,
                        allowStartExceedsEnd,
                        &output).FromNative();
                    slice = result == ResultCode.RESULT_OK ? new AudioFeatureSlice(_audioFeature, output) : null;
                    return result;
                }
                finally
                {
                    ReleaseNative();
                }
            }
        }

        internal unsafe VoicevoxAudioFeatureSlice AcquireNative()
        {
            _audioFeature.AcquireHandle();
            if (_nativeSlice.audio_feature == null)
            {
                ReleaseNative();
                throw new ObjectDisposedException(nameof(AudioFeatureSlice));
            }

            return _nativeSlice;
        }

        internal void ReleaseNative()
        {
            _audioFeature.Handle.DangerousRelease();
        }
    }
}
