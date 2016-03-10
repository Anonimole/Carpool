using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carpool
{
    public partial class Dashboard : TabbedPage
    {
        public Dashboard()
        {
            
            InitializeComponent();

            


            this.routesList.ItemsSource = new string[]{
              "mono",
              "monodroid",
              "monotouch",
              "monorail",
              "monodevelop",
              "monotone",
              "monopoly",
              "monomodal",
              "mononucleosis"
            };
        }
    }
}
