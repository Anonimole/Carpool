using Plugin.Geolocator;
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
    public partial class MapStartingPoint : ContentPage
    {
        private ExtMap myMap;
        private bool pinFlag;
        private Route newRoute;
        public MapStartingPoint()
        {
            InitializeComponent();

            pinFlag = false;

            myMap = new ExtMap
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser = true
            };
            myMap.Tapped += MyMap_Tapped;

            this.IsBusy = true;
            this.Locator();

            if (Application.Current.Properties["route"] != null)
            {
                newRoute = (Route)Application.Current.Properties["route"];
            }
            else
            {
                newRoute = new Route();
            }

        }


        private async void Save()
        {
            string latitude = "" + myMap.Pins.First().Position.Latitude;
            string longitude = "" + myMap.Pins.First().Position.Longitude;

            newRoute.From_Latitude = latitude;
            newRoute.From_Longitude = longitude;

            Application.Current.Properties["route"] = newRoute;

            await Navigation.PopAsync(true);

        }

        private void Cancel()
        {
            pinFlag = false;
            infoLabel.Text = "Tap starting point in the Map";
            this.myMap.Pins.Clear();
            this.ToolbarItems.Clear();
        }

        private async void MyMap_Tapped(object sender, MapTapEventArgs e)
        {

            if (pinFlag == false)
            {
                infoLabel.Text = "Adding starting point";
                pinFlag = true;
                var latitude = e.Position.Latitude;
                var longitude = e.Position.Longitude;

                var position = new Position(latitude, longitude);

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = "Start",
                    Address = "",
                };

                myMap.Pins.Add(pin);

                var saveButton = new ToolbarItem
                {
                    Name = "Save",
                    Command = new Command(this.Save),
                };

                var cancelButton = new ToolbarItem
                {
                    Name = "Cancel",
                    Command = new Command(this.Cancel),
                };
                
                this.ToolbarItems.Add(saveButton);
                this.ToolbarItems.Add(cancelButton);

                infoLabel.Text = "";
            }

        }

        async void searchAdress()
        {
            Geocoder geocoder = new Geocoder();
            //IEnumerable<string> adresses = await geocoder.GetAddressesForPositionAsync(position);
            await geocoder.GetPositionsForAddressAsync("");

        }

        async void Locator()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 40000);
                var pos = new Position(position.Latitude, position.Longitude);
                myMap.MoveToRegion(new MapSpan(pos, 0.01, 0.01));
                stackMap.Children.Add(myMap);
                this.IsBusy = false;
                infoLabel.Text = "Tap starting point in the Map";
            }
            catch (Exception ex)
            {
                infoLabel.Text = "Unable to get location, check GPS connection";
            }
        }
    }
}
