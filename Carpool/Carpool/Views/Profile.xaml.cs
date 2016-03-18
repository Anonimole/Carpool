using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Diagnostics;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Carpool
{
	public partial class Profile : ContentPage
	{
        private string genderSelected;

        public Profile ()
        {
	        genderSelected = "";
            
			InitializeComponent ();

            genderPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (genderPicker.SelectedIndex == -1)
                {
                    genderSelected="";
                }
                else
                {
                    genderSelected = genderPicker.Items[genderPicker.SelectedIndex];
                    genderPicker.BackgroundColor = Color.FromHex("#004D40");

                }
            };
            
        }

        async void Dashboard(object sender, EventArgs e)
        {

            string name = this.nameEntry.Text;
            string age = this.ageEntry.Text;
            string phone = this.phoneEntry.Text;

	        
            Debug.WriteLine("{0} {1} {2} {3} " ,name,age,phone,genderSelected);

            //Application.Current.MainPage = new NavigationPage(new Dashboard());
        }
	}
}
