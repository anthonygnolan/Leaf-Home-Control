using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leaf.Shared.Helpers
{
    public class AzureAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns></returns>
        public static async Task<bool> SignIn(string token)
        {
            bool success = false;

            try
            {
                await MobileService.SetMobileServiceUserAsync(token);
                success = true;
                Debug.WriteLine("AppService.SignIn - User is signed in");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message.ToString());
            }

            return success;
        }
    }
}
