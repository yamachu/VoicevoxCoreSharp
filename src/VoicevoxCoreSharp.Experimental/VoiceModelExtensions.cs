using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class VoiceModelFileExtensions
    {
        [Obsolete("Use VoiceModelFile.OpenAsync(string modelPath) instead.")]
        [NonBlocking]
        public static partial Task<VoiceModelFile> NewAsync(string modelPath);

        [NonBlocking]
        public static partial Task<VoiceModelFile> OpenAsync(string modelPath);
    }
}
