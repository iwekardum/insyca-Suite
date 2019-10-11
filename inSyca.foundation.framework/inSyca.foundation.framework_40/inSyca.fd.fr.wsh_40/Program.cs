using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace inSyca.foundation.framework.wsh
{
    static class Program
    {
        internal static string serviceName = "inSyca Framework Service";

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

            Log.InfoFormat("inSyca.foundation.framework.wsh started\n{0}", logString);
#if (!DEBUG)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new FrameworkService() 
			};
            ServiceBase.Run(ServicesToRun);

            Log.WarnFormat("inSyca.foundation.framework.wsh stopped\n{0}", logString);

#else
            // Debug code: this allows the process to run as a non-service.
            // It will kick off the service start point, but never kill it.
            // Shut down the debugger to exit
            Log.Error("inSyca.foundation.framework.wsh started in DEBUG mode");

            try
            {

                FrameworkService service = new FrameworkService();

                service.Start();

                Log.InfoFormat("inSyca.foundation.framework.wsh ready\n{0}", logString);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { }, ex));
                throw ex;
            }

            // Put a breakpoint on the following line to always catch
            // your service when it has finished its work
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

            Log.InfoFormat("inSyca.foundation.framework.wsh shutdown\n{0}", logString);
#endif
        }
    }
}
