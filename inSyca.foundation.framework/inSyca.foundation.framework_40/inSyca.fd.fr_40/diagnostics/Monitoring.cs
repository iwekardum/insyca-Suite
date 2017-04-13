using System;
using System.Management;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.IO;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.data;

namespace inSyca.foundation.framework.diagnostics
{
    public static class Monitoring
    {
        static public event EventHandler<MonitoringEventArgs> MonitoringEvent;
        static public event EventHandler<MonitoringEventArgs> EventEntryEvent;

        static private List<ManagementEventWatcher> eventLogApplicationWatcherList;
        static private List<ManagementEventWatcher> eventLogSystemWatcherList;
        static private List<ManagementEventWatcher> eventLogSecurityWatcherList;

        public static void disposeWatcher()
        {
            Log.Debug("disposeWatcher()");

            if (eventLogApplicationWatcherList != null)
            {
                foreach (ManagementEventWatcher eventLogApplicationWatcher in eventLogApplicationWatcherList)
                {
                    eventLogApplicationWatcher.Stop();
                    eventLogApplicationWatcher.Dispose();
                }
                eventLogApplicationWatcherList.Clear();
            }

            if (eventLogSystemWatcherList != null)
            {
                foreach (ManagementEventWatcher eventLogSystemWatcher in eventLogSystemWatcherList)
                {
                    eventLogSystemWatcher.Stop();
                    eventLogSystemWatcher.Dispose();
                }
                eventLogSystemWatcherList.Clear();
            }

            if (eventLogSecurityWatcherList != null)
            {
                foreach (ManagementEventWatcher eventLogSecurityWatcher in eventLogSecurityWatcherList)
                {
                    eventLogSecurityWatcher.Stop();
                    eventLogSecurityWatcher.Dispose();
                }
                eventLogSecurityWatcherList.Clear();
            }
        }

