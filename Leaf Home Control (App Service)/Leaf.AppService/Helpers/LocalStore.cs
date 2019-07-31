using Leaf.Shared.Controllers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leaf.Shared.Helpers
{
    public class LocalStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> InitLocalStoreAsync()
        {
            bool sucess = false;
            Debug.WriteLine("LocalStore.InitLocalStoreAsync - Initialising Local Store on device");
            if (!MobileService.Client.SyncContext.IsInitialized)
            {
                try
                {
                    var store = new MobileServiceSQLiteStore("localstore.db");
                    store.DefineTable<HomeItem>();
                    Debug.WriteLine("LocalStore.InitLocalStoreAsync - HomeTable defined in local store");
                    store.DefineTable<RoomItem>();
                    Debug.WriteLine("LocalStore.InitLocalStoreAsync - RoomTable defined in local store");
                    store.DefineTable<DeviceItem>();
                    Debug.WriteLine("LocalStore.InitLocalStoreAsync - DeviceTable defined in local store");
                    await MobileService.Client.SyncContext.InitializeAsync(store);
                    Debug.WriteLine("LocalStore.InitLocalStoreAsync - MobileService.SyncContext initialised");
                    sucess = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("LocalStore.InitLocalStoreAsync Error - Message recieved: " + e.Message);
                }
            }
            else
            {
                Debug.WriteLine("LocalStore.InitLocalStoreAsync - MobileService.SyncContext is already initialised");
            }
            return sucess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SyncLocalStoreAsync()
        {
            Debug.WriteLine("LocalStore.SyncLocalStoreAsync - Pushing Local Store");
            try
            {
                await PushLocalStoreAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("LocalStore.SyncLocalStoreAsync - Message recieved: " + e.Message);
                return false;
            }
            try
            {
                Debug.WriteLine("LocalStore.SyncLocalStoreAsync -  Pulling Local Store");
                await PullLocalStoreAsync();
                Debug.WriteLine("LocalStore.SyncLocalStoreAsync -  Local Store synced sucessfully");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("LocalStore.SyncLocalStoreAsync - Message recieved: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> RefreshLocalStoreAsync()
        {
            Debug.WriteLine("LocalStore.RefreshLocalStoreAsync - Refreshing Local Store");
            if (await PurgeLocalStoreAsync())
            {
                if (await PullLocalStoreAsync())
                {
                    Debug.WriteLine("LocalStore.RefreshingLocalStoreAsync -  Local Store refreshed sucessfully");
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> PushLocalStoreAsync()
        {
            Debug.WriteLine("LocalStore.PushLocalStoreAsync - Pushing Local Store to server");
            try
            {
                await MobileService.Client.SyncContext.PushAsync();
                Debug.WriteLine("LocalStore.PushLocalStoreAsync - Local Store pushed to server successful");
                return true;
            }
            catch (MobileServicePushFailedException e)
            {
                var d = e.PushResult;
                Debug.WriteLine("LocalStore.PushLocalStoreAsync - Error message recieved: " + e.Message);
                return false;
            }
            catch (Exception t)
            {
                Debug.WriteLine("LocalStore.PushLocalStoreAsync - Error message recieved: " + t.Message);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> PullLocalStoreAsync()
        {
            Debug.WriteLine("LocalStore.PullLocalStoreAsync - Pulling Local Store from server");
            if (await HomeTable.PullTableAsync() && await RoomTable.PullTableAsync() && await DeviceTable.PullTableAsync())
            {
                Debug.WriteLine("LocalStore.PullLocalStoreAsync -  Local Store pulled sucessfully");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> PurgeLocalStoreAsync()
        {
            Debug.WriteLine("LocalStore.PurgeLocalStoreAsync - Purging Local Store from device");
            if (await HomeTable.PurgeTableAsync() && await RoomTable.PurgeTableAsync() && await DeviceTable.PurgeTableAsync())
            {
                Debug.WriteLine("LocalStore.PurgeLocalStoreAsync - Local Store Purged from device");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
