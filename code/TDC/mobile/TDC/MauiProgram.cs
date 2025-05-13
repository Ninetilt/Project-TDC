using Microsoft.Extensions.Logging;
using TDC.Services;             // <-- Hinzufügen
using TDC.IRepository;          // <-- Hinzufügen
using TDC.Repositories;         // <-- Hinzufügen
using TDC.Views.Login;          // <-- Hinzufügen

namespace TDC
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUISystemIcons");
                    fonts.AddFont("NicoMoji-Regular.ttf", "Title");
                    fonts.AddFont("Montserrat-Regular.ttf", "Text");
                    fonts.AddFont("Montserrat-Bold.ttf", "Text-Bold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // ------------ HIER DIE FEHLENDEN DI-REGISTRIERUNGEN EINFÜGEN ------------
            // Services registrieren
            builder.Services.AddSingleton<UserService>(); // Einmal pro App-Lebenszeit
            builder.Services.AddSingleton<IAccountRepository, AccountRepository>(); // Einmal pro App-Lebenszeit
            builder.Services.AddTransient<ListView>(); // wichtig für DI


            // Pages registrieren (Transient = jedes Mal eine neue Instanz, wenn sie benötigt wird)
            builder.Services.AddTransient<LoginPage>();
            // Füge hier ggf. weitere Pages hinzu, die DI verwenden
            // builder.Services.AddTransient<MainPage>(); // Beispiel, falls MainPage auch DI braucht
            // -----------------------------------------------------------------------

            return builder.Build();
        }
    }
}