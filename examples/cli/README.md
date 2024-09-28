# CLI版サンプルコード

voicevox_core を C# アプリケーションから実行するサンプルコードです。

https://github.com/VOICEVOX/voicevox_core/tree/main/example/cpp/unix を参考に作っているため、同等の機能を有しています。

## Requirements

.NET 8 以降が必要です。

[voicevox_core](https://github.com/voicevox/voicevox_core) リポジトリの Release から Downloader を取得し、このディレクトリで実行してください。

```sh
# macOS Arm64の場合
$ ./download-osx-arm64 --version 0.15.0-preview.15
```

2024/09/28 現在 main ブランチのみをサポートしています。
そのため上記の Downloader を用いた製品版の利用はできません。
互換のあるバージョンは [こちら](../../src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props) の `VoicevoxCoreCommitHash` に記載されているコミットハッシュを参照してください。

ダウンロードが完了すると `voicevox_core` ディレクトリに必要なファイルが配置されます。

## Run

```sh
# dotnet run -- <読み上げさせたい文章>
dotnet run -- これはテストです
```

正常に実行されると `audio.wav` が生成されます。
