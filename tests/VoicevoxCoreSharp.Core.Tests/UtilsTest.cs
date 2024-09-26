using System;
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
            var option = LoadOnnxruntimeOptions.Default();
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
    }
}
