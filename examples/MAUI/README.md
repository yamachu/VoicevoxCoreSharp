# MAUI Sample

voicevox_core 0.16.0-preview.1 を使用した MAUI サンプルです。

## Usage

### Requirements

- .NET 9.0 SDK
- .NET MAUI Workloads

#### Resources

Download models and dict.

```sh
$ download-osx-arm64 --only models dict -o ./examples/MAUI/voicevox_core
```

#### macOS

```sh
$ download-osx-arm64 --c-api-version 0.16.0-preview.1 --onnxruntime-version voicevox_onnxruntime-1.17.3 -o ./examples/MAUI/voicevox_core
```

#### Android / iOS

Download `voicevox_core` and `onnxruntime` from the following URL.

- https://github.com/VOICEVOX/voicevox_core/releases/tag/0.16.0-preview.1
- https://github.com/VOICEVOX/onnxruntime-builder/releases/tag/voicevox_onnxruntime-1.17.3

and extract them to `./examples/MAUI/voicevox_core`.

- Android => `./examples/MAUI/voicevox_core/android/`
- iOS => `./examples/MAUI/voicevox_core/ios/`

see: [./MAUI.csproj](./MAUI.csproj)

### Build and Run

iOS (on Device)

```sh
$ dotnet build -t:Run -f net9.0-ios -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=YOUR_DEVICE_UDID
```

iOS (on Simulator)

```sh
$ dotnet build -t:Run -f net9.0-ios -r iossimulator-arm64
```

See also: https://learn.microsoft.com/ja-jp/dotnet/maui/ios/cli?view=net-maui-9.0

Android

```sh
$ dotnet build -t:Run -f net9.0-android
```

macOS

```sh
$ dotnet build -t:Run -f net9.0-maccatalyst -r maccatalyst-arm64
```
