using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Carpool
{
    class CarsManager
    {
        IMobileServiceTable<Cars> carsTable;
        MobileServiceClient client;

        public CarsManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            carsTable = client.GetTable<Cars>();
        }

        public async Task SaveCarAsync(Cars car)
        {
            if (car.ID == null)
            {
                await carsTable.InsertAsync(car);
            }
            else
            {
                await carsTable.UpdateAsync(car);
            }
        }


        public async Task<List<Cars>> GetMyCarsAsync(Users user)
        {
            try
            {
                return await carsTable.Where(cars => cars.ID_User == user.ID).ToListAsync();
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
