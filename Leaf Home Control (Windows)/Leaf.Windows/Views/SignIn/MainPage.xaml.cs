using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Leaf.Windows.Helpers;
using Leaf.Windows.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.SignIn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool autoSignIn = true;
        ImageMenuItem user = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is bool)
            {
                autoSignIn = (bool)e.Parameter;
            }

            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += OnAccountCommandsRequested;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= OnAccountCommandsRequested;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (autoSignIn)
            {
                Instructions.Visibility = Visibility.Collapsed;
                user = await MicrosoftAccount.AutoSignIn();
                if (user != null)
                {
                    ResultsText.Text = "Welcome back " + user.AccountName + "!";
                    ProfileImage.Fill = user.Image;
                    OutputPanel.Visibility = Visibility.Visible;
                }
            }
            Loading_PR.IsActive = false;
            Loading_SP.Visibility = Visibility.Collapsed;
        }

        private async void OnAccountCommandsRequested(AccountsSettingsPane sender, AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            AccountsSettingsPaneEventDeferral deferral = e.GetDeferral();
            WebAccountProviderCommand providerCommand = new WebAccountProviderCommand(await MicrosoftAccount.GetProvider(), WebAccountProviderCommandInvoked);
            e.WebAccountProviderCommands.Add(providerCommand);
            deferral.Complete();
        }

        private async void WebAccountProviderCommandInvoked(WebAccountProviderCommand command)
        {
            // Show UI for signing in in the background in case of a slow connection and hide the sign in button
            LoadingHeader.Text = "Signing in..";
            Loading_PR.IsActive = true;
            GetStarted_sp.Visibility = Visibility.Collapsed;
            Loading_SP.Visibility = Visibility.Visible;

            user = await MicrosoftAccount.SignIn(command.WebAccountProvider);

            if (user != null)
            {
                Loading_SP.Visibility = Visibility.Collapsed;
                GetStarted_sp.Visibility = Visibility.Collapsed;
                OutputPanel.Visibility = Visibility.Visible;
                ResultsText.Text = "Hi " + user.AccountName + "!";
                ProfileImage.Fill = user.Image;
                Loading_PR.IsActive = false;
            }
            else
            {
                Loading_PR.IsActive = false;
                Loading_SP.Visibility = Visibility.Collapsed;
                GetStarted_sp.Visibility = Visibility.Visible;
            }
        }











        ////////////////////////////////
        ///


        private static async Task<ImageMenuItem> SignIn(WebAccountProvider provider)
        {
            ImageMenuItem user = new ImageMenuItem();
            return user;
        }

        private void CreateHome_Click(object sender, RoutedEventArgs e)
        {


        }

        private void JoinHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SignInApp_button_Click(object sender, RoutedEventArgs e)
        {
            OutputPanel.Visibility = Visibility.Collapsed;
            GetStarted_sp.Visibility = Visibility.Visible;
            SignInWindows_button.IsEnabled = false;
            AccountsSettingsPane.Show();
            await Task.Delay(1000);
            SignInWindows_button.IsEnabled = true;
        }

        private void Next_bt_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if ((string)button.Content == "Get Started")
            {
                Instructions.Visibility = Visibility.Collapsed;
                GetStarted_sp.Visibility = Visibility.Visible;
            }
            else
            {
                if (Instructions.SelectedIndex < 2)
                {
                    Instructions.SelectedIndex++;
                }
            }
        }

        private async void SignIn_button_Click(object sender, RoutedEventArgs e)
        {
            OutputPanel.Visibility = Visibility.Collapsed;
            GetStarted_sp.Visibility = Visibility.Visible;
            SignInWindows_button.IsEnabled = false;
            AccountsSettingsPane.Show();
            await Task.Delay(1000);
            SignInWindows_button.IsEnabled = true;
        }

        private async void Skip_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Continue_button_Click(object sender, RoutedEventArgs e)
        {
            OutputPanel.Visibility = Visibility.Collapsed;
            Syncing_PR.IsActive = true;
            Syncing_SP.Visibility = Visibility.Visible;

            if (await MicrosoftAccount.SignIntoAppService())
            {
                Initalise();
            }
        }

        private async void Initalise()
        {
            await Shared.Services.Storage.InitaliseAsync();
            LoadingHeader.Text = "Nearly finished..";
            await Shared.Services.Storage.SyncAsync();


            await App.HomesViewModel.Initalise();
            if (App.HomesViewModel.Homes.Count() == 0)
            {
                App.HomesViewModel.HomeName = "My Home";
                App.HomesViewModel.UserName = user.AccountName;
                App.HomesViewModel.AddHomeCommand.Execute(null);
            }
            try
            {
                App.RoomsViewModel.SelectedHomeId = App.HomesViewModel.SelectedHome.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ActivationService.Initalise - Error loading rooms: " + ex.Message);
            }

            AppShell shell = Window.Current.Content as AppShell;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (shell == null)
            {
                // Create a AppShell to act as the navigation context and navigate to the first page
                shell = new AppShell();
            }

            // Place our app shell in the current Window
            Window.Current.Content = shell;

            if (shell.AppFrame.Content == null)
            {
                AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= OnAccountCommandsRequested;
                shell.AppFrame.Navigate(typeof(Views.Overview.MainPage));
            }

            Window.Current.Activate();
        }
    }
}
