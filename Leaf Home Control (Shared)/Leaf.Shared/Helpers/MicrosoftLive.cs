using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leaf.Shared.Helpers
{
    public class MicrosoftLive
    {
        const string AccountScopeRequested = "wl.basic";
        const string AccountClientId = "none";

        public static async Task<string> GetAccountDetails(string token, string details, string accountId = null)
        {
            Uri detailUri = new Uri(@"https://apis.live.net/v5.0/me?access_token=" + token);

            if (accountId != null)
            {
                detailUri = new Uri(@"https://apis.live.net/v5.0/" + accountId + "?access_token=" + token);
            }

            using (var client = new HttpClient())
            {
                var infoResult = await client.GetAsync(detailUri);
                string content = await infoResult.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(content);
                return jObject[details].ToString();
            }
        }

        public static async Task<Stream> GetAccountPictureAsStream(string token, string accountId = null)
        {
            Uri pictureUri = new Uri(@"https://apis.live.net/v5.0/me/picture?access_token=" + token);

            if (accountId != null)
            {
                pictureUri = new Uri(@"https://apis.live.net/v5.0/" + accountId + "/picture?access_token=" + token);
            }

            using (var client = new HttpClient())
            {
                var photoResult = await client.GetAsync(pictureUri);
                return await photoResult.Content.ReadAsStreamAsync();
            }
        }
    }
}
