using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Bridge
    {
        public List<Light> lights { get; set; }
        public List<Group> groups { get; set; }
        public Config config { get; set; }
        public List<Schedule> schedules { get; set; }
        public List<Scene> scenes { get; set; }
        public List<Sensor> sensors { get; set; }
        public List<Rule> rules { get; set; }
    }
}
