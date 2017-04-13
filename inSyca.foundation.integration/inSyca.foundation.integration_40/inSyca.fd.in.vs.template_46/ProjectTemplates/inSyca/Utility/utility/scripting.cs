using inSyca.foundation.framework;

namespace inSyca.utility.ns
{
    public class scripting
    {
        public static string transformStatus(string status)
        {
            //inSyca Logging
            Log.DebugFormat("transformStatus(string status {0})", status);

            switch (status)
            {
                case "Invoiced":
                case "Closed":
                    return "3";
                case "Canceled":
                    return "9";
                default:
                    return "1";
            }
        }
    }
}
