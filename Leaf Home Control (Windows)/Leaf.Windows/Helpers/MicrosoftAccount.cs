using Leaf.Shared.Helpers;
using Leaf.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Leaf.Windows.Helpers
{
    public class MicrosoftAccount
    {
        const string StoredAccountKey = "accountid";
        const string StoredNameKey = "nameid";
        const string StoredEmailKey = "emailid";

        const string MicrosoftAccountProviderId = "https://login.microsoft.com";
        const string ConsumerAuthority = "consumers";
        const string AccountClientId = "none";
        const string GraphScope = "user.read";
        const string LiveScope = "wl.basic";
        const string DualScopes = "wl.basic,user.read";

        const string FirstName = "givenName";
        const string FullName = "displayName"; 
        const string Email = "userPrincipalName";

        static WebTokenRequest request = null;
        static WebAccountProvider provider = null;
        static WebAccount account = null;
        static WebTokenRequestResult result = null;

        static string token = null;

        /// <summary>
        /// Add items to the AccountsSettingsPane
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<ImageMenuItem> SignIn(WebAccountProvider provider)
        {
            //The initial request includes both the Live SDK scope and the Microsoft Graph Scope
            request = new WebTokenRequest(provider, DualScopes, AccountClientId);
            token = await GetTokenSilently();

            if (token == null)
            {
                token = await GetToken(); 
                return null;
            }
            request = new WebTokenRequest(provider, GraphScope, AccountClientId);
            token = await GetTokenSilently();
            ImageMenuItem userDetails = await GetUser(token, FirstName);
            return userDetails;
        }

        /// <summary>
        /// Add items to the AccountsSettingsPane
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<ImageMenuItem> AutoSignIn()
        {
            IReadOnlyList<User> users = await User.FindAllAsync();
            if (users.Count != 0)
            {
                User user = User.GetFromId(users.ElementAt(0).NonRoamableId);
                provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority, user);
                request = new WebTokenRequest(provider, GraphScope, AccountClientId);
                token =  await GetTokenSilently();

                if (token != null)
                {
                    ImageMenuItem userDetails = await GetUser(token, FirstName);
                    return userDetails;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<ImageMenuItem> GetUser(string token, string details)
        {
            ImageMenuItem userDetails = new ImageMenuItem();
            userDetails.AccountName = await MicrosoftGraph.GetAccountDetails(token, details);
            using (var imageStream = await MicrosoftGraph.GetAccountPicture(token))
            {
                using (var randomStream = imageStream.AsRandomAccessStream())
                {
                    BitmapImage image = new BitmapImage();
                    try
                    {
                        await image.SetSourceAsync(randomStream);
                    }
                    catch(Exception)
                    {
                        image = new BitmapImage(new Uri("ms-appx:///Leaf.Shared/Images/ApplicationLogo.png"));
                    }
                    try
                    {
                        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomStream);
                        SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                        StorageFile file_Save = await ApplicationData.Current.LocalFolder.CreateFileAsync("ProfilePicture.jpg", CreationCollisionOption.ReplaceExisting);
                        BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, await file_Save.OpenAsync(FileAccessMode.ReadWrite));
                        encoder.SetSoftwareBitmap(softwareBitmap);
                        await encoder.FlushAsync();
                    }
                    catch (Exception)
                    {

                    }
                    ImageBrush imageBrush = new ImageBrush
                    {
                        ImageSource = image
                    };
                    userDetails.Image = imageBrush;
                }
            }
            return userDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<WebAccountProvider> GetProvider()
        {
            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);
            return provider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<string> GetToken()
        {
            token = null;

            result = await WebAuthenticationCoreManager.RequestTokenAsync(request);
            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);
                ApplicationData.Current.LocalSettings.Values[StoredAccountKey] = result.ResponseData[0].WebAccount.Id;
                token = result.ResponseData[0].Token;
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<string> GetTokenSilently()
        {
            token = null;

            result = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(request);
            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);
                ApplicationData.Current.LocalSettings.Values[StoredAccountKey] = result.ResponseData[0].WebAccount.Id;
                token = result.ResponseData[0].Token;
            }
            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<bool> SignIntoAppService()
        {
            provider = await GetProvider();
            String accountID = (String)ApplicationData.Current.LocalSettings.Values[StoredAccountKey];
            WebAccount account = await WebAuthenticationCoreManager.FindAccountAsync(provider, accountID);
            WebTokenRequest webTokenRequest = new WebTokenRequest(provider, LiveScope, AccountClientId);
            WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest, account);

            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                if (await AzureAppService.SignIn(webTokenRequestResult.ResponseData[0].Token))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static async Task<string> GetAppServiceToken()
        {
            result = await WebAuthenticationCoreManager.RequestTokenAsync(request);
            return "";
        }

        public static async Task<ImageMenuItem> SignIn()
        {
            provider = await GetProvider();
            String accountID = (String)ApplicationData.Current.LocalSettings.Values[StoredAccountKey];
            WebAccount account = await WebAuthenticationCoreManager.FindAccountAsync(provider, accountID);
            WebTokenRequest webTokenRequest = new WebTokenRequest(provider, GraphScope, AccountClientId);
            WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest, account);

            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);
                ApplicationData.Current.LocalSettings.Values[StoredAccountKey] = webTokenRequestResult.ResponseData[0].WebAccount.Id;
                ImageMenuItem userDetails = await GetUser(webTokenRequestResult.ResponseData[0].Token, FullName);
                ApplicationData.Current.LocalSettings.Values[StoredNameKey] = userDetails.AccountName;
                ImageMenuItem userDetail = await GetUser(webTokenRequestResult.ResponseData[0].Token, Email);
                ApplicationData.Current.LocalSettings.Values[StoredEmailKey] = userDetail.AccountName;
                userDetails.Arguments = userDetail.AccountName;
                return userDetails;
            }
            return null;
        }
    }
}