        public static void invokeWatcher()
        {
            Log.Debug("invokeWatcher()");

            string[] serverCollection = Configuration.GetTextAppSettingsValue("LogServerNames").Split(';');
            string[] eventLogApplicationSourceNames = Configuration.GetTextAppSettingsValue("ApplicationSource").Split(';');
            string[] eventLogSystemSourceNames = Configuration.GetTextAppSettingsValue("SystemSource").Split(';');

            if (serverCollection.GetLength(0) == 0)
                return;

            eventLogApplicationWatcherList = new List<ManagementEventWatcher>();
            eventLogSystemWatcherList = new List<ManagementEventWatcher>();
            eventLogSecurityWatcherList = new List<ManagementEventWatcher>();

            foreach (string server in serverCollection)
            {
                Log.Info(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Setup watcher for server: {0}", new object[] { server }));

                MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Start WMI for Server: " + server));

                if (string.IsNullOrEmpty(server))
                    return;

                ManagementPath managementPathSystem = new ManagementPath(string.Format(@"\\{0}\root\cimv2", server));
                ConnectionOptions managementConnectionOptions = new ConnectionOptions();

                managementConnectionOptions.Authentication = AuthenticationLevel.Packet;
                managementConnectionOptions.Timeout = new TimeSpan(0, 0, 5, 0);
                managementConnectionOptions.EnablePrivileges = true;

                if (server.ToUpper() != Dns.GetHostName().ToUpper() && server.ToUpper() != "LOCALHOST")
                {
                    managementConnectionOptions.Username = "user";
                    managementConnectionOptions.Password = "password";
                }

                ManagementEventWatcher eventLogApplicationWatcher = null;

                try
                {
                    foreach (string eventLogApplicationSourceName in eventLogApplicationSourceNames)
                    {
                        if (!string.IsNullOrEmpty(eventLogApplicationSourceName))
                        {
                            WqlEventQuery eventLogApplicationQuery = new WqlEventQuery("__InstanceCreationEvent", string.Format("TargetInstance ISA 'Win32_NTLogEvent' AND TargetInstance.LogFile='Application' AND TargetInstance.SourceName LIKE '%{0}%'", eventLogApplicationSourceName));
                            eventLogApplicationWatcher = new ManagementEventWatcher(new ManagementScope(managementPathSystem, managementConnectionOptions), eventLogApplicationQuery);
                            eventLogApplicationWatcher.EventArrived += new EventArrivedEventHandler(eventLogApplicationEventHandler);
                            eventLogApplicationWatcher.Start();
                            eventLogApplicationWatcherList.Add(eventLogApplicationWatcher);

                            MonitoringEvent?.Invoke(null, new MonitoringEventArgs("eventLogApplicationWatcher started. Query:" + eventLogApplicationQuery.QueryString));
                        }
                    }
                }
                catch (Exception excep)
                {
                    if (eventLogApplicationWatcher != null)
                    {
                        eventLogApplicationWatcher.Stop();
                        eventLogApplicationWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (eventLogApplicationWatcher): " + excep.Message));
                }

                ManagementEventWatcher eventLogSystemWatcher = null;

                try
                {
                    foreach (string eventLogSystemSourceName in eventLogSystemSourceNames)
                    {
                        if (!string.IsNullOrEmpty(eventLogSystemSourceName))
                        {
                            WqlEventQuery eventLogSystemQuery = new WqlEventQuery("__InstanceCreationEvent", string.Format("TargetInstance ISA 'Win32_NTLogEvent' AND TargetInstance.LogFile='System' AND TargetInstance.SourceName LIKE '%{0}%'", eventLogSystemSourceName));
                            eventLogSystemWatcher = new ManagementEventWatcher(new ManagementScope(managementPathSystem, managementConnectionOptions), eventLogSystemQuery);
                            eventLogSystemWatcher.EventArrived += new EventArrivedEventHandler(eventLogSystemEventHandler);
                            eventLogSystemWatcher.Start();
                            eventLogSystemWatcherList.Add(eventLogSystemWatcher);

                            MonitoringEvent?.Invoke(null, new MonitoringEventArgs("eventLogSystemWatcher started. Query:" + eventLogSystemQuery.QueryString));
                        }
                    }
                }
                catch (Exception excep)
                {
                    if (eventLogSystemWatcher != null)
                    {
                        eventLogSystemWatcher.Stop();
                        eventLogSystemWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (eventLogSystemWatcher): " + excep.Message));
                }

                ManagementEventWatcher eventLogSecurityWatcher = null;

                try
                {
                    WqlEventQuery eventLogQuery = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 10), "TargetInstance ISA \"Win32_NTLogEvent\" AND TargetInstance.LogFile=\"Security\"");
                    eventLogSecurityWatcher = new ManagementEventWatcher(new ManagementScope(managementPathSystem, managementConnectionOptions), eventLogQuery);
                    eventLogSecurityWatcher.EventArrived += new EventArrivedEventHandler(eventLogSecurityEventHandler);
                    //eventLogSecurityWatcher.Start();
                    //eventLogSecurityWatcherList.Add(eventLogSecurityWatcher);

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("eventLogSecurityWatcher started. Query:" + eventLogQuery.QueryString));
                }
                catch (Exception excep)
                {
                    if (eventLogSecurityWatcher != null)
                    {
                        eventLogSecurityWatcher.Stop();
                        eventLogSecurityWatcher.Dispose();
                    }

                    MonitoringEvent?.Invoke(null, new MonitoringEventArgs("Error (eventLogSecurityWatcher): " + excep.Message));
                }

                Log.Info(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Setup done", new object[] { server }));
            }
        }

        static public void eventLogApplicationEventHandler(object sender, EventArrivedEventArgs e)
        {
            MonitorEventLog monitorEventLog = new MonitorEventLog();
            monitorEventLog.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorEventLog_MonitoringEvent);
            DataRow eventEntryRow;
            monitorEventLog.SetEvent(e, WindowsEventType.eventLogApplicationEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static public void eventLogSystemEventHandler(object sender, EventArrivedEventArgs e)
        {
            MonitorEventLog monitorEventLog = new MonitorEventLog();
            monitorEventLog.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorEventLog_MonitoringEvent);
            DataRow eventEntryRow;
            monitorEventLog.SetEvent(e, WindowsEventType.eventLogSystemEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static public void eventLogSecurityEventHandler(object sender, EventArrivedEventArgs e)
        {
            MonitorEventLog monitorEventLog = new MonitorEventLog();
            monitorEventLog.MonitoringEvent += new EventHandler<MonitoringEventArgs>(monitorEventLog_MonitoringEvent);
            DataRow eventEntryRow;
            monitorEventLog.SetEvent(e, WindowsEventType.eventLogSecurityEvent, out eventEntryRow);
            EventEntryEvent(null, new MonitoringEventArgs(eventEntryRow));
        }

        static void monitorEventLog_MonitoringEvent(object sender, MonitoringEventArgs e)
        {
            MonitoringEvent(null, e);
        }
    }
}
