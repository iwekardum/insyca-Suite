using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using inSyca.foundation.framework.data;
using System.Data;
using System.Globalization;
using inSyca.foundation.framework.configuration;

namespace inSyca.foundation.framework.diagnostics
{
    public class WindowsEventType : EventType
    {
        internal const string eventLogApplicationEvent = "Application Log Event";
        internal const string eventLogSystemEvent = "System Log Event";
        internal const string eventLogSecurityEvent = "Security Log Event";
    }

    internal class MonitorEventLog : Monitor<MonitorEventLog>
    {
        internal MonitorEventLog()
        {
        }

        protected override void SetupLogEntry(EventArrivedEventArgs eventArrivedEventArgs, string eventType, out DataTable logEntryDataTable)
        {
            dsLogEntry logEntry = new dsLogEntry();
            logEntry.EnforceConstraints = false;
            dsLogEntry.dtLogEntryRow logEntryRow = logEntry.dtLogEntry.NewdtLogEntryRow();

            logEntryRow.dtLogEntry_Id = Guid.NewGuid().ToString();
            logEntryRow.eventtype = eventType;

            SetLogEntry(eventArrivedEventArgs, eventType, logEntry, logEntryRow);

            logEntryDataTable = logEntry.dtLogEntry;
        }

        protected override int SetupEventEntry(DataTable logEntryTable, out DataRow eventEntryDataRow)
        {
            dsEventEntry eventEntry = new dsEventEntry();
            eventEntryDataRow = eventEntry.dtEventEntry.NewdtEventEntryRow();

            TransformLogEntry(logEntryTable, eventEntryDataRow);

            eventEntry.dtEventEntry.AdddtEventEntryRow((dsEventEntry.dtEventEntryRow)eventEntryDataRow);
            eventEntry.AcceptChanges();

            return Convert.ToInt32(((dsEventEntry.dtEventEntryRow)eventEntryDataRow).entrytypecode);
        }

        protected override void InsertAdditionalEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, ManagementBaseObject managementBaseObject)
        {
        }

