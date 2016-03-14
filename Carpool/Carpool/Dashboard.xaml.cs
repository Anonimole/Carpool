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
        public Dashboard()
        {

            InitializeComponent();

            /*this.routesList.ItemsSource = new string[]{
              "Cluster - Catedral",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta",
              "Cluster - Catedral",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta"
            };*/
        }

        async void RouteDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Route());
        }

        async void ProfileDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        async void LogOff(object sender, EventArgs e)
        {
           
        }

    }
}
