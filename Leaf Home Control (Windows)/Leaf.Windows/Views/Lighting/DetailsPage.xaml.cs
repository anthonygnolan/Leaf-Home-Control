using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Lighting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {

        private Device light = new Device();

        public DetailsPage()
        {
            this.InitializeComponent();
            DataContext = App.RoomsViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Device)
            {
                light = (Device)e.Parameter;
            }
        }

        private WriteableBitmap ColorSelectWriteableBitmap;
        private byte[] sourcePixels;

        private void ColorSelectImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Point cordinates = e.GetPosition(sender as Image);
            //int col = (int)cordinates.X;
            //int row = (int)cordinates.Y;

            //BitmapSource image = (WriteableBitmap)ColorSelectImage.Source;
            //int pixelWidth = image.PixelWidth;

            //int xy = 4 * pixelWidth * row + 4 * col;

            ////RGB values are pre-multiplied by normalized a;
            //var a = sourcePixels[xy + 3];
            //var rpm = (double)sourcePixels[xy + 2];
            //var gpm = (double)sourcePixels[xy + 1];
            //var bpm = (double)sourcePixels[xy];

            ////Correct pre-multiplication
            //var r = (byte)((a != 0) ? 255 * rpm / a : 255);
            //var g = (byte)((a != 0) ? 255 * gpm / a : 255);
            //var b = (byte)((a != 0) ? 255 * bpm / a : 255);

            //Color myColor = new Color();
            //myColor = Color.FromArgb(a, r, g, b);
            //SolidColorBrush brush = new SolidColorBrush(myColor);
            //SelectedColor.Fill = brush;
            //EditSettings_bt.Foreground = brush;
            ////DeviceName_hb.Foreground = brush;
            ////DeviceLocation_hb.Foreground = brush;
            //Brightness.Foreground = brush;

            //Pointer_gd.Children.Remove(ellipsePixelOne);
            //Pointer_gd.Children.Remove(ellipsePixelTwo);
            //Pointer_gd.Children.Remove(ellipsePixelThree);
            //SelectorCanvas.Children.Remove(Pointer_gd);


            //ellipsePixelOne = new Ellipse();
            //ellipsePixelOne.Fill = new SolidColorBrush(Colors.Black);
            //ellipsePixelOne.Height = 30;
            //ellipsePixelOne.Width = 30;

            //ellipsePixelTwo = new Ellipse();
            //ellipsePixelTwo.Fill = new SolidColorBrush(Colors.White);
            //ellipsePixelTwo.Height = 25;
            //ellipsePixelTwo.Width = 25;

            //ellipsePixelThree = new Ellipse();
            //ellipsePixelThree.Fill = brush;
            //ellipsePixelThree.Height = 20;
            //ellipsePixelThree.Width = 20;

            //Pointer_gd = new Grid();
            //Pointer_gd.Children.Add(ellipsePixelOne);
            //Pointer_gd.Children.Add(ellipsePixelTwo);
            //Pointer_gd.Children.Add(ellipsePixelThree);
            //Canvas.SetTop(Pointer_gd, row);
            //Canvas.SetLeft(Pointer_gd, col);
            //SelectorCanvas.Children.Add(Pointer_gd);
        }

        private async void EditSettings_bt_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await EditDeviceSettings.ShowAsync();
            DeviceName_tb.IsFocusEngaged = false;
            if (result == ContentDialogResult.Primary)
            {
                DeviceName_hb.Content = DeviceName_tb.Text;

                ComboBoxItem selectedItem = (ComboBoxItem)DeviceLocation_cb.SelectedItem;
                DeviceLocation_hb.Content = selectedItem.Content;

            }
        }
    }
}
