# CLI版サンプルコード

voicevox_core を C# アプリケーションから実行するサンプルコードです。

https://github.com/VOICEVOX/voicevox_core/tree/main/example/cpp/unix を参考に作っているため、同等の機能を有しています。

## Requirements

.NET 8 以降が必要です。

[voicevox_core](https://github.com/voicevox/voicevox_core) リポジトリの Release から Downloader を取得し、このディレクトリで実行してください。

```sh
# macOS Arm64の場合
$ ./download-osx-arm64 --c-api-version 0.16.0 --onnxruntime-version voicevox_onnxruntime-1.17.3 -o voicevox_core
```

このリポジトリは voicevox_core の main ブランチに追従しているため、examples/cli のコードが動作しない場合もあります。
動作しない場合、Release タグが打たれているバージョンに switch して実行してください。

トラッキングしている main のバージョンは[こちら](../../src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props) の `VoicevoxCoreCommitHash` に記載されているコミットハッシュを参照してください。

ダウンロードが完了すると `voicevox_core` ディレクトリに必要なファイルが配置されます。

## Run

```sh
# dotnet run -- <読み上げさせたい文章>
dotnet run -- これはテストです
```

正常に実行されると `audio.wav` が生成されます。
