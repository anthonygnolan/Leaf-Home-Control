using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Models
{
    public class Config
    {
        public string name { get; set; }
        public int zigbeechannel { get; set; }
        public string bridgeid { get; set; }
        public string mac { get; set; }
        public bool dhcp { get; set; }
        public string ipaddress { get; set; }
        public string netmask { get; set; }
        public string gateway { get; set; }
        public string proxyaddress { get; set; }
        public int proxyport { get; set; }
        public string UTC { get; set; }
        public string localtime { get; set; }
        public string timezone { get; set; }
        public string modelid { get; set; }
        public string datastoreversion { get; set; }
        public string swversion { get; set; }
        public string apiversion { get; set; }
        public Swupdate swupdate { get; set; }
        public bool linkbutton { get; set; }
        public bool portalservices { get; set; }
        public string portalconnection { get; set; }
        public Portalstate portalstate { get; set; }
        public bool factorynew { get; set; }
        public object replacesbridgeid { get; set; }
        public BackUp backup { get; set; }
        public Whitelist whitelist { get; set; }
    }

    public class Whitelist
    {
        public List<string> whitelist { get; set; }
    }

    public class User
    {
        public string lastusedate { get; set; }
        public string createdate { get; set; }
        public string name { get; set; }
    }

    public class Swupdate
    {
        public int updatestate { get; set; }
        public bool checkforupdate { get; set; }
        public Devicetypes devicetypes { get; set; }
        public string url { get; set; }
        public string text { get; set; }
        public bool notify { get; set; }
    }

    public class Devicetypes
    {
        public bool bridge { get; set; }
        public List<object> lights { get; set; }
        public List<object> sensors { get; set; }
    }

    public class Portalstate
    {
        public bool signedon { get; set; }
        public bool incoming { get; set; }
        public bool outgoing { get; set; }
        public string communication { get; set; }
    }

    public class BackUp
    {
        public string status { get; set; }
        public int errorcode { get; set; }
    }
}
