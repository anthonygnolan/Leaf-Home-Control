using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Group
    {
        public GroupAction action { get; set; }
        public string[] lights { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string modelid { get; set; }
        public string uniqueid { get; set; }
        public string Class { get; set; }
    }

    public class GroupAction
    {
        public bool on { get; set; }
        public int bri { get; set; }
        public int hue { get; set; }
        public int sat { get; set; }
        public decimal[] xy { get; set; }
        public int ct { get; set; }
        public string alert { get; set; }
        public string effect { get; set; }
        public string colormode { get; set; }
    }
}
