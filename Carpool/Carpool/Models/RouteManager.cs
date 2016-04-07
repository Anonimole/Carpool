using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<Routes> GetRouteWhere(Expression<Func<Routes,bool>> linq)
        {
            try
            {
                List<Routes> routesList = await routesTable.Where(linq).Take(1).ToListAsync();
                return routesList.First();
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

        public async Task<List<Routes>> ListRoutesWhere(Expression<Func<Routes, bool>> linq)
        {
            try
            {
                return new List<Routes>
                (
                    await routesTable.Where(linq).ToListAsync()
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
