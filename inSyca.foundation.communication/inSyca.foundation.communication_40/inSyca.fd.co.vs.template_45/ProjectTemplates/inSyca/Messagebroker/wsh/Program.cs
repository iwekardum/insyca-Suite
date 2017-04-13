using inSyca.foundation.framework;
using System.ServiceProcess;
using System.ServiceModel;
using System;

namespace inSyca.messagebroker.ns
{
    class Program
    {
        internal static string serviceName = "inSyca.messagebroker.ns";

        static void Main(string[] args)
        {
#if (!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
                { 
                    new MessageBroker() 
                };
            ServiceBase.Run(ServicesToRun);
#else
            // Debug code: this allows the process to run as a non-service.
            // It will kick off the service start point, but never kill it.
            // Shut down the debugger to exit

            Log.Info("inSyca.messagebroker.ns started in DEBUG mode");
            ServiceHost messageBrokerServiceHost = new inSyca.messagebroker.root.ns.svc.MessageBrokerServiceHost();

            try
            {
                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                messageBrokerServiceHost.Open();

                Log.Info("inSyca.messagebroker.ns ready");
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                throw ex;
            }

            // Put a breakpoint on the following line to always catch
            // your service when it has finished its work
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

            Log.Info("inSyca.messagebroker.ns shutdown");
#endif

        }
    }
}
