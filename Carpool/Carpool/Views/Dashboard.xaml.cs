using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

            routesListView.ItemTapped += RoutesListView_ItemTapped;
            
            routesListView.Refreshing += RoutesListView_Refreshing;
        }

        private async void RoutesListView_Refreshing(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        private async void RoutesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var route = e.Item as Routes;
            await Navigation.PushAsync(new RoutesView(route));
        }

        private async void LoadRoutes()
        {

            routesListView.IsRefreshing = true;
            ObservableCollection<Routes> routesCollection= await routeManager.GetRoutesAsync(currentUser);
            errorLayout.Children.Clear();
            if (routesCollection.Count==0)
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

        protected override void OnAppearing()
        {
            routesListView.IsRefreshing = true;
            base.OnAppearing();
            this.LoadRoutes();
        }

    }
}
