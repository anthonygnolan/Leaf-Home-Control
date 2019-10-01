using Leaf.Shared.Models;
using Leaf.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaf.Windows.Views.Overview
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        StorageFile oldBackgroundImage;
        StorageFile newBackgroundImage;
        private HomesViewModel _homesViewModel { get { return this.DataContext as HomesViewModel; } }
        ObservableCollection<NavigationMenuItem> MenuItems = new ObservableCollection<NavigationMenuItem>();

        public DetailPage()
        {
            InitializeComponent();
            DataContext = App.HomesViewModel;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Home)
            {
                _homesViewModel.SelectedHome = (Home)e.Parameter;
            }
            try
            {
                oldBackgroundImage = await ApplicationData.Current.LocalFolder.GetFileAsync("HomeBackground" + _homesViewModel.SelectedHome.Id + ".jpg");
                await oldBackgroundImage.DeleteAsync();
            }
            catch (FileNotFoundException ex)
            {

            }
            catch (FileLoadException ex)
            {

            }
            LoadBackgrounds();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Update();
        }

        private void HomeName_hb_Click(object sender, RoutedEventArgs e)
        {
            HomeName_hb.Visibility = Visibility.Collapsed;
            EditHomeName_tb.Visibility = Visibility.Visible;
        }

        private void EditHomeName_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EditHomeName_tb.Text == "")
            {
                HomeNameEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                HomeNameEmpty.Visibility = Visibility.Collapsed;
                HomeName_hb.Content = EditHomeName_tb.Text;
                HomeName_hb.Visibility = Visibility.Visible;
                EditHomeName_tb.Visibility = Visibility.Collapsed;

                App.HomesViewModel.SelectedHome.Name = EditHomeName_tb.Text;
            }
        }

        private void ChangeHomeLocation_hb_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(HomeLocationPage));
        }

        private async void SelectPicture_bt_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile selectedImage = await openPicker.PickSingleFileAsync();

            if (selectedImage != null)
            {
                BitmapImage backgroundImage = new BitmapImage();
                FileRandomAccessStream stream = (FileRandomAccessStream)await selectedImage.OpenAsync(FileAccessMode.Read);
                backgroundImage.SetSource(stream);
                SetBackground(backgroundImage);

                try
                {
                    oldBackgroundImage = await ApplicationData.Current.LocalFolder.GetFileAsync("HomeBackground" + _homesViewModel.SelectedHome.Id + ".jpg");
                    await oldBackgroundImage.RenameAsync(_homesViewModel.SelectedHome.Id + "old.jpg", NameCollisionOption.ReplaceExisting);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    await selectedImage.CopyAsync(ApplicationData.Current.LocalFolder);
                    newBackgroundImage = await ApplicationData.Current.LocalFolder.GetFileAsync(selectedImage.Name);
                }
                catch (UnauthorizedAccessException ex)
                {

                }
                await newBackgroundImage.RenameAsync("HomeBackground" + _homesViewModel.SelectedHome.Id + ".jpg", NameCollisionOption.ReplaceExisting);
                App.HomesViewModel.SelectedHome.BackgroundUri = "ms-appdata:///local/HomeBackground" + _homesViewModel.SelectedHome.Id + ".jpg";
            }
        }

        private void BackgroundOpacitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }

        private void DeleteHomeButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentDialogResult result = await DeleteHomeConfirmation.ShowAsync();
            //if (result == ContentDialogResult.Primary)
            //{
            //    LeafHomeManager.DeleteHome(Account.HomeList, Home);
            //    UpdateAccount(Account);
            //    if (Account.HomeList.Count() == 0)
            //    {

            //    }
            //    else
            //    {
            //        this.Frame.Navigate(typeof(Overview.MainPage), Account.HomeList[0]);
            //    }
            //}
        }

        private void Update()
        {
            App.HomesViewModel.UpdateHomeCommand.Execute(null);
        }

        private void DeleteHome_CD_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DeleteHome_CD.Hide();
            this.Frame.Navigate(typeof(MainPage), new DrillOutThemeAnimation());
        }

        private void DeleteHome_CD_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DeleteHome_CD.Hide();
        }

        private void DeleteHome_HL_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            DeleteHome_CD.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void SelectLocation_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Overview.Location));
        }

        private void PictureLocation_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = PictureLocation_cb.SelectedIndex;

            try
            {
                switch (selectedIndex)
                {
                    case 0:
                        SelectPicture_bt.Visibility = Visibility.Collapsed;
                        BackgroundsGridView.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        SelectPicture_bt.Visibility = Visibility.Visible;
                        BackgroundsGridView.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        SelectPicture_bt.Visibility = Visibility.Collapsed;
                        BackgroundsGridView.Visibility = Visibility.Visible;
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void LoadBackgrounds()
        {
            NavigationMenuItem menuItemOne = new NavigationMenuItem
            {
                Label = "Home 1",
                Image = new Uri("ms-appx:///Leaf.Shared/Images/DefaultHomeBackground.jpg"),
                Arguments = ""
            };
            NavigationMenuItem menuItemTwo = new NavigationMenuItem
            {
                Label = "Home 2",
                Image = new Uri("ms-appx:///Leaf.Shared/Images/HomeBackground1.jpg"),
                Arguments = ""
            };
            NavigationMenuItem menuItemThree = new NavigationMenuItem
            {
                Label = "Home 3",
                Image = new Uri("ms-appx:///Leaf.Shared/Images/HomeBackground2.jpg"),
                Arguments = ""
            };
            NavigationMenuItem menuItemFour = new NavigationMenuItem
            {
                Label = "Home 4",
                Image = new Uri("ms-appx:///Leaf.Shared/Images/HomeBackground3.jpg"),
                Arguments = ""
            };
            MenuItems.Add(menuItemOne);
            MenuItems.Add(menuItemTwo);
            MenuItems.Add(menuItemThree);
            MenuItems.Add(menuItemFour);
        }

        private void BackgroundsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedBackground = (NavigationMenuItem)e.ClickedItem;
            BitmapImage bitmapImage = new BitmapImage(selectedBackground.Image);
            SetBackground(bitmapImage);
            App.HomesViewModel.SelectedHome.BackgroundUri = selectedBackground.Image.ToString();

        }

        private void SetBackground(BitmapImage backgroundImage)
        {
            BackgroundOne.Source = backgroundImage;
            BackgroundTwo.Source = backgroundImage;
            HomeImageBrush = new ImageBrush
            {
                ImageSource = backgroundImage,
                Stretch = Stretch.UniformToFill
            };
            HomeImage.Fill = HomeImageBrush;
        }
    }
}