        protected override void InsertLogEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, PropertyData data)
        {
            dsLogEntry logEntry = (dsLogEntry)logEntryDataSet;
            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)logEntryDataRow;

            if (data.Value != null && data.Name == "TargetInstance")
            {
                ManagementBaseObject mbo = (ManagementBaseObject)data.Value;
                dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow = logEntry.dtManagementBaseObject.NewdtManagementBaseObjectRow();
                managementBaseObjectRow.dtLogEntryRow = logEntryRow;
                managementBaseObjectRow.classname = mbo.ClassPath.ClassName;

                foreach (var item in mbo.Properties)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtPropertiesRow propertiesRow = logEntry.dtProperties.NewdtPropertiesRow();
                        propertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        propertiesRow.name = item.Name;
                        propertiesRow.value = item.Value.ToString();
                        logEntry.dtProperties.AdddtPropertiesRow(propertiesRow);
                    }

                foreach (var item in mbo.Qualifiers)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtQualifiersRow qualifiersRow = logEntry.dtQualifiers.NewdtQualifiersRow();
                        qualifiersRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        qualifiersRow.name = item.Name;
                        qualifiersRow.value = item.Value.ToString();
                        logEntry.dtQualifiers.AdddtQualifiersRow(qualifiersRow);
                    }

                foreach (var item in mbo.SystemProperties)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtSystemPropertiesRow systemPropertiesRow = logEntry.dtSystemProperties.NewdtSystemPropertiesRow();
                        systemPropertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        systemPropertiesRow.name = item.Name;
                        systemPropertiesRow.value = item.Value.ToString();
                        logEntry.dtSystemProperties.AdddtSystemPropertiesRow(systemPropertiesRow);
                    }

                logEntry.dtManagementBaseObject.AdddtManagementBaseObjectRow(managementBaseObjectRow);

                logEntry.dtLogEntry.AdddtLogEntryRow(logEntryRow);
            }
        }

        protected override void SetEventEntry(DataRow logEntryDataRow, DataRow eventEntryDataRow)
        {
            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)logEntryDataRow;
            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)eventEntryDataRow;

            eventEntryRow.eventtype = logEntryRow.eventtype;

            switch (logEntryRow.eventtype)
            {
                case WindowsEventType.eventLogApplicationEvent:
                case WindowsEventType.eventLogSecurityEvent:
                case WindowsEventType.eventLogSystemEvent:
                    {
                        foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
                        {
                            eventEntryRow.classname = managementBaseObjectRow.classname;

                            foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                                switch (propertiesRow.name)
                                {
                                    case "RunningServer":
                                        eventEntryRow.computername = propertiesRow.value;
                                        break;
                                    case "Logfile":
                                        eventEntryRow.logname = propertiesRow.value;
                                        break;
                                    case "ComputerName":
                                        eventEntryRow.computername = propertiesRow.value;
                                        break;
                                    case "Message":
                                        eventEntryRow.message = propertiesRow.value;
                                        break;
                                    case "Type":
                                        eventEntryRow.entrytype = propertiesRow.value;
                                        break;
                                    case "EventType":
                                        eventEntryRow.entrytypecode = propertiesRow.value;
                                        break;
                                    case "TimeGenerated":
                                        eventEntryRow.timegenerated = propertiesRow.value;
                                        break;
                                    case "HostName":
                                        eventEntryRow.hostname = propertiesRow.value;
                                        break;
                                    case "IsDisabled":
                                        eventEntryRow.isdisabled = propertiesRow.value;
                                        break;
                                    case "Logon":
                                        eventEntryRow.logon = propertiesRow.value;
                                        break;
                                    case "NTGroupName":
                                        eventEntryRow.ntgroupname = propertiesRow.value;
                                        break;
                                    case "UserID":
                                        eventEntryRow.user = propertiesRow.value;
                                        break;
                                    case "SourceName":
                                        eventEntryRow.source = propertiesRow.value;
                                        break;
                                    case "EventCode":
                                        eventEntryRow.eventcode = propertiesRow.value;
                                        break;
                                }
                            foreach (dsLogEntry.dtQualifiersRow qualifiersRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtQualifiers"))
                                ;
                            foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                                ;

                            if (string.IsNullOrEmpty(eventEntryRow.user))
                                eventEntryRow.user = "N/A";
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        protected override void SetHtmlMessage(DataRow eventEntryDataRow, out LogEntry logEntry)
        {
            logEntry = new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { eventEntryDataRow, null });

            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)eventEntryDataRow;

            string entryTypeColor;

            switch (Convert.ToInt32(eventEntryRow.entrytypecode))
            {
                case (int)System.Diagnostics.EventLogEntryType.Error:
                    entryTypeColor = EntryType.ErrorColor;
                    break;
                case (int)System.Diagnostics.EventLogEntryType.Warning:
                    entryTypeColor = EntryType.WarningColor;
                    break;
                default:
                    entryTypeColor = EntryType.InformationColor;
                    break;
            }

            string htmlHeader = string.Empty;
            string htmlBody = string.Empty;

            switch (eventEntryRow.eventtype)
            {
                case WindowsEventType.eventLogApplicationEvent:
                case WindowsEventType.eventLogSecurityEvent:
                case WindowsEventType.eventLogSystemEvent:
                    {
                        logEntry.AdditionalString = "Computername: {0}, Logname: {1}, Source: {2}, Message: {3}";
                        logEntry.AdditionalParameters = new object[] { eventEntryRow.computername, eventEntryRow.logname, eventEntryRow.source, eventEntryRow.message.Replace("\r\n", "\t").Replace(" \t", "\t").Replace("\t ", "\t") };

                        htmlHeader = Properties.Resources.EventLog.Substring(0, Properties.Resources.EventLog.IndexOf("<body>"));
                        htmlBody = string.Format(Properties.Resources.EventLog.Substring(Properties.Resources.EventLog.IndexOf("<body>")),
                                                        string.Format("<span style='color: {0}'>{1}</span>", entryTypeColor, eventEntryRow.entrytype),
                                                        eventEntryRow.computername,
                                                        eventEntryRow.user,
                                                        eventEntryRow.source,
                                                        eventEntryRow.eventcode,
                                                        eventEntryRow.logname,
                                                        string.Format("{0}", System.Management.ManagementDateTimeConverter.ToDateTime(eventEntryRow.timegenerated)),
                                                        eventEntryRow.message.Replace("\r\n", "<br/>").Replace("\n", "<br/>"));
                    }
                    break;
            }

            logEntry.HtmlString = htmlHeader + htmlBody;
            logEntry.AttachmentCollection.AddRange(Attachments);
        }
    }
}
