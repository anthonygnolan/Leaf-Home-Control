using Leaf.AppService.DataObjects;
using Newtonsoft.Json;
using System;

namespace Leaf.Shared.Models
{
    public class HomeItem : BaseItem
    {
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; } = "";

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; set; } = false;

        [JsonProperty(PropertyName = "backgroundUri")]
        public string BackgroundUri { get; set; } = "ms-appx:///Leaf.Shared/Images/DefaultHomeBackground.jpg";

        [JsonProperty(PropertyName = "backgroundOpacity")]
        public double BackgroundOpacity { get; set; } = 0.5;

        [JsonProperty(PropertyName = "backgroundBlur")]
        public int BackgroundBlur { get; set; } = 20;

        [JsonProperty(PropertyName = "isLocationAvailable")]
        public bool IsLocationAvailable { get; set; } = false;

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; } = "";

        [JsonProperty(PropertyName = "menuItems")]
        public string MenuItems { get; set; } = @"{""RoomsVisibility"":true,""LightingVisibility"":false}";
    }
}
