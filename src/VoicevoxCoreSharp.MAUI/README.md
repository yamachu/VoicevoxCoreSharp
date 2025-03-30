# VoicevoxCoreSharp.MAUI

VoicevoxCoreSharp の MAUI アプリケーション向けのヘルパーライブラリです。

## Usage

Android や iOS で使用する場合、voicevox_core のライブラリのパスの解決に問題があるため、本ライブラリを import して利用します。

### Android

MainApplications.cs のコンストラクタなどで以下のように呼び出します。

```cs
using VoicevoxCoreSharp.MAUI; // <- Add

...

public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        CoreHelper.Initialize(null); // <- Add
    }
```

### iOS

Program.cs の Main メソッドなどで以下のように呼び出します。

```cs
using VoicevoxCoreSharp.MAUI; // <- Add

...

static void Main(string[] args)
    {
        CoreHelper.Initialize(null);
    }
```

その他の VoicevoxCoreSharp の使い方は、[VoicevoxCoreSharp](https://github.com/yamachu/VoicevoxCoreSharp) を参照してください。

## License

[MIT](../../LICENSE)
