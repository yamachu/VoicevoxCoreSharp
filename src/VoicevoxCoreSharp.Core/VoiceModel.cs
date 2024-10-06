using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    internal class VoiceModelFileHandle : SafeHandle
    {
        public VoiceModelFileHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                CoreUnsafe.voicevox_voice_model_file_close((VoicevoxVoiceModelFile*)handle.ToPointer());
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator VoicevoxVoiceModelFile*(VoiceModelFileHandle handle) => (VoicevoxVoiceModelFile*)handle.handle.ToPointer();
    }

    public class VoiceModelFile : IDisposable
    {
        internal VoiceModelFileHandle Handle { get; private set; }
        private bool _disposed = false;

        private unsafe VoiceModelFile(VoicevoxVoiceModelFile* modelHandle)
        {
            Handle = new VoiceModelFileHandle(new IntPtr(modelHandle));
        }

        public static ResultCode New(string modelPath, out VoiceModelFile voiceModel)
        {
            unsafe
            {
                var p = (VoicevoxVoiceModelFile*)IntPtr.Zero.ToPointer();
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(modelPath))
                {
                    var result = CoreUnsafe.voicevox_voice_model_file_open(ptr, &p).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        voiceModel = new VoiceModelFile(p);
                    }
                    else
                    {
                        voiceModel = new VoiceModelFile(null);
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
                    byte* ptr = stackalloc byte[16];
                    CoreUnsafe.voicevox_voice_model_file_id((VoicevoxVoiceModelFile*)Handle, ptr);
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
                    var ptr = CoreUnsafe.voicevox_voice_model_file_create_metas_json((VoicevoxVoiceModelFile*)Handle);
                    var json = StringConvertCompat.ToUTF8String(ptr);
                    CoreUnsafe.voicevox_json_free(ptr);
                    return json;
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
