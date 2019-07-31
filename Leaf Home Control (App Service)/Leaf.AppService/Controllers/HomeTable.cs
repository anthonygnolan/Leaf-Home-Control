using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leaf.Shared.Controllers
{
    public class HomeTable
    {
        public static HomeItem HomeItem;
        public static MobileServiceCollection<HomeItem, HomeItem> HomeItems;
        public static IMobileServiceSyncTable<HomeItem> HomeSyncTable = MobileService.Client.GetSyncTable<HomeItem>();

        public async static Task<bool> Create(HomeItem homeItem)
        {
            try
            {
                await HomeSyncTable.InsertAsync(homeItem);
                HomeItem = homeItem;
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("HomeTableController.Create - Unable to add item to table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Read(IMobileServiceTableQuery<HomeItem> query)
        {
            try
            {
                HomeItems = await query.ToCollectionAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("HomeTableController.Read - Unable to read items from table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Update(HomeItem homeItem)
        {
            try
            {
                await HomeSyncTable.UpdateAsync(homeItem);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("HomeTableController.Update - Unable to update item in table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Delete(HomeItem homeItem)
        {
            try
            {
                await HomeSyncTable.DeleteAsync(homeItem);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("HomeTableController.Delete - Unable to delete table item. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> SyncTableAsync()
        {
            Debug.WriteLine("HomeTableController.SyncTableAsync - Syncing Table");
            if (MobileService.Client.CurrentUser != null)
            {
                if (await LocalStore.PushLocalStoreAsync())
                {
                    //vreturn await PullTableAsync();
                    return true;
                }
                return false;
            }
            else
            {
                Debug.WriteLine("HomeTableController.SyncTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> RefreshTableAsync()
        {
            Debug.WriteLine("HomeTableController.RefreshTableAsync - Refreshing Table");
            if (MobileService.Client.CurrentUser != null)
            {
                if (await PurgeTableAsync())
                {
                    return await PullTableAsync();
                }
                return false;
            }
            else
            {
                Debug.WriteLine("HomeTableController.RefreshTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> PullTableAsync()
        {
            try
            {
                await HomeSyncTable.PullAsync("homeItems", HomeSyncTable.CreateQuery());
                Debug.WriteLine("HomeTableController.SyncTableAsync - Table pulled sucessfully from server");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("HomeTableController.SyncTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> PurgeTableAsync()
        {
            try
            {
                await HomeSyncTable.PurgeAsync(true);
                Debug.WriteLine("HomeTableController.PurgeTableAsync - Table Purged");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("AccountTableController.PurgeTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }
    }
}
