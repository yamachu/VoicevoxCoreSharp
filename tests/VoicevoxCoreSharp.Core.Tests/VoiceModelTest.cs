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
            var openResult = VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            Assert.Equal(ResultCode.RESULT_OK, openResult);
            Assert.NotNull(voiceModel);

            Assert.NotEmpty(voiceModel.Id);
            Assert.NotEmpty(voiceModel.MetasJson);
        }

        [Fact]
        public void AccessMemberAfterDisposed()
        {
            var _ = VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            using (voiceModel) { }

            Assert.NotEmpty(voiceModel.Id);
            Assert.NotEmpty(voiceModel.MetasJson);

            Assert.True(voiceModel.Id.Length > 0);
            Assert.True(voiceModel.MetasJson.Length > 0);
        }
    }
}
