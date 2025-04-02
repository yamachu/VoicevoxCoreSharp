namespace MAUI;

using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.FileSystemGlobbing;
using Plugin.Maui.Audio;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Experimental;

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
        await PickOpenJTalkDirectoryImpl();
        // await PickOpenJTalkDirectoryImplAsync();
    }

    private async Task PickOpenJTalkDirectoryImpl()
    {
        Synthesizer = null;
        OpenJTalkDictPath = "";
        VvmModelDirectoryPath = "";

        var openJtalkDictPath = await GetOpenJTalkDictionaryPath();
        if (openJtalkDictPath is null)
        {
            return;
        }
        var openResult = OpenJtalk.New(openJtalkDictPath, out var openJTalk);
        if (openResult != ResultCode.RESULT_OK)
        {
            return;
        }

        var libraryPath = "";
#if MACCATALYST
        var bundlePath = Foundation.NSBundle.MainBundle.BundlePath;
        libraryPath = Path.Combine(bundlePath, "Contents", "MonoBundle", "libvoicevox_onnxruntime.1.17.3.dylib");
#elif ANDROID
        var bundlePath = Android.App.Application.Context.ApplicationInfo?.NativeLibraryDir;
        libraryPath = Path.Combine(bundlePath, "libvoicevox_onnxruntime.so");
#elif LINUX
        libraryPath = Path.Combine(AppContext.BaseDirectory, "libvoicevox_onnxruntime.so");
#elif WINDOWS
        // TODO: Check the actual path
        libraryPath = Path.Combine(AppContext.BaseDirectory, "libvoicevox_onnxruntime.dll");
#endif

        var onnxruntimeloadOption = new LoadOnnxruntimeOptions(libraryPath);
#if IOS
        if (Onnxruntime.InitOnce(out var onnxruntime) != ResultCode.RESULT_OK)
        {
            return;
        }
#else
        if (Onnxruntime.LoadOnce(onnxruntimeloadOption, out var onnxruntime) != ResultCode.RESULT_OK)
        {
            return;
        }
#endif
        openResult = Synthesizer.New(onnxruntime, openJTalk, InitializeOptions.Default(), out var synthesizer);
        if (openResult != ResultCode.RESULT_OK)
        {
            using (openJTalk) { }
            return;
        }

        Synthesizer = synthesizer;
        OpenJTalkDictPath = openJtalkDictPath;
        VvmModelDirectoryPath = "";
    }

    private async Task PickOpenJTalkDirectoryImplAsync()
    {
        Synthesizer = null;
        OpenJTalkDictPath = "";
        VvmModelDirectoryPath = "";

        var openJtalkDictPath = await GetOpenJTalkDictionaryPath();
        if (openJtalkDictPath is null)
        {
            return;
        }
        var openJTalk = await OpenJtalkExtensions.NewAsync(openJtalkDictPath);

        var libraryPath = "";
#if MACCATALYST
        var bundlePath = Foundation.NSBundle.MainBundle.BundlePath;
        libraryPath = Path.Combine(bundlePath, "Contents", "MonoBundle", "libvoicevox_onnxruntime.1.17.3.dylib");
#elif ANDROID
        var bundlePath = Android.App.Application.Context.ApplicationInfo?.NativeLibraryDir;
        libraryPath = Path.Combine(bundlePath, "libvoicevox_onnxruntime.so");
#elif LINUX
        libraryPath = Path.Combine(AppContext.BaseDirectory, "libvoicevox_onnxruntime.so");
#elif WINDOWS
        // TODO: Check the actual path
        libraryPath = Path.Combine(AppContext.BaseDirectory, "libvoicevox_onnxruntime.dll");
#endif

        var onnxruntimeloadOption = new LoadOnnxruntimeOptions(libraryPath);
#if IOS
        var onnxruntime = await OnnxruntimeExtensions.InitOnceAsync();
#else
        var onnxruntime = await OnnxruntimeExtensions.LoadOnceAsync(onnxruntimeloadOption);
#endif
        var openResult = Synthesizer.New(onnxruntime, openJTalk, InitializeOptions.Default(), out var synthesizer);
        if (openResult != ResultCode.RESULT_OK)
        {
            using (openJTalk) { }
            return;
        }

        Synthesizer = synthesizer;
        OpenJTalkDictPath = openJtalkDictPath;
        VvmModelDirectoryPath = "";
    }

    [RelayCommand(CanExecute = nameof(CanPickVVMDirectory))]
    private async Task PickVVMDirectory()
    {
#if IOS || ANDROID
        var vvmPath = await GetVVMModelPathForMobile();
        await LoadVVMModels(vvmPath);
        VvmModelDirectoryPath = vvmPath;
#else
        // 自由にディレクトリが選択できるPC向け
        var result = await FolderPicker.Default.PickAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), CancellationToken.None);
        if (!result.IsSuccessful)
        {
            return;
        }
        await LoadVVMModels(result.Folder.Path);
        VvmModelDirectoryPath = result.Folder.Path;
