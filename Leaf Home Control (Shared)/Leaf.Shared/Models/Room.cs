using Leaf.Shared.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Models
{
    public class Room : ObservableObject
    {

        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Version { get; set; }

        public string OwnerId { get; set; }

        public string UserId { get; set; }

        public bool HasAccess { get; set; }

        public string HomeId { get; set; }

        public string RoomId { get; set; }

        public string Name { get; set; } = "My Home";

        public string BackgroundUri { get; set; } = "ms-appx:///Leaf.Shared/Images/DefaultRoomBackground.jpg";

        public double BackgroundOpacity { get; set; } = 0.5;

        public int BackgroundBlur { get; set; } = 20;

        private bool _isSelected { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public ObservableCollection<Device> Devices = new ObservableCollection<Device>();

        public bool Deleted { get; set; } = false;
    }
}
