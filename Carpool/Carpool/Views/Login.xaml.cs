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
            var page = new NavigationPage(new Dashboard());
            
            Application.Current.MainPage = page;

        }

        async void SignUp(object sender, EventArgs e)
        {
            var signUpPage = new SignUp();
            await Navigation.PushAsync(signUpPage);
        }
        
	}
}
