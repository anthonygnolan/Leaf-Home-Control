using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Leaf.Shared.Models
{
    public class Account : Home
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

        private Uri _imageSource { get; set; }
        public Uri ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        private BitmapImage _accountPicture { get; set; }
        public BitmapImage AccountPicture
        {
            get { return _accountPicture; }
            set
            {
                _accountPicture = value;
                this.OnPropertyChanged("AccountImage");
            }
        }
    }
}
