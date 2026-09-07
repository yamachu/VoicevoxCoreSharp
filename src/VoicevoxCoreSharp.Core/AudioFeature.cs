using System;
using System.Runtime.InteropServices;
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
                    if (Handle == null || Handle.IsInvalid)
                    {
                        throw new ObjectDisposedException(nameof(AudioFeature));
                    }

                    return CoreUnsafe.voicevox_audio_feature_frame_length((VoicevoxAudioFeature*)Handle);
                }
            }
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
