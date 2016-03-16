using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();
		}

        async void Profile(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new Profile());
            await Navigation.PopAsync();
        }
	}
}
