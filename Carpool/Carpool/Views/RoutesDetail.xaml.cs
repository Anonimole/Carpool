using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Carpool
{
    public partial class RoutesDetail : ContentPage
    {
        private UsersManager usersManager;
        private Users userRoute;
        private ExtMap myMap;
        private Routes route;
        private ReservationsManager reservationsManager;
        private Users currentUser;

        public RoutesDetail(Routes route)
        {
            InitializeComponent();
            this.route = route;

            userRoute = new Users
            {
                ID = route.ID_User
            };

            usersManager = new UsersManager();
            reservationsManager = new ReservationsManager();
            currentUser = (Users)Application.Current.Properties["user"];

            this.LoadReservation();
            this.LoadData();

        }

        private async void LoadData()
        {
            this.IsBusy = true;

            userRoute = await usersManager.GetUserWhere(userSelect=>userSelect.ID== userRoute.ID);

            nameLabel.Text = userRoute.Name;
            ageLabel.Text = "Age: " + userRoute.Age;
            phoneLabel.Text = "Phone: " + userRoute.Phone;
            descriptionLabel.Text = route.Comments;
            departureLabel.Text = "Departure Hour:" + route.Depart_Time;
            
            Reservations reservation=new Reservations
            {
                ID_Route = route.ID
            };

            List<Reservations> reservations = await reservationsManager.GetReservationsWhere(reserv => reserv.ID_Route == reservation.ID_Route);
            
            seatsLabel.Text = "Seats Available: " + (route.Capacity-reservations.Count);

            this.IsBusy = false;
        }

        private async void LoadReservation()
        {
            string id_user = currentUser.ID;
            string id_route = route.ID;

            Reservations reservation = new Reservations
            {
                ID_User = id_user,
                ID_Route = id_route
            };

            List<Reservations> reservationResult = await reservationsManager.GetReservationsWhere(reserv => reserv.ID_Route == reservation.ID_Route && reserv.ID_User == reservation.ID_User);

            if (reservationResult.Count != 0)
            {
                cancelButton.IsVisible = true;
                reserveButton.IsVisible = false;
            }
            else
            {
                reserveButton.IsVisible = true;
            }
        }

        private async void OnStartingPoint(object sender, EventArgs e)
        {
            var latitude = Double.Parse(route.From_Latitude);
            var longitude = Double.Parse(route.From_Longitude);

            var position = new Position(latitude, longitude);

            await Navigation.PushAsync(new MapStartingPoint(position));

        }

        private async void OnEndingPoint(object sender, EventArgs e)
        {
            var latitude = Double.Parse(route.To_Latitude);
            var longitude = Double.Parse(route.To_Longitude);

            var position = new Position(latitude, longitude);

            await Navigation.PushAsync(new MapEndingPoint(position));
        }

        private async void OnReserved(object sender, EventArgs e)
        {
            bool r = await DisplayAlert("Reservation", "Add reservation?", "Accept", "Cancel");
            if (r == true)
            {
                activityIndicator.IsRunning = true;

                var newReservation = new Reservations
                {
                    ID_Route = route.ID,
                    ID_User = currentUser.ID
                };

                await reservationsManager.SaveReservationAsync(newReservation);

                activityIndicator.IsRunning = false;
                cancelButton.IsVisible = true;
                reserveButton.IsVisible = false;
            }

        }

        private async void OnCancelReservation(object sender, EventArgs e)
        {

        }
    }
}
