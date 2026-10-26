using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core
{
    internal class AudioFeatureHandle : SafeHandle
    {
        public AudioFeatureHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                CoreUnsafe.voicevox_audio_feature_delete((VoicevoxAudioFeature*)handle.ToPointer());
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator VoicevoxAudioFeature*(AudioFeatureHandle handle) => (VoicevoxAudioFeature*)handle.handle.ToPointer();
    }

    public class AudioFeature : IDisposable
    {
        internal AudioFeatureHandle Handle { get; private set; }
        private bool _disposed = false;

        internal unsafe AudioFeature(VoicevoxAudioFeature* audioFeature)
        {
            Handle = new AudioFeatureHandle(new IntPtr(audioFeature));
        }

        public nuint FrameLength
        {
            get
            {
                unsafe
                {
                    var audioFeature = AcquireHandle();
                    try
                    {
                        return CoreUnsafe.voicevox_audio_feature_len(audioFeature);
                    }
                    finally
                    {
                        Handle.DangerousRelease();
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
                    var audioFeature = AcquireHandle();
                    try
                    {
                        return CoreUnsafe.voicevox_audio_feature_is_empty(audioFeature);
                    }
                    finally
                    {
                        Handle.DangerousRelease();
                    }
                }
            }
        }

        public AudioFeatureSlice Slice(bool clamp = false, bool allowStartExceedsEnd = false)
        {
            unsafe
            {
                var audioFeature = AcquireHandle();
                try
                {
                    var slice = CoreUnsafe.voicevox_audio_feature_slice_new(audioFeature, clamp, allowStartExceedsEnd);
                    return new AudioFeatureSlice(this, slice);
                }
                finally
                {
                    Handle.DangerousRelease();
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
                var audioFeature = AcquireHandle();
                try
                {
                    VoicevoxAudioFeatureSlice nativeSlice;
                    var result = CoreUnsafe.voicevox_audio_feature_slice(
                        audioFeature,
                        startInclusive,
                        end,
                        endIsInclusive,
                        clamp,
                        allowStartExceedsEnd,
                        &nativeSlice).FromNative();
                    slice = result == ResultCode.RESULT_OK ? new AudioFeatureSlice(this, nativeSlice) : null;
                    return result;
                }
                finally
                {
                    Handle.DangerousRelease();
                }
            }
        }

        internal unsafe VoicevoxAudioFeature* AcquireHandle()
        {
            if (Handle == null || Handle.IsInvalid)
            {
                throw new ObjectDisposedException(nameof(AudioFeature));
            }

            var addedReference = false;
            Handle.DangerousAddRef(ref addedReference);
            if (!addedReference)
            {
                throw new ObjectDisposedException(nameof(AudioFeature));
            }

            return (VoicevoxAudioFeature*)Handle.DangerousGetHandle().ToPointer();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Handle != null && !Handle.IsInvalid)
                    {
                        Handle.Dispose();
                    }
                }

                _disposed = true;
            }
        }
    }
}
