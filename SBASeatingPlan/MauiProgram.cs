using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using SBASeatingPlan.Auth0;

namespace SBASeatingPlan
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
            builder.Services.AddMatBlazor();
            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-kkiz0nhcdyjfur5c.us.auth0.com",
                ClientId = "a4fWMPaDYwJ5cCbFNmxFwCcK3dP4ib8y",
                Scope = "openid profile",
                RedirectUri = "myapp://callback"
            }));
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();

            return builder.Build();
        }
    }
}
