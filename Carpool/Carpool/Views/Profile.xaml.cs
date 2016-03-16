using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class Profile : ContentPage
	{
		public Profile ()
		{
			InitializeComponent ();
		}

        async void Dashboard(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Dashboard());
        }
	}
}
