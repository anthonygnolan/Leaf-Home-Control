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
    public sealed partial class MainPage : Page
    {
        ObservableCollection<NavigationMenuItem> MenuItems = new ObservableCollection<NavigationMenuItem>();

        public MainPage()
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
                Symbol = "\uE2AF",
                Label = "Account",
                Description = "Manage your account",
                DestinationPage = typeof(AccountPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemTwo = new NavigationMenuItem
            {
                Symbol = "\uE10F",
                Label = "Homes",
                Description = "View home detials",
                DestinationPage = typeof(HomesPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemThree = new NavigationMenuItem
            {
                Symbol = "\uE154",
                Label = "Rooms",
                Description = "View room settings for the selected home",
                DestinationPage = typeof(RoomsPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemFour = new NavigationMenuItem
            {
                Symbol = "\uE212",
                Label = "Devices",
                Description = "View and edit your devices",
                DestinationPage = typeof(DevicesPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemFive = new NavigationMenuItem
            {
                Symbol = "\uE771",
                Label = "Personalisation",
                Description = "Change the Look and Feel of the App",
                DestinationPage = typeof(PersonalisationPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemSix = new NavigationMenuItem
            {
                Symbol = "\uE1F6",
                Label = "Privacy",
                Description = "View and Manage Privacy Settings",
                DestinationPage = typeof(PrivacyPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemSeven = new NavigationMenuItem
            {
                Symbol = "\uE783",
                Label = "About",
                Description = "View Details about the App",
                DestinationPage = typeof(AboutPage),
                Arguments = ""
            };
            NavigationMenuItem menuItemEight = new NavigationMenuItem
            {
                Symbol = "\uF785",
                Label = "Sample Data",
                Description = "Load Sample data",
                DestinationPage = typeof(SampleDataPage),
                Arguments = ""
            };


            MenuItems.Add(menuItemOne);
            MenuItems.Add(menuItemTwo);
            MenuItems.Add(menuItemThree);
            MenuItems.Add(menuItemFour);
            MenuItems.Add(menuItemFive);
            MenuItems.Add(menuItemSix);
            MenuItems.Add(menuItemSeven);
            MenuItems.Add(menuItemEight);
        }
    }
}
