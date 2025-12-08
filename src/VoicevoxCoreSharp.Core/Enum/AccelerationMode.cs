using System;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Enum
{
    /// <summary>
    /// ハードウェアアクセラレーションモードを設定する設定値。
    /// </summary>
    public enum AccelerationMode : int
    {
        /// <summary>
        /// 実行環境に合った適切なハードウェアアクセラレーションモードを選択する
        /// </summary>
        AUTO = 0,
        /// <summary>
        /// ハードウェアアクセラレーションモードを"CPU"に設定する
        /// </summary>
        CPU = 1,
        /// <summary>
        /// ハードウェアアクセラレーションモードを"GPU"に設定する
        /// </summary>
        GPU = 2,
    }

    internal static class AccelerationModeExt
    {
        public static AccelerationMode FromNative(this VoicevoxAccelerationMode mode)
        {
#pragma warning disable CS8524
            return mode switch
            {
                VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_AUTO => AccelerationMode.AUTO,
                VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_CPU => AccelerationMode.CPU,
                VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_GPU => AccelerationMode.GPU,
            };
#pragma warning restore CS8524
        }

        public static VoicevoxAccelerationMode ToNative(this AccelerationMode mode)
        {
#pragma warning disable CS8524
            return mode switch
            {
                AccelerationMode.AUTO => VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_AUTO,
                AccelerationMode.CPU => VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_CPU,
                AccelerationMode.GPU => VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_GPU,
            };
#pragma warning restore CS8524
        }
    }
}
