using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Management;

namespace inSyca.foundation.framework.diagnostics
{
    public abstract class EventType
    {
    }

    public class EntryType
    {
        public static readonly string Information = System.Diagnostics.EventLogEntryType.Information.ToString();
        public static readonly string Warning = System.Diagnostics.EventLogEntryType.Warning.ToString();
        public static readonly string Error = System.Diagnostics.EventLogEntryType.Error.ToString();

        public const string InformationColor = "#CCCCCC";
        public const string WarningColor = "#FFCC00";
        public const string ErrorColor = "#FF0000";

        public const string ActiveColor = "#00FF00";
        public const string InactiveColor = "#FF0000";
    }

    public abstract class Monitor<T>
    {
        public event EventHandler<MonitoringEventArgs> MonitoringEvent;

        protected List<Attachment> Attachments;

        private log4net.ILog monitorLogger;

        public Monitor()
        {
            monitorLogger = log4net.LogManager.GetLogger(typeof(T).Module.Name, typeof(T).Name);

            Attachments = new List<Attachment>();
        }

        public void SetEvent(EventArrivedEventArgs e, string eventType, out DataRow eventEntryDataRow)
        {
            Log.DebugFormat("SetEvent(EventArrivedEventArgs e {0}, string eventType {1}, out DataRow eventEntryDataRow)", e, eventType);

            DataTable logEntryDataTable;
            SetupLogEntry(e, eventType, out logEntryDataTable);

            int eventEntryTypeCode = SetupEventEntry(logEntryDataTable, out eventEntryDataRow);

            Log.DebugFormat("SetEvent(EventArrivedEventArgs e {0}, string eventType {1}, out DataRow eventEntryDataRow\nEventEntryType: {2})", e, eventType, eventEntryTypeCode );

            switch (eventEntryTypeCode)
            {
                case (int)System.Diagnostics.EventLogEntryType.Error:
                    SetError(eventEntryDataRow);
                    break;
                case (int)System.Diagnostics.EventLogEntryType.Warning:
                    SetWarning(eventEntryDataRow);
                    break;
                case (int)System.Diagnostics.EventLogEntryType.Information:
                    SetInformation(eventEntryDataRow);
                    break;
                default:
                    Log.DebugFormat("SetEvent(EventArrivedEventArgs e {0}, string eventType {1}, out DataRow eventEntryDataRow\nEvententry type unknown: {2})", e, eventType, eventEntryTypeCode );
                    break;
            }
        }

