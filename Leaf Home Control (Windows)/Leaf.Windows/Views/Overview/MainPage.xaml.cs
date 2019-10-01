using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Leaf.Windows.Views.Overview
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.HomesViewModel;
        }

        private HomesViewModel _homesViewModel { get { return this.DataContext as HomesViewModel; } }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                App.RoomsViewModel.SelectedHomeId = App.HomesViewModel.SelectedHome.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeviceTable.Create - Unable to add item to table. Message recieved: " + ex.Message);

            }
            App.RoomsViewModel.GetRoomsCommand.Execute(null);
        }

        private void EditHomeButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundOne.Source = null;
            HomeImageSource.UriSource = null;
            this.Frame.Navigate(typeof(DetailPage), new DrillInNavigationTransitionInfo());
        }

        private void Family_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Family.TestPage), new DrillInNavigationTransitionInfo());
        }

        private void Rooms_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Rooms.MainPage), new DrillInNavigationTransitionInfo());
        }

        private void Notifications_bt_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Views.Notifications.MainPage), new DrillInNavigationTransitionInfo());
        }

        private void Adddevice_Click(object sender, RoutedEventArgs e)
        {
            More_ContentDialogue.Hide();
        }

        private async void More_bt_Click(object sender, RoutedEventArgs e)
        {
            await More_ContentDialogue.ShowAsync();
        }

        private void More_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Grid clickedItem = (Grid)e.ClickedItem;
            More_ContentDialogue.Hide();
            switch (clickedItem.Name)
            {
                case "Edit":
                    this.Frame.Navigate(typeof(DetailPage), new DrillInNavigationTransitionInfo());
                    break;
                case "Add":
                    this.Frame.Navigate(typeof(Settings.AddDevicePage), new DrillInNavigationTransitionInfo());
                    break;
                default:
                    break;
            }
        }

        private async void FavouriteDevices_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            Device device = (Device)toggleButton.DataContext;

            Leaf.Shared.Devices.Lights.SetLightState(device);
        }
    }
}
