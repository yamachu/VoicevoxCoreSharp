using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        /* Unmerged change from project 'MAUI(net8.0-ios)'
        Before:
                builder
                    .UseMauiApp<App>()
        After:
                builder
                    .UseMauiApp<App>()
        */

        /* Unmerged change from project 'MAUI(net8.0-maccatalyst)'
        Before:
                builder
                    .UseMauiApp<App>()
        After:
                builder
                    .UseMauiApp<App>()
        */
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
