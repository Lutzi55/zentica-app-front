using Microsoft.Extensions.Logging;
using Zentica_frontend.Services;

namespace Zentica_frontend
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif


            // Fechas y números en español (Argentina)
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = Formato.Es;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = Formato.Es;

            // URL de la API según dónde corra la app
            // UsarHosting = true  -> todas las plataformas usan la API publicada en MonsterASP
            // UsarHosting = false -> Windows usa la API local (localhost)
            const bool UsarHosting = true;
            const string UrlHosting = "http://zentica.runasp.net";
            string baseUrl = UsarHosting || DeviceInfo.Platform == DevicePlatform.Android
                ? UrlHosting
                : "http://localhost:5025";

            // Singleton: un solo HttpClient y un solo ApiService (guarda el token de la sesión)
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(20)
            });
            builder.Services.AddSingleton<ApiService>();

            return builder.Build();
        }
    }
}
