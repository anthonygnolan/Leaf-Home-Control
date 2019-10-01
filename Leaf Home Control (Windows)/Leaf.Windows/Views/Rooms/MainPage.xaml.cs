using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Rooms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = App.RoomsViewModel;
        }

        private RoomsViewModel _roomsViewModel { get { return this.DataContext as RoomsViewModel; } }

        private void Home_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Overview.MainPage));
        }

        private void Rooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Rooms.SelectedIndex != -1)
            {
                foreach (Room room in _roomsViewModel.Rooms)
                {
                    room.IsSelected = false;
                }
                int selected = Rooms.SelectedIndex;
                if (_roomsViewModel.Rooms.Count() != 0)
                {
                    _roomsViewModel.Rooms[selected].IsSelected = true;
                }
            }
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            App.RoomsViewModel.UpdateSelectedRoomCommand.Execute(null);
            this.Frame.Navigate(typeof(DetailPage));
        }

        private void Family_bt_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Family.MainPage));
        }

        private void Rooms_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Rooms.MainPage));
        }

        private void Notifications_bt_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Views.Notifications.MainPage), new DrillInNavigationTransitionInfo());
        }

        private void Adddevice_Click(object sender, RoutedEventArgs e)
        {
            //More_ContentDialogue.Hide();
        }

        private void More_bt_Click(object sender, RoutedEventArgs e)
        {
            //await More_ContentDialogue.ShowAsync();
        }

        private void More_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Grid clickedItem = (Grid)e.ClickedItem;
            //More_ContentDialogue.Hide();
            //switch (clickedItem.Name)
            //{
            //    case "Edit":
            //        this.Frame.Navigate(typeof(EditPage), new DrillInNavigationTransitionInfo());
            //        break;
            //    case "Add":
            //        //this.Frame.Navigate(typeof(Settings.AddDevicePage), new DrillInNavigationTransitionInfo());
            //        break;
            //    default:
            //        break;
            //}
        }

        private void AddRoom_bt_Click(object sender, RoutedEventArgs e)
        {
            NewRoomName.Text = "";
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            AddNewRoom_CD.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void AddNewRoom_CD_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewRoom_CD.Hide();
        }

        private void AddNewRoom_CD_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            NewRoomName.Text = "";
            AddNewRoom_CD.Hide();
        }

        private void NewRoomName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewRoomName.Text != "")
            {
                AddNewRoom_CD.IsPrimaryButtonEnabled = true;
            }
            else
            {
                AddNewRoom_CD.IsPrimaryButtonEnabled = false;
            }
        }

        private void FavouriteDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeviceButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //ToggleButton toggleButton = (ToggleButton)sender;
            //DeviceItem device = (DeviceItem)toggleButton.DataContext;
            //string preIP = "http://";
            //string IP = "192.168.1.4";
            //string postIP = "/api";
            //string username = "/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC";
            //string selection = "/lights/";
            //string lightsApi = "/state";
            //string url = preIP + IP + postIP + username + selection + device.DeviceId + lightsApi;

            //string body = "";
            //switch (toggleButton.IsChecked)
            //{
            //    case false:
            //        body = "{\"on\": false}";
            //        break;
            //    case true:
            //        body = "{\"hue\": 8597,\"on\": true,\"bri\": 254}";
            //        break;
            //    default:
            //        body = "{\"on\": false}";
            //        break;
            //}

            //var result = await(new HttpClient()).PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).ContinueWith(response => response.Result.Content.ReadAsStringAsync());
            //string responseFromServer = result.Result;
        }
    }
}
