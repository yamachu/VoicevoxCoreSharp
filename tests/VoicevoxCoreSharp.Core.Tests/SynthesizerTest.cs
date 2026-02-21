using System;
using System.IO;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class SynthesizerTest
    {
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
            var loadResult = synthesizer.LoadVoiceModel(voiceModel);

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);
            Assert.NotEmpty(synthesizer.MetasJson);

            var ttsResult = synthesizer.Tts("こんにちは", 0, TtsOptions.Default(), out var wavLength, out var wav);

            Assert.Equal(ResultCode.RESULT_OK, ttsResult);
            Assert.True(wavLength > 0);
            Assert.NotNull(wav);
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
            synthesizer.LoadVoiceModel(voiceModel);

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
            synthesizer.LoadVoiceModel(voiceModel);

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

            var createSingFrameAudioResult = synthesizer.CreateSingFrameAudioQuery(score, 6000 /* singing_teacher */, out var frameAudioQueryJson);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameAudioResult);
            Assert.NotEmpty(frameAudioQueryJson);

            var createSingFrameF0 = synthesizer.CreateSingFrameF0(score, frameAudioQueryJson, 6000 /* singing_teacher */, out var frameF0Json);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameF0);
            Assert.NotEmpty(frameF0Json);

            var createSingFrameVolumeResult = synthesizer.CreateSingFrameVolume(score, frameAudioQueryJson, 6000 /* singing_teacher */, out var frameVolumeJson);
            Assert.Equal(ResultCode.RESULT_OK, createSingFrameVolumeResult);
            Assert.NotEmpty(frameVolumeJson);

            var frameSynthesisResult = synthesizer.FrameSynthesis(frameAudioQueryJson, 3000 /* frame_decode */, out var outputWavLength, out var outputWav);
            Assert.Equal(ResultCode.RESULT_OK, frameSynthesisResult);
            Assert.True(outputWavLength > 0);
            Assert.NotNull(outputWav);
        }
    }
}
