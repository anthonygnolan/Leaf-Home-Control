
using Leaf.Shared.Devices;
using Leaf.Shared.Services;
using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SampleDataPage : Page
    {
        public SampleDataPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await SampleDataService.CreateSampleHome();
            await SampleDataService.CreateSampleRoom("Living Room");
            //await SampleDataService.CreateSampleDevice("Ceiling Light", "2");
            await SampleDataService.CreateSampleDevice("Ceiling Light", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/2");
            await SampleDataService.CreateSampleRoom("Kitchen");
            //await SampleDataService.CreateSampleDevice("Ceiling Light", "6");
            await SampleDataService.CreateSampleDevice("Ceiling Light", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/3");
            await SampleDataService.CreateSampleRoom("Hall");
            //await SampleDataService.CreateSampleDevice("Ceiling Light 1", "6");
            await SampleDataService.CreateSampleDevice("Ceiling Light 1", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/6");
            //await SampleDataService.CreateSampleDevice("Ceiling Light 2", "4");
            await SampleDataService.CreateSampleDevice("Ceiling Light 2", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/4");
            await SampleDataService.CreateSampleRoom("Bedroom");
            //await SampleDataService.CreateSampleDevice("Ceiling Light", "5");
            await SampleDataService.CreateSampleDevice("Ceiling Light", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/5");
            //await SampleDataService.CreateSampleDevice("Ceiling Light 1", "6");
            await SampleDataService.CreateSampleDevice("Bedside Lamp", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/6");
            await SampleDataService.CreateSampleDevice("Lamp", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/10");
            await SampleDataService.CreateSampleRoom("Bathroom");
            //await SampleDataService.CreateSampleDevice("Ceiling Light", "7");
            await SampleDataService.CreateSampleDevice("Ceiling Light", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/7");
            await SampleDataService.CreateSampleRoom("Office");
            //await SampleDataService.CreateSampleDevice("Ceiling Light", "2");
            await SampleDataService.CreateSampleDevice("Ceiling Light", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/1");

            Initalise();
            var messageDialog = new MessageDialog("Sample data has been loaded.");
            await messageDialog.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool sucess = await Leaf.Shared.Services.Storage.PurgeAsync();
            var messageDialog = new MessageDialog("Local store has been erased.");
            await messageDialog.ShowAsync();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Leaf.Shared.Devices.Lights.GetState("http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC");
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            await Shared.Helpers.LocalStore.RefreshLocalStoreAsync();
            Initalise();
            var messageDialog = new MessageDialog("All Items Refreshed.");
            await messageDialog.ShowAsync();
        }

        private void Initalise()
        {
            App.HomesViewModel = new HomesViewModel();
            App.RoomsViewModel = new RoomsViewModel();

            App.HomesViewModel.Initalise();

            try
            {
                App.RoomsViewModel.SelectedHomeId = App.HomesViewModel.SelectedHome.Id;
                App.RoomsViewModel.GetRoomsCommand.Execute(null);
            }
            catch (Exception et)
            {

            }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            await SampleDataService.CreateSampleHome("Cork Institute of Technology");
            await SampleDataService.CreateSampleRoom("B176");
            await SampleDataService.CreateSampleDevice("Lamp 1", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/2");
            await SampleDataService.CreateSampleDevice("Lamp 2", "http://192.168.1.4/api/xUMVgvrOq4iYOU5qYMxooC04fRPApMWRMcYSvPvC/lights/3");
            await SampleDataService.CreateSampleRoom("B174");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
