using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Carpool
{
    class ReservationsManager
    {
        IMobileServiceTable<Reservations> reservationsTable;
        MobileServiceClient client;

        public ReservationsManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            reservationsTable = client.GetTable<Reservations>();
        }

        public async Task SaveReservationAsync(Reservations reservation)
        {
            if (reservation.ID == null)
            {
                await reservationsTable.InsertAsync(reservation);
            }
            else
            {
                await reservationsTable.UpdateAsync(reservation);
            }
        }

        public async Task<List<Reservations>> GetReservationsAsync(Reservations reservations)
        {
            try
            {
                return await reservationsTable.Where(reserv => reserv.ID_Route == reservations.ID_Route&& reserv.ID_User==reservations.ID_User).ToListAsync();
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

        public async Task<List<Reservations>> GetRouteReservationsAsync(Reservations reservations)
        {
            try
            {
                return await reservationsTable.Where(reserv => reserv.ID_Route == reservations.ID_Route).ToListAsync();
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
