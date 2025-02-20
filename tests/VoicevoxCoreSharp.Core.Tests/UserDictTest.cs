using System;
using System.IO;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class UserDictTest
    {
        [Fact]
        public void ToJSON()
        {
            var userDict = new UserDict();

            var result = userDict.ToJson(out var json);

            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.Equal("{}", json);
        }

        [Fact]
        public void Save()
        {
            var userDict = new UserDict();
            Assert.Equal(ResultCode.RESULT_OK, userDict.Save(Path.Combine(Helper.GetTestResultsDirectory(), $"{GetType().Name}_Save.json")));
        }

        [Fact]
        public void Load()
        {
            var userDict = new UserDict();
            Assert.Equal(ResultCode.RESULT_OK, userDict.Load(Path.Combine(Helper.GetTestFixturesDirectory(), "hoge_user_dict.json")));
        }

        [Fact]
        public void CreateDelete()
        {
            var userDict = new UserDict();

            var pronunciation = "ホゲ";
            uint priority = 10;
            nuint accentType = 2;
            var wordType = UserDictWordType.ADJECTIVE;

            var word = UserDictWord.Create("hoge", pronunciation, accentType);
            word.WordType = wordType;
            word.Priority = priority;

            var addResult = userDict.AddWord(word, out var wordUuid);
            Assert.Equal(ResultCode.RESULT_OK, addResult);
            Assert.NotNull(wordUuid);

            var result = userDict.ToJson(out var json);
            Assert.Equal(ResultCode.RESULT_OK, result);
            var expectedJson = $"{{\"{wordUuid}\":{{\"surface\":\"ｈｏｇｅ\",\"priority\":{priority},\"context_id\":20,\"part_of_speech\":\"形容詞\",\"part_of_speech_detail_1\":\"自立\",\"part_of_speech_detail_2\":\"*\",\"part_of_speech_detail_3\":\"*\",\"inflectional_type\":\"*\",\"inflectional_form\":\"*\",\"stem\":\"*\",\"yomi\":\"ホゲ\",\"pronunciation\":\"{pronunciation}\",\"accent_type\":{accentType},\"mora_count\":2,\"accent_associative_rule\":\"*\"}}}}";
            Assert.Equal(expectedJson, json);

            var removeResult = userDict.RemoveWord(wordUuid);
            Assert.Equal(ResultCode.RESULT_OK, removeResult);
            result = userDict.ToJson(out var removedJson);
            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.Equal("{}", removedJson);
        }
    }
}
