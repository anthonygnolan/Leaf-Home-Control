using Leaf.AppService.DataObjects;
using Newtonsoft.Json;
using System;

namespace Leaf.Shared.Models
{
    public class DeviceItem : BaseItem
    {
        [JsonProperty(PropertyName = "roomId")]
        public string RoomId { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "room")]
        public string Room { get; set; } = "";

        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; } = true;

        [JsonProperty(PropertyName = "level")]
        public string Level { get; set; } = "";
    }
}
