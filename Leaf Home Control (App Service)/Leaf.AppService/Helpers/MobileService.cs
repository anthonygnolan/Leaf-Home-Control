using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Helpers
{
    public class MobileService
    {
        public static MobileServiceClient Client = new MobileServiceClient("https://leafhomecontrol.azurewebsites.net");

        public static async Task<bool> SetMobileServiceUserAsync(string token)
        {
            bool success = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://leafhomecontrol.azurewebsites.net");

                var jsonToPost = new JObject(new JProperty("access_token", token));

                var contentToPost = new StringContent(JsonConvert.SerializeObject(jsonToPost), Encoding.UTF8, "application/json");
                var asyncResult = await client.PostAsync("/.auth/login/microsoftaccount", contentToPost);

                if (asyncResult.Content == null)
                {
                    throw new InvalidOperationException("Result from call was null.");
                }
                else
                {
                    var resultContentAsString = await asyncResult.Content.ReadAsStringAsync();
                    var jOUser = JObject.Parse(resultContentAsString);
                    var jOuser = JObject.Parse(jOUser["user"].ToString());

#pragma warning disable IDE0017 // Simplify object initialization
                    MobileServiceUser User = new MobileServiceUser(jOuser["userId"].ToString());
#pragma warning restore IDE0017 // Simplify object initialization
                    User.MobileServiceAuthenticationToken = jOUser["authenticationToken"].ToString();
                    Client.CurrentUser = User;
                    success = true;
                }
            }
            return success;
        }

        public static async Task<bool> SetMicrosoftUserAsync(string token)
        {
            bool success = false;

            var user = await Client.LoginWithMicrosoftAccountAsync(token);
            return success;
        }

        ///////////////////////////////////////////////////////////////////
        ///
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
                await SetMobileServiceUserAsync(token);
                success = true;
                Debug.WriteLine("AppService.SignIn - User is signed in");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message.ToString());
            }

            return success;
        }

        public static async Task<bool> SetMobileServiceUserAsync5(string token)
        {
            bool success = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://leafhomecontrol.azurewebsites.net");

                var jsonToPost = new JObject(new JProperty("access_token", token));

                var contentToPost = new StringContent(JsonConvert.SerializeObject(jsonToPost), Encoding.UTF8, "application/json");
                //var asyncResult = await client.PostAsync("/.auth/login/microsoftaccount", contentToPost);

                //if (asyncResult.Content == null)
                //{
                //    throw new InvalidOperationException("Result from call was null.");
                //}
                //else
                //{
                //    var resultContentAsString = await asyncResult.Content.ReadAsStringAsync();
                //    //var jOUser = JObject.Parse(resultContentAsString);
                //    //var jOuser = JObject.Parse(jOUser["user"].ToString());
                //    //sid: 79cb2d8a9896fd48bac1f3969a9965cc
                //    //sid: 79cb2d8a9896fd48bac1f3969a9965cc
                //    //await Shared.Authentication.MobileServiceUser.SetUserAsync(token);
                //    //HomeTableController.Create(new MobileBackend.DataObjects.HomeItem());
                //    var tokent = new JObject
                //    {
                //        // Replace access_token_value with actual value of your access token obtained
                //        // using the Facebook or Google SDK.
                //        { "authenticationToken", token }
                //    };

                //    success = true;
                //}
                try
                {
                    var tokent = new JObject
                    {
                        // Replace access_token_value with actual value of your access token obtained
                        // using the Facebook or Google SDK.
                        { "authenticationToken", token }
                    };
                    MobileServiceUser user = await Client.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount, tokent);
                }
                catch (Exception e)
                {
                    var t = e.InnerException;
                }
            }


            //            bool success = false;
            //            using (var client = new HttpClient())
            //            {
            //                client.BaseAddress = new Uri("https://leafhomecontrol.azurewebsites.net");

            //                //var jsonToPost = new JObject(new JProperty("authenticationToken", token));
            //                var jsonToPost = new JObject(new JProperty("access_token", token));

            //                var contentToPost = new StringContent(JsonConvert.SerializeObject(jsonToPost), Encoding.UTF8, "application/json");
            //                var asyncResult = await client.PostAsync("/.auth/login/microsoftaccount", contentToPost);

            //                if (asyncResult.Content == null)
            //                {
            //                    throw new InvalidOperationException("Result from call was null.");
            //                }
            //                else
            //                {
            //                    var resultContentAsString = await asyncResult.Content.ReadAsStringAsync();
            //                    var jOUser = JObject.Parse(resultContentAsString);
            //                    var jOuser = JObject.Parse(jOUser["user"].ToString());

            //#pragma warning disable IDE0017 // Simplify object initialization
            //                    MobileServiceUser User = new MobileServiceUser(jOuser["userId"].ToString());
            //#pragma warning restore IDE0017 // Simplify object initialization
            //                    User.MobileServiceAuthenticationToken = jOUser["authenticationToken"].ToString();
            //                    Client.CurrentUser = User;
            //                    success = true;
            //                }
            //            }
            return success;
        }

        public static async Task<bool> SetMicrosoftUserAsync8(string token)
        {
            //bool success = false;

            //var user = await Client.LoginWithMicrosoftAccountAsync(token);
            //return success;
            string message;
            bool success = false;
            try
            {
                // Change 'MobileService' to the name of your MobileServiceClient instance.
                // Sign-in using Facebook authentication.
                //MobileServiceUser user = await Client.LoginAsync(MobileServiceAuthenticationProvider.Facebook, "{url_scheme_of_your_app}");
                //message =
                //    string.Format("You are now signed in - {0}", user.UserId);

                //success = true;
            }
            catch (InvalidOperationException)
            {
                message = "You must log in. Login Required";
            }


            return success;
        }

        public static async Task<bool> SetUserAsync(string token)
        {
            bool success = false;
            JObject Token = new JObject(new JProperty("authenticationToken", token));
            var user = await Client.LoginAsync("MicrosoftAccount", Token);
            return success;

            //JsonObject jo = new JsonObject();
            //jo.addProperty("access_token", token);
        }
    }
}
