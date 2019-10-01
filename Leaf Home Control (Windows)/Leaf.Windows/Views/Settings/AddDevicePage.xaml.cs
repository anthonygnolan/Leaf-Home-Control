using Leaf.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDevicePage : Page
    {
        private string pageNameNarrow = "ADD A DEVICE";
        private string pageNameWide = "Add A Device";
        ObservableCollection<NavigationMenuItem> MenuItems = new ObservableCollection<NavigationMenuItem>();

        public AddDevicePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenuItems();
        }

        private void MenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (NavigationMenuItem)e.ClickedItem;
            Frame.Navigate(menuItem.DestinationPage, menuItem.Arguments);
        }

        private void LoadMenuItems()
        {
            NavigationMenuItem menuItemOne = new NavigationMenuItem
            {
                Symbol = "\uF785",
                Label = "Philips Hue",
                Description = "Philips Hue Lighting",
                Image = new Uri("ms-appx:///Leaf.Shared/Images/PhilipsHueLogo.jpg"),
                DestinationPage = typeof(Devices.PhilipsHue),
                Arguments = ""
            };
            NavigationMenuItem menuItemTwo = new NavigationMenuItem
            {
                Symbol = "\uF785",
                Label = "Sonos",
                Description = "Sonos Coontrollers",
                DestinationPage = typeof(Views.Routines.MainPage),
                Arguments = ""
            };
            MenuItems.Add(menuItemOne);
            MenuItems.Add(menuItemTwo);
        }
    }
}
