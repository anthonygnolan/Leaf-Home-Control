using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Groups
    {
        public void GetAllGroups()
        {
            /// URL: /api/<username>/groups
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void CreateGroup()
        {
            /// URL: /api/<username>/groups
            /// Method: POST
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void GetGroupAttributes()
        {
            /// URL: /api/<username>/groups/<id>
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void SetGroupAttributes()
        {
            /// URL: /api/<username>/groups/<id>
            /// Method: PUT
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void SetGroupState()
        {
            /// URL: /api/<username>/groups/<id>/action
            /// Method: PUT
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void DeleteGroup()
        {
            /// URL: /api/<username>/groups/<id>
            /// Method: DELETE
            /// Version: 1.0
            /// Permission: Whitelist
        }
    }
}
