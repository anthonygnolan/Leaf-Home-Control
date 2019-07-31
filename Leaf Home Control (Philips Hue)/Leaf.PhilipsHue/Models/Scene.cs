using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Scene
    {
        public string name { get; set; }
        public List<int> lights { get; set; }
        public string owner { get; set; }
        public bool recycle { get; set; }
        public bool locked { get; set; }
        public AppData appdata { get; set; }
        public string picture { get; set; }
        public string lastupdated { get; set; }
        public int version { get; set; }
        public List<SceneLightState> lightstates { get; set; }
    }

    public class AppData
    {
        public int version { get; set; }
        public string data { get; set; }
    }

    public class SceneLightState
    {
        public bool on { get; set; }
        public int bri { get; set; }
        public List<double> xy { get; set; }
    }
}
