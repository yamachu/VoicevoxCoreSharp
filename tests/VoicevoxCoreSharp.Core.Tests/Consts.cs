using System;
using System.IO;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class Consts
    {
        public static readonly string OpenJTalkDictDir = Path.Combine(Helper.GetProjectDirectory(), "resources", "open_jtalk_dic_utf_8-1.11");
        public static readonly string SampleVoiceModel = Path.Combine(Helper.GetBaseVoicevoxCoreDirectory(), "model", "sample.vvm");
    }
}
