using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class Dashboard : ContentPage
    {
        private Users currentUser;

        public Dashboard()
        {
            InitializeComponent();

            currentUser =(Users) Application.Current.Properties["user"];
        }

        async void RouteDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoutesView());
        }

        async void MyRoutes(object sende, EventArgs e)
        {
            await Navigation.PushAsync(new MyRoutes());
        }

        async void ProfileDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        void LogOut(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Login());
        }

    }
}
