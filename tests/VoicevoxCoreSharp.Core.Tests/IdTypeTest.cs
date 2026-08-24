using System;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class IdTypeTest
    {
        [Fact]
        public void StyleIdBehavesLikeValueObject()
        {
            var left = new StyleId(10);
            var right = (StyleId)10u;

            Assert.Equal(left, right);
            Assert.Equal("10", left.ToString());
            Assert.Equal(10u, (uint)left);
        }

        [Fact]
        public void VoiceModelIdBehavesLikeValueObject()
        {
            var guid = Guid.Parse("12345678-1234-5678-9abc-def012345678");
            var voiceModelId = new VoiceModelId(guid);
            var parsed = (VoiceModelId)"12345678-1234-5678-9abc-def012345678";

            Assert.Equal(voiceModelId, parsed);
            Assert.Equal(guid, (Guid)voiceModelId);
            Assert.Equal("12345678-1234-5678-9abc-def012345678", voiceModelId.ToString());
        }
    }
}
