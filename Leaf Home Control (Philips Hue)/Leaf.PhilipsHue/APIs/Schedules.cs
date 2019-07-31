using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.APIs
{
    public class Schedules
    {
        public void GetAllSchedules()
        {
            /// URL: /api/<username>/schedules
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void CreateSchedule()
        {
            /// URL: /api/<username>/schedules
            /// Method: POST
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void GetScheduleAttributes()
        {
            /// URL: /api/<username>/schedules/<id>
            /// Method: GET
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void SetScheduleAttributes()
        {
            /// URL: /api/<username>/schedules/<id>
            /// Method: PUT
            /// Version: 1.0
            /// Permission: Whitelist
        }

        public void DeleteSchedule()
        {
            /// URL: /api/<username>/schedules/<id>
            /// Method: DELETE
            /// Version: 1.0
            /// Permission: Whitelist
        }
    }
}
