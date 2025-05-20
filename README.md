# VoicevoxCoreSharp

[voicevox_core](https://github.com/voicevox/voicevox_core) を C# から使用するためのラッパー。
薄いラッパーであるため、基本的には voicevox_core のドキュメントを参照し使用できます。

CLI アプリケーションや MAUI、Unity アプリケーションなどで、直接 voicevox_core を扱うことを目的としています。

## Install

[NuGet](https://www.nuget.org/packages/VoicevoxCoreSharp.Core/) からインストールできます。

```bash
$ dotnet add package VoicevoxCoreSharp.Core
```

Unity の場合は Package Manager から以下の URL を追加してください。
`#0.16.0` はバージョンを指定しています。

```
https://github.com/yamachu/VoicevoxCoreSharp.git?path=/src/VoicevoxCoreSharp.Core.Unity#0.16.0
```

MAUI の場合は以下のパッケージを追加すると便利です。

https://www.nuget.org/packages/VoicevoxCoreSharp.MAUI

```bash
$ dotnet add package VoicevoxCoreSharp.MAUI
```

## Usage

このライブラリを使用するには、voicevox_core のライブラリを自身で用意する必要があります。
[voicevox_core](https://github.com/voicevox/voicevox_core) の Release ページから対応するバージョンのライブラリをダウンロードしてください。

それぞれダウンロードしたライブラリを .NET アプリケーションから参照できるように配置してください。
各プラットフォームやアプリケーションの種類によって配置先が異なる場合があります。

以下の Sample に CLI、MAUI、Unity のサンプルがあるため、そのサンプルの csproj などを参考にしてください。

特に MAUI アプリケーションで Android や iOS で使用する場合は、Native Library のパス解決の問題があるため、[VoicevoxCoreSharp.MAUI](./src/VoicevoxCoreSharp.MAUI) を使用することをお勧めします。

## Sample

[examples/cli](./examples/cli) にコマンドラインから実行するサンプル実装があります。

[examples/UnitySample](./examples/UnitySample) に Unity アプリケーションのサンプルがあります。

[examples/MAUI](./examples/MAUI) に MAUI アプリケーションのサンプルがあります。

## Supported

- .NET Standard 2.0
- C# 9.0
  - Unity 2021.3 (LTS) を最低限サポートするため

## Notice

本リポジトリは voicevox_core の main ブランチをトラッキングしているため、main ブランチの状態は製品版のリリースとの互換性がない場合があります。
voicevox_core のリリースに合わせて、Release タグや、nuget パッケージを提供しているため、実際に利用する場合はそちらを利用してください。

詳細なトラッキングしているバージョンは Git Submodule として取得している [voicevox_core](./binding/voicevox_core) のコミットハッシュ、もしくは [VoicevoxCoreCommitHash](./src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props) を参照してください。

サブモジュールの更新は GitHub Dependabot により自動的に PR が作成されます。PR の説明には変更内容のサマリーが含まれ、変更の追跡が容易になっています。

今後のリリースで、voicevox_engine で利用されている compatible_engine などの対応を検討しています。

## License

[MIT](./LICENSE)
