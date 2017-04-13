using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework.configuration;
using System.Threading;

namespace inSyca.foundation.framework.wsh
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
                    case "fs":
                        io.FileSystem fileSystem = new io.FileSystem();
                        fileSystem.Process();
                        Thread.Sleep(1000);
                        return;
                    default:
                        break;
                }
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new FrameworkService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
