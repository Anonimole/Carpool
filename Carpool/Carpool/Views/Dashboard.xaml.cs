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
            await Navigation.PushAsync(new Route());
        }

        async void ProfileDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        async void LogOut(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Login());
        }

    }
}
