using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using MatBlazor;
using Microsoft.Extensions.Logging;

namespace SBASeatingPlan
{
    public static class MauiProgram
    {
        private const string BASE_URL = "lkpfcsba.firebaseapp.com";
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
            builder.Services.AddMatBlazor();

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "\r\nAIzaSyBgg0RWTSlQWh1RG14iKNqxd4it9YDbApA",
                AuthDomain = BASE_URL,
                Providers =
                [
                    new EmailProvider(), new GoogleProvider(),
                ]
            }));
            builder.Services.AddSingleton(new FirebaseClient("https://lkpfcsba-default-rtdb.asia-southeast1.firebasedatabase.app/"));

            return builder.Build();
        }
    }
}
