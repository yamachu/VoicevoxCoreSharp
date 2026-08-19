using System;

namespace VoicevoxCoreSharp.Core
{
    public readonly struct VoiceModelId : IEquatable<VoiceModelId>
    {
        public static VoiceModelId Empty { get; } = new VoiceModelId(Guid.Empty);

        public Guid Value { get; }

        public VoiceModelId(Guid value)
        {
            Value = value;
        }

        public VoiceModelId(string value) : this(Guid.Parse(value))
        {
        }

        public bool Equals(VoiceModelId other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is VoiceModelId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString("D");
        }

        public static explicit operator Guid(VoiceModelId voiceModelId)
        {
            return voiceModelId.Value;
        }

        public static explicit operator VoiceModelId(Guid value)
        {
            return new VoiceModelId(value);
        }

        public static explicit operator VoiceModelId(string value)
        {
            return new VoiceModelId(value);
        }

        public static explicit operator string(VoiceModelId voiceModelId)
        {
            return voiceModelId.ToString();
        }

        public static bool operator ==(VoiceModelId left, VoiceModelId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(VoiceModelId left, VoiceModelId right)
        {
            return !left.Equals(right);
        }
    }

    internal static class VoiceModelIdExt
    {
        internal static unsafe VoiceModelId FromNative(byte* ptr)
        {
            return new VoiceModelId(NativeUuid.ToGuid(ptr));
        }

        internal static byte[] ToNative(this VoiceModelId voiceModelId)
        {
            return NativeUuid.ToUUIDv4ByteArray(voiceModelId.Value);
        }
    }
}
