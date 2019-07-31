
using Leaf.Shared.Common;
using Leaf.Shared.Controllers;
using Leaf.Shared.Converters;
using Leaf.Shared.Devices;
using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Leaf.Shared.ViewModels
{
    public class HomesViewModel : ObservableObject
    {
        #region Fields

        private string _homeName;
        private string _userName;
        private int _selected = -1;
        private ObservableCollection<Home> _homes = new ObservableCollection<Home>();
        private Home _selectedHome;
        private ObservableCollection<Device> _devices = new ObservableCollection<Device>();
        private ICommand _getHomeCommand;
        private ICommand _getHomesCommand;
        private ICommand _addHomeCommand;
        private ICommand _updateHomeCommand;
        private ICommand _updateSelectedHomeCommand;
        private ICommand _deleteHomeCommand;
        private ICommand _getDevicesCommand;

        #endregion

        #region Public Properties/Commands

        public async Task<bool> Initalise()
        {
            await GetHomes();
            await GetHome();
            return true;
        }

        public string HomeName
        {
            get { return _homeName; }
            set
            {
                if (value != _homeName)
                {
                    _homeName = value;
                    OnPropertyChanged("HomeName");
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        public int Selected
        {
            get { return _selected; }
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        public ObservableCollection<Home> Homes
        {
            get { return _homes; }
            set
            {
                if (value != _homes)
                {
                    _homes = value;
                    OnPropertyChanged("Homes");
                }
            }
        }

        public Home SelectedHome
        {
            get { return _selectedHome; }
            set
            {
                if (value != _selectedHome)
                {
                    _selectedHome = value;
                    OnPropertyChanged("SelectedHome");
                }
            }
        }

        public ObservableCollection<Device> FavouriteDevices
        {
            get { return _devices; }
            set
            {
                if (value != _devices)
                {
                    _devices = value;
                    OnPropertyChanged("Devices");
                }
            }
        }

        public ICommand GetHomeCommand
        {
            get
            {
                if (_getHomeCommand == null)
                {
                    _getHomeCommand = new RelayCommand(_getHome);
                }
                return _getHomeCommand;
            }
        }

        public ICommand GetHomesCommand
        {
            get
            {
                if (_getHomesCommand == null)
                {
                    _getHomesCommand = new RelayCommand(_getHomes);
                }
                return _getHomesCommand;
            }
        }

        public ICommand AddHomeCommand
        {
            get
            {
                if (_addHomeCommand == null)
                {
                    _addHomeCommand = new RelayCommand(AddHome);
                }
                return _addHomeCommand;
            }
        }

        public ICommand UpdateHomeCommand
        {
            get
            {
                if (_updateHomeCommand == null)
                {
                    _updateHomeCommand = new RelayCommand(UpdateHome);
                }
                return _updateHomeCommand;
            }
        }

        public ICommand UpdateSelectedHomeCommand
        {
            get
            {
                if (_updateSelectedHomeCommand == null)
                {
                    _updateSelectedHomeCommand = new RelayCommand(UpdateSelectedHome);
                }
                return _updateSelectedHomeCommand;
            }
        }

        public ICommand DeleteHomeCommand
        {
            get
            {
                if (_deleteHomeCommand == null)
                {
                    _deleteHomeCommand = new RelayCommand(DeleteHome);
                }
                return _deleteHomeCommand;
            }
        }

        public ICommand GetDevicesCommand
        {
            get
            {
                if (_getDevicesCommand == null)
                {
                    _getDevicesCommand = new RelayCommand(GetFavouriteDevices);
                }
                return _getDevicesCommand;
            }
        }

        #endregion

        #region Private Helpers

        private async Task<bool> GetHome()
        {
            IMobileServiceTableQuery<HomeItem> homeQuery;
            if (MobileService.Client.CurrentUser != null)
            {
                if (SelectedHome != null)
                {
                    homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.Id == SelectedHome.Id && homeItem.HasAccess == true && homeItem.UserId == MobileService.Client.CurrentUser.UserId && homeItem.Deleted == false);
                }
                else
                {
                    homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.HasAccess == true && homeItem.UserId == MobileService.Client.CurrentUser.UserId && homeItem.Deleted == false);
                }

            }
            else
            {
                if (SelectedHome != null)
                {
                    homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.Id == SelectedHome.Id && homeItem.HasAccess == true && homeItem.Deleted == false);
                }
                else
                {
                    homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.HasAccess == true && homeItem.Deleted == false);
                }
            }
            await HomeTable.Read(homeQuery);
            if (HomeTable.HomeItems.Count() != 0)
            {
                SelectedHome = Converters.HomeConverter.CreateFrom(HomeTable.HomeItems.ElementAt(0));
            }
            return true;
        }

        private async void _getHome()
        {
            GetHome();
        }

        private async Task<bool> getHome()
        {
            return await GetHome();
        }

        private async Task<bool> GetHomes()
        {
            Homes.Clear();
            IMobileServiceTableQuery<HomeItem> homeQuery;
            if (MobileService.Client.CurrentUser != null)
            {
                homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.HasAccess == true && homeItem.UserId == MobileService.Client.CurrentUser.UserId && homeItem.Deleted == false);
            }
            else
            {
                homeQuery = HomeTable.HomeSyncTable.Where(homeItem => homeItem.HasAccess == true && homeItem.Deleted == false);
            }
            await HomeTable.Read(homeQuery);
            if (HomeTable.HomeItems.Count() != 0)
            {
                foreach (HomeItem HomeItem in HomeTable.HomeItems)
                {
                    Homes.Add(Converters.HomeConverter.CreateFrom(HomeItem));
                }
            }
            return true;
        }

        private async void _getHomes()
        {
            GetHomes();
        }

        private async Task<bool> getHomes()
        {
            return await GetHomes();
        }

        private async void AddHome()
        {
            HomeItem home = new HomeItem
            {
                Name = HomeName,
                UserName = UserName
            };
            if (MobileService.Client.CurrentUser != null)
            {
                home.OwnerId = MobileService.Client.CurrentUser.UserId;
                home.UserId = MobileService.Client.CurrentUser.UserId;
            }
            await HomeTable.Create(home);
            home.HomeId = HomeTable.HomeItem.Id;
            await HomeTable.Update(home);
            SelectedHome = Converters.HomeConverter.CreateFrom(HomeTable.HomeItem);
            GetHomes();
        }

        private async void UpdateHome()
        {
            await HomeTable.Update(HomeConverter.CreateFrom(SelectedHome));
            GetHomes();
            GetHome();
        }

        private void UpdateSelectedHome()
        {
            SelectedHome = Homes[Selected];
            GetFavouriteDevices();
        }

        private async void DeleteHome()
        {
            SelectedHome.Deleted = true;
            await HomeTable.Update(HomeConverter.CreateFrom(SelectedHome));
            GetHome();
            GetHomes();

        }

        private async void GetFavouriteDevices()
        {
            FavouriteDevices.Clear();
            IMobileServiceTableQuery<DeviceItem> deviceQuery;
            deviceQuery = DeviceTable.deviceTable.Where(p => p.HomeId == SelectedHome.Id && p.Deleted == false);
            if (await DeviceTable.Read(deviceQuery))
            {
                foreach (DeviceItem DeviceItem in DeviceTable.deviceItems)
                {
                    Device device = DeviceConverter.CreateFrom(DeviceItem);
                    try
                    {
                        device = await Lights.GetLightState(device);
                    }
                    catch (Exception e)
                    {
                        device.HasAccess = false;
                    }
                    FavouriteDevices.Add(device);
                }
            }
        }

        #endregion
    }
}
