using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Overview
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Location : Page
    {
        string address = "";
        private HomesViewModel _homesViewModel { get { return this.DataContext as HomesViewModel; } }

        public Location()
        {
            this.InitializeComponent();
            DataContext = App.HomesViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MapService.ServiceToken = "w0Liy8GfWh7QJ0Vf3oKS~eZWPKcr32Kuierg9o3MALA~ApX3gsxp_26GUpHCfebArqPRcl60jH-CA1xilc9CtbTivIOTJsb7UiWUfQPzWPrc";
            var geolocator = new Geolocator();
            var position = await geolocator.GetGeopositionAsync();
            SetLocationMap.Center = position.Coordinate.Point;
            SetLocationMap.ZoomLevel = 19;

            var mapLocation = await MapLocationFinder.FindLocationsAtAsync(position.Coordinate.Point);
            if (mapLocation.Status == MapLocationFinderStatus.Success)
            {
                address = mapLocation.Locations[0].Address.StreetNumber + " " + mapLocation.Locations[0].Address.Street + ", " + mapLocation.Locations[0].Address.Town + ", " + mapLocation.Locations[0].Address.Country;
                MapIcon mapPin = new MapIcon();
                mapPin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Images/MapPin.png"));
                mapPin.Location = position.Coordinate.Point;
                mapPin.Title = address;
                SetLocationMap.MapElements.Clear();
                SetLocationMap.MapElements.Add(mapPin);
                MapAddress.Text = address;
            }
            else
            {
                //dialog.Content = "Not Found";
            }
        }

        private async void SetLocationMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var selectedLocation = args.Location;
            var mapLocation = await MapLocationFinder.FindLocationsAtAsync(selectedLocation, 0);
            if (mapLocation.Status == MapLocationFinderStatus.Success)
            {
                string address = mapLocation.Locations[0].Address.StreetNumber + " " + mapLocation.Locations[0].Address.Street + ", " + mapLocation.Locations[0].Address.Town + ", " + mapLocation.Locations[0].Address.Country;
                MapIcon mapPin = new MapIcon();
                mapPin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Leaf.Shared/Assets/Images/mappin.png"));
                mapPin.Location = selectedLocation;
                mapPin.Title = address;
                SetLocationMap.MapElements.Clear();
                SetLocationMap.MapElements.Add(mapPin);
                MapAddress.Text = address;
            }
        }

        private void SelectLocation_bt_Click(object sender, RoutedEventArgs e)
        {
            //App.HomesViewModel.SelectedHome.Location = address;
            Update();
            this.Frame.Navigate(typeof(Overview.DetailPage));
        }

        private void Update()
        {
            App.HomesViewModel.UpdateHomeCommand.Execute(null);
        }
    }
}
