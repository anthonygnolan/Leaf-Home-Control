using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHue.Helpers
{
    public class ErrorHelper
    {
        public static bool CheckForError(string response, bool success)
        {
            if (response.Contains("success"))
            {
                success = true;
            }
            else
            {
                FindError(response);
            }
            return success;
        }

        public static void FindError(string error)
        {

        }
    }
}
