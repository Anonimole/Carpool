using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;
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

        public async Task<ObservableCollection<Routes>> GetRoutesAsync(Users user)
        {
            try
            {
                return new ObservableCollection<Routes>
                (
                    await routesTable.Where(route=>route.ID_User!=user.ID).ToListAsync()
                );
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Routes>> GetMyRoutesAsync(Users user)
        {
            try
            {
                return new ObservableCollection<Routes>
                (
                    await routesTable.Where(route => route.ID_User == user.ID).ToListAsync()
                );
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

    }
}
