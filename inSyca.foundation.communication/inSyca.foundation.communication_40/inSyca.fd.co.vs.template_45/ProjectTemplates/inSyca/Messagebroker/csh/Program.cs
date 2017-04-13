using inSyca.foundation.framework;
using System;
using System.ServiceModel;

namespace inSyca.messagebroker.ns
{
    public class Program
    {
        public ServiceHost serviceHost = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Log.Info("inSyca.messagebroker.ns started...(Warm-up)");

            Program oProgramm = new Program();

            oProgramm.Start();

            Log.Info("inSyca.messagebroker.ns ready");

            Log.Info("Press <ENTER> to terminate service.");
            Console.ReadLine();

            oProgramm.Stop();
        }

        public void Start()
        {
            if (serviceHost != null)
            {
                Log.Info("inSyca.messagebroker.ns already initialized - close and re-initialize");

                serviceHost.Close();
            }

            // Create a ServiceHost and 
            // provide the base address.
            serviceHost = new inSyca.messagebroker.root.ns.svc.MessageBrokerServiceHost();

            try
            {
                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                serviceHost.Open();
                Log.Info("inSyca.messagebroker.ns ready");
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                throw ex;
            }
        }

        public void Stop()
        {
            if (serviceHost != null)
            {
                try
                {
                    serviceHost.Close();
                    Log.Info("inSyca.messagebroker.ns closed");
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                    throw ex;
                }

                serviceHost = null;
            }
        }
    }
}
