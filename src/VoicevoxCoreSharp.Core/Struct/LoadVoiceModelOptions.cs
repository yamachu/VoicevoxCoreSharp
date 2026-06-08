using System;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Struct
{
    /// <summary>
    /// ::voicevox_synthesizer_load_voice_model のオプション。
    /// </summary>
    public struct LoadVoiceModelOptions
    {
        /// <summary>
        /// 同じIDの ::VoicevoxVoiceModelFile が既に読み込まれていたときのふるまい
        /// </summary>
        public OnExistingVoiceModelId OnExisting { get; set; }

        public static LoadVoiceModelOptions Default()
        {
            return LoadVoiceModelOptionsDefault.Value;
        }
    }

    internal static class LoadVoiceModelOptionsDefault
    {
        public static readonly LoadVoiceModelOptions Value = CoreUnsafe.voicevox_make_default_load_voice_model_options().FromNative();
    }

    internal static class LoadVoiceModelOptionsExt
    {
        internal static VoicevoxLoadVoiceModelOptions ToNative(this LoadVoiceModelOptions loadVoiceModelOptions)
        {
            return new VoicevoxLoadVoiceModelOptions
            {
                on_existing = loadVoiceModelOptions.OnExisting.ToNative()
            };
        }

        internal static LoadVoiceModelOptions FromNative(this VoicevoxLoadVoiceModelOptions loadVoiceModelOptions)
        {
            return new LoadVoiceModelOptions
            {
                OnExisting = loadVoiceModelOptions.on_existing.FromNative()
            };
        }
    }
}
