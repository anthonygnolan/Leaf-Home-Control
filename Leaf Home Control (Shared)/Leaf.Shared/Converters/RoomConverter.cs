using Leaf.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Converters
{
    public class RoomConverter
    {
        public static RoomItem CreateFrom(Room room)
        {
            RoomItem roomItem = new RoomItem
            {
                Id = room.Id,
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
                Version = room.Version,
                OwnerId = room.OwnerId,
                UserId = room.UserId,
                RoomId = room.RoomId,
                HomeId = room.HomeId,
                HasAccess = room.HasAccess,
                Name = room.Name,
                BackgroundUri = room.BackgroundUri,
                BackgroundOpacity = room.BackgroundOpacity,
                BackgroundBlur = room.BackgroundBlur,
                Deleted = room.Deleted
            };

            return roomItem;
        }

        public static Room CreateFrom(RoomItem roomItem)
        {
            Room room = new Room
            {
                Id = roomItem.Id,
                CreatedAt = roomItem.CreatedAt,
                UpdatedAt = roomItem.UpdatedAt,
                Version = roomItem.Version,
                OwnerId = roomItem.OwnerId,
                UserId = roomItem.UserId,
                RoomId = roomItem.RoomId,
                HomeId = roomItem.HomeId,
                HasAccess = roomItem.HasAccess,
                Name = roomItem.Name,
                BackgroundUri = roomItem.BackgroundUri,
                BackgroundOpacity = roomItem.BackgroundOpacity,
                BackgroundBlur = roomItem.BackgroundBlur,
                Deleted = roomItem.Deleted
            };

            return room;
        }
    }
}
