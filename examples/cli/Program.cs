using System.CommandLine;
using System.CommandLine.Parsing;
using Microsoft.Extensions.FileSystemGlobbing;

using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Core.Enum;

const string OutputWavName = "audio.wav";
const uint StyleId = 0;

static int RunTts(string text, string? resourcePath = "voicevox_core")
{
    var openJTalkDictPath = $"{resourcePath}/open_jtalk_dic_utf_8-1.11";

    Console.WriteLine("coreの初期化中");

    var initializeOptions = InitializeOptions.Default();
    var result = OpenJtalk.New(openJTalkDictPath, out var openJtalk);
    if (result != ResultCode.RESULT_OK)
    {
        Console.Error.WriteLine(result.ToMessage());
        return 1;
    }

    var loadOnnxruntimeOptions = LoadOnnxruntimeOptions.Default();
    if (Onnxruntime.LoadOnce(loadOnnxruntimeOptions, out var onnxruntime) != ResultCode.RESULT_OK)
    {
        Console.Error.WriteLine("Failed to initialize onnxruntime");
        return 1;
    }
    result = Synthesizer.New(onnxruntime, openJtalk, initializeOptions, out var synthesizer);
    if (result != ResultCode.RESULT_OK)
    {
        Console.Error.WriteLine(result.ToMessage());
        return 1;
    }

    using (openJtalk) { }

    var matcher = new Matcher();
    matcher.AddIncludePatterns(new[] { "*.vvm" });

    foreach (var path in matcher.GetResultsInFullPath($"{resourcePath}/model"))
    {
        result = VoiceModel.New(path, out var voiceModel);
        if (result != ResultCode.RESULT_OK)
        {
            Console.Error.WriteLine(result.ToMessage());
            return 1;
        }

        result = synthesizer.LoadVoiceModel(voiceModel);
        if (result != ResultCode.RESULT_OK)
        {
            Console.Error.WriteLine(result.ToMessage());
            return 1;
        }

        using (voiceModel) { }
    }

    Console.WriteLine("音声生成中...");

    result = synthesizer.Tts(text, StyleId, TtsOptions.Default(), out var outputWavSize, out var outputWav);
    if (result != ResultCode.RESULT_OK)
    {
        Console.Error.WriteLine(result.ToMessage());
        return 1;
    }

    Console.WriteLine("音声ファイル保存中...");

    using var writer = new BinaryWriter(File.OpenWrite(OutputWavName));
    writer.Write(outputWav!);

    Console.WriteLine($"音声ファイル完了 ({OutputWavName})");

    using (synthesizer) { }

    return 0;
}

var returnCode = 0;
var text = new Argument<string>("文章");

var command = new RootCommand
{
    text
};

var resourcePath = new Option<string>("--resource", "リソースディレクトリパス");
command.AddOption(resourcePath);
command.SetHandler((context) =>
{
    var textValue = context.ParseResult.GetValueForArgument(text);
    var resourceValue = context.ParseResult.GetValueForOption(resourcePath);
    returnCode = RunTts(textValue, resourceValue);
});
returnCode = command.Invoke(args);

Environment.Exit(returnCode);
