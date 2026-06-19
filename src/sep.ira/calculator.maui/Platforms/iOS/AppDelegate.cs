using Foundation;

namespace cc.isr.Finance.Sep.Ira;

[Register( "AppDelegate" )]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public class AppDelegate : MauiUIApplicationDelegate
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}
