using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.diagnostics;
using System.Collections.Generic;
using System.IO;

namespace inSyca.foundation.integration.biztalk.management
{
    public class Management
    {
        private List<Attachment> Attachments;
        private data.dsManagementConfig dsManagementConfig;

        public Management()
        {
            Attachments = new List<Attachment>();
            dsManagementConfig = new data.dsManagementConfig();
        }

        public void Process()
        {
            Log.Debug("Process()");

            dsManagementConfig = new data.dsManagementConfig();
            dsManagementConfig.ReadXml(Path.Combine(new FileInfo(Configuration.ConfigFileInfo).DirectoryName, "foundation.integration.biztalk.xml"));

            if (dsManagementConfig.Entry != null)
            {
                foreach (data.dsManagementConfig.EntryRow oEntryRow in dsManagementConfig.Entry.Rows)
                {
                    Log.DebugFormat("Process()\nServername: {0}\nObject: {1}\nObjectname: {2}\nAction: {3}", oEntryRow.Servername, oEntryRow.Object, oEntryRow.Objectname, oEntryRow.Action);

                    switch (oEntryRow.Object.ToLower())
                    {
                        case "host":
                            Host(oEntryRow);
                            break;
                        case "hostinstance":
                            HostInstance(oEntryRow);
                            break;
                        case "receivelocation":
                            ReceiveLocation(oEntryRow);
                            break;
                        case "sendport":
                            SendPort(oEntryRow);
                            break;
                        case "sendportgroup": 
                            SendPortGroup(oEntryRow);
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private void SendPortGroup(data.dsManagementConfig.EntryRow oEntryRow)
        {
            Log.DebugFormat("SendPortGroup(data.dsManagementConfig.EntryRow oEntryRow {0})\nServername: {1}\nObjectname: {2}\nAction: {3}", oEntryRow, oEntryRow.Servername, oEntryRow.Objectname, oEntryRow.Action);

            SendPortGroup sendPortGroup = new SendPortGroup(oEntryRow.Servername, oEntryRow.Objectname);

            switch (oEntryRow.Action.ToLower())
            {
                case "enlist":
                    sendPortGroup.Enlist();
                    break;
                case "start":
                    sendPortGroup.Start();
                    break;
                case "stop":
                    sendPortGroup.Stop();
                    break;
                case "unenlist":
                    sendPortGroup.Unenlist();
                    break;
            }
        }

        private void SendPort(data.dsManagementConfig.EntryRow oEntryRow)
        {
            Log.DebugFormat("SendPort(data.dsManagementConfig.EntryRow oEntryRow {0})\nServername: {1}\nObjectname: {2}\nAction: {3}", oEntryRow, oEntryRow.Servername, oEntryRow.Objectname, oEntryRow.Action);

            SendPort sendPort = new SendPort(oEntryRow.Servername, oEntryRow.Objectname);

            switch (oEntryRow.Action.ToLower())
            {
                case "enlist":
                    sendPort.Enlist();
                    break;
                case "start":
                    sendPort.Start();
                    break;
                case "stop":
                    sendPort.Stop();
                    break;
                case "unenlist":
                    sendPort.Unenlist();
                    break;
            }
        }

        private void ReceiveLocation(data.dsManagementConfig.EntryRow oEntryRow)
        {
            Log.DebugFormat("ReceiveLocation(data.dsManagementConfig.EntryRow oEntryRow {0})\nServername: {1}\nObjectname: {2}\nAction: {3}", oEntryRow, oEntryRow.Servername, oEntryRow.Objectname, oEntryRow.Action);

            ReceiveLocation receiveLocation = new ReceiveLocation(oEntryRow.Servername, oEntryRow.Objectname);

            switch (oEntryRow.Action.ToLower())
            {
                case "enable":
                    receiveLocation.Enable();
                    break;
                case "disable":
                    receiveLocation.Disable();
                    break;
            }
        }

        private void HostInstance(data.dsManagementConfig.EntryRow oEntryRow)
        {
            Log.DebugFormat("HostInstance(data.dsManagementConfig.EntryRow oEntryRow {0})\nServername: {1}\nObjectname: {2}\nAction: {3}", oEntryRow, oEntryRow.Servername, oEntryRow.Objectname, oEntryRow.Action);

            HostInstance hostInstance = new HostInstance(oEntryRow.Servername, oEntryRow.Objectname);

            switch (oEntryRow.Action.ToLower())
            {
                case "start":
                    hostInstance.Start();
                    break;
                case "stop":
                    hostInstance.Stop();
                    break;
            }
        }

        private void Host(data.dsManagementConfig.EntryRow oEntryRow)
        {
            Log.DebugFormat("Host(data.dsManagementConfig.EntryRow oEntryRow {0})\nServername: {1}\nObjectname: {2}\nAction: {3}", oEntryRow, oEntryRow.Servername, oEntryRow.Objectname, oEntryRow.Action);

            Host host = new Host(oEntryRow.Servername, oEntryRow.Objectname);

            switch (oEntryRow.Action.ToLower())
            {
                case "start":
                    host.Start();
                    break;
                case "stop":
                    host.Stop();
                    break;
            }
        }
    }
}
