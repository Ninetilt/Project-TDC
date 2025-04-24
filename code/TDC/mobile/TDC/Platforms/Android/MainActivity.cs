using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;

namespace TDC
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            Page p = Shell.Current.CurrentPage;

            if (p is IOnPageKeyDown)
            {
                bool handled = (p as IOnPageKeyDown).OnPageKeyDown(keyCode, e);

                if (handled) return true;
                else return base.OnKeyDown(keyCode, e);
            }
            else return base.OnKeyDown(keyCode, e);
        }
    }
}
