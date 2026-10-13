using System;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Struct
{
    public struct AudioQueryFrameLengthOptions
    {
        public AudioQueryFrameLengthOptions(bool enableInterrogativeUpspeak)
        {
            EnableInterrogativeUpspeak = enableInterrogativeUpspeak;
        }

        public bool EnableInterrogativeUpspeak { get; }

        public static AudioQueryFrameLengthOptions Default()
        {
            return AudioQueryFrameLengthOptionsDefault.Value;
        }
    }

    internal static class AudioQueryFrameLengthOptionsDefault
    {
        public static readonly AudioQueryFrameLengthOptions Value = CoreUnsafe.voicevox_make_default_audio_query_frame_length_options().FromNative();
    }

    internal static class AudioQueryFrameLengthOptionsExt
    {
        internal static AudioQueryFrameLengthOptions FromNative(this VoicevoxAudioQueryFrameLengthOptions voicevoxAudioQueryFrameLengthOptions)
        {
            unsafe
            {
                return new AudioQueryFrameLengthOptions(
                    voicevoxAudioQueryFrameLengthOptions.enable_interrogative_upspeak
                );
            }
        }

        internal static VoicevoxAudioQueryFrameLengthOptions ToNative(this AudioQueryFrameLengthOptions audioQueryFrameLengthOptions)
        {
            unsafe
            {
                return new VoicevoxAudioQueryFrameLengthOptions
                {
                    enable_interrogative_upspeak = audioQueryFrameLengthOptions.EnableInterrogativeUpspeak
                };
            }
        }
    }
}
