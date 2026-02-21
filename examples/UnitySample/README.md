# Unity版サンプルコード

voicevox_core を Unity アプリケーションから実行するサンプルコードです。

## Notice

- Unity版は基本的にサポート外
- Unity Editor 上だと vvm ファイルの読み込みが不安定で、パッケージング後じゃないと正常動作しないことがある（原因調査やコントリビュート歓迎です）
- macOS M1 でのみ動作確認

## Requirements

Unity 2022.3.x 以降が必要です。

voicevox_core のバージョンなどについては [examples/cli/README.md](../../cli/README.md) などを参照してください。

その他必要なリソースは、

- Assets/StreamingAssets/voicevox_core
- Assets/Plugins/runtimes/\<プラットフォーム\>/native/

に配置してください。

サンプルとして osx.10.14-arm64 (M1 mac)用のテンプレートが用意してあります。

## Note

SampleScene の GameObject にアタッチされているスクリプトは [SampleVoicevoxCoreSharpScript.cs](./Assets/SampleVoicevoxCoreSharpScript.cs) です。
CLI版のサンプルコードと同じような処理を行い、AudioSource Component に音声を流し込む実装となっています。
