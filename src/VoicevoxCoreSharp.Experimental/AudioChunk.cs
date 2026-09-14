using System;

namespace VoicevoxCoreSharp.Experimental
{
    public readonly struct AudioChunk
    {
        public AudioChunk(ReadOnlyMemory<byte> pcm, nuint startInclusive, nuint endExclusive, bool isEndOfStream)
        {
            Pcm = pcm;
            StartInclusive = startInclusive;
            EndExclusive = endExclusive;
            IsEndOfStream = isEndOfStream;
        }

        public ReadOnlyMemory<byte> Pcm { get; }
        public nuint StartInclusive { get; }
        public nuint EndExclusive { get; }
        public bool IsEndOfStream { get; }
    }
}
