using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class MyRoutes : ContentPage
    {
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;
        public MyRoutes()
        {
            routesList = new List<Routes>();
            routeManager = new RouteManager();
            InitializeComponent();

            currentUser = (Users)Application.Current.Properties["user"];
            routesListView.ItemTemplate = new DataTemplate(typeof(RoutesCell));

            routesListView.Refreshing += RoutesListView_Refreshing;

        }

        private void RoutesListView_Refreshing(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        private async void LoadRoutes()
        {
            routesListView.IsRefreshing = true;
            ObservableCollection<Routes> routesCollection = await routeManager.GetMyRoutesAsync(currentUser);
            errorLayout.Children.Clear();
            if (routesCollection.Count == 0)
            {

                errorLayout.Children.Add(new Label
                {
                    Text = "No routes available",
                    TextColor = Color.White,
                    FontSize = 25,
                    HorizontalTextAlignment = TextAlignment.Center
                });
            }
            else
            {
                routesListView.ItemsSource = routesCollection;
            }

            routesListView.IsRefreshing = false;
        }

        async void AddRoute(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRoute());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.LoadRoutes();
        }
    }
}
