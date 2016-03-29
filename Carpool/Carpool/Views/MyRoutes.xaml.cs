using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class MyRoutes : ContentPage
	{
		public MyRoutes ()
		{
			InitializeComponent ();
		}

        async void RouteDetails(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new RoutesView());
        }

        async void AddRoute(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRoute());
        }

    }
}
