using System;
using VoicevoxCoreSharp.Core.Enum;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class ResultCodeTest
    {
        [Fact]
        public void ToMessage()
        {
            Assert.NotEmpty(ResultCode.RESULT_MODEL_ALREADY_LOADED_ERROR.ToMessage());
        }
    }
}
