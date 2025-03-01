using System;
using System.IO;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class UtilsTest
    {
        [Fact]
        public void CreateSupportedDevicesJson()
        {
            var option = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(option, out var onnruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }
            var result = Utils.CreateSupportedDevicesJson(onnruntime, out var supportedDevicesJson);
            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(supportedDevicesJson);
        }

        [Fact]
        public void GetVersion()
        {
            var version = Utils.GetVersion();
            Assert.NotNull(version);
        }

        [Fact]
        public void CreateAudioQueryFromAccentPhrases()
        {
            var accentPhrasesJson = $$"""[{"moras":[{"text":"コ","consonant":"k","consonant_length":0.0,"vowel":"o","vowel_length":0.0,"pitch":0.0},{"text":"ン","consonant":null,"consonant_length":null,"vowel":"N","vowel_length":0.0,"pitch":0.0},{"text":"ニ","consonant":"n","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"チ","consonant":"ch","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"ワ","consonant":"w","consonant_length":0.0,"vowel":"a","vowel_length":0.0,"pitch":0.0}],"accent":5,"pause_mora":null,"is_interrogative":false}]""";
            var result = Utils.CreateAudioQueryFromAccentPhrases(accentPhrasesJson, out var outputAudioQueryJson);
            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(outputAudioQueryJson);
            Assert.Equal($$"""{"accent_phrases":[{"moras":[{"text":"コ","consonant":"k","consonant_length":0.0,"vowel":"o","vowel_length":0.0,"pitch":0.0},{"text":"ン","consonant":null,"consonant_length":null,"vowel":"N","vowel_length":0.0,"pitch":0.0},{"text":"ニ","consonant":"n","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"チ","consonant":"ch","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"ワ","consonant":"w","consonant_length":0.0,"vowel":"a","vowel_length":0.0,"pitch":0.0}],"accent":5,"pause_mora":null,"is_interrogative":false}],"speedScale":1.0,"pitchScale":0.0,"intonationScale":1.0,"volumeScale":1.0,"prePhonemeLength":0.1,"postPhonemeLength":0.1,"outputSamplingRate":24000,"outputStereo":false,"kana":"コンニチワ'"}""", outputAudioQueryJson);
        }
    }
}
