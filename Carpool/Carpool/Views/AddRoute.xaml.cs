using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Carpool
{
    public partial class AddRoute : ContentPage
    {
        private CarsManager carsManager;
        private RouteManager routeManager;
        private Users currentUser;
        private Routes newRoute;
        private bool carSelected;
        private IDictionary<string, object> properties;
        private List<Cars> carsList;

        public AddRoute()
        {
            carSelected = false;
            carsManager = new CarsManager();
            routeManager = new RouteManager();

            properties = Application.Current.Properties;
            currentUser = (Users)Application.Current.Properties["user"];
            this.IsBusy = true;
            InitializeComponent();

            //departureDatePicker.MinimumDate = DateTime.Now;
            departureTimePicker.Time = DateTime.Now.TimeOfDay;

        }

        async void OnAdd(object sender, EventArgs e)
        {
            carSelected = false;
            await Navigation.PushAsync(new AddCar());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            if (carSelected == false)
            {
                carsList = await carsManager.GetMyCarsAsync(currentUser);

                carPicker.Items.Clear();

                foreach (var car in carsList)
                {
                    carPicker.Items.Add(car.Model);
                }

                carPicker.IsEnabled = carPicker.Items.Count > 0 ? true : false;

                this.IsBusy = false;
            }

            if (properties.ContainsKey("route"))
            {
                newRoute = (Routes)Application.Current.Properties["route"];

                if (!string.IsNullOrEmpty(newRoute.From_Latitude))
                    startingPointButton.Text = "Change Starting point";
                if (!string.IsNullOrEmpty(newRoute.To_Latitude))
                    endingPointButton.Text = "Change Ending point";
            }
        }


        public async void OnStartingPoint(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapStartingPoint());
        }

        public async void OnEndingPoint(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapEndingPoint());
        }

        public void OnCarPicker(object sender, EventArgs e)
        {
            carSelected = true;
            carPicker.BackgroundColor = Color.FromHex("#00897B");
        }

        public async void OnSaveRoute(object sender, EventArgs e)
        {
            newRoute = (Routes)properties["route"];
            newRoute.From = startingNameEntry.Text;
            newRoute.To = endingNameEntry.Text;
            newRoute.Capacity = Int32.Parse("" + seatsEntry.Text);
            newRoute.Comments = commentsEditor.Text;
            newRoute.Depart_Time = departureTimePicker.Time.ToString();
            newRoute.ID_User = currentUser.ID;

            DateTimeOffset dateRoute = new DateTimeOffset(departureDatePicker.Date.Add(departureTimePicker.Time));

            newRoute.Depart_Date = dateRoute.DateTime;
            newRoute.Depart_Time = departureTimePicker.Time.ToString();
            string carSelected = carPicker.Items.ElementAt(carPicker.SelectedIndex);
            Cars car = carsList.Where(cars => cars.Model == carSelected).First();
            
            newRoute.ID_Car = car.ID;
            
            activityIndicator.IsRunning = true;
            await routeManager.SaveRouteAsync(newRoute);
            activityIndicator.IsRunning = false;

            await DisplayAlert("Success", "Route added successfully", "Accept");
            properties.Remove("route");
            await Navigation.PopAsync(true);

        }

    }
}
