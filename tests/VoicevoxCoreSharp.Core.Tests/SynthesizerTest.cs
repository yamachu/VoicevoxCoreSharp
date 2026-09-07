using System;
using System.IO;
using System.Text.Json;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class SynthesizerTest
    {
        private static readonly StyleId DefaultStyleId = new StyleId(0);
        private static readonly StyleId SingingTeacherStyleId = new StyleId(6000);
        private static readonly StyleId FrameDecodeStyleId = new StyleId(3000);
        private static readonly StyleId StreamingTalkStyleId = new StyleId(302);

        [Fact]
        public void Tts()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            var synthesizerResult = Synthesizer.New(onnruntime, openJtalk, initializeOptions, out var synthesizer);

            VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            var loadResult = synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);
            Assert.NotEmpty(synthesizer.MetasJson);

            var ttsResult = synthesizer.Tts("こんにちは", DefaultStyleId, TtsOptions.Default(), out var wavLength, out var wav);

            Assert.Equal(ResultCode.RESULT_OK, ttsResult);
            Assert.True(wavLength > 0);
            Assert.NotNull(wav);
        }

        [Fact]
        public void SynthesisByRender()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            var synthesizerResult = Synthesizer.New(onnruntime, openJtalk, initializeOptions, out var synthesizer);

            VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            var loadResult = synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);
            Assert.NotEmpty(synthesizer.MetasJson);

            var createAudioQueryResult = synthesizer.CreateAudioQuery("こんにちは？", StreamingTalkStyleId, out var audioQueryJson);
            Assert.Equal(ResultCode.RESULT_OK, createAudioQueryResult);
            Assert.NotNull(audioQueryJson);

            var synthesisResult = synthesizer.Synthesis(audioQueryJson, StreamingTalkStyleId, SynthesisOptions.Default(), out var wavLength, out var wav);
            Assert.Equal(ResultCode.RESULT_OK, synthesisResult);
            Assert.True(wavLength > 0);
            Assert.NotNull(wav);

            var createAudioFeatureResult = synthesizer.CreateAudioFeature(audioQueryJson, StreamingTalkStyleId, SynthesisOptions.Default(), out var audioFeature);
            Assert.Equal(ResultCode.RESULT_OK, createAudioFeatureResult);
            Assert.NotNull(audioFeature);

            var renderResult = synthesizer.Render(audioFeature, 0, audioFeature.FrameLength, out var pcmLength, out var pcm);
            Assert.Equal(ResultCode.RESULT_OK, renderResult);
            Assert.True(pcmLength > 0);
            Assert.NotNull(pcm);

            Assert.True(audioFeature.FrameLength > 1);
            var splitFrameIndex = audioFeature.FrameLength / 2;
            Assert.True(splitFrameIndex > 0);
            Assert.True(splitFrameIndex < audioFeature.FrameLength);

            var firstRenderResult = synthesizer.Render(audioFeature, 0, splitFrameIndex, out var firstPcmLength, out var firstPcm);
            Assert.Equal(ResultCode.RESULT_OK, firstRenderResult);
            Assert.True(firstPcmLength > 0);
            Assert.NotNull(firstPcm);

            var secondRenderResult = synthesizer.Render(audioFeature, splitFrameIndex, audioFeature.FrameLength, out var secondPcmLength, out var secondPcm);
            Assert.Equal(ResultCode.RESULT_OK, secondRenderResult);
            Assert.True(secondPcmLength > 0);
            Assert.NotNull(secondPcm);

            var concatenatedPcm = new byte[(int)(firstPcmLength + secondPcmLength)];
            Array.Copy(firstPcm, 0, concatenatedPcm, 0, (int)firstPcmLength);
            Array.Copy(secondPcm, 0, concatenatedPcm, (int)firstPcmLength, (int)secondPcmLength);
            Assert.Equal(pcm, concatenatedPcm);

            using var audioQuery = JsonDocument.Parse(audioQueryJson);
            var outputSamplingRate = audioQuery.RootElement.GetProperty("outputSamplingRate").GetUInt32();
            var outputStereo = audioQuery.RootElement.GetProperty("outputStereo").GetBoolean();
            Utils.WavFromS16Le(pcmLength, pcm, outputSamplingRate, outputStereo, out var renderedWavLength, out var renderedWav);

            Assert.Equal(wavLength, renderedWavLength);
            Assert.Equal(wav, renderedWav);
        }

        [Fact]
        public void VoiceModelLoaded()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            Synthesizer.New(onnruntime, openJtalk, initializeOptions, out var synthesizer);

            VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());

            Assert.True(synthesizer.IsLoadedVoiceModel(voiceModel.Id));
            synthesizer.UnloadVoiceModel(voiceModel.Id);
            Assert.False(synthesizer.IsLoadedVoiceModel(voiceModel.Id));
        }

        [Fact]
        public void SingSynthesis()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnxruntime);
            Synthesizer.New(onnxruntime, openJtalk, initializeOptions, out var synthesizer);
            VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());

            var score = """
            {
                "tempo": 120,
                "notes": [
                    { "key": null, "frame_length": 15, "lyric": "" },
                    { "key": 60, "frame_length": 45, "lyric": "ド" },
                    { "key": 62, "frame_length": 45, "lyric": "レ" },
                    { "key": 64, "frame_length": 45, "lyric": "ミ" },
                    { "key": null, "frame_length": 15, "lyric": "" }
                ]
            }
            """.Trim();

            var createSingFrameAudioResult = synthesizer.CreateSingFrameAudioQuery(score, SingingTeacherStyleId, out var frameAudioQueryJson);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameAudioResult);
            Assert.NotEmpty(frameAudioQueryJson);

            var createSingFrameF0 = synthesizer.CreateSingFrameF0(score, frameAudioQueryJson, SingingTeacherStyleId, out var frameF0Json);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameF0);
            Assert.NotEmpty(frameF0Json);

            var createSingFrameVolumeResult = synthesizer.CreateSingFrameVolume(score, frameAudioQueryJson, SingingTeacherStyleId, out var frameVolumeJson);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameVolumeResult);
            Assert.NotEmpty(frameVolumeJson);

            var frameSynthesisResult = synthesizer.FrameSynthesis(frameAudioQueryJson, FrameDecodeStyleId, out var outputWavLength, out var outputWav);
            Assert.Equal(ResultCode.RESULT_OK, frameSynthesisResult);
            Assert.True(outputWavLength > 0);
            Assert.NotNull(outputWav);
        }
    }
}
