using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Leaf.Shared.Services;
using Leaf.Shared.ViewModels;
using Leaf.Windows.Controls;
using Leaf.Windows.Helpers;
using Leaf.Windows.Models;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Graphics.Imaging;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        private HomesViewModel _homesViewModel { get { return this.DataContext as HomesViewModel; } }
        public Frame AppFrame { get { return this.shellFrame; } }
        public object RequestMSAToken { get; private set; }
        public static ImageMenuItem user;

        public AppShell()
        {
            InitializeComponent();
            DataContext = App.HomesViewModel;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.

            if (this.AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (this.AppFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                var stack = AppFrame.BackStack;
                this.shellFrame.GoBack();
            }
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (HamburgerMenuSplitView.IsPaneOpen)
            {
                HamburgerMenuSplitView.IsPaneOpen = false;
            }
            else
            {
                HamburgerMenuSplitView.IsPaneOpen = true;
            }
        }

        private void NavMenu_ListView_ItemInvoked(object sender, ListViewItem e)
        {
            ClearNavMenu();

            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(e);

            if (item != null)
            {
                item.IsSelected = true;
                //item.Arguments = Home.Id;

                if (item.DestPage != null && item.DestPage != AppFrame.CurrentSourcePageType)
                {
                    shellFrame.Navigate(item.DestPage, item.Arguments);
                    if (item.DestPage == typeof(Views.Overview.MainPage))
                    {
                        AppFrame.BackStack.Clear();
                    }
                }
            }
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

        private void HomeListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Homes_ListView.SelectedIndex = -1;
            SelectHome_ContentDialogue.IsPrimaryButtonEnabled = false;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            SelectHome_ContentDialogue.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void Homes_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectHome_ContentDialogue.IsPrimaryButtonEnabled = true;
        }

        private void SelectHome_ContentDialogue_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SelectHome_ContentDialogue.Hide();
            ClearNavMenu();
            if (HamburgerMenuSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                HamburgerMenuSplitView.IsPaneOpen = false;
            }
            App.HomesViewModel.SelectedHome = App.HomesViewModel.Homes[Homes_ListView.SelectedIndex];
            MyHome_ListView.SelectedIndex = 0;
            NavMenuListView.navlistone[0].IsSelected = true;
            SelectHome_ContentDialogue.IsPrimaryButtonEnabled = false;

            shellFrame.Navigate(typeof(Overview.MainPage));
        }

        private void SelectHome_ContentDialogue_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void AddHome_bt_Click(object sender, RoutedEventArgs e)
        {
            SelectHome_ContentDialogue.Hide();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            AddNewHome_CD.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NewHomeName.Text = "";
        }

        private void AddNewHome_CD_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewHome_CD.Hide();
            if (HamburgerMenuSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                HamburgerMenuSplitView.IsPaneOpen = false;
            }
            ClearNavMenu();
            MyHome_ListView.SelectedIndex = 0;

            //Homes_ListView.SelectedIndex = -1;
            NavMenuListView.navlistone[0].IsSelected = true;
            shellFrame.Navigate(typeof(Overview.MainPage));
        }

        private void AddNewHome_CD_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SelectHome_ContentDialogue.IsPrimaryButtonEnabled = false;
            AddNewHome_CD.Hide();
        }

        private void NewHomeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewHomeName.Text != "")
            {
                AddNewHome_CD.IsPrimaryButtonEnabled = true;
            }
            else
            {
                AddNewHome_CD.IsPrimaryButtonEnabled = false;
            }
        }

        private void ClearNavMenu()
        {
            MyHome_ListView.SelectedItem = null;
            foreach (var i in NavMenuListView.navlistone)
            {
                i.IsSelected = false;
            }

            MyDevices_ListView.SelectedItem = null;
            foreach (var i in NavMenuListView.navlisttwo)
            {
                i.IsSelected = false;
            }

            RoutinesAndScenes_ListView.SelectedItem = null;
            foreach (var i in NavMenuListView.navlistthree)
            {
                i.IsSelected = false;
            }

            Settings_ListView.SelectedItem = null;
            foreach (var i in NavMenuListView.navlistfour)
            {
                i.IsSelected = false;
            }
        }

        #region Accounts Settings Pane

        const string MicrosoftAccountProviderId = "https://login.microsoft.com";
        const string ConsumerAuthority = "consumers";
        const string AccountScopeOne = "user.read";
        const string AccountScopeTwo = "wl.basic";
        const string AccountClientId = "none";
        const string StoredAccountKey = "accountid";
        const string StoredEmailKey = "emailid";
        const string StoredNameKey = "nameid";
        static string TokenOne = "";
        static string TokenTwo = "";

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var device = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (device == "Windows.Desktop" || device == "Windows.Tablet")
            {
                AppTitleBar.Visibility = Visibility.Visible;
                // Hide default title bar.
                var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;
                var applicationView = ApplicationView.GetForCurrentView();
                var titleBar = applicationView.TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
            }
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += OnAccountCommandsRequested;

            user = await MicrosoftAccount.SignIn();
            AccountName.Text = user.AccountName;
            AccountImage.Fill = user.Image;
            SignInListViewItem.Visibility = Visibility.Collapsed;
            AccountListViewItem.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += OnAccountCommandsRequested;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= OnAccountCommandsRequested;
        }

        private void AccountListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AccountsSettingsPane.Show();
        }

        // This event handler is called when the Account settings pane is to be launched.
        private async void OnAccountCommandsRequested(AccountsSettingsPane sender, AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            // In order to make async calls within this callback, the deferral object is needed
            AccountsSettingsPaneEventDeferral deferral = e.GetDeferral();

            // This scenario only lets the user have one account at a time.
            // If there already is an account, we do not include a provider in the list
            // This will prevent the add account button from showing up.
            bool isPresent = ApplicationData.Current.LocalSettings.Values.ContainsKey(StoredAccountKey);

            if (isPresent)
            {
                await AddWebAccount(e);
            }
            else
            {
                await AddWebAccountProvider(e);
                AddLinksAndDescription(e);
            }

            deferral.Complete();
        }

        private async Task AddWebAccountProvider(AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);

            WebAccountProviderCommand providerCommand = new WebAccountProviderCommand(provider, WebAccountProviderCommandInvoked);
            e.WebAccountProviderCommands.Add(providerCommand);
        }

        private async Task AddWebAccount(AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);
            String accountID = (String)ApplicationData.Current.LocalSettings.Values[StoredAccountKey];
            WebAccount account = await WebAuthenticationCoreManager.FindAccountAsync(provider, accountID);
            WebAccountProvider web = new WebAccountProvider("818", "MicrosoftAccount", new Uri("ms-appdata:///local/ProfilePicture.jpg"));
            WebAccount account2 = new WebAccount(web, (string)ApplicationData.Current.LocalSettings.Values[StoredEmailKey], WebAccountState.Connected);
            if (account == null)
            {
                ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);
            }
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(StoredEmailKey))
            {
                WebAccountCommand command = new WebAccountCommand(account2, WebAccountInvoked, SupportedWebAccountActions.Remove);
                e.WebAccountCommands.Add(command);
            }
            else
            {
                WebAccountCommand command = new WebAccountCommand(account, WebAccountInvoked, SupportedWebAccountActions.Remove);
                e.WebAccountCommands.Add(command);
            }
        }

        private void AddLinksAndDescription(AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            e.HeaderText = "Describe what adding an account to your application will do for the user";

            // You can add links such as privacy policy, help, general account settings
            e.Commands.Add(new SettingsCommand("privacypolicy", "Privacy policy", PrivacyPolicyInvoked));
            e.Commands.Add(new SettingsCommand("otherlink", "Other link", OtherLinkInvoked));
        }

        private async void WebAccountProviderCommandInvoked(WebAccountProviderCommand command)
        {
            await RequestTokenAndSaveAccount(command.WebAccountProvider, AccountScopeOne, AccountClientId);
        }

        private async void WebAccountInvoked(WebAccountCommand command, WebAccountInvokedArgs args)
        {
            if (args.Action == WebAccountAction.Remove)
            {
                await LogoffAndRemoveAccount();
            }
        }

        private void PrivacyPolicyInvoked(IUICommand command)
        {

        }

        private void OtherLinkInvoked(IUICommand command)
        {

        }

        private async Task RequestTokenAndSaveAccount(WebAccountProvider Provider, String Scope, String ClientID)
        {
            try
            {
                WebTokenRequest webTokenRequest = new WebTokenRequest(Provider, Scope, ClientID);

                WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);

                if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                {
                    ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);

                    ApplicationData.Current.LocalSettings.Values[StoredAccountKey] = webTokenRequestResult.ResponseData[0].WebAccount.Id;

                    TokenOne = webTokenRequestResult.ResponseData[0].Token;

                    webTokenRequest = new WebTokenRequest(Provider, AccountScopeTwo, ClientID);
                    webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);

                    if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                    {
                        TokenTwo = webTokenRequestResult.ResponseData[0].Token;
                    }
                    SignIntoAppService();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void SignIntoAppService()
        {
            if (await AzureAppService.SignIn(TokenTwo))
            {
                AccountName.Text = await MicrosoftGraph.GetAccountDetails(TokenOne, "displayName");
                ApplicationData.Current.LocalSettings.Values.Remove(StoredNameKey);
                ApplicationData.Current.LocalSettings.Values[StoredNameKey] = AccountName.Text;

                using (var imageStream = await MicrosoftGraph.GetAccountPicture(TokenOne))
                {
                    using (var randomStream = imageStream.AsRandomAccessStream())
                    {
                        BitmapImage image = new BitmapImage();
                        await image.SetSourceAsync(randomStream);
                        ImageBrush imageBrush = new ImageBrush
                        {
                            ImageSource = image
                        };
                        AccountImage.Fill = imageBrush;
                        SignInListViewItem.Visibility = Visibility.Collapsed;
                        AccountListViewItem.Visibility = Visibility.Visible;
                    }
                }

                ApplicationData.Current.LocalSettings.Values.Remove(StoredEmailKey);
                ApplicationData.Current.LocalSettings.Values[StoredEmailKey] = await MicrosoftGraph.GetAccountDetails(TokenOne, "userPrincipalName");

                using (var imageStream = await MicrosoftGraph.GetAccountPicture(TokenOne))
                {
                    using (var randomStream = imageStream.AsRandomAccessStream())
                    {
                        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomStream);
                        SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                        StorageFile file_Save = await ApplicationData.Current.LocalFolder.CreateFileAsync("ProfilePicture.jpg", CreationCollisionOption.ReplaceExisting);
                        BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, await file_Save.OpenAsync(FileAccessMode.ReadWrite));
                        encoder.SetSoftwareBitmap(softwareBitmap);
                        await encoder.FlushAsync();
                    }
                }
            }
        }

        private async Task LogoffAndRemoveAccount()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(StoredAccountKey))
            {
                WebAccountProvider providertoDelete = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);

                WebAccount accountToDelete = await WebAuthenticationCoreManager.FindAccountAsync(providertoDelete, (string)ApplicationData.Current.LocalSettings.Values[StoredAccountKey]);

                if (accountToDelete != null)
                {
                    await accountToDelete.SignOutAsync();
                }

                ApplicationData.Current.LocalSettings.Values.Remove(StoredAccountKey);
                ApplicationData.Current.LocalSettings.Values.Remove(StoredEmailKey);
                AccountListViewItem.Visibility = Visibility.Collapsed;
                SignInListViewItem.Visibility = Visibility.Visible;

                await Shared.Helpers.LocalStore.PurgeLocalStoreAsync();
                App.HomesViewModel = new HomesViewModel();
                App.RoomsViewModel = new RoomsViewModel();

                Frame rootFrame = Window.Current.Content as Frame;

                if (rootFrame == null)
                {
                    rootFrame = new Frame();

                    Window.Current.Content = rootFrame;
                }

                if (rootFrame.Content == null)
                {
                    bool autoSignIn = false;
                    AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= OnAccountCommandsRequested;
                    rootFrame.Navigate(typeof(Views.SignIn.MainPage), autoSignIn);
                }

                Window.Current.Activate();
            }
        }

        #endregion
    }
}
