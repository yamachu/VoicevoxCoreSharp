using System;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class OpenJtalkTest
    {
        [Fact]
        public void Open()
        {
            var openResult = OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);

            Assert.Equal(ResultCode.RESULT_OK, openResult);
            Assert.NotNull(openJtalk);
        }

        [Fact]
        public void UseUserDict()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            // 空っぽの辞書をコンパイルしようとするとクラッシュするので足す
            var userDict = new UserDict();
            userDict.AddWord(UserDictWord.Create("hoge", "ホゲ", 0), out var _);

            var result = openJtalk.UseUserDict(userDict);

            Assert.Equal(ResultCode.RESULT_OK, result);
        }

        [Fact]
        public void Analyze()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);

            var result = openJtalk.Analyze("こんにちは", out var outputAccentPhrasesJson);

            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(outputAccentPhrasesJson);
            Assert.Equal($$"""[{"moras":[{"text":"コ","consonant":"k","consonant_length":0.0,"vowel":"o","vowel_length":0.0,"pitch":0.0},{"text":"ン","consonant":null,"consonant_length":null,"vowel":"N","vowel_length":0.0,"pitch":0.0},{"text":"ニ","consonant":"n","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"チ","consonant":"ch","consonant_length":0.0,"vowel":"i","vowel_length":0.0,"pitch":0.0},{"text":"ワ","consonant":"w","consonant_length":0.0,"vowel":"a","vowel_length":0.0,"pitch":0.0}],"accent":5,"pause_mora":null,"is_interrogative":false}]""", outputAccentPhrasesJson);
        }
    }
}
