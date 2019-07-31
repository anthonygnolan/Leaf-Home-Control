using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leaf.Shared.Controllers
{
    public class RoomTable
    {
        public static RoomItem RoomItem;
        public static MobileServiceCollection<RoomItem, RoomItem> RoomItems;
        public static IMobileServiceSyncTable<RoomItem> RoomSyncTable = MobileService.Client.GetSyncTable<RoomItem>();

        public async static Task<bool> Create(RoomItem roomItem)
        {
            try
            {
                await RoomSyncTable.InsertAsync(roomItem);
                RoomItem = roomItem;
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoomTableController.Create - Unable to add item to table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Read(IMobileServiceTableQuery<RoomItem> query)
        {
            try
            {
                RoomItems = await query.ToCollectionAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoomTableController.Read - Unable to read items from table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Update(RoomItem roomItem)
        {
            try
            {
                await RoomSyncTable.UpdateAsync(roomItem);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoomTableController.Update - Unable to update item in table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Delete(RoomItem roomItem)
        {
            try
            {
                await RoomSyncTable.DeleteAsync(roomItem);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoomTableController.Delete - Unable to delete table item. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> SyncTableAsync()
        {
            Debug.WriteLine("RoomTableController.SyncTableAsync - Syncing Table");
            if (MobileService.Client.CurrentUser != null)
            {
                if (await LocalStore.PushLocalStoreAsync())
                {
                    return await PullTableAsync();
                }
                return false;
            }
            else
            {
                Debug.WriteLine("RoomTableController.SyncTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> RefreshTableAsync()
        {
            Debug.WriteLine("RoomTableController.RefreshTableAsync - Refreshing Table");
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
                Debug.WriteLine("RoomTableController.RefreshTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> PullTableAsync()
        {
            try
            {
                await RoomSyncTable.PullAsync("roomItems", RoomSyncTable.CreateQuery());
                Debug.WriteLine("RoomTableController.SyncTableAsync - Table pulled sucessfully from server");
                return true;
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine("RoomTableController.SyncTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> PurgeTableAsync()
        {
            try
            {
                await RoomSyncTable.PurgeAsync(true);
                Debug.WriteLine("RoomTableController.PurgeTableAsync - Table Purged");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RoomTableController.PurgeTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }
    }
}
