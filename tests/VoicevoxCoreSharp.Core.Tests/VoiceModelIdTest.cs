using System;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class VoiceModelIdTest
    {
        [Fact]
        public void CreateAndConvertToString()
        {
            var id = new VoiceModelId("test-id");
            Assert.Equal("test-id", id.Id);
            Assert.Equal("test-id", id.ToString());
            
            // Test implicit conversion to string
            string idString = id;
            Assert.Equal("test-id", idString);
        }
        
        [Fact]
        public void CreateFromString()
        {
            VoiceModelId id = "test-id";
            Assert.Equal("test-id", id.Id);
        }
        
        [Fact]
        public void Equality()
        {
            var id1 = new VoiceModelId("test-id");
            var id2 = new VoiceModelId("test-id");
            var id3 = new VoiceModelId("different-id");
            
            Assert.Equal(id1, id2);
            Assert.NotEqual(id1, id3);
        }
    }
}