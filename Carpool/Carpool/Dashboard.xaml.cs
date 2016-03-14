using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            
            InitializeComponent();
            
            this.routesList.ItemsSource = new string[]{
              "Cluster - Catedral",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta",
              "Cluster - Catedral",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta",
              "Cluster - Av. Ventura Puente",
              "Cluster - Plaza Las Americas",
              "Cluster - Plaza la Huerta"
            };
        }
    }
}
