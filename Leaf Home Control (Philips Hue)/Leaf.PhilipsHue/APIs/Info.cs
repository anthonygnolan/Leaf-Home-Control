using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Info
    {
        public void GetAllTimeZones()
        {
            /// URL: /api/<username>/info/timezones
            /// Method: GET
            /// Version: 1.2.1  Depreciated as of v1.15.0 - use /api/<username>/capabilities/timezones
            /// Permission: Whitelist
        }
    }
}
