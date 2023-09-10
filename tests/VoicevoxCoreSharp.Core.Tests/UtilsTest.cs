using System;
using VoicevoxCoreSharp.Core.Enum;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class UtilsTest
    {
        [Fact]
        public void CreateSupportedDevicesJson()
        {
            var result = Utils.CreateSupportedDevicesJson(out var supportedDevicesJson);
            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(supportedDevicesJson);
        }

        [Fact]
        public void GetVersion()
        {
            var version = Utils.GetVersion();
            Assert.NotNull(version);
        }
    }
}
