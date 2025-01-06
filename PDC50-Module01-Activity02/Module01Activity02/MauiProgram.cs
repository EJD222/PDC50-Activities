using Microsoft.Extensions.Logging;

namespace Module01Activity02
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter-Semibold.ttf", "InterSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
