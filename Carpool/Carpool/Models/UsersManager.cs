using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Carpool
{
    class UsersManager
    {
        IMobileServiceTable<Users> usersTable;
        MobileServiceClient client;

        public UsersManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            usersTable = client.GetTable<Users>();
        }

        public async Task<Users> GetUserWhere(Expression<Func<Users, bool>> linq)
        {
            try
            {
                List<Users> newUser = await usersTable.Where(linq).Take(1).ToListAsync();
                return newUser.First();
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

        public async Task<Users> SaveGetUserAsync(Users user)
        {
            if (user.ID == null)
            {
                await usersTable.InsertAsync(user);
            }
            else
            {
                await usersTable.UpdateAsync(user);
            }

            try
            {
                List<Users> newUser = await usersTable.Where(userSelect => userSelect.Email == user.Email).ToListAsync();
                return newUser.First();
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
