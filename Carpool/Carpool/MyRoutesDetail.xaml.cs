using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Carpool
{
	public partial class MyRoutesDetail : ContentPage
	{
        private Routes route;
	    private Users userRoute;
	    private UsersManager usersManager;
	    private Users currentUser;
	    private ReservationsManager reservationsManager;
	    private List<Reservations> reservationResult;


        public MyRoutesDetail (Routes route)
        {
            this.route = route;

            currentUser = (Users) Application.Current.Properties["user"];
            reservationsManager=new ReservationsManager();
			InitializeComponent ();

            userRoute = new Users
            {
                ID = this.route.ID_User
            };

            usersManager = new UsersManager();

            this.LoadReservation();
            this.LoadData();
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

            reservationResult = await reservationsManager.GetReservationsWhere(res=>res.ID_Route==reservation.ID_Route);

            if (reservationResult.Count != 0)
            {
                foreach (var res in reservationResult)
                {
                    contentLayout.Children.Add(new Label { Text = "algo" });
                }
            }
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
            seatsLabel.Text = "Seats Available: " + (route.Capacity-reservationResult.Count)+"/"+route.Capacity;
            this.IsBusy = false;
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


    }
}
