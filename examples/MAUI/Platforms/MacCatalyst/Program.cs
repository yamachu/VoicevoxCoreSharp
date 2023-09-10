using ObjCRuntime;
using UIKit;

namespace MAUI;

public class Program
/* Unmerged change from project 'MAUI(net8.0-maccatalyst)'
Before:
{
	// This is the main entry point of the application.
	static void Main(string[] args)
After:
{
    // This is the main entry point of the application.
    static void Main(string[] args)
*/

{
    // This is the main entry point of the application.
    static void Main(string[] args)
    /* Unmerged change from project 'MAUI(net8.0-maccatalyst)'
    Before:
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    After:
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    */

    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
