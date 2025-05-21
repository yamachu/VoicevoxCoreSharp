using System;

namespace VoicevoxCoreSharp.Core
{
    public record VoiceModelId(string Id)
    {
        // Target framework is netstandard2.0, so init-only properties not available
        public string Id { get; } = Id;

        public static implicit operator string(VoiceModelId id) => id.Id;
        
        public static implicit operator VoiceModelId(string id) => new VoiceModelId(id);
        
        public override string ToString() => Id;
    }
}