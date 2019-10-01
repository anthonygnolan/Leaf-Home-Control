using Leaf.Shared.Controllers;
using Leaf.Shared.Converters;
using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using Leaf.Windows.Controls;
using Leaf.Windows.Helpers;
using Leaf.Windows.Models;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing.Mobile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Family
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        private HomesViewModel _homesViewModel = App.HomesViewModel;
        private AccountsViewModel _accountsViewModel = new AccountsViewModel();
        private ObservableCollection<Home> Homes = new ObservableCollection<Home>();
        private ObservableCollection<Room> Rooms = new ObservableCollection<Room>();
        private ObservableCollection<Device> Devices = new ObservableCollection<Device>();

        private MobileServiceClient MobileService2 = new MobileServiceClient("https://leafhomecontrol.azurewebsites.net");
        private MobileServiceUser user;

        ObservableCollection<ImageMenuItem> items = new ObservableCollection<ImageMenuItem>();
        //Set the API Endpoint to Graph 'me' endpoint
        string _graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";//Set the scope for API call to user.read
        string[] _scopes = new string[] { "user.read" };

        public TestPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Uri)
            {
                MobileService2.ResumeWithURL(e.Parameter as Uri);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }
        private void GetStarted_button_Click(object sender, RoutedEventArgs e)
        {
            GetStarted_sp.Visibility = Visibility.Collapsed;
        }

        private void AddPerson_bt_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            AddNewUser_CD.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private async void AddGuest_bt_Click(object sender, RoutedEventArgs e)
        {
            string newUser = await ScanAsync();
            AccountsViewModel.AddUser(newUser, _homesViewModel.SelectedHome.HomeId);
        }

        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }

        const string MicrosoftAccountProviderId = "https://login.microsoft.com";
        const string ConsumerAuthority = "consumers";
        const string AccountScopeOne = "user.read";
        const string AccountScopeTwo = "wl.basic";
        const string AccountClientId = "none";
        const string StoredAccountKey = "accountid";

        private async void GetUsers()
        {
            await _accountsViewModel.GetUsers(_homesViewModel.SelectedHome.HomeId);
            foreach (Home Home in _accountsViewModel.Homes)
            {
                Homes.Add(Home);
            }
            //if (ApplicationData.Current.LocalSettings.Values.ContainsKey("accountid"))
            //{
            //    WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);
            //    String accountID = (String)ApplicationData.Current.LocalSettings.Values["accountid"];
            //    WebAccount account = await WebAuthenticationCoreManager.FindAccountAsync(provider, accountID);
            //    WebTokenRequest webTokenRequest = new WebTokenRequest(provider, "user.read", "none");
            //    WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest, account);
            //    if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            //    {
            //        string token = webTokenRequestResult.ResponseData[0].Token;
            //        GetUser(token);
            //    }
            //}
        }

        private async void GetUser(string token)
        {
            ImageMenuItem item = new ImageMenuItem
            {
                IsSelected = false,
                Image = new ImageBrush(),
                HasAccess = _homesViewModel.SelectedHome.HasAccess
            };

            if (token != null)
            {
                try
                {
                    item.AccountName = await MicrosoftGraph.GetAccountDetails(token, "givenName");
                    using (var imageStream = await MicrosoftGraph.GetAccountPicture(token))
                    {
                        using (var randomStream = imageStream.AsRandomAccessStream())
                        {
                            BitmapImage image = new BitmapImage();
                            await image.SetSourceAsync(randomStream);
                            item.Image.ImageSource = image;
                        }
                    }
                    items.Add(item);
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            };
        }

        private void AddNewUser_CD_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddUser();
        }

        private async void AddUser()
        {
            try
            {
                user = await MobileService2.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount, "leafhomecontrol.azurewebsites.net");
                AccountsViewModel.AddUser(user.UserId, _homesViewModel.SelectedHome.Id);
            }
            catch (InvalidOperationException ex)
            {

            }

        }

        private void AddNewUser_CD_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewUser_CD.Hide();
        }

        private void HasAccess_ts_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var t = e.OriginalSource;
        }

        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        private async void People_lv_ItemInvoked(object sender, ListViewItem e)
        {

            Devices.Clear();
            Rooms.Clear();
            var selectedItem = (Home)((NavMenuListView)sender).ItemFromContainer(e);
            await _accountsViewModel.GetRooms(selectedItem.Id);
            foreach (Room Room in _accountsViewModel.Rooms)
            {
                Rooms.Add(Room);
            }

            //foreach (ImageMenuItem _item in items)
            //{
            //    _item.IsSelected = false;
            //}

            //var item = (ImageMenuItem)((NavMenuListView)sender).ItemFromContainer(e);

            //if (item != null)
            //{
            //    item.IsSelected = true;
            //    Rooms.Clear();
            //    Devices.Clear();
            //    foreach (Room Room in App.RoomsViewModel.Rooms)
            //    {
            //        Rooms.Add(Room);
            //    }
            //}
        }

        private async void Rooms_lv_ItemInvoked(object sender, ListViewItem e)
        {
            Devices.Clear();
            var selectedItem = (Room)((NavMenuListView)sender).ItemFromContainer(e);
            await _accountsViewModel.GetDevices(selectedItem.Id);
            foreach (Device Device in _accountsViewModel.Devices)
            {
                Devices.Add(Device);
            }
        }

        private void Devices_lv_ItemInvoked(object sender, ListViewItem e)
        {

        }

        private async void HasAccess_ts_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch clickedItem = (ToggleSwitch)e.OriginalSource;
            try
            {
                var item = clickedItem.DataContext;
                if (item.GetType() == typeof(Home))
                {
                    await HomeTable.Update(HomeConverter.CreateFrom((Home)item));
                }
                else if (item.GetType() == typeof(Room))
                {
                    await RoomTable.Update(RoomConverter.CreateFrom((Room)item));
                }
                else if (item.GetType() == typeof(Device))
                {
                    await DeviceTable.Update(DeviceConverter.CreateFrom((Device)item));
                }
                await Shared.Services.Storage.SyncAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}
