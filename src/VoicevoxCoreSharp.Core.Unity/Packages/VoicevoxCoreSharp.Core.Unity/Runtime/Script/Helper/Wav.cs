using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VoicevoxCoreSharp.Core.Helper
{
    // Support Linear PCM only
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public unsafe struct Wav
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U1)]
        public readonly byte* Riff;
        [FieldOffset(4)]
        public readonly uint WavSize;
        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U1)]
        public readonly byte* Format;
        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U1)]
        public readonly byte* FormatSignature;
        [FieldOffset(16)]
        public readonly uint FormatChunkSize;
        [FieldOffset(20)]
        public readonly ushort FormatId;
        [FieldOffset(22)]
        public readonly ushort Channel;
        [FieldOffset(24)]
        public readonly uint SampleRate;
        [FieldOffset(28)]
        public readonly uint BytePerSec;
        [FieldOffset(32)]
        public readonly ushort BlockSize;
        [FieldOffset(34)]
        public readonly ushort BitDepth;
        [FieldOffset(36)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U1)]
        public readonly byte* DataChunkHeader;
        [FieldOffset(40)]
        public readonly uint DataChunkSize;
        [FieldOffset(44)]
        public byte* Data; // TODO: readonlyにしたい

        public static Wav FromBytes(byte[] bytes)
        {
            var wav = MemoryMarshal.Read<Wav>(bytes.AsSpan());
            wav.Data = (byte*)Unsafe.AsPointer(ref bytes.AsSpan().Slice(44).GetPinnableReference());
            // wav.Data = (byte*)Marshal.AllocHGlobal((int)wav.DataChunkSize);
            // Marshal.Copy(bytes, 44, (IntPtr)wav.Data, (int)wav.DataChunkSize);
            return wav;
        }
    }

    public static class WavExt
    {
        public static int[] ToIntArray(this Wav wav)
        {
            if (wav.BitDepth == 16)
            {
                return ToInt16Array(wav).Select(v => (int)v).ToArray();
            }
            else if (wav.BitDepth == 8)
            {
                return ToInt8Array(wav).Select(v => (int)v).ToArray();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static unsafe byte[] ToByteArray(this Wav wav)
        {
            var arr = new byte[wav.WavSize + 8];
            MemoryMarshal.Write(arr.AsSpan(), ref wav);
            var a2 = new byte[(int)wav.DataChunkSize];
            Marshal.Copy((IntPtr)wav.Data, a2, 0, (int)wav.DataChunkSize);
            return arr.Take(44).ToArray().Concat(a2).ToArray();
        }

        private static unsafe short[] ToInt16Array(Wav wav)
        {
            var len = wav.DataChunkSize / (wav.BitDepth / 8);
            var arr = new byte[(int)wav.DataChunkSize];
            Marshal.Copy((IntPtr)wav.Data, arr, 0, (int)wav.DataChunkSize);
            var result = new short[len];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = BitConverter.ToInt16(arr, i * 2);
            }
            return result;
        }

        private static unsafe sbyte[] ToInt8Array(Wav wav)
        {
            var len = wav.DataChunkSize / (wav.BitDepth / 8);
            var arr = new byte[len];
            Marshal.Copy((IntPtr)wav.Data, arr, 0, (int)len);
            return Array.ConvertAll(arr, b => (sbyte)b);
        }
    }
}
