using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
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

        public async Task<List<Reservations>> GetReservationsWhere(Expression<Func<Reservations, bool>> linq)
        {
            try
            {
                return await reservationsTable.Where(linq).ToListAsync();
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

        public async Task DeleteReservationsAsync(Reservations reservation)
        {
            try
            {
                List<Reservations> reservationsList = await GetReservationsWhere(res => res.ID_Route == reservation.ID_Route);
                foreach (var res in reservationsList)
                {
                    await reservationsTable.DeleteAsync(res);
                }
                    
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }

        }

        public async Task DeleteReservationAsync(Reservations reservation)
        {
            try
            {
                List<Reservations> reservationsList = await GetReservationsWhere(res => res.ID_Route == reservation.ID_Route&& res.ID_User==reservation.ID_User);
                foreach (var res in reservationsList)
                {
                    await reservationsTable.DeleteAsync(res);
                }

            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }

        }

    }
}
