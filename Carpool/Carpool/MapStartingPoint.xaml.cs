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
        public MapStartingPoint()
        {
            InitializeComponent();

            pinFlag = false;

            myMap = new ExtMap
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser = true
            };

            stackMap.Children.Add(myMap);
            myMap.Tapped += MyMap_Tapped;
            locator();

            var saveButton = new ToolbarItem
            {
                Name = "Save",
                Command = new Command(this.Save),
            };

            this.ToolbarItems.Add(saveButton);

        }


        private async void Save()
        {
            await Navigation.PopAsync(true);
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

                pin.Clicked += Pin_Clicked;
                myMap.Pins.Add(pin);
            }
            infoLabel.Text = "";

        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
            
        }

        async void searchAdress()
        {
            Geocoder geocoder = new Geocoder();
            //IEnumerable<string> adresses = await geocoder.GetAddressesForPositionAsync(position);
            await geocoder.GetPositionsForAddressAsync("");

        }

        async void locator()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 40000);
                Console.WriteLine("Position Status: {0}", position.Timestamp);
                Console.WriteLine("Position Latitude: {0}", position.Latitude);
                Console.WriteLine("Position Longitude: {0}", position.Longitude);
                var pos = new Position(position.Latitude, position.Longitude);
                myMap.MoveToRegion(new MapSpan(pos, 0.01, 0.01));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
        }
    }
}
