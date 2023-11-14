# VoicevoxCoreSharp

[voicevox_core](https://github.com/voicevox/voicevox_core) を C# から使用するためのラッパー。
薄いラッパーであるため、基本的には voicevox_core のドキュメントを参照し使用できます。

CLI アプリケーションや Unity アプリケーションなどで、直接 voicevox_core を扱うことを目的としています。

## Notice

現在 Preview として Release されている voicevox_core 0.15.x 系のみをサポートしています。

詳細なバージョンは Git Submodule として取得している [voicevox_core](./binding/voicevox_core) のコミットハッシュ、もしくは [VoicevoxCoreCommitHash](./src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props) を参照してください。

## Sample

[examples/cli](./examples/cli) にコマンドラインから実行するサンプル実装があります。

[examples/UnitySample](./examples/UnitySample) に Unity アプリケーションのサンプルがあります（Editor 上では音声の生成に失敗することがあるため、パッケージングを行ってください）。

## Supported

- .NET Standard 2.0
- C# 9.0
  - Unity 2021.3 (LTS) を最低限サポートするため
