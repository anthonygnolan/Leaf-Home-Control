using Leaf.Shared.ViewModels;
using Leaf.Windows.Services;
using Leaf.Windows.Views;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Leaf.Windows
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        const string StoredAccountKey = "accountid";

        public static HomesViewModel HomesViewModel = new HomesViewModel();
        public static RoomsViewModel RoomsViewModel = new RoomsViewModel();

        private static string ClientId = "0c60be8d-4f18-4eeb-a787-94637381d172";

        //public static PublicClientApplication PublicClientApp = new PublicClientApplicationBuilder();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(StoredAccountKey))
            {
                ActivationService.Initalise();


                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (!(Window.Current.Content is AppShell shell))
                {
                    // Create a AppShell to act as the navigation context and navigate to the first page
                    shell = new AppShell();

                    shell.AppFrame.NavigationFailed += OnNavigationFailed;

                    if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        //ApplicationData.Current.LocalSettings.Values["PreviouslyLaunched"] = false;
                        //TODO: Load state from previously suspended application
                    }
                }

                // Place our app shell in the current Window
                Window.Current.Content = shell;

                if (shell.AppFrame.Content == null)
                {

                    shell.AppFrame.Navigate(typeof(Views.Overview.MainPage));
                }
            }
            else
            {

                if (!(Window.Current.Content is Frame rootFrame))
                {
                    rootFrame = new Frame();

                    rootFrame.NavigationFailed += OnNavigationFailed;

                    if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        //TODO: Load state from previously suspended application
                    }

                    Window.Current.Content = rootFrame;
                }

                if (e.PrelaunchActivated == false)
                {
                    if (rootFrame.Content == null)
                    {
                        rootFrame.Navigate(typeof(Views.SignIn.MainPage), e.Arguments);
                    }
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.Protocol)
            {
                ProtocolActivatedEventArgs protocolArgs = args as ProtocolActivatedEventArgs;
                AppShell content = Window.Current.Content as AppShell;
                if (content.AppFrame.Content.GetType() == typeof(Views.Family.TestPage))
                {
                    content.AppFrame.Navigate(typeof(Views.Family.TestPage), protocolArgs.Uri);
                }
            }
            Window.Current.Activate();
            base.OnActivated(args);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
