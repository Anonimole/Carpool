using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Carpool
{
    public partial class AddRoute : ContentPage
    {
        private CarsManager carsManager;
        private Users currentUser;
        private bool carSelected;

        public AddRoute()
        {
            carSelected = false;
            carsManager = new CarsManager();
            currentUser = (Users)Application.Current.Properties["user"];
            this.IsBusy = true;
            InitializeComponent();
        }

        async void OnAdd(object sender, EventArgs e)
        {
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

        }


        public async void OnStartingPoint(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapStartingPoint());
        }

        public async void OnCarPicker(object sender, EventArgs e)
        {
            carSelected = true;
        }

    }
}
