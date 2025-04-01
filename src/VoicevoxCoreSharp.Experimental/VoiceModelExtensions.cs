using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class VoiceModelFileExtensions
    {
        [NonBlocking]
        public static partial Task<VoiceModelFile> NewAsync(string modelPath);
    }
}
