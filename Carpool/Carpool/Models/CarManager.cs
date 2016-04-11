using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Carpool
{
    class CarManager
    {
        IMobileServiceTable<Car> carsTable;
        MobileServiceClient client;

        public CarManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            carsTable = client.GetTable<Car>();
        }

        public async Task SaveCarAsync(Car car)
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


        public async Task<List<Car>> GetMyCarsAsync(User user)
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
