using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class OnnxruntimeTest
    {
        [Fact]
        public void LoadOnce()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Assert.True(true, "TODO: GitHub Actionsでも試せるようにする");
            }
            var option = new LoadOnnxruntimeOptions("libonnxruntime_another_path.dylib");
            var result = Onnxruntime.LoadOnce(option, out var onnruntime);

            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(onnruntime);
        }
    }
}
