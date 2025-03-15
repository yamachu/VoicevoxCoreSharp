using ObjCRuntime;
using UIKit;
using VoicevoxCoreSharp.MAUI;

namespace MAUI;

public class Program
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        // VoicevoxCoreのネイティブ依存のパス解決のため、ライブラリが読み込まれる前に実行する
        CoreHelper.Initialize(null);

        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
