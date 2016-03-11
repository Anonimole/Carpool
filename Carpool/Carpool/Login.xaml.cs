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
		}

        void SignIn(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new Dashboard());
            var page = new NavigationPage(new Dashboard());
            page.BarBackgroundColor = Color.FromHex("#004D40");
            page.BarTextColor=Color.White;
            

            Application.Current.MainPage = page;

        }
	}
}
