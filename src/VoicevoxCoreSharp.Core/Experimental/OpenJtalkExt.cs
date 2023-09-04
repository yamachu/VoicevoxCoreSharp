using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core.Enum;

namespace VoicevoxCoreSharp.Core.Experimental
{
    public static class AsyncOpenJtalk
    {
        public static Task<(ResultCode, OpenJtalk)> NewAsync(string openJtalkDicDir)
        {
            return Task.Run(() =>
            {
                var result = OpenJtalk.New(openJtalkDicDir, out var openJtalk);
                return (result, openJtalk);
            });
        }

        public static Task<ResultCode> UseUserDictAsync(this OpenJtalk openJtalk, UserDict userDict)
        {
            return Task.Run(() =>
            {
                var result = openJtalk.UseUserDict(userDict);
                return result;
            });
        }
    }
}
