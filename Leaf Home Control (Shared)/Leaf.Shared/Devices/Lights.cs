using Leaf.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Devices
{
    public class Lights
    {
        public static async Task<Device> GetLightState(Device device)
        {
            var t = await PhilipsHue.APIs.Lights.GetLightsAttributesAndState(device.DeviceId);
            if (t.name != null)
            {
                device.Status = t.state.on;
                int level = (int)Math.Round((((float)t.state.bri / 255) * 100));
                device.Level = level.ToString() + "%";
            }
            return device;
        }

        public static async Task<Device> SetLightState(Device device)
        {
            //string preIP = "http://";
            //string IP = "192.168.1.4";
            //string postIP = "/api";
            //string username = "/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC";
            //string selection = "/lights/";
            //string lightsApi = "/state";
            //string url = preIP + IP + postIP + username + selection + device.DeviceId + lightsApi;

            string body = "";
            switch (device.Status)
            {
                case false:
                    device.Level = "Off";
                    body = "{\"on\": false}";
                    break;
                case true:
                    device.Level = "100%";
                    body = "{\"hue\": 8597,\"on\": true,\"bri\": 254}";
                    break;
                default:
                    body = "{\"on\": false}";
                    break;
            }

            bool success = await PhilipsHue.APIs.Lights.SetLightState(device.DeviceId, body);

            return device;
        }

        public static async Task<string> GetState(string device)
        {
            var t = PhilipsHue.APIs.Configuration.GetFullState(device);

            string body = "";



            return device;
        }
    }
}
