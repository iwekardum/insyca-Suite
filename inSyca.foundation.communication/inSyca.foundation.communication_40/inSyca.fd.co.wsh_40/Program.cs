using inSyca.foundation.framework;
using System.ServiceProcess;

namespace inSyca.foundation.communication.wsh
{
    class Program
    {
        internal static string serviceName = "inSyca.messagebroker";
    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.GetLength(0) > 0)
            {
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new CommunicationService() 
			};
            ServiceBase.Run(ServicesToRun);

            Log.InfoFormat("Main(string[] args {0})\ninSyca.foundation.communication.wsh started", args);
        }
    }
}
