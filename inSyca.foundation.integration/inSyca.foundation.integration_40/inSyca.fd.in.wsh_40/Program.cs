using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk;
using inSyca.foundation.integration.wsh.diagnostics;

namespace inSyca.foundation.integration.wsh
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Log.InfoFormat("Main(string[] args {0}\nApplication started)", args);

            if (args.GetLength(0) > 0)
            {
                switch (args[0].ToLower())
                {
                    case "mgmt":
                        biztalk.management.Management management = new biztalk.management.Management();
                        management.Process();
                        return;
                }
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new IntegrationService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
