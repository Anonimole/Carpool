using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Carpool
{
    public partial class Profile : ContentPage
    {
        private string genderSelected;
        private Users currentUser;
        UsersManager manager;

        public Profile()
        {
            genderSelected = "";

            InitializeComponent();

            manager = new UsersManager();

            currentUser = (Users)Application.Current.Properties["user"];

            genderPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (genderPicker.SelectedIndex == -1)
                {
                    genderSelected = "";
                }
                else
                {
                    genderSelected = genderPicker.Items[genderPicker.SelectedIndex];
                    genderPicker.BackgroundColor = Color.FromHex("#004D40");

                }
            };

        }

        async Task UpdateUser(Users user)
        {
            Users userResponse = await manager.SaveGetUserAsync(user);
            Application.Current.Properties["user"] = userResponse;
        }

        async void Dashboard(object sender, EventArgs e)
        {

            string name = this.nameEntry.Text;
            int age = Int32.Parse(this.ageEntry.Text);
            string phone = this.phoneEntry.Text;


            var user = new Users
            {
                ID = currentUser.ID,
                Email = currentUser.Email,
                Password = currentUser.Password,
                Name = name,
                Age = age,
                Phone = phone,
                Gender = genderSelected
            };

            activityIndicator.IsRunning = true;

            await UpdateUser(user);

            Application.Current.MainPage = new NavigationPage(new Dashboard());
        }
    }
}
