using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class AddCar : ContentPage
	{
        private Users currentUser;
	    private CarsManager manager;

        public AddCar ()
        {

			InitializeComponent ();

            currentUser = (Users)Application.Current.Properties["user"];
            manager = new CarsManager();
        }


	    async Task AddNewCar(Cars car)
	    {
	        await manager.SaveCarAsync(car);
	    }

	    public async void OnAdd(object sender, EventArgs e)
	    {
	        string model = modelEntry.Text;
	        string color = colorEntry.Text;

	        activityIndicator.IsRunning = true;

	        var car = new Cars {Color = color, Model = model, ID_User = currentUser.ID};

            if (!string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(color))
            {
                await AddNewCar(car);
                activityIndicator.IsRunning = false;

                await Navigation.PopAsync(true);
            }
            else
            {
                await DisplayAlert("Incorrect", "Model or Color fields cannot be empty, please insert a value.", "Close");
                activityIndicator.IsRunning = false;
            }
	    }
	}
}
