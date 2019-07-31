using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Models
{
    public class Light : Device
    {
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
    }
}
