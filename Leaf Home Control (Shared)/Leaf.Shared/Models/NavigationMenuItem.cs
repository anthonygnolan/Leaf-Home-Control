using Leaf.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Models
{
    public class NavigationMenuItem : ObservableObject
    {
        public string Label { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }
        public Type DestinationPage { get; set; }
        public string Arguments { get; set; }
    }
}
