using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class SignUp : ContentPage
    {
        UsersManager manager;

        public SignUp()
        {
            InitializeComponent();

            manager = new UsersManager();
        }

        async Task AddUser(Users user)
        {
            await manager.SaveUserAsync(user);

        }

        async void OnSignUp(object sender, EventArgs e)
        {

            string email = this.emailEntry.Text;
            string password = this.passwordEntry.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                this.activityIndicator.IsRunning = true;
                this.signUpButton.IsEnabled = false;

                var user = new Users
                {
                    Email = email,
                    Password = password,
                    Name = string.Empty,
                    Age = 0,
                    Gender = string.Empty,
                    Phone = string.Empty
                };

                await AddUser(user);
                
                Collection<Users> usersCollection= await manager.SearchUserAsync(email);

                Users userObject = usersCollection.First();
                Application.Current.Properties["user"]= userObject;
                Debug.WriteLine(userObject.ID);
                
                await Navigation.PushModalAsync(new Profile());
                await Navigation.PopAsync();
            }
            else
            {
                this.emailEntry.PlaceholderColor = this.emailEntry.TextColor = Color.FromHex("#f44336");
                this.passwordEntry.PlaceholderColor = this.passwordEntry.TextColor = Color.FromHex("#f44336");
            }

        }

        async void EmailTextChanged(object sender, EventArgs e)
        {
            Entry emailEntry = (Entry)sender;

            string email = this.emailEntry.Text;

            if (!string.IsNullOrEmpty(email))
            {
                this.emailEntry.PlaceholderColor = this.emailEntry.TextColor = Color.FromHex("#00695C");
                this.activityIndicator.IsRunning = true;
                ObservableCollection<Users> usersList = await manager.GetUsersAsync(email);
                this.emailEntryError.IsVisible = usersList.Count > 0 ? true : false;

                this.activityIndicator.IsRunning = false;
            }

        }

        void PasswordTextChanged(object sender, EventArgs e)
        {
            string password = this.passwordEntry.Text;
            if (!string.IsNullOrEmpty(password))
            {
                this.passwordEntry.PlaceholderColor = this.passwordEntry.TextColor = Color.FromHex("#00695C");
            }
            //signUpButton.IsEnabled = password.Length > 0 ? true : false;
        }
    }
}
