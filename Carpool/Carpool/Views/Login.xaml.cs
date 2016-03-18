﻿using System;
using Xamarin.Forms;
using System.Diagnostics;


namespace Carpool
{
    public partial class Login : ContentPage
    {
        private Users currentUser;
        private UsersManager manager;

        public Login()
        {


            InitializeComponent();

            manager = new UsersManager();

            NavigationPage.SetHasNavigationBar(this, false);

            this.IsBusy = true;

            //if (Application.Current.Properties.ContainsKey("user"))
            //{
            //    DisplayAlert("alerta", "ya estas logeado", "aceptar");
            //}

        }

        async void SignIn(object sender, EventArgs e)
        {
            string email = this.emailEntry.Text;
            string password = this.passwordEntry.Text;
            var user = new Users { Email = email, Password = password };

            activityIndicator.IsRunning = true;
            Users userResponse = await manager.SearchUserAsync(user);
            activityIndicator.IsRunning = false;

            if (userResponse != null)
            {
                Debug.WriteLine("Si esta");
                if (userResponse.Password.Equals(password))
                {
                    Application.Current.Properties["user"] = userResponse;
                    Application.Current.MainPage = new NavigationPage(new Dashboard());
                }
                else
                {
                    await DisplayAlert("Incorrect", "Incorrect Password", "Close");
                }
            }
            else
            {
                await DisplayAlert("Incorrect", "Incorrect Username", "Close");
            }


        }

        async void SignUp(object sender, EventArgs e)
        {
            var signUpPage = new SignUp();
            await Navigation.PushAsync(signUpPage);
        }


    }
}
