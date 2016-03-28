using Foundation;
using UIKit;

namespace Carpool.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            SetStyles();
			global::Xamarin.Forms.Forms.Init ();
            Xamarin.FormsMaps.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            LoadApplication (new Carpool.AppStart ());

			return base.FinishedLaunching (app, options);
		}

        static void SetStyles()
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(0,77,64);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.White
            });

            UIToolbar.Appearance.BarTintColor= UIColor.FromRGB(38, 166, 154);

        }
    }
}
