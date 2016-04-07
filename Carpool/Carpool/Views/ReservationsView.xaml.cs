using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class ReservationsView : ContentPage
    {
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;
        private List<Routes> routesReservations;

        private List<Reservations> reservationsList;
        private ReservationsManager reservationManager;

        public ReservationsView()
        {
            InitializeComponent();
            
            currentUser = (Users)Application.Current.Properties["user"];

            routesList = new List<Routes>();
            routeManager = new RouteManager();

            reservationsList = new List<Reservations>();
            reservationManager = new ReservationsManager();

            

            routesListView.ItemTemplate = new DataTemplate(typeof(RoutesCell));

            routesListView.ItemTapped += RoutesListView_ItemTapped;

            routesListView.Refreshing += RoutesListView_Refreshing;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRoutes();
        }

        private async void LoadRoutes()
        {

            routesReservations = new List<Routes>();

            routesList = await routeManager.ListRoutesWhere(route => route.ID_User != currentUser.ID && route.Depart_Date > DateTime.Now);
            reservationsList = await reservationManager.GetReservationsWhere(reservation => reservation.ID_User == currentUser.ID);

            foreach (var reservation in reservationsList)
            {
                var route = routesList.Find(routes => routes.ID == reservation.ID_Route);
                if (route != null)
                {
                    routesReservations.Add(route);
                }
            }

            if (routesReservations.Count > 0)
            {
                errorLayout.IsVisible = false;
            }
            else
            {
                errorLayout.IsVisible=true;
                errorLabel.Text = "No reservations available";
                routesListView.BackgroundColor = Color.FromHex("#009688");
                await Navigation.PopAsync();
            }

            routesListView.ItemsSource = routesReservations;

            routesListView.IsRefreshing = false;
        }

        private void RoutesListView_Refreshing(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        private async void RoutesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var route = e.Item as Routes;
            await Navigation.PushAsync(new RoutesDetail(route));
        }

        public void OnSearch(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                routesListView.ItemsSource = routesReservations;
            }
            else
            {
                routesListView.ItemsSource =
                    routesReservations.Where(
                        route => route.From.Contains(e.NewTextValue) || route.To.Contains(e.NewTextValue));
            }
        }
    }
}
