using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Carpool
{
    public partial class RoutesView : ContentPage
    {
        private UsersManager usersManager;
        private Users userRoute;
        private ExtMap myMap;
        private Routes routes;

        public RoutesView(Routes routes)
        {
            InitializeComponent();
            this.routes = routes;
            userRoute=new Users
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
            nameLabel.Text =userRoute.Name;
            ageLabel.Text = "Age: " + userRoute.Age;
            phoneLabel.Text = "Phone: " + userRoute.Phone;
            descriptionLabel.Text = routes.Comments;
            departureLabel.Text ="Departure Hour:"+ routes.Depart_Time;
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
