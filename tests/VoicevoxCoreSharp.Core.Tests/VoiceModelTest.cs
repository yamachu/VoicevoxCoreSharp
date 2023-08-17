using System;
using VoicevoxCoreSharp.Core.Enum;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class VoiceModelTest
    {
        [Fact]
        public void Open()
        {
            var openResult = VoiceModel.New(Consts.SampleVoiceModel, out var voiceModel);
            Assert.Equal(ResultCode.RESULT_OK, openResult);
            Assert.NotNull(voiceModel);

            Assert.NotEmpty(voiceModel.Id);
            Assert.NotEmpty(voiceModel.MetasJson);
        }
    }
}
