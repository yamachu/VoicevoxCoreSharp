using System;
using System.Globalization;

namespace VoicevoxCoreSharp.Core
{
    public readonly struct StyleId : IEquatable<StyleId>
    {
        public uint Value { get; }

        public StyleId(uint value)
        {
            Value = value;
        }

        public bool Equals(StyleId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is StyleId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public static explicit operator uint(StyleId styleId)
        {
            return styleId.Value;
        }

        public static explicit operator StyleId(uint value)
        {
            return new StyleId(value);
        }

        public static bool operator ==(StyleId left, StyleId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StyleId left, StyleId right)
        {
            return !left.Equals(right);
        }
    }

    internal static class StyleIdExt
    {
        internal static uint ToNative(this StyleId styleId)
        {
            return styleId.Value;
        }
    }
}
