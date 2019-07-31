using Leaf.AppService.DataObjects;
using Newtonsoft.Json;
using System;

namespace Leaf.Shared.Models
{
    public class RoomItem : BaseItem
    {
        [JsonProperty(PropertyName = "roomId")]
        public string RoomId { get; set; } = "";

        [JsonProperty(PropertyName = "backgroundUri")]
        public string BackgroundUri { get; set; } = "ms-appx:///Leaf.Shared/Images/DefaultRoomBackground.jpg";

        [JsonProperty(PropertyName = "backgroundOpacity")]
        public double BackgroundOpacity { get; set; } = 0.2;

        [JsonProperty(PropertyName = "backgroundBlur")]
        public int BackgroundBlur { get; set; } = 20;
    }

}
