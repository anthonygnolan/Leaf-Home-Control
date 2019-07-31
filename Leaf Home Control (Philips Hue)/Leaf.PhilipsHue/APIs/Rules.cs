using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Rules
    {
        public void GetAllRules()
        {
            /// URL: /api/<username>/rules
            /// Method: GET
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void GetRule()
        {
            /// URL: /api/<username>/rules/<id>
            /// Method: GET
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void CreateRule()
        {
            /// URL: /api/<username>/rules
            /// Method: POST
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void UpdateRule()
        {
            /// URL: /api/<username>/rules/<id>
            /// Method: PUT
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void DeleteRule()
        {
            /// URL: /api/<username>/rules/<id>
            /// Method: DELETE
            /// Version: 1.3
            /// Permission: Whitelist
        }
    }
}
