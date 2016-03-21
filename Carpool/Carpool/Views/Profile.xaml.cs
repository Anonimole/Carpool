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
            
            InitializeComponent();

            genderSelected = "";
            manager = new UsersManager();
            currentUser = (Users)Application.Current.Properties["user"];

            loadData();

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


        void loadData()
        {
            string[] genders = { "Male", "Female" };

            if (currentUser != null)
            {
                nameEntry.Text = currentUser.Name;
                ageEntry.Text = currentUser.Age+"";
                phoneEntry.Text = currentUser.Phone;
                genderPicker.SelectedIndex= Array.IndexOf(genders, currentUser.Gender);

            }
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
