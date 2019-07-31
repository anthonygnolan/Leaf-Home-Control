using Leaf.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Converters
{
    public class DeviceConverter
    {
        public static DeviceItem CreateFrom(Device device)
        {
            DeviceItem deviceItem = new DeviceItem
            {
                Id = device.Id,
                CreatedAt = device.CreatedAt,
                UpdatedAt = device.UpdatedAt,
                Version = device.Version,
                OwnerId = device.OwnerId,
                HomeId = device.HomeId,
                HasAccess = device.HasAccess,
                Name = device.Name,
                Room = device.Room,
                RoomId = device.RoomId,
                Status = device.Status,
                Level = device.Level,
                DeviceId = device.DeviceId,
                Deleted = device.Deleted
            };

            return deviceItem;
        }

        public static Device CreateFrom(DeviceItem deviceItem)
        {
            Device device = new Device
            {
                Id = deviceItem.Id,
                CreatedAt = deviceItem.CreatedAt,
                UpdatedAt = deviceItem.UpdatedAt,
                Version = deviceItem.Version,
                OwnerId = deviceItem.OwnerId,
                HomeId = deviceItem.HomeId,
                HasAccess = deviceItem.HasAccess,
                Name = deviceItem.Name,
                Room = deviceItem.Room,
                RoomId = deviceItem.RoomId,
                Status = deviceItem.Status,
                Level = deviceItem.Level,
                DeviceId = deviceItem.DeviceId,
                Deleted = deviceItem.Deleted
            };

            return device;
        }
    }
}
