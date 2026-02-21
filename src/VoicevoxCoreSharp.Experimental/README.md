# VoicevoxCoreSharp.Experimental

VoicevoxCoreSharp.Core の実験的な機能を提供するライブラリです。

API は予告なく変更される場合があります。

## Feature

- 非同期 API

### 非同期 API

クラス名に `Extensions` が付属していないメソッドは、VoiceVoxCoreSharp.Core で提供するクラスの拡張メソッドとして提供されています。

- OnnxruntimeExtensions.LoadOnceAsync
- OnnxruntimeExtensions.InitOnceAsync
- OpenJtalkExtensions.NewAsync
- OpenJtalk.AnalyzeAsync
- Synthesizer.LoadVoiceModelAsync
- Synthesizer.SynthesizeAsync
- Synthesizer.CreateAudioQueryAsync
- Synthesizer.CreateAudioQueryFromKanaAsync
- Synthesizer.CreateAccentPhrasesAsync
- Synthesizer.CreateAccentPhrasesFromKanaAsync
- Synthesizer.ReplaceMoraDataAsync
- Synthesizer.ReplacePhonemeLengthAsync
- Synthesizer.ReplaceMoraPitchAsync
- Synthesizer.TtsFromKanaAsync
- Synthesizer.TtsAsync
- Synthesizer.CreateSingFrameAudioQueryAsync
- Synthesizer.CreateSingFrameF0Async
- Synthesizer.CreateSingFrameVolumeAsync
- Synthesizer.FrameSynthesisAsync
- UserDict.SaveAsync
- UserDict.LoadAsync
- ~~VoiceModelFileExtensions.NewAsync~~
- VoiceModelFileExtensions.OpenAsync

## Usage

```
using VoicevoxCoreSharp.Experimental;
```

詳しくは [MAUI サンプル](https://github.com/yamachu/VoicevoxCoreSharp/tree/main/examples/MAUI/) を参照してください。

## License

[MIT](https://github.com/yamachu/VoicevoxCoreSharp/blob/main/LICENSE)
