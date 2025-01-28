using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class Helper
    {
        public static string GetProjectDirectory()
        {
            var maybeProjectDir = Environment.GetEnvironmentVariable("_TEST_PROJECT_DIR_");
            var projectDir = maybeProjectDir ?? Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", ".."));
            return projectDir;
        }

        public static string GetTestFixturesDirectory()
        {
            return Path.Combine(GetProjectDirectory(), "fixtures");
        }

        public static string GetBaseVoicevoxCoreDirectory()
        {
            return Path.Combine(GetProjectDirectory(), "..", "..", "binding", "voicevox_core");
        }

        public static string GetTestResultsDirectory()
        {
            return Path.Combine(GetProjectDirectory(), "results");
        }

        public static string GetOnnxruntimeAssemblyName()
        {
            return 0 switch
            {
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => "onnxruntime.dll",
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => "libonnxruntime.so",
                var _ when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => "libonnxruntime.dylib",
                _ => throw new PlatformNotSupportedException()
            };
        }
    }
}
