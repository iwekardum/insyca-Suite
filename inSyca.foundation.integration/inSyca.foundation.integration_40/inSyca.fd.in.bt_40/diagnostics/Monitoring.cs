using System;
using System.Management;
using inSyca.foundation.framework;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework.data;
using Microsoft.BizTalk.Operations;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace inSyca.foundation.integration.biztalk.diagnostics
{
    public static class Monitoring
    {
        static public event EventHandler<MonitoringEventArgs> MonitoringEvent;
        static public event EventHandler<MonitoringEventArgs> EventEntryEvent;

        static private List<ManagementEventWatcher> hostInstanceWatcherList;
        static private List<ManagementEventWatcher> serviceInstanceSuspendedWatcherList;
        static private List<ManagementEventWatcher> receiveLocationWatcherList;
        static private List<ManagementEventWatcher> sendPortWatcherList;

        static private MonitorBizTalk monitorBizTalk;

        public static void disposeWatcher()
        {
            Log.Debug("disposeWatcher()");

            if (hostInstanceWatcherList != null)
            {
                foreach (ManagementEventWatcher hostInstanceWatcher in hostInstanceWatcherList)
                {
                    hostInstanceWatcher.Stop();
                    hostInstanceWatcher.Dispose();
                }
                hostInstanceWatcherList.Clear();
            }

            if (serviceInstanceSuspendedWatcherList != null)
            {
                foreach (ManagementEventWatcher serviceInstanceSuspendedWatcher in serviceInstanceSuspendedWatcherList)
                {
                    serviceInstanceSuspendedWatcher.Stop();
                    serviceInstanceSuspendedWatcher.Dispose();
                }
                serviceInstanceSuspendedWatcherList.Clear();
            }

            if (receiveLocationWatcherList != null)
            {
                foreach (ManagementEventWatcher receiveLocationWatcher in receiveLocationWatcherList)
                {
                    receiveLocationWatcher.Stop();
                    receiveLocationWatcher.Dispose();
                }
                receiveLocationWatcherList.Clear();
            }

            if (sendPortWatcherList != null)
            {
                foreach (ManagementEventWatcher sendPortWatcher in sendPortWatcherList)
                {
                    sendPortWatcher.Stop();
                    sendPortWatcher.Dispose();
                }
                sendPortWatcherList.Clear();
            }
        }

        public static void invokeWatcher()
        {
            Log.Debug("invokeWatcher()");

            monitorBizTalk = new MonitorBizTalk();
            monitorBizTalk.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorBizTalk_MonitoringEvent);

            string[] serverCollection = Configuration.GetTextAppSettingsValue("LogServerNames").Split(';');

            if (serverCollection.GetLength(0) == 0)
                return;

            hostInstanceWatcherList = new List<ManagementEventWatcher>();
            serviceInstanceSuspendedWatcherList = new List<ManagementEventWatcher>();
            receiveLocationWatcherList = new List<ManagementEventWatcher>();
            sendPortWatcherList = new List<ManagementEventWatcher>();

            foreach (string server in serverCollection)
            {
               Log.InfoFormat("Setup watcher for server: {0}", server );

                MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Start WMI for Server: " + server));

                if (string.IsNullOrEmpty(server))
                    return;

                ManagementPath managementPathBizTalk = new ManagementPath(string.Format(@"\\{0}\root\MicrosoftBizTalkServer", server));
                ConnectionOptions managementConnectionOptions = new ConnectionOptions();

                managementConnectionOptions.Authentication = AuthenticationLevel.Packet;
                managementConnectionOptions.Timeout = new TimeSpan(0, 0, 5, 0);
                managementConnectionOptions.EnablePrivileges = true;

                if (server.ToUpper() != Dns.GetHostName().ToUpper() && server.ToUpper() != "LOCALHOST")
                {
                    Log.InfoFormat("Applied user credentials for remote host: {0}", server );

                    //managementConnectionOptions.Username = "USERNAME";
                    //managementConnectionOptions.Password = "PASSWORD";
                }

                ManagementEventWatcher hostInstanceWatcher = null;

                try
                {
                    WqlEventQuery hostInstanceQuery = new WqlEventQuery("__InstanceModificationEvent", new TimeSpan(0, 0, 10), "TargetInstance ISA \"MSBTS_HostInstance\"");
                    hostInstanceWatcher = new ManagementEventWatcher(new ManagementScope(managementPathBizTalk, managementConnectionOptions), hostInstanceQuery);
                    hostInstanceWatcher.EventArrived += new EventArrivedEventHandler(hostInstanceEventHandler);
                    hostInstanceWatcher.Start();
                    hostInstanceWatcherList.Add(hostInstanceWatcher);

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("hostInstanceWatcher started. Query:" + hostInstanceQuery.QueryString));

                    Log.InfoFormat("hostInstanceWatcher started. Query: {0}", hostInstanceQuery.QueryString );
                }
                catch (Exception ex)
                {
                    if (hostInstanceWatcher != null)
                    {
                        hostInstanceWatcher.Stop();
                        hostInstanceWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (hostInstanceWatcher): " + ex.Message));

                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (hostInstanceWatcher): {0}", new object[] { ex.Message }));
                }

                ManagementEventWatcher serviceInstanceSuspendedWatcher = null;

                try
                {
                    WqlEventQuery serviceInstanceSuspendedQuery = new WqlEventQuery(String.Format(@"SELECT * FROM MSBTS_ServiceInstanceSuspendedEvent", "5"));
                    serviceInstanceSuspendedWatcher = new ManagementEventWatcher(new ManagementScope(managementPathBizTalk, managementConnectionOptions), serviceInstanceSuspendedQuery);
                    serviceInstanceSuspendedWatcher.EventArrived += new EventArrivedEventHandler(suspendedMessageEventHandler);
                    serviceInstanceSuspendedWatcher.Start();
                    serviceInstanceSuspendedWatcherList.Add(serviceInstanceSuspendedWatcher);

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("serviceInstanceSuspendedWatcher started. Query:" + serviceInstanceSuspendedQuery.QueryString));

                    Log.InfoFormat("serviceInstanceSuspendedWatcher started. Query: {0}", serviceInstanceSuspendedQuery.QueryString );
                }
                catch (Exception ex)
                {
                    if (serviceInstanceSuspendedWatcher != null)
                    {
                        serviceInstanceSuspendedWatcher.Stop();
                        serviceInstanceSuspendedWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (serviceInstanceSuspendedWatcher): " + ex.Message));

                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (serviceInstanceSuspendedWatcher): {0}", new object[] { ex.Message }));
                }

                ManagementEventWatcher receiveLocationWatcher = null;

                try
                {
                    WqlEventQuery receiveLocationQuery = new WqlEventQuery("__InstanceModificationEvent", new TimeSpan(0, 0, 10), "TargetInstance ISA \"MSBTS_ReceiveLocation\"");
                    receiveLocationWatcher = new ManagementEventWatcher(new ManagementScope(managementPathBizTalk, managementConnectionOptions), receiveLocationQuery);
                    receiveLocationWatcher.EventArrived += new EventArrivedEventHandler(receiveLocationEventHandler);
                    receiveLocationWatcher.Start();
                    receiveLocationWatcherList.Add(receiveLocationWatcher);

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("receiveLocationWatcher started. Query:" + receiveLocationQuery.QueryString));

                    Log.InfoFormat("receiveLocationWatcher started. Query: {0}",  receiveLocationQuery.QueryString );
                }
                catch (Exception ex)
                {
                    if (receiveLocationWatcher != null)
                    {
                        receiveLocationWatcher.Stop();
                        receiveLocationWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (receiveLocationWatcher): " + ex.Message));

                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (receiveLocationWatcher): {0}", new object[] { ex.Message }));
                }

                ManagementEventWatcher sendPortWatcher = null;

                try
                {
                    WqlEventQuery sendPortQuery = new WqlEventQuery("__InstanceModificationEvent", new TimeSpan(0, 0, 10), "TargetInstance ISA \"MSBTS_SendPort\"");
                    sendPortWatcher = new ManagementEventWatcher(new ManagementScope(managementPathBizTalk, managementConnectionOptions), sendPortQuery);
                    sendPortWatcher.EventArrived += new EventArrivedEventHandler(sendPortEventHandler);
                    sendPortWatcher.Start();
                    sendPortWatcherList.Add(sendPortWatcher);

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("sendPortWatcher started. Query:" + sendPortQuery.QueryString));

                    Log.InfoFormat("sendPortWatcher started. Query: {0}", sendPortQuery.QueryString );
                }
                catch (Exception ex)
                {
                    if (sendPortWatcher != null)
                    {
                        sendPortWatcher.Stop();
                        sendPortWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (sendPortWatcher): " + ex.Message));

                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (sendPortWatcher): {0}", new object[] { ex.Message }));
                }

               Log.DebugFormat("Setup done - Server{0}", server );
            }
        }

        static public void hostInstanceEventHandler(object sender, EventArrivedEventArgs e)
        {
           Log.DebugFormat("hostInstanceEventHandler(object sender {0}, EventArrivedEventArgs e {1})", sender, e );

            //MonitorBizTalk monitorBizTalk = new MonitorBizTalk();
            //monitorBizTalk.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorBizTalk_MonitoringEvent);
            DataRow eventEntryRow;
            monitorBizTalk.SetEvent(e, BizTalkEventType.hostInstanceEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static public void receiveLocationEventHandler(object sender, EventArrivedEventArgs e)
        {
           Log.DebugFormat("receiveLocationEventHandler(object sender {0}, EventArrivedEventArgs e {1})", sender, e );

            //MonitorBizTalk monitorBizTalk = new MonitorBizTalk();
            //monitorBizTalk.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorBizTalk_MonitoringEvent);
            DataRow eventEntryRow;
            monitorBizTalk.SetEvent(e, BizTalkEventType.receiveLocationEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static public void sendPortEventHandler(object sender, EventArrivedEventArgs e)
        {
            Log.DebugFormat("sendPortEventHandler(object sender {0}, EventArrivedEventArgs e {1})", sender, e);

            //MonitorBizTalk monitorBizTalk = new MonitorBizTalk();
            //monitorBizTalk.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorBizTalk_MonitoringEvent);
            DataRow eventEntryRow;
            monitorBizTalk.SetEvent(e, BizTalkEventType.sendPortEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static public void suspendedMessageEventHandler(object sender, EventArrivedEventArgs e)
        {
            Log.DebugFormat("suspendedMessageEventHandler(object sender {0}, EventArrivedEventArgs e {1})", sender, e);

            //MonitorBizTalk monitorBizTalk = new MonitorBizTalk();
            //monitorBizTalk.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorBizTalk_MonitoringEvent);
            DataRow eventEntryRow;
            monitorBizTalk.SetEvent(e, BizTalkEventType.suspendedMessageEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static void monitorBizTalk_MonitoringEvent(object sender, MonitoringEventArgs e)
        {
            MonitoringEvent(null, e);
        }
    }
}
