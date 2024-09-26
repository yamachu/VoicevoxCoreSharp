using System;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    public class Utils
    {
        public static ResultCode CreateSupportedDevicesJson(Onnxruntime onnxruntime, out string? supportedDevicesJson)
        {
            unsafe
            {
                byte* jsonPtr;
                var result = CoreUnsafe.voicevox_onnxruntime_create_supported_devices_json((VoicevoxOnnxruntime*)onnxruntime.Handle, &jsonPtr);
                if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                {
                    supportedDevicesJson = StringConvertCompat.ToUTF8String(jsonPtr);
                }
                else
                {
                    supportedDevicesJson = null;
                }

                CoreUnsafe.voicevox_json_free(jsonPtr);

                return result.FromNative();
            }
        }

        public static string GetVersion()
        {
            unsafe
            {
                return StringConvertCompat.ToUTF8String(CoreUnsafe.voicevox_get_version());
            }
        }
    }
}
