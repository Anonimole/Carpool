using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
	public partial class AppStart : Application
	{
		public AppStart ()
		{
			InitializeComponent ();
            MainPage = new Login();
            //MainPage = new NavigationPage(new Dashboard());
		}
	}
}
