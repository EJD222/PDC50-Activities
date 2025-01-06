using Microsoft.Extensions.Logging;
using Module03Exercise01.View;
using Module03Exercise01.Services;
using CommunityToolkit.Maui;

namespace Module03Exercise01
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                 .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            //Register the Service
            builder.Services.AddSingleton<IMyService, MyService>();

            //Register the ContentPage
            builder.Services.AddTransient<LoginPage>();

    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
