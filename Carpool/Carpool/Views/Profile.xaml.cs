using System;
using System.Threading.Tasks;
using Xamarin.Forms;

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

                if (!string.IsNullOrEmpty(currentUser.Name))
                    nameEntry.Text = currentUser.Name;

                if (currentUser.Age!=0)
                    ageEntry.Text = currentUser.Age + "";

                if (!string.IsNullOrEmpty(currentUser.Phone))
                    phoneEntry.Text = currentUser.Phone;

                if (!string.IsNullOrEmpty(currentUser.Phone))
                {
                    genderPicker.SelectedIndex = Array.IndexOf(genders, currentUser.Gender);
                    genderPicker.BackgroundColor = Color.FromHex("#004D40");
                }

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
            activityIndicator.IsRunning = false;
            Application.Current.MainPage = new NavigationPage(new Dashboard());
        }
    }
}
