using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core.Experimental
{
    public static class AsyncSynthesizer
    {
        public static Task<(ResultCode, Synthesizer)> NewAsync(OpenJtalk openJtalk, InitializeOptions options)
        {
            return Task.Run(() =>
            {
                var result = Synthesizer.New(openJtalk, options, out var synthesizer);
                return (result, synthesizer);
            });
        }

        public static Task<ResultCode> LoadVoiceModelAsync(this Synthesizer synthesizer, VoiceModel voiceModel)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.LoadVoiceModel(voiceModel);
                return result;
            });
        }

        public static Task<ResultCode> UnloadVoiceModelAsync(this Synthesizer synthesizer, string modelId)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.UnloadVoiceModel(modelId);
                return result;
            });
        }

        public static Task<(ResultCode, string?)> CreateAudioQueryAsync(this Synthesizer synthesizer, string text, uint styleId, AudioQueryOptions options)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.CreateAudioQuery(text, styleId, options, out var audioQuery);
                return (result, audioQuery);
            });
        }

        public static Task<(ResultCode, string?)> CreateAccentPhrasesAsync(this Synthesizer synthesizer, string text, uint styleId, AccentPhrasesOptions options)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.CreateAccentPhrases(text, styleId, options, out var accentPhrases);
                return (result, accentPhrases);
            });
        }

        public static Task<(ResultCode, string?)> ReplaceMoraDataAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.ReplaceMoraData(accentPhrasesJson, styleId, out var outputAccentPhrasesJson);
                return (result, outputAccentPhrasesJson);
            });
        }

        public static Task<(ResultCode, string?)> ReplacePhonemeLengthAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.ReplaceMoraData(accentPhrasesJson, styleId, out var outputAccentPhrasesJson);
                return (result, outputAccentPhrasesJson);
            });
        }

        public static Task<(ResultCode, string?)> ReplaceMoraPitchAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.ReplaceMoraData(accentPhrasesJson, styleId, out var outputAccentPhrasesJson);
                return (result, outputAccentPhrasesJson);
            });
        }

        public static Task<(ResultCode, nuint, byte[]?)> SynthesisAsync(this Synthesizer synthesizer, string audioQueryJson, uint styleId, SynthesisOptions options)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.Synthesis(audioQueryJson, styleId, options, out var outputWavLength, out var outputWav);
                return (result, outputWavLength, outputWav);
            });
        }

        public static Task<(ResultCode, nuint, byte[]?)> TtsAsync(this Synthesizer synthesizer, string text, uint styleId, TtsOptions options)
        {
            return Task.Run(() =>
            {
                var result = synthesizer.Tts(text, styleId, options, out var outputWavLength, out var outputWav);
                return (result, outputWavLength, outputWav);
            });
        }
    }
}