        protected void SetLogEntry(EventArrivedEventArgs eventArrivedEventArgs, string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow)
        {
            Log.DebugFormat("SetLogEntry(EventArrivedEventArgs eventArrivedEventArgs {0}, string eventType {1}, DataSet logEntryDataSet {2}, DataRow logEntryDataRow {3})", eventArrivedEventArgs, eventType, logEntryDataSet, logEntryDataRow );

            using (ManagementBaseObject managementBaseObject = eventArrivedEventArgs.NewEvent)
            {
                PropertyDataCollection propertyDataCollection = managementBaseObject.Properties;
                PropertyDataCollection.PropertyDataEnumerator propertyDataEnumerator = propertyDataCollection.GetEnumerator();

                while (propertyDataEnumerator.MoveNext())
                {
                    PropertyData data = propertyDataEnumerator.Current;
                    InsertLogEntryRow(eventType, logEntryDataSet, logEntryDataRow, data);
                }

                QualifierDataCollection qualifierDataCollection = managementBaseObject.Qualifiers;
                QualifierDataCollection.QualifierDataEnumerator qualifierDataEnumerator = qualifierDataCollection.GetEnumerator();

                while (qualifierDataEnumerator.MoveNext())
                {
                    QualifierData data = qualifierDataEnumerator.Current;
//                    InsertLogEntryRow(eventType, logEntryDataSet, logEntryDataRow, data);
                }

                PropertyDataCollection systemPropertyDataCollection = managementBaseObject.SystemProperties;
                PropertyDataCollection.PropertyDataEnumerator systemPropertyDataEnumerator = systemPropertyDataCollection.GetEnumerator();

                while (systemPropertyDataEnumerator.MoveNext())
                {
                    PropertyData data = systemPropertyDataEnumerator.Current;
                    //InsertLogEntryRow(eventType, logEntryDataSet, logEntryDataRow, data);
                }

                propertyDataEnumerator.Reset();
                InsertAdditionalEntryRow(eventType, logEntryDataSet, logEntryDataRow, managementBaseObject);

                logEntryDataSet.AcceptChanges();

                logEntryDataSet.EnforceConstraints = true;

                Log.DebugFormat("SetLogEntry(EventArrivedEventArgs eventArrivedEventArgs {0}, string eventType {1}, DataSet logEntryDataSet {2}, DataRow logEntryDataRow {3})\nBefore Fire Monitoring Event: MonitoringEvent {4}", eventArrivedEventArgs, eventType, logEntryDataSet, logEntryDataRow, MonitoringEvent );

                MonitoringEvent?.Invoke(null, new MonitoringEventArgs(logEntryDataRow));

                Log.DebugFormat("SetLogEntry(EventArrivedEventArgs eventArrivedEventArgs {0}, string eventType {1}, DataSet logEntryDataSet {2}, DataRow logEntryDataRow {3})\nAfter Fire Monitoring Event: MonitoringEvent {4}", eventArrivedEventArgs, eventType, logEntryDataSet, logEntryDataRow, MonitoringEvent);
            }

            Log.DebugFormat("SetLogEntry(EventArrivedEventArgs eventArrivedEventArgs {0}, string eventType {1}, DataSet logEntryDataSet {2}, DataRow logEntryDataRow {3})\nFunction finished", eventArrivedEventArgs, eventType, logEntryDataSet, logEntryDataRow);
        }

        protected void TransformLogEntry(DataTable logEntryTable, DataRow eventEntryDataRow)
        {
            Log.DebugFormat("TransformLogEntry(DataTable logEntryTable {0}, DataRow eventEntryDataRow {1})", logEntryTable, eventEntryDataRow);

            Stream logEntryStream = new MemoryStream();
            logEntryTable.WriteXml(logEntryStream);
            logEntryStream.Position = 0;
            Attachments.Add(new Attachment(logEntryStream, "LogEntry.xml"));

            foreach (DataRow logEntryRow in logEntryTable.Rows)
                SetEventEntry(logEntryRow, eventEntryDataRow);
        }

        private void SetError(DataRow eventEntryRow)
        {
            Log.DebugFormat("SetError(DataRow eventEntryRow {0})", eventEntryRow);

            LogEntry logEntry;

            SetHtmlMessage(eventEntryRow, out logEntry);
            monitorLogger.Error(logEntry);
        }

        private void SetWarning(DataRow eventEntryRow)
        {
            Log.DebugFormat("SetWarning(DataRow eventEntryRow {0})", eventEntryRow);

            LogEntry logEntry;

            SetHtmlMessage(eventEntryRow, out logEntry);
            monitorLogger.Warn(logEntry);
        }

        private void SetInformation(DataRow eventEntryRow)
        {
            Log.DebugFormat("SetInformation(DataRow eventEntryRow {0})", eventEntryRow);

            LogEntry logEntry;

            SetHtmlMessage(eventEntryRow, out logEntry);
            monitorLogger.Info(logEntry);
        }

        protected abstract void InsertAdditionalEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, ManagementBaseObject managementBaseObject);

        protected abstract void InsertLogEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, PropertyData data);

        protected abstract int SetupEventEntry(DataTable logEntryTable, out DataRow eventEntryDataRow);

        protected abstract void SetupLogEntry(EventArrivedEventArgs e, string eventType, out DataTable logEntryDataTable);

        protected abstract void SetEventEntry(DataRow logEntryRow, DataRow eventEntryRow);

        protected abstract void SetHtmlMessage(DataRow eventEntryDataRow, out LogEntry logEntry);
    }
}