#endif
    }

    [RelayCommand(CanExecute = nameof(CanGenerateAndPlay))]
    private async Task GenerateAndPlay()
    {
        GenerateAndPlayImpl();
        // await GenerateAndPlayImplAsync();
    }

    private void GenerateAndPlayImpl()
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

    private async Task GenerateAndPlayImplAsync()
    {
        // TODO: Select StyleID
        var (outputWavLength, outputWav) = await Synthesizer.TtsAsync(SynthesisText, 0, TtsOptions.Default());

        using var stream = new MemoryStream(outputWav, 0, (int)outputWavLength);

        var audioPlayer = AudioManager.Current.CreatePlayer(stream);
        audioPlayer.Play();
    }

    private bool CanPickVVMDirectory() => Synthesizer != null;

    private bool CanGenerateAndPlay() => Synthesizer != null && !string.IsNullOrWhiteSpace(SynthesisText) && !string.IsNullOrWhiteSpace(VvmModelDirectoryPath);

    /// <summary>
    /// VVMモデルのパスを取得します
    /// </summary>
    private async Task<string> GetVVMModelPathForMobile()
    {
#if IOS
        var cachePath = Path.Combine(
            Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal)),
            "Library",
            "Caches");
        var destDir = Path.Combine(cachePath, "models", "vvms");

        var bundlePath = Foundation.NSBundle.MainBundle.BundlePath;
        var sourceModelPath = Path.Combine(bundlePath, "models", "vvms");

        // TODO: .done みたいなファイルを置いたりして、何度もコピーを走らせないようにしたりするとよさそう
        await CopyAccessibleDirectory(sourceModelPath, destDir);

        return destDir;
#elif ANDROID
        var filesDir = Android.App.Application.Context.FilesDir.AbsolutePath;
        var destDir = Path.Combine(filesDir, "models", "vvms");

        // TODO: .done みたいなファイルを置いたりして、何度もコピーを走らせないようにしたりするとよさそう
        await CopyAccessibleDirectory("models/vvms", destDir);

        return destDir;
#else
        throw new PlatformNotSupportedException();
