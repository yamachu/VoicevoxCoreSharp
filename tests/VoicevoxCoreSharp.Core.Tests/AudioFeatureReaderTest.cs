using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental;
using Xunit;

namespace VoicevoxCoreSharp.Core.Tests
{
    public class AudioFeatureReaderTest
    {
        private static readonly StyleId StreamingTalkStyleId = new StyleId(302);

        [Fact]
        public void ReadVariableSizedChunksMatchesFullRender()
        {
            var context = CreateStreamingContext();
            using var audioFeature = context.AudioFeature;

            var fullRenderResult = context.Synthesizer.Render(audioFeature, 0, audioFeature.FrameLength, out _, out var fullPcm);
            Assert.Equal(ResultCode.RESULT_OK, fullRenderResult);
            Assert.NotNull(fullPcm);

            using var reader = context.Synthesizer.CreateAudioFeatureReader(audioFeature);
            var combinedPcm = new List<byte>();
            var frameCounts = new nuint[] { 1, 3, 7, 2, 11 };
            var readIndex = 0;
            AudioChunk lastChunk = default;

            while (!reader.EndOfStream)
            {
                lastChunk = reader.Read(frameCounts[readIndex % frameCounts.Length]);
                combinedPcm.AddRange(lastChunk.Pcm.ToArray());
                readIndex++;
            }

            Assert.True(lastChunk.IsEndOfStream);
            Assert.Equal(audioFeature.FrameLength, reader.Position);
            Assert.Equal(fullPcm, combinedPcm.ToArray());
        }

        [Fact]
        public void SeekReplaysTheSameRange()
        {
            var context = CreateStreamingContext();
            using var audioFeature = context.AudioFeature;
            using var reader = context.Synthesizer.CreateAudioFeatureReader(audioFeature);

            var frameCount = audioFeature.FrameLength > 1 ? audioFeature.FrameLength / 2 : 1;
            var firstChunk = reader.Read(frameCount);

            reader.Seek(firstChunk.StartInclusive);
            var replayedChunk = reader.Read(frameCount);

            Assert.Equal(firstChunk.StartInclusive, replayedChunk.StartInclusive);
            Assert.Equal(firstChunk.EndExclusive, replayedChunk.EndExclusive);
            Assert.Equal(firstChunk.IsEndOfStream, replayedChunk.IsEndOfStream);
            Assert.Equal(firstChunk.Pcm.ToArray(), replayedChunk.Pcm.ToArray());
        }

        [Fact]
        public async Task ReadAllAsyncWithFixedFrameCountMatchesFullRender()
        {
            var context = CreateStreamingContext();
            using var audioFeature = context.AudioFeature;

            var fullRenderResult = context.Synthesizer.Render(audioFeature, 0, audioFeature.FrameLength, out _, out var fullPcm);
            Assert.Equal(ResultCode.RESULT_OK, fullRenderResult);
            Assert.NotNull(fullPcm);

            using var reader = context.Synthesizer.CreateAudioFeatureReader(audioFeature);
            var combinedPcm = new List<byte>();

            await foreach (var chunk in reader.ReadAllAsync(5))
            {
                combinedPcm.AddRange(chunk.Pcm.ToArray());
            }

            Assert.Equal(audioFeature.FrameLength, reader.Position);
            Assert.Equal(fullPcm, combinedPcm.ToArray());
        }

        [Fact]
        public async Task ReadAllAsyncWithFrameCountProviderMatchesFullRender()
        {
            var context = CreateStreamingContext();
            using var reader = context.Synthesizer.CreateAudioFeatureReader(context.AudioQueryJson, StreamingTalkStyleId, SynthesisOptions.Default());
            var fullRenderResult = context.Synthesizer.Render(context.AudioFeature, 0, context.AudioFeature.FrameLength, out _, out var fullPcm);
            Assert.Equal(ResultCode.RESULT_OK, fullRenderResult);
            Assert.NotNull(fullPcm);

            var combinedPcm = new List<byte>();
            var frameCounts = new nuint[] { 2, 1, 9, 4 };
            var readIndex = 0;

            await foreach (var chunk in reader.ReadAllAsync(_ => frameCounts[readIndex++ % frameCounts.Length]))
            {
                combinedPcm.AddRange(chunk.Pcm.ToArray());
            }

            Assert.Equal(fullPcm, combinedPcm.ToArray());
        }

        [Fact]
        public async Task ReadAllAsyncWithZeroFrameCountThrows()
        {
            var context = CreateStreamingContext();
            using var audioFeature = context.AudioFeature;
            using var reader = context.Synthesizer.CreateAudioFeatureReader(audioFeature);

            await using var enumerator = reader.ReadAllAsync(0).GetAsyncEnumerator();

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await enumerator.MoveNextAsync().AsTask());
            Assert.Equal("frameCount", exception.ParamName);
        }

        [Fact]
        public async Task ReadAllAsyncWithZeroFrameCountProviderResultThrows()
        {
            var context = CreateStreamingContext();
            using var audioFeature = context.AudioFeature;
            using var reader = context.Synthesizer.CreateAudioFeatureReader(audioFeature);

            await using var enumerator = reader.ReadAllAsync(_ => 0).GetAsyncEnumerator();

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await enumerator.MoveNextAsync().AsTask());
            Assert.Equal("frameCountProvider", exception.ParamName);
        }

        private static (Onnxruntime Onnxruntime, OpenJtalk OpenJtalk, VoiceModelFile VoiceModel, Synthesizer Synthesizer, string AudioQueryJson, AudioFeature AudioFeature) CreateStreamingContext()
        {
            OpenJtalk.New(Consts.OpenJTalkDictDir, out var openJtalk);
            var initializeOptions = InitializeOptions.Default();
            var onnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Join(AppContext.BaseDirectory, Helper.GetOnnxruntimeAssemblyName()));
            if (Onnxruntime.LoadOnce(onnxruntimeOptions, out var onnxruntime) != ResultCode.RESULT_OK)
            {
                Assert.Fail("Failed to initialize onnxruntime");
            }

            var synthesizerResult = Synthesizer.New(onnxruntime, openJtalk, initializeOptions, out var synthesizer);
            VoiceModelFile.Open(Consts.SampleVoiceModel, out var voiceModel);
            var loadResult = synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());

            Assert.Equal(ResultCode.RESULT_OK, synthesizerResult);
            Assert.Equal(ResultCode.RESULT_OK, loadResult);

            var createAudioQueryResult = synthesizer.CreateAudioQuery("こんにちは？", StreamingTalkStyleId, out var audioQueryJson);
            Assert.Equal(ResultCode.RESULT_OK, createAudioQueryResult);
            Assert.NotNull(audioQueryJson);

            var createAudioFeatureResult = synthesizer.CreateAudioFeature(audioQueryJson, StreamingTalkStyleId, SynthesisOptions.Default(), out var audioFeature);
            Assert.Equal(ResultCode.RESULT_OK, createAudioFeatureResult);
            Assert.NotNull(audioFeature);

            return (onnxruntime, openJtalk, voiceModel, synthesizer, audioQueryJson, audioFeature);
        }
    }
}
