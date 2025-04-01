using VoicevoxCoreSharp.Core.Enum;

namespace VoicevoxCoreSharp.Experimental.Exception
{
    [System.Serializable]
    public class VoicevoxCoreResultException : System.Exception
    {
        public ResultCode ResultCode { get; }

        public VoicevoxCoreResultException(ResultCode resultCode) : base(resultCode.ToMessage())
        {
            ResultCode = resultCode;
        }

        protected VoicevoxCoreResultException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
