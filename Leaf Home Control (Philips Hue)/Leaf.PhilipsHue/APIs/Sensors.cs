using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Sensors
    {
        public void GetAllSensors()
        {
            /// URL: /api/<username>/sensors
            /// Method: GET
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void CreateSensor()
        {
            /// URL: /api/<username>/sensors
            /// Method: POST
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void FindNewSensors()
        {
            /// URL: /api/<username>/sensors
            /// Method: POST
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void GetNewSensors()
        {
            /// URL: /api/<username>/sensors/new
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void GetSensor()
        {
            /// URL: /api/<username>/sensors/<id>
            /// Method: GET
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void UpdateSensor()
        {
            /// URL: /api/<username>/sensors/<id>
            /// Method: PUT
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void DeleteSensor()
        {
            /// URL: /api/<username>/sensors/<id>
            /// Method: DELETE
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void ChangeSensorConfig()
        {
            /// URL: /api/<username>/sensors/<id>/config
            /// Method: PUT
            /// Version: 1.3
            /// Permission: Whitelist
        }

        public void ChangeSensorState()
        {
            /// URL: /api/<username>/sensors/<id>/state
            /// Method: PUT
            /// Version: 1.3
            /// Permission: Whitelist
        }
    }
}
