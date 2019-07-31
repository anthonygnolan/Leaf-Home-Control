using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhilipsHue.Helpers;
using PhilipsHue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Configuration
    {
        /// <summary>
        /// Documentation
        /// https://www.developers.meethue.com/documentation/configuration-api
        /// </summary>
        

        /// <summary>
        /// 7.1 Create user
        /// URL: http://<bridgeipaddress>/api
        /// Method: POST
        /// Version: 1.0
        /// Permission: All
        /// </summary>
        /// <param name="url"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public static async Task<string> CreateUser(string url, string deviceName)
        {

            ///{ "devicetype": "Leaf_Home_Control#iphone peter"}
            var body = $"{{\"devicetype\": \"Leaf_Home_Control#{deviceName}\"}}";

            string response = await RESTHelper.Post(url, body);

            if (response.Contains("success"))
            {
                string user = response;
            }
            else if (response.Contains("101"))
            {
                response = "link button not pressed";
            }
            else
            {
                response = "failure";
            }

            return response;
        }

        /// <summary>
        /// 7.2 Get configuration
        /// URL: http://<bridgeipaddress>/api/<username>/config
        /// Method: GET
        /// Version: 1.0
        /// Permission: Whitelist
        /// </summary>
        /// <param name="url"></param>
        public static async Task<Config> GetConfiguration(string url)
        {
            url = url + "/config";
            string response = await RESTHelper.Get(url);
            Config config = JsonConvert.DeserializeObject<Config>(response);
            return config;
        }

        public void ModifyConfiguration()
        {
            /// URL: /api/<username>/config
            /// Method: PUT
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void DeleteUserFromWhitelist()
        {
            /// URL: /api/<username>/config/whitelist/<username2>
            /// Method: DELETE
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public static async Task<Bridge> GetFullState(string url)
        {
            /// URL: /api/<username>
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist

            
            string response = await RESTHelper.Get(url);

            Bridge HueBridge = new Bridge();
            List<Light> lights = new List<Light>();
            HueBridge.lights = lights;
            List<Group> groups = new List<Group>();
            HueBridge.groups = groups;
            List<Schedule> schedules = new List<Schedule>();
            HueBridge.schedules = schedules;
            List<Scene> scenes = new List<Scene>();
            HueBridge.scenes = scenes;
            List<Sensor> sensors = new List<Sensor>();
            HueBridge.sensors = sensors;
            List<Rule> rules = new List<Rule>();
            HueBridge.rules = rules;

            JToken token = JToken.Parse(response);
            var jsonResult = (JObject)token;

            foreach (var prop in jsonResult.Properties())
            {
                var result = (JObject)prop.Value;
                switch (prop.Path)
                {

                    case "lights":
                        {
                            foreach (var p in result.Properties())
                            {
                                Light newLight = JsonConvert.DeserializeObject<Light>(p.Value.ToString());
                                HueBridge.lights.Add(newLight);
                            }
                            break;
                        }

                    case "groups":
                        {
                            foreach (var p in result.Properties())
                            {
                                Group newGroup = JsonConvert.DeserializeObject<Group>(p.Value.ToString());
                                HueBridge.groups.Add(newGroup);
                            }
                            break;
                        }

                    case "config":
                        {
                            Config config = JsonConvert.DeserializeObject<Config>(prop.Value.ToString());
                            break;
                        }

                    case "schedules":
                        {
                            foreach (var p in result.Properties())
                            {
                                Schedule newSchedule = JsonConvert.DeserializeObject<Schedule>(p.Value.ToString());
                                HueBridge.schedules.Add(newSchedule);
                            }
                            break;
                        }

                    case "scenes":
                        {
                            foreach (var p in result.Properties())
                            {
                                Scene newScene = JsonConvert.DeserializeObject<Scene>(p.Value.ToString());
                                HueBridge.scenes.Add(newScene);
                            }
                            break;
                        }

                    case "sensors":
                        {
                            foreach (var p in result.Properties())
                            {
                                Sensor newSensor = JsonConvert.DeserializeObject<Sensor>(p.Value.ToString());
                                HueBridge.sensors.Add(newSensor); ;
                            }
                            break;
                        }

                    case "rules":
                        {
                            foreach (var p in result.Properties())
                            {
                                Rule newRule = JsonConvert.DeserializeObject<Rule>(p.Value.ToString());
                                HueBridge.rules.Add(newRule);
                            }
                            break;
                        }


                    // As a last resort, launch the app in the foreground.
                    default:
                        break;
                }

            }
            return HueBridge;
        }
    }
}
