using Acr.UserDialogs;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;

namespace AndroidPatientAppMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                   // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   // fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SanchezSlab.ttf", "SanchezSlab");
                })
                .ConfigureEffects(effects =>
                {
                    effects.AddCompatibilityEffects(typeof(Xamarin.CommunityToolkit.Effects.TouchEffect).Assembly);
                })
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnApplicationCreate(app => UserDialogs.Init(app)));
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if ANDROID
            // builder.Services.AddSingleton(UserDialogs.Instance);
#endif
            return builder.Build();
        }
    }
}
