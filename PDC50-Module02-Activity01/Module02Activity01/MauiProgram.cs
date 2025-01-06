using Microsoft.Extensions.Logging;

namespace Module02Activity01
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
                    fonts.AddFont("Nunito-Regular", "NunitoRegular");
                    fonts.AddFont("Nunito-SemiBold", "NunitoSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
