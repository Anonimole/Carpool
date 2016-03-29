using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class Dashboard : ContentPage
    {
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;

        public Dashboard()
        {
            InitializeComponent();

            routesList=new List<Routes>();
            routeManager=new RouteManager();

            currentUser =(Users) Application.Current.Properties["user"];
            routesListView.ItemTemplate=new DataTemplate(typeof(RoutesCell));
            this.LoadRoutes();

            routesListView.ItemTapped += RoutesListView_ItemTapped; ;
        }

        private async void RoutesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var route = e.Item as Routes;
            await Navigation.PushAsync(new RoutesView(route));
        }

        private async void LoadRoutes()
        {
            ObservableCollection<Routes> routesCollection= await routeManager.GetRoutesAsync();
            routesListView.ItemsSource = routesCollection;
        }

        async void RouteDetails(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new RoutesView());
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
