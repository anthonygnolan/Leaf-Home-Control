using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Rooms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
            DataContext = App.RoomsViewModel;
        }

        private RoomsViewModel _roomsViewModel { get { return this.DataContext as RoomsViewModel; } }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Room)
            {
                _roomsViewModel.SelectedRoom = (Room)e.Parameter;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.RoomsViewModel.UpdateRoomCommand.Execute(null);
        }

        private void RoomName_hb_Click(object sender, RoutedEventArgs e)
        {
            RoomName_hb.Visibility = Visibility.Collapsed;
            EditRoomName_tb.Visibility = Visibility.Visible;
        }

        private void EditRoomName_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EditRoomName_tb.Text == "")
            {
                RoomNameEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                RoomNameEmpty.Visibility = Visibility.Collapsed;
                RoomName_hb.Content = EditRoomName_tb.Text;
                RoomName_hb.Visibility = Visibility.Visible;
                EditRoomName_tb.Visibility = Visibility.Collapsed;

                //HomeViewModel.Home.Name = EditHomeName_tb.Text;
            }
        }

        private void DeleteRoom_HL_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            DeleteRoom_CD.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void DeleteRoom_CD_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //App.Home.Rooms.Remove(Room);
            DeleteRoom_CD.Hide();
            this.Frame.Navigate(typeof(MainPage), new DrillOutThemeAnimation());
        }

        private void DeleteRoom_CD_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DeleteRoom_CD.Hide();
        }

        private void BackgroundOpacitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }

        private void SelectPicture_bt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
