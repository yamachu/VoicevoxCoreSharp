﻿using System;
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
            var onnxruntimeOptions = LoadOnnxruntimeOptions.Default();
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            var synthesizerResult = Synthesizer.New(onnruntime, openJtalk, initializeOptions, out var synthesizer);

            VoiceModel.New(Consts.SampleVoiceModel, out var voiceModel);
            var loadResult = synthesizer.LoadVoiceModel(voiceModel);

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);
            Assert.NotEmpty(synthesizer.MetasJson);

            var ttsResult = synthesizer.Tts("こんにちは", 0, TtsOptions.Default(), out var wavLength, out var wav);

            Assert.Equal(ResultCode.RESULT_OK, ttsResult);
            Assert.True(wavLength > 0);
            Assert.NotNull(wav);
        }
    }
}
