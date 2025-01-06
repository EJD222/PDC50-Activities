using Microsoft.Extensions.Logging;

namespace Module05Exercise01
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
                    fonts.AddFont("Sora-Regular.ttf", "SoraRegular");
                    fonts.AddFont("Sora-Semibold.ttf", "SoraSemibold");
                    fonts.AddFont("Sora-Bold.ttf", "SoraBold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
