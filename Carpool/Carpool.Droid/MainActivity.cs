using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Carpool.Droid
{
	[Activity(Theme = "@style/Theme.Example", Label = "Carpool", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            Xamarin.FormsMaps.Init(this,bundle);
			LoadApplication (new Carpool.AppStart ());
		}
	}
}

