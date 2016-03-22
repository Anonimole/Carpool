using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

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


        public async Task<ObservableCollection<Users>> GetUsersAsync(string email)
        {
            try
            {
                return new ObservableCollection<Users>
                (
                    await usersTable.Where (user => user.Email==email).ToListAsync ()
                );
            } catch (MobileServiceInvalidOperationException msioe) {
                Debug.WriteLine (@"INVALID {0}", msioe.Message);
			} catch (Exception e) {
				Debug.WriteLine (@"ERROR {0}", e.Message);
			}
            return null;
        }


        //public async Task<Collection<Users>> SearchUserAsync(string email)
        //{
        //    try
        //    {
        //        return new ObservableCollection<Users>
        //        (
        //            await usersTable.Where(user => user.Email == email).ToListAsync()
        //        );
        //    }
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return null;
        //}

        public async Task<Users> SearchUserAsync(Users user)
        {
            try
            {
                List<Users> newUser = await usersTable.Where(userSelect => userSelect.Email == user.Email).Take(1).ToListAsync();
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


        //public async Task SaveUserAsync(Users user)
        //{
        //    if (user.ID == null)
        //    {
        //        await usersTable.InsertAsync(user);
        //    }
        //    else
        //    {
        //        await usersTable.UpdateAsync(user);
        //    }
        //}

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
