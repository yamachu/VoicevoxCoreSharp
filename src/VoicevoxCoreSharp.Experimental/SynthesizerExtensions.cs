using System;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;
using VoicevoxCoreSharp.Experimental.Exception;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class SynthesizerExtensions
    {
        [NonBlocking]
        public static partial Task LoadVoiceModelAsync(this Synthesizer synthesizer, VoiceModelFile voiceModel, LoadVoiceModelOptions options);

        [Obsolete("Use SynthesisAsync(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions options) instead.")]
        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> SynthesisAsync(this Synthesizer synthesizer, string audioQueryJson, uint styleId, SynthesisOptions options);

        public static Task<(nuint outputWavLength, byte[] outputWav)> SynthesisAsync(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions options)
        {
            return SynthesisAsync(synthesizer, audioQueryJson, styleId.Value, options);
        }

        [Obsolete("Use CreateAudioQueryAsync(this Synthesizer synthesizer, string text, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateAudioQueryAsync(this Synthesizer synthesizer, string text, uint styleId);

        public static Task<string> CreateAudioQueryAsync(this Synthesizer synthesizer, string text, StyleId styleId)
        {
            return CreateAudioQueryAsync(synthesizer, text, styleId.Value);
        }

        [Obsolete("Use CreateAudioQueryFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateAudioQueryFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId);

        public static Task<string> CreateAudioQueryFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId)
        {
            return CreateAudioQueryFromKanaAsync(synthesizer, kana, styleId.Value);
        }

        [Obsolete("Use CreateAccentPhrasesAsync(this Synthesizer synthesizer, string text, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateAccentPhrasesAsync(this Synthesizer synthesizer, string text, uint styleId);

        public static Task<string> CreateAccentPhrasesAsync(this Synthesizer synthesizer, string text, StyleId styleId)
        {
            return CreateAccentPhrasesAsync(synthesizer, text, styleId.Value);
        }

        [Obsolete("Use CreateAccentPhrasesFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateAccentPhrasesFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId);

        public static Task<string> CreateAccentPhrasesFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId)
        {
            return CreateAccentPhrasesFromKanaAsync(synthesizer, kana, styleId.Value);
        }

        [Obsolete("Use ReplaceMoraDataAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> ReplaceMoraDataAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        public static Task<string> ReplaceMoraDataAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId)
        {
            return ReplaceMoraDataAsync(synthesizer, accentPhrasesJson, styleId.Value);
        }

        [Obsolete("Use ReplacePhonemeLengthAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> ReplacePhonemeLengthAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        public static Task<string> ReplacePhonemeLengthAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId)
        {
            return ReplacePhonemeLengthAsync(synthesizer, accentPhrasesJson, styleId.Value);
        }

        [Obsolete("Use ReplaceMoraPitchAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> ReplaceMoraPitchAsync(this Synthesizer synthesizer, string accentPhrasesJson, uint styleId);

        public static Task<string> ReplaceMoraPitchAsync(this Synthesizer synthesizer, string accentPhrasesJson, StyleId styleId)
        {
            return ReplaceMoraPitchAsync(synthesizer, accentPhrasesJson, styleId.Value);
        }

        [Obsolete("Use TtsFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId, TtsOptions options) instead.")]
        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> TtsFromKanaAsync(this Synthesizer synthesizer, string kana, uint styleId, TtsOptions options);

        public static Task<(nuint outputWavLength, byte[] outputWav)> TtsFromKanaAsync(this Synthesizer synthesizer, string kana, StyleId styleId, TtsOptions options)
        {
            return TtsFromKanaAsync(synthesizer, kana, styleId.Value, options);
        }

        [Obsolete("Use TtsAsync(this Synthesizer synthesizer, string text, StyleId styleId, TtsOptions options) instead.")]
        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> TtsAsync(this Synthesizer synthesizer, string text, uint styleId, TtsOptions options);

        public static Task<(nuint outputWavLength, byte[] outputWav)> TtsAsync(this Synthesizer synthesizer, string text, StyleId styleId, TtsOptions options)
        {
            return TtsAsync(synthesizer, text, styleId.Value, options);
        }

        [Obsolete("Use CreateSingFrameAudioQueryAsync(this Synthesizer synthesizer, string scoreJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateSingFrameAudioQueryAsync(this Synthesizer synthesizer, string scoreJson, uint styleId);

        public static Task<string> CreateSingFrameAudioQueryAsync(this Synthesizer synthesizer, string scoreJson, StyleId styleId)
        {
            return CreateSingFrameAudioQueryAsync(synthesizer, scoreJson, styleId.Value);
        }

        [Obsolete("Use CreateSingFrameF0Async(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateSingFrameF0Async(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, uint styleId);

        public static Task<string> CreateSingFrameF0Async(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, StyleId styleId)
        {
            return CreateSingFrameF0Async(synthesizer, scoreJson, frameAudioQueryJson, styleId.Value);
        }

        [Obsolete("Use CreateSingFrameVolumeAsync(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<string> CreateSingFrameVolumeAsync(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, uint styleId);

        public static Task<string> CreateSingFrameVolumeAsync(this Synthesizer synthesizer, string scoreJson, string frameAudioQueryJson, StyleId styleId)
        {
            return CreateSingFrameVolumeAsync(synthesizer, scoreJson, frameAudioQueryJson, styleId.Value);
        }

        [Obsolete("Use FrameSynthesisAsync(this Synthesizer synthesizer, string frameAudioQueryJson, StyleId styleId) instead.")]
        [NonBlocking]
        public static partial Task<(nuint outputWavLength, byte[] outputWav)> FrameSynthesisAsync(this Synthesizer synthesizer, string frameAudioQueryJson, uint styleId);

        public static Task<(nuint outputWavLength, byte[] outputWav)> FrameSynthesisAsync(this Synthesizer synthesizer, string frameAudioQueryJson, StyleId styleId)
        {
            return FrameSynthesisAsync(synthesizer, frameAudioQueryJson, styleId.Value);
        }

        [NonBlocking]
        public static partial Task<(nuint outputPcmLength, byte[] outputPcm)> RenderAsync(this Synthesizer synthesizer, AudioFeature audioFeature, nuint startInclusive, nuint endExclusive);

        public static AudioFeatureReader CreateAudioFeatureReader(this Synthesizer synthesizer, AudioFeature audioFeature)
        {
            return new AudioFeatureReader(synthesizer, audioFeature);
        }

        [Obsolete("Use CreateAudioFeatureReader(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions synthesisOptions) instead.")]
        public static AudioFeatureReader CreateAudioFeatureReader(this Synthesizer synthesizer, string audioQueryJson, uint styleId, SynthesisOptions synthesisOptions)
        {
            return CreateAudioFeatureReader(synthesizer, audioQueryJson, new StyleId(styleId), synthesisOptions);
        }

        public static AudioFeatureReader CreateAudioFeatureReader(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions synthesisOptions)
        {
            var resultCode = synthesizer.CreateAudioFeature(audioQueryJson, styleId, synthesisOptions, out var audioFeature);
            if (resultCode != Core.Enum.ResultCode.RESULT_OK || audioFeature == null)
            {
                throw new VoicevoxCoreResultException(resultCode);
            }

            return new AudioFeatureReader(synthesizer, audioFeature, true);
        }

        [Obsolete("Use CreateAudioFeatureReaderAsync(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions synthesisOptions, CancellationToken cancellationToken = default) instead.")]
        public static Task<AudioFeatureReader> CreateAudioFeatureReaderAsync(this Synthesizer synthesizer, string audioQueryJson, uint styleId, SynthesisOptions synthesisOptions, CancellationToken cancellationToken = default)
        {
            return CreateAudioFeatureReaderAsync(synthesizer, audioQueryJson, new StyleId(styleId), synthesisOptions, cancellationToken);
        }

        public static Task<AudioFeatureReader> CreateAudioFeatureReaderAsync(this Synthesizer synthesizer, string audioQueryJson, StyleId styleId, SynthesisOptions synthesisOptions, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return CreateAudioFeatureReader(synthesizer, audioQueryJson, styleId, synthesisOptions);
            }, cancellationToken);
        }
    }
}
