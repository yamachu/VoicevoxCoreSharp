using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class SynthesizerExtensions
    {
        [NonBlocking]
        public static partial Task LoadVoiceModelAsync(this Synthesizer synthesizer, VoiceModelFile voiceModel);

        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> SynthesisAsync(this Synthesizer synthesizer, string audioQueryJson, uint styleId, SynthesisOptions options);

        [NonBlocking]
        public static partial Task<string> CreateAudioQueryAsync(this Synthesizer synthesizer, string text, uint styleId);

        [NonBlocking]
        public static partial Task<string> CreateAudioQueryFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId);

        [NonBlocking]
        public static partial Task<string> CreateAccentPhrasesAsync(this Synthesizer synthesizer, string text, uint styleId);

        [NonBlocking]
        public static partial Task<string> CreateAccentPhrasesFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId);

        [NonBlocking]
        public static partial Task<string> ReplaceMoraDataAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        [NonBlocking]
        public static partial Task<string> ReplacePhonemeLengthAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        [NonBlocking]
        public static partial Task<string> ReplaceMoraPitchAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> TtsFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId, TtsOptions options);

        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> TtsAsync(this Synthesizer synthesizer, string text, uint styleId, TtsOptions options);

        [NonBlocking]
        public static partial Task<string> CreateSingFrameAudioQueryAsync(this Synthesizer synthesizer, string scoreJson, uint styleId);

        [NonBlocking]
        public static partial Task<string> CreateSingFrameF0Async(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, uint styleId);

        [NonBlocking]
        public static partial Task<string> CreateSingFrameVolumeAsync(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, uint styleId);

        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> FrameSynthesisAsync(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, uint styleId);
    }
}
