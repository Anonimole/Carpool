using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class MyRoutes : ContentPage
	{
        private Users currentUser;
        private List<Routes> routesList;
        private RouteManager routeManager;
        public MyRoutes ()
		{
            routesList = new List<Routes>();
            routeManager = new RouteManager();
            InitializeComponent ();

            currentUser = (Users)Application.Current.Properties["user"];
            routesListView.ItemTemplate = new DataTemplate(typeof(RoutesCell));
            
        }

        private async void LoadRoutes()
        {
            ObservableCollection<Routes> routesCollection = await routeManager.GetMyRoutesAsync(currentUser);
            routesListView.ItemsSource = routesCollection;
        }

        async void AddRoute(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRoute());
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            this.LoadRoutes();
        }
	}
}
