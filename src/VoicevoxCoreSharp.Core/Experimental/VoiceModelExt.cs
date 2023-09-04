using System;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core.Enum;

namespace VoicevoxCoreSharp.Core.Experimental
{
    public static class AsyncVoiceModel
    {
        public static Task<(ResultCode, VoiceModel)> NewAsync(string modelPath)
        {
            return Task.Run(() =>
            {
                var result = VoiceModel.New(modelPath, out var voiceModel);
                return (result, voiceModel);
            });
        }
    }
}
