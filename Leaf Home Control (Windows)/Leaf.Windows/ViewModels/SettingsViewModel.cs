using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Leaf.Shared.Models;
using Leaf.Windows.Helpers;
using Leaf.Windows.Services;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace Leaf.Windows.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable
    {
       public  ObservableCollection<NavigationMenuItem> MenuItems = new ObservableCollection<NavigationMenuItem>();

        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        public SettingsViewModel()
        {
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
            LoadMenuItems();
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void LoadMenuItems()
        {
            NavigationMenuItem menuItemOne = new NavigationMenuItem
            {
                Symbol = "\uE744",
                Label = "Zones",
                Description = "",
                IsSelected = true,
                Arguments = ""
            };
            NavigationMenuItem menuItemTwo = new NavigationMenuItem
            {
                Symbol = "\uED39",
                Label = "Scenes",
                Description = "",
                IsSelected = true,
                Arguments = ""
            };
            NavigationMenuItem menuItemThree = new NavigationMenuItem
            {
                Symbol = "\uE121",
                Label = "Routines",
                Description = "",
                IsSelected = true,
                Arguments = ""
            };
            NavigationMenuItem menuItemFour = new NavigationMenuItem
            {
                Symbol = "\uEA80",
                Label = "Lighting",
                Description = "",
                IsSelected = true,
                Arguments = ""
            };
            NavigationMenuItem menuItemFive = new NavigationMenuItem
            {
                Symbol = "\uEA6C",
                Label = "Appliances",
                Description = "",
                IsSelected = false,
                Arguments = ""
            };
            NavigationMenuItem menuItemSix = new NavigationMenuItem
            {
                Symbol = "\uE72E",
                Label = "Security",
                Description = "",
                IsSelected = false,
                Arguments = ""
            };
            NavigationMenuItem menuItemSeven = new NavigationMenuItem
            {
                Symbol = "\uE9CA",
                Label = "Climate",
                Description = "",
                IsSelected = true,
                Arguments = ""
            };

            MenuItems.Add(menuItemOne);
            MenuItems.Add(menuItemTwo);
            MenuItems.Add(menuItemThree);
            MenuItems.Add(menuItemFour);
            MenuItems.Add(menuItemFive);
            MenuItems.Add(menuItemSix);
            MenuItems.Add(menuItemSeven);
        }
    }
}
