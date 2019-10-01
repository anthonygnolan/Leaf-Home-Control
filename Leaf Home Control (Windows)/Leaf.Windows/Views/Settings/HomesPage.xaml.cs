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

namespace Leaf.Windows.Views.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomesPage : Page
    {
        private HomesViewModel _homesViewModel { get { return this.DataContext as HomesViewModel; } }

        public HomesPage()
        {
            this.InitializeComponent();
            DataContext = App.HomesViewModel;
        }

        private void HomesMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Home)e.ClickedItem;
            this.Frame.Navigate(typeof(Overview.DetailPage), clickedItem, new EntranceNavigationTransitionInfo());
        }

        private void AddHomeButton_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            HomeName.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
