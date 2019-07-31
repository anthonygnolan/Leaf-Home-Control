using System;

using Leaf.Shared.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Leaf.Shared.Helpers;
using Leaf.Shared.Controllers;
using Leaf.Shared.Converters;

namespace Leaf.Shared.Services
{
    public class SampleDataService
    {
        public static async Task<bool> CreateSampleData()
        {
            await CreateSampleHome();

            return true;
        }

        public static async Task<bool> CreateSampleHome(string homeName = "Railway House")
        {
            Home home = new Home
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                OwnerId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                UserId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                HomeId = "",
                HasAccess = true,
                Name = homeName,
                IsPrimary = true,
                BackgroundUri = "ms-appx:///Leaf.Shared/Images/DefaultHomeBackground.jpg",
                BackgroundOpacity = 0.5,
                BackgroundBlur = 20,
                IsLocationAvailable = false,
                Location = "Castleconell",
                UserName = "Anthony",
                Deleted = false
            };
            HomeItem item = HomeConverter.CreateFrom(home);
            await HomeTable.Create(item);
            item.HomeId = HomeTable.HomeItem.Id;
            await HomeTable.Update(item);


            //Home hom = (Home)home;
            //Home Home = new Home();
            //{
            //    Id = "0001",
            //    CreatedAt = "",
            //    UpdatedAt = "",
            //    Version = "",
            //    OwnerId = "",
            //    UserId = "",

            //    HomeId = "",
            //    Name = homeName,
            //    IsPrimary = true,
            //    Background = new Background
            //    {
            //        Uri = "ms-appx:///Leaf.Shared/Images/DefaultHomeBackground.jpg",
            //        Opacity = 0.4,
            //        Blur = 50
            //    },
            //    Location = new Location
            //    {
            //        IsLocationAvailable = true,
            //        Address = "Castleconnell"
            //    },
            //    Rooms = new ObservableCollection<Room>()
            //};
            //Room Room = CreateSampleRoom("0001", "Living Room");
            //Home.Rooms.Add(Room);
            //Room = CreateSampleRoom("0002", "Kitchen");
            //Home.Rooms.Add(Room);
            //Room = CreateSampleRoom("0003", "Bedroom");
            //Home.Rooms.Add(Room);
            //Room = CreateSampleRoom("0004", "Office");
            //Home.Rooms.Add(Room);
            //Room = CreateSampleRoom("0004", "Bathroom");
            //Home.Rooms.Add(Room);
            return true;
        }




        public static async Task<bool> CreateSampleRoom(string roomName)
        {
            Room Room = new Room
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                OwnerId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                UserId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                Name = roomName,
                HomeId = HomeTable.HomeItem.Id,
                RoomId = "",
                HasAccess = true,
                BackgroundOpacity = 0.5,
                BackgroundBlur = 20,
                IsSelected = false,
                Deleted = false
            };

            RoomItem item = RoomConverter.CreateFrom(Room);
            await RoomTable.Create(item);
            item.RoomId = RoomTable.RoomItem.Id;
            bool success = await RoomTable.Update(item);
            return success;
        }

        public static async Task<bool> CreateSampleDevice(string deviceName, string deviceId)
        {
            Device Device = new Device
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                OwnerId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                UserId = "sid:79cb2d8a9896fd48bac1f3969a9965cc",
                HomeId = HomeTable.HomeItem.Id,
                RoomId = RoomTable.RoomItem.Id,
                HasAccess = true,
                Name = deviceName,
                Room = RoomTable.RoomItem.Name,
                DeviceId = deviceId,
                Status = false,
                Level = "40%",
                Deleted = false
            };

            DeviceItem item = DeviceConverter.CreateFrom(Device);
            await DeviceTable.Create(item);
            return true;
        }
    }
}
