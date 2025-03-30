using System;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Struct
{
    /// <summary>
    /// voicevox_synthesizer_tts のオプション。
    /// </summary>
    public struct TtsOptions
    {
        /// <summary>
        /// 疑問文の調整を有効にする
        /// </summary>
        public bool EnableInterrogativeUpspeak { get; set; }

        public static TtsOptions Default()
        {
            return TtsOptionsDefault.Value;
        }
    }

    internal static class TtsOptionsDefault
    {
        public static readonly TtsOptions Value = CoreUnsafe.voicevox_make_default_tts_options().FromNative();
    }

    internal static class TtsOptionsExt
    {
        internal static VoicevoxTtsOptions ToNative(this TtsOptions ttsOptions)
        {
            return new VoicevoxTtsOptions
            {
                enable_interrogative_upspeak = ttsOptions.EnableInterrogativeUpspeak
            };
        }

        internal static TtsOptions FromNative(this VoicevoxTtsOptions ttsOptions)
        {
            return new TtsOptions
            {
                EnableInterrogativeUpspeak = ttsOptions.enable_interrogative_upspeak
            };
        }
    }
}
