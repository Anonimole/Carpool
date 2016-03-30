using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Carpool
{
    public partial class Dashboard : ContentPage
    {
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;
        private ObservableCollection<Routes> routesCollection;

        public Dashboard()
        {
            InitializeComponent();
            
            routesList = new List<Routes>();
            routeManager = new RouteManager();

            currentUser = (Users)Application.Current.Properties["user"];
            routesListView.ItemTemplate = new DataTemplate(typeof(RoutesCell));

            routesListView.ItemTapped += RoutesListView_ItemTapped;

            routesListView.Refreshing += RoutesListView_Refreshing;
        }

        private void RoutesListView_Refreshing(object sender, EventArgs e)
        {
            searchBar.Text = "";
            LoadRoutes();
        }

        private async void RoutesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var route = e.Item as Routes;
            await Navigation.PushAsync(new RoutesDetail(route));
        }

        private async void LoadRoutes()
        {
            routesListView.IsRefreshing = true;
            routesCollection = await routeManager.GetRoutesAsync(currentUser);
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

        protected override void OnAppearing()
        {
            routesListView.IsRefreshing = true;
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
                routesListView.ItemsSource = routesCollection.Where(route => route.From.Contains(e.NewTextValue) || route.To.Contains(e.NewTextValue));
            }
        }

    }
}
