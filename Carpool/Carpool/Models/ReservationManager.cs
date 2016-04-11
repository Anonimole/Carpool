using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carpool
{
    class ReservationManager
    {
        IMobileServiceTable<Reservation> reservationsTable;
        MobileServiceClient client;

        public ReservationManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            reservationsTable = client.GetTable<Reservation>();
        }

        public async Task SaveReservationAsync(Reservation reservation)
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

        public async Task<List<Reservation>> GetReservationsWhere(Expression<Func<Reservation, bool>> linq)
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

        public async Task DeleteReservationsAsync(Reservation reservation)
        {
            try
            {
                List<Reservation> reservationsList = await GetReservationsWhere(res => res.ID_Route == reservation.ID_Route);
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

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            try
            {
                List<Reservation> reservationsList = await GetReservationsWhere(res => res.ID_Route == reservation.ID_Route&& res.ID_User==reservation.ID_User);
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
