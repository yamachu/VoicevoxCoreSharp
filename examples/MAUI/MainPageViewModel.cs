namespace MAUI;

using Microsoft.Extensions.FileSystemGlobbing;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using Plugin.Maui.Audio;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string openJTalkDictPath = "";

    [ObservableProperty]
    private string vvmModelDirectoryPath = "";

    // TODO: Select StyleID

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PickVVMDirectoryCommand))]
    private Synthesizer synthesizer = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PickVVMDirectoryCommand))]
    [NotifyCanExecuteChangedFor(nameof(GenerateAndPlayCommand))]
    private string synthesisText = "";

    [RelayCommand]
    private async Task PickOpenJTalkDirectory()
    {
        Synthesizer = null;
        OpenJTalkDictPath = "";
        VvmModelDirectoryPath = "";

        var result = await FolderPicker.Default.PickAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), CancellationToken.None);
        if (!result.IsSuccessful)
        {
            return;
        }
        var openResult = OpenJtalk.New(result.Folder.Path, out var openJTalk);
        if (openResult != ResultCode.RESULT_OK)
        {
            return;
        }

        openResult = Synthesizer.New(openJTalk, InitializeOptions.Default(), out var synthesizer);
        if (openResult != ResultCode.RESULT_OK)
        {
            using (openJTalk) { }
            return;
        }

        Synthesizer = synthesizer;
        OpenJTalkDictPath = result.Folder.Path;
        VvmModelDirectoryPath = "";
    }

    [RelayCommand(CanExecute = nameof(CanPickVVMDirectory))]
    private async Task PickVVMDirectory()
    {
        var result = await FolderPicker.Default.PickAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), CancellationToken.None);
        if (!result.IsSuccessful)
        {
            return;
        }
        var matcher = new Matcher();
        matcher.AddIncludePatterns(new[] { "*.vvm" });

        foreach (var path in matcher.GetResultsInFullPath(result.Folder.Path))
        {
            var openResult = VoiceModel.New(path, out var voiceModel);
            if (openResult != ResultCode.RESULT_OK)
            {
                return;
            }

            openResult = Synthesizer.LoadVoiceModel(voiceModel);
            if (openResult != ResultCode.RESULT_OK)
            {
                using (voiceModel) { }
                return;
            }

            using (voiceModel) { }
        }

        VvmModelDirectoryPath = result.Folder.Path;
    }

    [RelayCommand(CanExecute = nameof(CanGenerateAndPlay))]
    private void GenerateAndPlay()
    {
        // TODO: Select StyleID
        var result = Synthesizer.Tts(SynthesisText, 0, TtsOptions.Default(), out var outputWavLength, out var outputWav);
        if (result != ResultCode.RESULT_OK)
        {
            return;
        }

        using var stream = new MemoryStream(outputWav, 0, (int)outputWavLength);

        var audioPlayer = AudioManager.Current.CreatePlayer(stream);
        audioPlayer.Play();
    }

    private bool CanPickVVMDirectory() => Synthesizer != null;

    private bool CanGenerateAndPlay() => Synthesizer != null && !string.IsNullOrWhiteSpace(SynthesisText);
}
