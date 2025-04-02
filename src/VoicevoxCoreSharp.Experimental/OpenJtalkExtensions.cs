using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class OpenJtalkExtensions
    {
        [NonBlocking]
        public static partial Task<OpenJtalk> NewAsync(string openJtalkDicDir);

        [NonBlocking]
        public static partial Task<string> AnalyzeAsync(this OpenJtalk openJtalk, string text);
    }
}
