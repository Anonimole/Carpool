using Xamarin.Forms;

namespace Carpool
{
    public partial class RoutesView : ContentPage
    {
        private UsersManager usersManager;
        private Users userRoute;

        private Routes routes;

        public RoutesView(Routes routes)
        {
            this.routes = routes;
            userRoute=new Users
            {
                ID = routes.ID_User
            };

            usersManager=new UsersManager();
            InitializeComponent();

            this.LoadData();
            


        }

        private async void LoadData()
        {
            userRoute = await usersManager.SearchIDUserAsync(userRoute);
            nameLabel.Text =userRoute.Name;
            ageLabel.Text = "Age: " + userRoute.Age;
            phoneLabel.Text = "Phone: " + userRoute.Phone;

            descriptionLabel.Text = routes.Comments;
            departureLabel.Text ="Departure Hour:"+ routes.Depart_Time;
            seatsLabel.Text = "Seats Available: " + routes.Capacity;
            
        }

    }
}
