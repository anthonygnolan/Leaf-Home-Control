using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leaf.Shared.Helpers
{
    public class MicrosoftGraph
    {
        static readonly string DetailsAPIEndpoint = "https://graph.microsoft.com/v1.0/me";//Set the scope for API call to user.read 
        static readonly string PhotoAPIEndpoint = "https://graph.microsoft.com/beta/me/photos('48x48')/$value";//Set the scope for API call to user.read


        /// <summary>
        /// Perform an HTTP GET request to a URL using an HTTP Authorization header
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="token">The token</param>
        /// <returns>String containing the results of the GET operation</returns>
        public static async Task<string> GetAccountDetails(string token, string details)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, DetailsAPIEndpoint);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(content);
                return jObject[details].ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Perform an HTTP GET request to a URL using an HTTP Authorization header
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="token">The token</param>
        /// <returns>String containing the results of the GET operation</returns>
        public static async Task<Stream> GetAccountPicture(string token)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, PhotoAPIEndpoint);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);
                return await response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
