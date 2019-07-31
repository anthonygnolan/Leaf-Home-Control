using Newtonsoft.Json;
using System;

namespace Leaf.AppService.DataObjects
{
    public class BaseItem : TableItem
    {
        [JsonProperty(PropertyName = "ownerId")]
        public string OwnerId { get; set; } = "";

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; } = "";

        [JsonProperty(PropertyName = "homeId")]
        public string HomeId { get; set; } = "";

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = "";

        [JsonProperty(PropertyName = "hasAccess")]
        public bool HasAccess { get; set; } = true;
    }
}
