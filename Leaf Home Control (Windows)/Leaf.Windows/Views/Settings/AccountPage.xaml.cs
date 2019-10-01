using Leaf.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZXing;
using ZXing.Mobile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AccountName.Text = AppShell.user.AccountName;
            AccountEmail.Text = AppShell.user.Arguments;
            AccountImage.Fill = AppShell.user.Image;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var write = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE
            };
            var wb = write.Write(MobileService.Client.CurrentUser.UserId);
            this.qrcodeImg.Source = wb;
        }
    }
}
