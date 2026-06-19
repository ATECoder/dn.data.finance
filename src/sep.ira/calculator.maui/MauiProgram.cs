using Microsoft.Extensions.Logging;

namespace cc.isr.Finance.Sep.Ira;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts( fonts =>
            {
                fonts.AddFont( "OpenSans-Regular.ttf", "OpenSansRegular" );
                fonts.AddFont( "OpenSans-Semibold.ttf", "OpenSansSemibold" );
                fonts.AddFont( "Cousine-Regular.ttf", "CousineRegular" );
            } );

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
