using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Devices
{
    public class Hue
    {
        public static async Task<string[]> FindBridge()
        {
            var t = await PhilipsHue.Helpers.BridgeHelper.FindBridgeAsync();
            if (t != null)
            {
                string[] result = new string[2];
                result[0] = t.name;
                result[1] = t.ipaddress;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