#endif
    }

    /// <summary>
    /// VVMモデルをロードしてSynthesizerに登録します
    /// </summary>
    private async Task LoadVVMModels(string directoryPath)
    {
        await LoadVVMModelsImpl(directoryPath);
        // await LoadVVMModelsImplAsync(directoryPath);
    }

    private async Task LoadVVMModelsImpl(string directoryPath)
    {
        var matcher = new Matcher();
        matcher.AddIncludePatterns(new[] { "*.vvm" });

        foreach (var path in matcher.GetResultsInFullPath(directoryPath))
        {
            var openResult = VoiceModelFile.Open(path, out var voiceModel);
            if (openResult != ResultCode.RESULT_OK)
            {
                continue;
            }

            openResult = Synthesizer.LoadVoiceModel(voiceModel);
            if (openResult != ResultCode.RESULT_OK)
            {
                using (voiceModel) { }
                continue;
            }

            using (voiceModel) { }
        }

        await Task.CompletedTask;
    }

    private async Task LoadVVMModelsImplAsync(string directoryPath)
    {
        var matcher = new Matcher();
        matcher.AddIncludePatterns(new[] { "*.vvm" });

        foreach (var path in matcher.GetResultsInFullPath(directoryPath))
        {
            using var voiceModel = await VoiceModelFileExtensions.NewAsync(path);
            await Synthesizer.LoadVoiceModelAsync(voiceModel);
        }
    }

    /// <summary>
    /// プラットフォームに応じてOpenJTalk辞書の絶対パスを取得します
    /// </summary>
    private async Task<string?> GetOpenJTalkDictionaryPath()
    {
#if IOS
        var cachePath = Path.Combine(
            Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal)),
            "Library",
            "Caches");
        var destDir = Path.Combine(cachePath, "open_jtalk_dic");

        var bundlePath = Foundation.NSBundle.MainBundle.BundlePath;
        var sourceDictPath = Path.Combine(bundlePath, "dict", "open_jtalk_dic_utf_8-1.11");

        // TODO: .done みたいなファイルを置いたりして、何度もコピーを走らせないようにしたりするとよさそう
        await CopyAccessibleDirectory(sourceDictPath, destDir);

        return destDir;
#elif ANDROID
        var context = Android.App.Application.Context;
        // アプリのプライベートディレクトリを使用
        var filesDir = context.FilesDir.AbsolutePath;
        var destDir = Path.Combine(filesDir, "open_jtalk_dic");

        // TODO: .done みたいなファイルを置いたりして、何度もコピーを走らせないようにしたりするとよさそう
        await CopyAccessibleDirectory("dict/open_jtalk_dic_utf_8-1.11", destDir);

        return destDir;
#else
        // 自由にディレクトリが選択できるPC向け
        var result = await FolderPicker.Default.PickAsync(AppContext.BaseDirectory, CancellationToken.None);
        if (!result.IsSuccessful)
        {
            return null;
        }
        return result.Folder.Path;
#endif
    }

    /// <summary>
    /// バンドルファイルをコピーするヘルパーメソッド
    /// </summary>
    private async Task CopyAccessibleDirectory(string sourceDir, string targetDir)
    {
#if IOS
        if (!Directory.Exists(sourceDir))
        {
            throw new FileNotFoundException($"source directory not found: {sourceDir}");
        }

        await CopyDirectory(sourceDir, targetDir);
#elif ANDROID
        if (Android.App.Application.Context.Assets?.List(sourceDir) is null)
        {
            throw new FileNotFoundException($"source directory not found: {sourceDir}");
        }

        await CopyDirectory(sourceDir, targetDir);
#else
        throw new PlatformNotSupportedException();
#endif
    }

    /// <summary>
    /// ディレクトリ全体をコピーするヘルパーメソッド
    /// </summary>
    private async Task CopyDirectory(string sourceDir, string destDir)
    {
        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

#if IOS
        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string destFile = Path.Combine(destDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
            string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
            await CopyDirectory(dir, destSubDir);
        }

        await Task.CompletedTask;
#elif ANDROID
        var assetManager = Android.App.Application.Context.Assets;
        foreach (var file in assetManager.List(sourceDir))
        {
            var assetPath = Path.Combine(sourceDir, file);
            var destPath = Path.Combine(destDir, file);

            using var inputStream = assetManager.Open(assetPath);
            using var outputStream = File.Create(destPath);
            await inputStream.CopyToAsync(outputStream);
        }
#else
        throw new PlatformNotSupportedException();
#endif
    }
}
