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
		public AddCar ()
		{
			InitializeComponent ();
		}

	    public async void OnAdd(object sender, EventArgs e)
	    {
	        string model = modelEntry.Text;
	        string color = colorEntry.Text;


	    }
	}
}
