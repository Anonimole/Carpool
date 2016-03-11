using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        void SignIn(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new Dashboard());
            var page = new NavigationPage(new Dashboard());
            

            Application.Current.MainPage = page;

        }

        async void SignUp(object sender, EventArgs e)
        {
            var signUpPage = new SignUp();
            
            await Navigation.PushAsync(new SignUp());
        }

        
        NavigationPage ToolBarInit(NavigationPage page)
        {
            page.BarBackgroundColor = Color.FromHex("#004D40");
            page.BarTextColor = Color.White;

            return page;
        }


	}
}
