using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Carpool
{
    public partial class AddRoute : ContentPage
    {
        private CarsManager carsManager;
        private Users currentUser;
        private Route newRoute;
        private bool carSelected;
        private IDictionary<string,object> properties;

        public AddRoute()
        {
            carSelected = false;
            carsManager = new CarsManager();
            properties = Application.Current.Properties;
            currentUser = (Users)Application.Current.Properties["user"];
            this.IsBusy = true;
            InitializeComponent();
            
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
                var carsList = await carsManager.GetMyCarsAsync(currentUser);

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
                newRoute = (Route)Application.Current.Properties["route"];

                if(!string.IsNullOrEmpty(newRoute.From_Latitude))
                startingPointButton.Text = "Change Starting point";
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
        }

    }
}
