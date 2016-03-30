using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Carpool
{
	public partial class MyRoutesDetail : ContentPage
	{
        private Routes routes;
	    private Users userRoute;
	    private UsersManager usersManager;


        public MyRoutesDetail (Routes route)
        {
            this.routes = route;
			InitializeComponent ();

            userRoute = new Users
            {
                ID = routes.ID_User
            };

            usersManager = new UsersManager();

            this.LoadData();
        }

        private async void LoadData()
        {
            this.IsBusy = true;
            userRoute = await usersManager.SearchIDUserAsync(userRoute);
            nameLabel.Text = userRoute.Name;
            ageLabel.Text = "Age: " + userRoute.Age;
            phoneLabel.Text = "Phone: " + userRoute.Phone;
            descriptionLabel.Text = routes.Comments;
            departureLabel.Text = "Departure Hour:" + routes.Depart_Time;
            seatsLabel.Text = "Seats Available: " + routes.Capacity;
            this.IsBusy = false;
        }

        private async void OnStartingPoint(object sender, EventArgs e)
        {
            var latitude = Double.Parse(routes.From_Latitude);
            var longitude = Double.Parse(routes.From_Longitude);

            var position = new Position(latitude, longitude);

            await Navigation.PushAsync(new MapStartingPoint(position));

        }

        private async void OnEndingPoint(object sender, EventArgs e)
        {
            var latitude = Double.Parse(routes.To_Latitude);
            var longitude = Double.Parse(routes.To_Longitude);

            var position = new Position(latitude, longitude);

            await Navigation.PushAsync(new MapEndingPoint(position));
        }


    }
}
