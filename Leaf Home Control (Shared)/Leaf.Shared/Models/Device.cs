using Leaf.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Models
{
    public class Device : ObservableObject
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Version { get; set; }

        public string OwnerId { get; set; }

        public string UserId { get; set; }

        public bool Deleted { get; set; } = false;

        public string AccountId { get; set; }

        public string Provider { get; set; } = "Microsoft";

        public string HomeId { get; set; }

        public string RoomId { get; set; }

        public bool HasAccess { get; set; } = true;

        public string Name { get; set; } = "My Device";

        private string _room { get; set; }

        public string Room
        {
            get { return _room; }
            set
            {
                _room = value;
                this.OnPropertyChanged("Room");
            }
        }

        private bool _status { get; set; }

        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                this.OnPropertyChanged("Status");
            }
        }

        private string _level { get; set; }

        public string Level
        {
            get { return _level; }
            set
            {
                _level = value;
                this.OnPropertyChanged("Level");
            }
        }

        public string DeviceId { get; set; }
    }
}
