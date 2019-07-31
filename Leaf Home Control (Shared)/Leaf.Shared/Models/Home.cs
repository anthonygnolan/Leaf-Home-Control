using Leaf.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Models
{
    public class Home : ObservableObject
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Version { get; set; }

        public string OwnerId { get; set; }

        public string UserId { get; set; }

        public string AccountId { get; set; }

        public string Provider { get; set; } = "Microsoft";

        public string HomeId { get; set; }

        public bool HasAccess { get; set; } = true;

        public string Name { get; set; } = "My Home";

        public bool IsPrimary { get; set; } = false;

        public string BackgroundUri { get; set; } = "ms-appx:///Leaf.Shared/Images/DefaultHomeBackground.jpg";

        public double BackgroundOpacity { get; set; } = 0.5;

        public int BackgroundBlur { get; set; } = 20;

        public bool IsLocationAvailable { get; set; } = false;

        public string Location { get; set; }

        public string MenuItems { get; set; } = @"{""RoomsVisibility"":true,""LightingVisibility"":false}";

        public string UserName { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
