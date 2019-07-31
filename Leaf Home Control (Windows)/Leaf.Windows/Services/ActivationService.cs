using Leaf.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;

namespace Leaf.Windows.Services
{
    public static class ActivationService
    {
        const string MicrosoftAccountProviderId = "https://login.microsoft.com";
        const string ConsumerAuthority = "consumers";

        public static async void Initalise()
        {
            await Shared.Services.Storage.InitaliseAsync();
            await MicrosoftAccount.SignIntoAppService();

            await App.HomesViewModel.Initalise();
            try
            {
                App.RoomsViewModel.SelectedHomeId = App.HomesViewModel.SelectedHome.Id;
                await App.RoomsViewModel.Initialize();
            }
            catch(Exception e)
            {
                Debug.WriteLine("ActivationService.Initalise - Error loading rooms: " + e.Message);
            }
            
            
        }
    }
    
}
