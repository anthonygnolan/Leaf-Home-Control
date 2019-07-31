using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.AppService.DataObjects
{
    public class TableItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; } = false;
    }
}
