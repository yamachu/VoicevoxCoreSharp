using System;
using System.IO;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class TtsTest
    {
        [Fact]
        public void TtsBySynthesis()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            var synthesizerResult = Synthesizer.New(onnruntime, openJtalk, initializeOptions, out var synthesizer);

            VoiceModelFile.New(Consts.SampleVoiceModel, out var voiceModel);
            var loadResult = synthesizer.LoadVoiceModel(voiceModel);

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);
            Assert.NotEmpty(synthesizer.MetasJson);

            openJtalk.Analyze("こんにちは", out var outputAccentPhrasesJson);
            Utils.CreateAudioQueryFromAccentPhrases(outputAccentPhrasesJson!, out var outputAudioQueryJson);

            var synthesisResult = synthesizer.Synthesis(outputAudioQueryJson!, 0, SynthesisOptions.Default(), out var wavLength, out var wav);
            Assert.Equal(ResultCode.RESULT_OK, synthesisResult);
            Assert.True(wavLength > 0);
            Assert.NotNull(wav);
        }
    }
}
