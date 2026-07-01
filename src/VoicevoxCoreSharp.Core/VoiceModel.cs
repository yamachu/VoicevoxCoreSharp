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
                CoreUnsafe.voicevox_voice_model_file_delete((VoicevoxVoiceModelFile*)handle.ToPointer());
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

        public VoiceModelId Id { get; private set; }

        public string MetasJson { get; private set; }

        private VoiceModelFile()
        {
            Handle = new VoiceModelFileHandle(IntPtr.Zero);
            Id = new VoiceModelId(string.Empty);
            MetasJson = string.Empty;
        }

        private unsafe VoiceModelFile(VoicevoxVoiceModelFile* modelHandle, string id, string metasJson)
        {
            Handle = new VoiceModelFileHandle(new IntPtr(modelHandle));
            Id = new VoiceModelId(id);
            MetasJson = metasJson;
        }

        [Obsolete("Use VoiceModelFile.Open(string modelPath, out VoiceModelFile voiceModel) instead.")]
        public static ResultCode New(string modelPath, out VoiceModelFile voiceModel)
        {
            return Open(modelPath, out voiceModel);
        }

        public static ResultCode Open(string modelPath, out VoiceModelFile voiceModel)
        {
            unsafe
            {
                var p = (VoicevoxVoiceModelFile*)IntPtr.Zero.ToPointer();
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(modelPath))
                {
                    var result = CoreUnsafe.voicevox_voice_model_file_open(ptr, &p).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        byte* idPtr = stackalloc byte[16];
                        CoreUnsafe.voicevox_voice_model_file_id(p, idPtr);
                        var id = NativeUuid.ToUUIDv4(idPtr);

                        var jsonPtr = CoreUnsafe.voicevox_voice_model_file_create_metas_json(p);
                        var json = StringConvertCompat.ToUTF8String(jsonPtr);
                        CoreUnsafe.voicevox_json_free(jsonPtr);

                        voiceModel = new VoiceModelFile(p, id, json);
                    }
                    else
                    {
                        voiceModel = new VoiceModelFile();
                    }

                    return result;
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
