using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Sensor
    {
        public string name { get; set; }
        public string modelid { get; set; }
        public string swversion { get; set; }
        public string type { get; set; }
        public string uniqueid { get; set; }
        public string manufacturername { get; set; }
        public State1 state { get; set; }
        public config config { get; set; }
        public bool recycle { get; set; }
    }

    public class State1
    {
        public bool presence { get; set; }
        public int buttonevent { get; set; }
        public string lastupdated { get; set; }
    }
    public class config
    {
        public bool on { get; set; }
        public bool reachable { get; set; }
        public int battery { get; set; }
    }
}
