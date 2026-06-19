using Android.App;
using Android.Runtime;

namespace cc.isr.Finance.Sep.Ira;

[Application]
public class MainApplication( IntPtr handle, JniHandleOwnership ownership ) : MauiApplication( handle, ownership )
{
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}
