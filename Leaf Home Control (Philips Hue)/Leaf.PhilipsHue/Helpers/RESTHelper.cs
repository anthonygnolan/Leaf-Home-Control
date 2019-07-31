using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Helpers
{
    public class RESTHelper
    {
        public static async Task<string> Get(string url)
        {
            var result = await (new HttpClient()).GetAsync(url)
                                      .ContinueWith(response => response.Result.Content.ReadAsStringAsync());
            string responseFromServer = result.Result;

            return responseFromServer;
        }

        public static async Task<string> Put(string url, string body)
        {
            var result = await (new HttpClient()).PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).ContinueWith(response => response.Result.Content.ReadAsStringAsync());
            string responseFromServer = result.Result;

            return responseFromServer;
        }

        public static async Task<string> Post(string url, string body)
        {
            var result = await (new HttpClient()).PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).ContinueWith(response => response.Result.Content.ReadAsStringAsync());
            string responseFromServer = result.Result;

            return responseFromServer;
        }

        public static async Task<string> Delete(string url)
        {
            var result = await (new HttpClient()).DeleteAsync(url).ContinueWith(response => response.Result.Content.ReadAsStringAsync());
            string responseFromServer = result.Result;

            return responseFromServer;
        }
    }
}
