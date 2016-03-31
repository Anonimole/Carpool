using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private ReservationsManager reservationManager;

        public Dashboard()
        {
            InitializeComponent();

            routesList = new List<Routes>();
            routeManager = new RouteManager();
            reservationManager = new ReservationsManager();
            
            currentUser = (Users) Application.Current.Properties["user"];

            this.LoadReservations();
            routesListView.ItemTemplate = new DataTemplate(typeof (RoutesCell));

            routesListView.ItemTapped += RoutesListView_ItemTapped;

            routesListView.Refreshing += RoutesListView_Refreshing;
        }

        private async void LoadReservations()
        {
            var reservation= new Reservations
            {
                ID_User = currentUser.ID
            };

            List<Reservations> reservationsList = await reservationManager.GetReservationsWhere(reserv => reserv.ID_User == reservation.ID_User);

            //Debug.WriteLine(reservationsList.First().ID_Route);
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
            routesCollection = await routeManager.ListRoutesWhere(route => route.ID_User != currentUser.ID);
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

        private async void MyRoutes(object sende, EventArgs e)
        {
            await Navigation.PushAsync(new MyRoutes());
        }

        private async void ProfileDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        private void LogOut(object sender, EventArgs e)
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
                routesListView.ItemsSource =
                    routesCollection.Where(
                        route => route.From.Contains(e.NewTextValue) || route.To.Contains(e.NewTextValue));
            }
        }
    }
}