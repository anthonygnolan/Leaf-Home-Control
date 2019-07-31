using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Rule
    {
        public string name { get; set; }
        public string lasttriggered { get; set; }
        public string creationtime { get; set; }
        public int timestriggered { get; set; }
        public string owner { get; set; }
        public string status { get; set; }
        public List<Condition> conditions { get; set; }
        public List<RuleAction> actions { get; set; }

    }

    public class Condition
    {
        public string address { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }

    public class RuleAction
    {
        public string address { get; set; }
        public string method { get; set; }
        public string body { get; set; }
    }
}
