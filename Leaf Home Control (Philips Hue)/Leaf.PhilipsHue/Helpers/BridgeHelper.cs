using Newtonsoft.Json.Linq;
using PhilipsHue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Helpers
{
    public class BridgeHelper
    {
        public static async Task<Config> FindBridgeAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(new Uri("https://www.meethue.com/api/nupnp"));
                    if (response == "[]")
                    {
                        return null;
                    }
                    string ip = JArray.Parse(response).First["internalipaddress"].ToString();

                    Config config = await APIs.Configuration.GetConfiguration("http://" + ip + "/api/config");
                    config.ipaddress = ip;
                    config.name = config.name + " - " + config.bridgeid.Substring(config.bridgeid.Length - 6);

                    ///Use the following to get the Hue logo from the bridge
                    //Stream stream = await client.GetStreamAsync(new Uri("http://" + ip + "/hue_logo_0.png"));
                    //BitmapImage bridgeLogo = new BitmapImage(new Uri("http://" + ip + "/hue_logo_0.png", UriKind.Absolute));

                    return config;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
