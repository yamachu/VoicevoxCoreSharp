using Android.App;
using Android.Runtime;
using VoicevoxCoreSharp.MAUI;

namespace MAUI;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        // VoicevoxCoreのネイティブ依存のパス解決のため、ライブラリが読み込まれる前に実行する
        CoreHelper.Initialize(null);
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
