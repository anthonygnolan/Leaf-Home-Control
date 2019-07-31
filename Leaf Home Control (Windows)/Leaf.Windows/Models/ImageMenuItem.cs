using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Leaf.Windows.Models
{
    public class ImageMenuItem : INotifyPropertyChanged
    {
        public string _accountName { get; set; }
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                this.OnPropertyChanged("AccountName");
            }
        }

        private ImageBrush _image { get; set; }
        public ImageBrush Image
        {
            get { return _image; }
            set
            {
                _image = value;
                this.OnPropertyChanged("Image");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SelectedVis = value ? Visibility.Visible : Visibility.Collapsed;
                this.OnPropertyChanged("IsSelected");
            }
        }

        private Visibility _selectedVis = Visibility.Collapsed;
        public Visibility SelectedVis
        {
            get { return _selectedVis; }
            set
            {
                _selectedVis = value;
                this.OnPropertyChanged("SelectedVis");
            }
        }

        private bool _hasAccess;
        public bool HasAccess
        {
            get { return _hasAccess; }
            set
            {
                _hasAccess = value;
                this.OnPropertyChanged("HasAccess");
            }
        }
        public Type DestPage { get; set; }
        public string Arguments { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged(string propertyName)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
