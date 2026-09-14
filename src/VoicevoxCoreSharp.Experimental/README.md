# VoicevoxCoreSharp.Experimental

VoicevoxCoreSharp.Core の実験的な機能を提供するライブラリです。

API は予告なく変更される場合があります。

## Feature

- 非同期 API
- AudioFeature Reader API

### 非同期 API

クラス名に `Extensions` が付属していないメソッドは、VoicevoxCoreSharp.Core で提供するクラスの拡張メソッドとして提供されています。

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
- Synthesizer.CreateAudioFeatureReader
- Synthesizer.CreateAudioFeatureReaderAsync
- UserDict.SaveAsync
- UserDict.LoadAsync
- ~~VoiceModelFileExtensions.NewAsync~~
- VoiceModelFileExtensions.OpenAsync

### AudioFeature Reader API

`AudioFeature` をベースに、シーク可能かつ可変サイズで PCM を読み出せる API です。

- `AudioFeatureReader.Read`
- `AudioFeatureReader.ReadAsync`
- `AudioFeatureReader.ReadAllAsync`

`ReadAllAsync` は固定チャンクサイズまたは chunk size provider を使って `IAsyncEnumerable<AudioChunk>` として順次取得できます。
`AudioFeatureReader` は状態を持つカーソルです。複数の consumer で同時に独立した読み取り位置を持ちたい場合は、reader を分けてください。

## Usage

```
using VoicevoxCoreSharp.Experimental;
```

詳しくは [MAUI サンプル](https://github.com/yamachu/VoicevoxCoreSharp/tree/main/examples/MAUI/) を参照してください。

## License

[MIT](https://github.com/yamachu/VoicevoxCoreSharp/blob/main/LICENSE)
