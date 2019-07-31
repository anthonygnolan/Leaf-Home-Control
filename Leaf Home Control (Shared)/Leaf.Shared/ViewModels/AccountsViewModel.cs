using Leaf.Shared.Common;
using Leaf.Shared.Controllers;
using Leaf.Shared.Converters;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.ViewModels
{
    public class AccountsViewModel : ObservableObject
    {

        private ObservableCollection<Account> Accounts = new ObservableCollection<Account>();
#pragma warning disable IDE0044 // Add readonly modifier
        private static ObservableCollection<Home> _homes = new ObservableCollection<Home>();
        private static ObservableCollection<Room> _rooms = new ObservableCollection<Room>();
        private ObservableCollection<Device> _devices = new ObservableCollection<Device>();
#pragma warning restore IDE0044 // Add readonly modifier

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

        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set
            {
                if (value != _rooms)
                {
                    _rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }

        public ObservableCollection<Device> Devices
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
        public async void GetUser()
        {
            IMobileServiceTableQuery<HomeItem> homeQuery;
            homeQuery = HomeTable.HomeSyncTable.Where(p => p.HasAccess == true && p.IsPrimary == true);
            await HomeTable.Read(homeQuery);
            if (HomeTable.HomeItems.Count() != 0)
            {
                //SelectedHome = Converters.HomeConverter.CreateFrom(HomeTable.HomeItems.ElementAt(0));
            }
        }

        public async Task<bool> GetUsers(string homeId)
        {
            Homes.Clear();
            IMobileServiceTableQuery<HomeItem> homeQuery;
            homeQuery = HomeTable.HomeSyncTable.Where(p => p.HomeId == homeId);
            await HomeTable.Read(homeQuery);
            foreach (HomeItem HomeItem in HomeTable.HomeItems)
            {
                Homes.Add(Converters.HomeConverter.CreateFrom(HomeItem));
            }
            return true;
        }

        public static async void AddUser(string userID, string homeId)
        {
            IMobileServiceTableQuery<HomeItem> homeQuery;
            homeQuery = HomeTable.HomeSyncTable.Where(p => p.HomeId == homeId);
            await HomeTable.Read(homeQuery);
            foreach (HomeItem HomeItem in HomeTable.HomeItems)
            {
                HomeItem.UserId = userID;
                HomeItem.UserName = "Pending Invitation";
                HomeItem.Id = null;
                await HomeTable.Create(HomeItem);

                IMobileServiceTableQuery<RoomItem> roomQuery;
                roomQuery = RoomTable.RoomSyncTable.Where(p => p.HomeId == homeId);
                await RoomTable.Read(roomQuery);
                foreach (RoomItem RoomItem in RoomTable.RoomItems)
                {
                    RoomItem.UserId = userID;
                    string roomId = RoomItem.Id;
                    RoomItem.Id = null;
                    RoomItem.HomeId = HomeTable.HomeItem.Id;
                    await RoomTable.Create(RoomItem);

                    IMobileServiceTableQuery<DeviceItem> deviceQuery;
                    deviceQuery = DeviceTable.deviceTable.Where(p => p.RoomId == roomId);
                    await DeviceTable.Read(deviceQuery);
                    foreach (DeviceItem DeviceItem in DeviceTable.deviceItems)
                    {
                        DeviceItem.UserId = userID;
                        DeviceItem.Id = null;
                        DeviceItem.RoomId = RoomTable.RoomItem.Id;
                        await DeviceTable.Create(DeviceItem);
                    }

                }

            }
        }

        public void UpdateUser()
        {

        }

        public void DeleteUser()
        {

        }

        public async Task<bool> GetRooms(string homeid)
        {
            Rooms.Clear();
            IMobileServiceTableQuery<RoomItem> roomQuery;
            roomQuery = RoomTable.RoomSyncTable.Where(p => p.HomeId == homeid);
            await RoomTable.Read(roomQuery);
            foreach (RoomItem RoomItem in RoomTable.RoomItems)
            {
                Room room = RoomConverter.CreateFrom(RoomItem);
                Rooms.Add(room);
            }
            return true;
        }

        public async Task<bool> GetDevices(string roomid)
        {
            Devices.Clear();
            IMobileServiceTableQuery<DeviceItem> deviceQuery;
            deviceQuery = DeviceTable.deviceTable.Where(p => p.RoomId == roomid);
            await DeviceTable.Read(deviceQuery);
            foreach (DeviceItem DeviceItem in DeviceTable.deviceItems)
            {
                Device device = DeviceConverter.CreateFrom(DeviceItem);
                Devices.Add(device);
            }
            return true;
        }
    }
}
