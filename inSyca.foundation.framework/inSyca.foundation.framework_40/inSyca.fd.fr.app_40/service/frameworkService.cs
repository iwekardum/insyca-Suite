using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework.schedules;
using Microsoft.Win32;
using System;
using System.Data;
using System.ServiceProcess;

namespace inSyca.foundation.framework.application.service
{
    abstract public class windowsServiceHost : ServiceBase
    {
        virtual protected AppSchedules appSchedules
        {
            get
            {
                return Configuration.GetAppSchedules();
            }
        }

        private Scheduler scheduler;

        protected DataSet eventEntry { get; set; }
        protected DataSet logEntry { get; set; }

        abstract protected void ImportLogEntryRow(DataRow dataRow);
        abstract protected void ImportEventEntryRow(DataRow dataRow);

        abstract protected void InvokeWatcher();
        abstract protected void DisposeWatcher();

        protected override void OnStart(string[] args)
        {
            Log.DebugFormat("OnStart(string[] args {0}) ", args);

            InvokeWatcher();

            bool bInitSuccess = true;

            if (!Init_Schedules())
                bInitSuccess = false;

            if (!bInitSuccess)
                throw new Exception("Error initializing Foundation Framework Service, more Information can be found in the eventlog");
        }

        protected override void OnStop()
        {
            DisposeWatcher();
        }

        protected void EvaluateRegistryKey(string keyName, string configDir, string configFile)
        {
            string registryKeyValue = string.Format(@"SOFTWARE\{0}\", keyName);

            RegistryKey baseRegistryKey32 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey baseRegistryKey64 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey registryKey32 = baseRegistryKey32.OpenSubKey(registryKeyValue);
            RegistryKey registryKey64 = baseRegistryKey64.OpenSubKey(registryKeyValue);

            if (registryKey32 == null)
            {
                registryKey32 = baseRegistryKey32.CreateSubKey(registryKeyValue);
                registryKey32.SetValue("ConfigDir", configDir);
                registryKey32.SetValue("ConfigFile", configFile);
            }

            if (registryKey64 == null)
            {
                registryKey64 = baseRegistryKey64.CreateSubKey(registryKeyValue);
                registryKey64.SetValue("ConfigDir", configDir);
                registryKey64.SetValue("ConfigFile", configFile);
            }
        }

        virtual protected bool Init_Schedules()
        {
            Log.Debug("Init_Schedules()");

            AppSchedules appSchedules = Configuration.GetAppSchedules();

            if (appSchedules == null)
                return true;

            scheduler = new Scheduler();

            foreach (AppSchedulesInstanceElement appSchedule in appSchedules.Instances)
            {
                try
                {
                    Schedulation schedulation;
                    Task task = new Task(appSchedule, new DTaskStart(OnScheduledEvent));
                    scheduler.AddNewTask(task);
                    schedulation = new Schedulation(DateTime.Now.AddMilliseconds(appSchedule.TaskStartupDelay), task);
                    schedulation.OccurrenceStartTime = appSchedule.OccurrenceStartTime;
                    schedulation.OccurrenceStopTime = appSchedule.OccurrenceEndTime;
                    schedulation.OccurrenceType = appSchedule.Occurrence;
                    scheduler.AddNewScheduling(schedulation);

                    scheduler.StartScheduling();
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                    return false;
                }
            }

            return true;
        }

        // Specify what you want to happen when the Elapsed event is 
        // raised.
        virtual protected void OnScheduledEvent(Task task)
        {
            Log.DebugFormat("OnScheduledEvent(Task task {0})", task);
           
            switch (task.AppSchedule.Name)
            {
                case "inSycaTestOnce":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestOnce", task);
                    break;
                case "inSycaTestEveryMinute":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestEveryMinute", task);
                    break;
                case "inSycaTestHourly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestHourly", task);
                    break;
                case "inSycaTestDaily":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestDaily", task);
                    break;
                case "inSycaTestWeekly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestWeekly", task);
                    break;
                case "inSycaTestMonthly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestMonthly", task);
                    break;
                case "inSycaTestYearly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestYearly", task);
                    break;
                case "ProcessFileOperations":
                    io.FileSystem fileSystem = new io.FileSystem();
                    fileSystem.Process();
                    break;
                default:
                    break;
            }
        }

        protected void EventEntryEvent(object sender, MonitoringEventArgs e)
        {
            if (e.Row != null)
            {
                ImportEventEntryRow(e.Row);
                eventEntry.AcceptChanges();
            }
        }

        protected void MonitoringEvent(object sender, MonitoringEventArgs e)
        {
            if (e.Row != null)
            {
                ImportLogEntryRow(e.Row);

                logEntry.AcceptChanges();
            }
        }

        public void Start()
        {
            OnStart(null);
        }
    }
}
