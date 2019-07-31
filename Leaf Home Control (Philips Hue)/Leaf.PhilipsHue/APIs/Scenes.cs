using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Scenes
    {
        public void GetAllScenes()
        {
            /// URL: /api/<username>/scenes
            /// Method: GET
            /// Version: 1.1
            /// Permission: Whitelist
        }

        public void CreateScene()
        {
            /// URL: /api/<username>/scenes/
            /// Method: POST
            /// Version: 1.11
            /// Permission: Whitelist
        }

        public void ModifyScene()
        {
            /// URL: /api/<username>/scenes/<id>/lightstates/<id>
            /// Method: PUT
            /// Version: 1.11
            /// Permission: Whitelist

            /// URL: /api/<username>/scenes/<id>/lights/<id>/state
            /// Method: PUT
            /// Version: 1.1.1 Deprecated as for 1.11
            /// Permission: Whitelist

            /// URL: /api/<username>/scenes/<id>
            /// Method: PUT
            /// Version: 1.1.0
            /// Permission: Whitelist
        }

        public void RecallScene()
        {
            /// To recall an existing scene you use the Groups API
        }

        public void DeleteScene()
        {
            /// URL: /api/<username>/scenes/<id>
            /// Method: DELETE
            /// Version: 1.11
            /// Permission: Whitelist
        }

        public void GetScene()
        {
            /// URL: /api/<username>/scenes/<id>
            /// Method: DELETE
            /// Version: 1.11
            /// Permission: Whitelist
        }
    }
}
