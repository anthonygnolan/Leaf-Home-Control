using Leaf.Shared.Common;
using Leaf.Shared.Controllers;
using Leaf.Shared.Converters;
using Leaf.Shared.Devices;
using Leaf.Shared.Helpers;
using Leaf.Shared.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//Inherit new objects from room
namespace Leaf.Shared.ViewModels
{
    public class RoomsViewModel : ObservableObject
    {
        #region Fields

        private string _selectedHomeId;
        private string _roomName;
        private int _selected;
        private ObservableCollection<Room> _rooms = new ObservableCollection<Room>();
        private Room _selectedRoom;
        private ICommand _getRoomCommand;
        private ICommand _getRoomsCommand;
        private ICommand _addRoomCommand;
        private ICommand _updateRoomCommand;
        private ICommand _updateSelectedRoomCommand;
        private ICommand _deleteRoomCommand;

        #endregion

        #region Public Properties/Commands

        public async Task<bool> Initialize()
        {
            await InitaliseRooms();
            await InitaliseRoom();
            return true;
        }

        public string SelectedHomeId
        {
            get { return _selectedHomeId; }
            set
            {
                if (value != _selectedHomeId)
                {
                    _selectedHomeId = value;
                    OnPropertyChanged("SelectedHomeId");
                }
            }
        }

        public string RoomName
        {
            get { return _roomName; }
            set
            {
                if (value != _roomName)
                {
                    _roomName = value;
                    OnPropertyChanged("RoomName");
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

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                if (value != _selectedRoom)
                {
                    _selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }

        public ICommand GetRoomCommand
        {
            get
            {
                if (_getRoomCommand == null)
                {
                    _getRoomCommand = new RelayCommand(GetRoom);
                }
                return _getRoomCommand;
            }
        }

        public ICommand GetRoomsCommand
        {
            get
            {
                if (_getRoomsCommand == null)
                {
                    _getRoomsCommand = new RelayCommand(GetRooms);
                }
                return _getRoomsCommand;
            }
        }

        public ICommand AddRoomCommand
        {
            get
            {
                if (_addRoomCommand == null)
                {
                    _addRoomCommand = new RelayCommand(AddRoom);
                }
                return _addRoomCommand;
            }
        }

        public ICommand UpdateRoomCommand
        {
            get
            {
                if (_updateRoomCommand == null)
                {
                    _updateRoomCommand = new RelayCommand(UpdateRoom);
                }
                return _updateRoomCommand;
            }
        }

        public ICommand UpdateSelectedRoomCommand
        {
            get
            {
                if (_updateSelectedRoomCommand == null)
                {
                    _updateSelectedRoomCommand = new RelayCommand(UpdateSelectedRoom);
                }
                return _updateSelectedRoomCommand;
            }
        }

        public ICommand DeleteRoomCommand
        {
            get
            {
                if (_deleteRoomCommand == null)
                {
                    _deleteRoomCommand = new RelayCommand(DeleteRoom);
                }
                return _deleteRoomCommand;
            }
        }

        #endregion

        #region Private Helpers

        private async void GetRoom()
        {
            IMobileServiceTableQuery<RoomItem> roomQuery;
            roomQuery = RoomTable.RoomSyncTable.Where(p => p.HasAccess == true);
            await RoomTable.Read(roomQuery);
            if (RoomTable.RoomItems.Count() != 0)
            {
                SelectedRoom = RoomConverter.CreateFrom(RoomTable.RoomItems.ElementAt(0));
            }
        }

        private async Task<bool> InitaliseRoom()
        {
            IMobileServiceTableQuery<RoomItem> roomQuery;
            roomQuery = RoomTable.RoomSyncTable.Where(p => p.HasAccess == true);
            await RoomTable.Read(roomQuery);
            if (RoomTable.RoomItems.Count() != 0)
            {
                SelectedRoom = RoomConverter.CreateFrom(RoomTable.RoomItems.ElementAt(0));
            }
            return true;
        }

        private async void GetRooms()
        {
            Rooms.Clear();
            IMobileServiceTableQuery<RoomItem> roomQuery;
            roomQuery = RoomTable.RoomSyncTable.Where(p => p.HomeId == SelectedHomeId);
            await RoomTable.Read(roomQuery);
            foreach (RoomItem RoomItem in RoomTable.RoomItems)
            {
                Room room = RoomConverter.CreateFrom(RoomItem);
                IMobileServiceTableQuery<DeviceItem> deviceQuery;
                deviceQuery = DeviceTable.deviceTable.Where(p => p.RoomId == room.Id);
                await DeviceTable.Read(deviceQuery);
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
                    room.Devices.Add(device);
                }
                Rooms.Add(room);
            }
        }

        private async Task<bool> InitaliseRooms()
        {
            Rooms.Clear();
            IMobileServiceTableQuery<RoomItem> roomQuery;
            roomQuery = RoomTable.RoomSyncTable.Where(p => p.HomeId == SelectedHomeId);
            await RoomTable.Read(roomQuery);
            foreach (RoomItem RoomItem in RoomTable.RoomItems)
            {
                Room room = RoomConverter.CreateFrom(RoomItem);
                IMobileServiceTableQuery<DeviceItem> deviceQuery;
                deviceQuery = DeviceTable.deviceTable.Where(p => p.RoomId == room.Id);
                await DeviceTable.Read(deviceQuery);
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
                }
                Rooms.Add(room);
            }
            return true;
        }

        private async void AddRoom()
        {
            RoomItem room = new RoomItem
            {
                Name = RoomName,
                HomeId = SelectedHomeId
            };
            if (MobileService.Client.CurrentUser != null)
            {
                room.OwnerId = MobileService.Client.CurrentUser.UserId;
                room.UserId = MobileService.Client.CurrentUser.UserId;
            }
            await RoomTable.Create(room);
            room.RoomId = RoomTable.RoomItem.Id;
            await RoomTable.Update(room);
            SelectedRoom = RoomConverter.CreateFrom(RoomTable.RoomItem);
            GetRooms();
        }

        private async void UpdateRoom()
        {
            await RoomTable.Update(RoomConverter.CreateFrom(SelectedRoom));
            GetRooms();
            GetRoom();
        }

        private void UpdateSelectedRoom()
        {
            SelectedRoom = Rooms[Selected];
        }

        private async void DeleteRoom()
        {
            await RoomTable.Delete(RoomConverter.CreateFrom(SelectedRoom));
            GetRooms();
        }

        #endregion

    }
}
