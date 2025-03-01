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

        public static ResultCode CreateAudioQueryFromAccentPhrases(string accentPhrasesJson, out string? outputAudioQueryJson)
        {
            unsafe
            {
                byte* outputAudioQueryJsonPtr;
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(accentPhrasesJson))
                {
                    var result = CoreUnsafe.voicevox_audio_query_create_from_accent_phrases(ptr, &outputAudioQueryJsonPtr);
                    if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                    {
                        outputAudioQueryJson = StringConvertCompat.ToUTF8String(outputAudioQueryJsonPtr);
                        CoreUnsafe.voicevox_json_free(outputAudioQueryJsonPtr);
                    }
                    else
                    {
                        outputAudioQueryJson = null;
                    }
                    return result.FromNative();
                }
            }
        }
    }
}
