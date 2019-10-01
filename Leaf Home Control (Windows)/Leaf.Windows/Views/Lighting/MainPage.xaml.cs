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

namespace Leaf.Windows.Views.Lighting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private RoomsViewModel _roomsViewModel { get { return this.DataContext as RoomsViewModel; } }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = App.RoomsViewModel;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Device light = (Device)button.DataContext;

            this.Frame.Navigate(typeof(Lighting.DetailsPage), light, new DrillInNavigationTransitionInfo());
        }

        private void Light_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var t = e;
        }

        private void ShowLights_Click(object sender, RoutedEventArgs e)
        {
            var t = e;
            var tt = (Button)t.OriginalSource;
            string ttt = tt.Name;


        }

    }
}
