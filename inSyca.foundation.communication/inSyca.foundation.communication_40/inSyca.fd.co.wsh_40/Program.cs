using System.ServiceProcess;

namespace inSyca.foundation.communication.wsh
{
    class Program
    {
        internal static string serviceName = "inSyca Communication Service";
    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string logString = "Main(string[] args)\nArguments:\n";

            if (args.Length < 1)
                logString += "none";
            else
                foreach (var argument in args)
                    logString += string.Format("{0}\n", argument);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new CommunicationService() 
			};
            ServiceBase.Run(ServicesToRun);

            Log.InfoFormat("inSyca.foundation.communication.wsh started\n{0}", logString);
        }
    }
}
