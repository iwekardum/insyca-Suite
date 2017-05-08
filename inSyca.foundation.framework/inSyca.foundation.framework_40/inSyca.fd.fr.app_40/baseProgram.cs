using inSyca.foundation.framework.io;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace inSyca.foundation.framework.application
{
    public static class Program
    {
        public static bool Uninstall()
        {
            string[] arguments = Environment.GetCommandLineArgs();

            foreach (string argument in arguments)
            {
                if (argument.Split('=')[0].ToLower() == "/u")
                {
                    string guid = argument.Split('=')[1];
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(path + @"\msiexec.exe", "/x " + guid);
                    Process.Start(processStartInfo);
                    Application.Exit();
                    return true;
                }
            }
            return false;
        }

        public static bool UnattendedMode()
        {
            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.GetLength(0) == 1)
                return false;

            int iFunctionNumber;

            string strInput = "0";
            string strParams = string.Empty;

            foreach (string arg in arguments)
                strParams = string.Format("{0} {1}", strParams, arg);

            if (arguments.GetLength(0) > 1)
            {
                Log.Debug("UnattendedMode()");

                try
                {
                    if (arguments[1] == "?")
                    {
                        //Help();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                    Console.WriteLine("Ungültiger Parameter. Erlaubte Werte: 0, 1 oder ?");
                    Console.WriteLine("+++ {0} +++", ex.Message);
                    Console.WriteLine("");
                    return false;
                }
            }

            if (arguments.GetLength(0) > 2)
            {
                try
                {
                    iFunctionNumber = Convert.ToInt32(arguments[1]);

                    if (iFunctionNumber < 0 && iFunctionNumber > 1)
                        throw new Exception("Undefined Value");

                    strInput = arguments[2];
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                    Console.WriteLine("Ungültiger Parameter 2. Erlaubte Werte: 0 oder 1");
                    Console.WriteLine("+++ {0} +++", ex.Message);
                    Console.WriteLine("");
                    return false;
                }
            }

            switch (strInput)
            {
                //case "1":
                //    RenameAllFilesInDirectory(ref strInput, args);
                //    break;
                case "2":
                        FileSystem fileSystem = new FileSystem();
                        fileSystem.Process();
                        return true;
                //case "3":
                //    MoveFilesInDirectory(ref strInput, args);
                //    break;
                //case "9":
                //    //CreateEventLogSource(ref strInput, string.Empty);
                //    break;
                default:
                    break;
            }

            Log.Info("UnattendedMode()\nApplication closed");

            return false;
        }
    }
}
