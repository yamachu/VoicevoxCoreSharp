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
            // See: VoicevoxCoreSharp.Core.Tests.csproj
            var libraryPath = 0 switch
            {
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => "onnxruntime_another_path.dll",
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => "libonnxruntime_another_path.so",
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => "libonnxruntime_another_path.dylib",
                _ => throw new PlatformNotSupportedException()
            };
            var option = new LoadOnnxruntimeOptions(libraryPath);
            var result = Onnxruntime.LoadOnce(option, out var onnruntime);

            Assert.Equal(ResultCode.RESULT_OK, result);
            Assert.NotNull(onnruntime);
        }
    }
}
