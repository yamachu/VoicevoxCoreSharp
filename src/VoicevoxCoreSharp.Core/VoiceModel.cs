using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    internal class VoiceModelHandle : SafeHandle
    {
        public VoiceModelHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                CoreUnsafe.voicevox_voice_model_delete((VoicevoxVoiceModel*)handle.ToPointer());
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator VoicevoxVoiceModel*(VoiceModelHandle handle) => (VoicevoxVoiceModel*)handle.handle.ToPointer();
    }

    public class VoiceModel : IDisposable
    {
        internal VoiceModelHandle Handle { get; private set; }
        private bool _disposed = false;

        private unsafe VoiceModel(VoicevoxVoiceModel* modelHandle)
        {
            Handle = new VoiceModelHandle(new IntPtr(modelHandle));
        }

        public static ResultCode New(string modelPath, out VoiceModel voiceModel)
        {
            unsafe
            {
                var p = (VoicevoxVoiceModel*)IntPtr.Zero.ToPointer();
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(modelPath))
                {
                    var result = CoreUnsafe.voicevox_voice_model_new_from_path(ptr, &p).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        voiceModel = new VoiceModel(p);
                    }
                    else
                    {
                        voiceModel = new VoiceModel(null);
                    }

                    return result;
                }
            }
        }

        public string Id
        {
            get
            {
                unsafe
                {
                    var ptr = CoreUnsafe.voicevox_voice_model_id((VoicevoxVoiceModel*)Handle);
                    return StringConvertCompat.ToUTF8String(ptr);
                }
            }
        }

        public string MetasJson
        {
            get
            {
                unsafe
                {
                    var ptr = CoreUnsafe.voicevox_voice_model_get_metas_json((VoicevoxVoiceModel*)Handle);
                    return StringConvertCompat.ToUTF8String(ptr);
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
