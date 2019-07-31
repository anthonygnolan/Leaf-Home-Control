using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Services
{
    public class Storage
    {
        public static async Task<bool> InitaliseAsync()
        {
            return await Helpers.LocalStore.InitLocalStoreAsync();
        }

        public static async Task<bool> SyncAsync()
        {
            return await Helpers.LocalStore.SyncLocalStoreAsync();
        }

        public static async Task<bool> RefreshAsync()
        {
            return await Helpers.LocalStore.RefreshLocalStoreAsync();
        }


        public static async Task<bool> PurgeAsync()
        {
            return await Helpers.LocalStore.PurgeLocalStoreAsync();
        }
    }
}
