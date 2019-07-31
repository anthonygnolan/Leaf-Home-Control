using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leaf.Shared.Controllers
{
    public class DeviceTable
    {
        public static DeviceItem deviceItem;
        public static MobileServiceCollection<DeviceItem, DeviceItem> deviceItems;
        public static IMobileServiceSyncTable<DeviceItem> deviceTable = MobileService.Client.GetSyncTable<DeviceItem>();

        public async static Task<bool> Create(DeviceItem device)
        {
            try
            {
                await deviceTable.InsertAsync(device);
                deviceItem = device;
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeviceTable.Create - Unable to add item to table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Read(IMobileServiceTableQuery<DeviceItem> query)
        {
            try
            {
                deviceItems = await query.ToCollectionAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeviceTable.Read - Unable to read items from table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Update(DeviceItem device)
        {
            try
            {
                await deviceTable.UpdateAsync(device);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeviceTable.Update - Unable to update item in table. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> Delete(DeviceItem deviceItem)
        {
            try
            {
                await deviceTable.DeleteAsync(deviceItem);
                await SyncTableAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeviceTable.Delete - Unable to delete table item. Message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> SyncTableAsync()
        {
            Debug.WriteLine("DeviceTable.SyncTableAsync - Syncing Table");
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
                Debug.WriteLine("DeviceTable.SyncTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> RefreshTableAsync()
        {
            Debug.WriteLine("DeviceTable.RefreshTableAsync - Refreshing Table");
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
                Debug.WriteLine("DeviceTable.RefreshTableAsync - User not set. Table not synced");
                return false;
            }
        }

        public static async Task<bool> PullTableAsync()
        {
            try
            {
                await deviceTable.PullAsync("deviceItems", deviceTable.CreateQuery());
                Debug.WriteLine("DeviceTable.SyncTableAsync - Table pulled sucessfully from server");
                return true;
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine("DeviceTable.SyncTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }

        public static async Task<bool> PurgeTableAsync()
        {
            try
            {
                await deviceTable.PurgeAsync(true);
                Debug.WriteLine("DeviceTable.PurgeTableAsync - Table Purged");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeviceTable.PurgeTableAsync - Error message recieved: " + e.Message);
                return false;
            }
        }
    }
}
