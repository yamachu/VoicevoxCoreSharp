using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental.Attribute;

namespace VoicevoxCoreSharp.Experimental
{
    public static partial class UserDictExtensions
    {
        [NonBlocking]
        public static partial Task SaveAsync(this UserDict userDict, string dictPath);

        [NonBlocking]
        public static partial Task LoadAsync(this UserDict userDict, string dictPath);
    }
}
