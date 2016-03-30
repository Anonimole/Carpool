using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Carpool
{
    public partial class MyRoutes : ContentPage
    {
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;
        private ObservableCollection<Routes> routesCollection;

        public MyRoutes()
        {
            routesList = new List<Routes>();
            routeManager = new RouteManager();
            InitializeComponent();
            currentUser = (Users)Application.Current.Properties["user"];
            routesListView.ItemTemplate = new DataTemplate(typeof(RoutesCell));
            routesListView.Refreshing += RoutesListView_Refreshing;
            routesListView.ItemTapped += RoutesListView_ItemTapped;
        }

        private async void RoutesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var route = e.Item as Routes;
            await Navigation.PushAsync(new MyRoutesDetail(route));
        }

        private void RoutesListView_Refreshing(object sender, EventArgs e)
        {
            searchBar.Text = "";
            LoadRoutes();
        }

        private async void LoadRoutes()
        {
            routesListView.IsRefreshing = true;
            routesCollection = await routeManager.GetMyRoutesAsync(currentUser);
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

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                routesListView.ItemsSource = routesCollection;
            }
            else
            {
                routesListView.ItemsSource = routesCollection.Where(route => (route.From).ToLower().Contains(e.NewTextValue.ToLower()) || (route.To).ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}
