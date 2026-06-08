using System;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Enum
{
    /// <summary>
    /// ::voicevox_synthesizer_load_voice_model の実行時に、同じIDの ::VoicevoxVoiceModelFile が既に読み込まれていたときのふるまい。
    /// </summary>
    public enum OnExistingVoiceModelId : int
    {
        /// <summary>
        /// エラーにする。デフォルトのふるまい
        /// </summary>
        VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR = 0,
        /// <summary>
        /// 再読み込みする。VOICEVOX COREでは、長文のテキストを一度に音声合成するとCPU/GPUメモリが大量に占有されたままになる。再読み込みを行うとメモリの使用量が元に戻る
        /// </summary>
        VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD = 1,
        /// <summary>
        /// 何もしない
        /// </summary>
        VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP = 2,
    }

    internal static class OnExistingVoiceModelIdExt
    {
        public static OnExistingVoiceModelId FromNative(this VoicevoxOnExistingVoiceModelId mode)
        {
#pragma warning disable CS8524
            return mode switch
            {
                VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR => OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR,
                VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD => OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD,
                VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP => OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP,
            };
#pragma warning restore CS8524
        }

        public static VoicevoxOnExistingVoiceModelId ToNative(this OnExistingVoiceModelId mode)
        {
#pragma warning disable CS8524
            return mode switch
            {
                OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR => VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR,
                OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD => VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD,
                OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP => VoicevoxOnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP,
            };
#pragma warning restore CS8524
        }
    }
}
