using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Carpool
{
    class RouteManager
    {
        private IMobileServiceTable<Routes> routesTable;
        private MobileServiceClient client;

        public RouteManager()
        {
            client = new MobileServiceClient(Constants.ApplicationURL,Constants.ApplicationKey);
            routesTable = client.GetTable<Routes>();
        }

        public async Task SaveRouteAsync(Routes route)
        {
            if (route.ID == null)
            {
                await routesTable.InsertAsync(route);
            }
            else
            {
                await routesTable.UpdateAsync(route);
            }
        }

    }
}
