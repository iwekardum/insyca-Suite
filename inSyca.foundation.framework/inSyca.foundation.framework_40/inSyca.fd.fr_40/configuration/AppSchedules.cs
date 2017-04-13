using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using inSyca.foundation.framework.schedules;

namespace inSyca.foundation.framework.configuration
{
    public class AppSchedules : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public AppSchedulesInstanceCollection Instances
        {
            get { return (AppSchedulesInstanceCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class AppSchedulesInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppSchedulesInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((AppSchedulesInstanceElement)element).Name;
        }

        public AppSchedulesInstanceElement this[int index]
        {
            get { return (AppSchedulesInstanceElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new AppSchedulesInstanceElement this[string key]
        {
            get { return (AppSchedulesInstanceElement)BaseGet(key); }
        }
    }

    public class AppSchedulesInstanceElement : ConfigurationElement
    {
        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}, Command: {2}, StartDate: {3}, EndDate: {4}, StartTime: {5}, EndTime: {6}, Interval: {7}, Repeat: {8}", Name, Type, Command, StartDate, EndDate, StartTime, EndTime, Interval, Repeat);
        }

        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = false)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("command", IsRequired = false)]
        public string Command
        {
            get { return (string)base["command"]; }
            set { base["command"] = value; }
        }

        [ConfigurationProperty("startupdelay", IsRequired = false)]
        public string StartupDelay
        {
            get { return (string)base["startupdelay"]; }
            set { base["startupdelay"] = value; }
        }

        [ConfigurationProperty("startdate", IsRequired = false)]
        public string StartDate
        {
            get { return (string)base["startdate"]; }
            set { base["startdate"] = value; }
        }

        [ConfigurationProperty("enddate", IsRequired = false)]
        public string EndDate
        {
            get { return (string)base["enddate"]; }
            set { base["enddate"] = value; }
        }

        [ConfigurationProperty("starttime", IsRequired = false)]
        public string StartTime
        {
            get { return (string)base["starttime"]; }
            set { base["starttime"] = value; }
        }

        [ConfigurationProperty("endtime", IsRequired = false)]
        public string EndTime
        {
            get { return (string)base["endtime"]; }
            set { base["endtime"] = value; }
        }

        [ConfigurationProperty("interval", IsRequired = false)]
        public string Interval
        {
            get { return (string)base["interval"]; }
            set { base["interval"] = value; }
        }

        [ConfigurationProperty("occurrence", IsRequired = false)]
        public string Repeat
        {
            get { return (string)base["occurrence"]; }
            set { base["occurrence"] = value; }
        }

        public EOccurrence Occurrence
        {
            get 
            {
                switch(Repeat.ToLower())
                {
                    case "once":
                        return EOccurrence.Once;
                    case "everysecond":
                        return EOccurrence.EverySecond;
                    case "everyminute":
                        return EOccurrence.EveryMinute;
                    case "hourly":
                        return EOccurrence.Hourly;
                    case "daily":
                        return EOccurrence.Daily;
                    case "weekly":
                        return EOccurrence.Weekly;
                    case "monthly":
                        return EOccurrence.Monthly;
                    case "yearly":
                        return EOccurrence.Yearly;
                }

                return EOccurrence.Never;
            }
        }

        public DateTime OccurrenceStartTime
        {
            get 
            {
                DateTime occurrenceStartTime;

                if (DateTime.TryParse(string.Format("{0} {1}", StartDate, StartTime), out occurrenceStartTime))
                    return occurrenceStartTime;

                if (string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(StartTime))
                    return DateTime.MinValue;

                return DateTime.MaxValue;
            }
        }

        public DateTime OccurrenceEndTime
        {
            get
            {
                DateTime occurrenceEndTime;
                if(DateTime.TryParse(string.Format("{0} {1}", EndDate, EndTime), out occurrenceEndTime))
                    return occurrenceEndTime;

                if (string.IsNullOrEmpty(EndDate) && string.IsNullOrEmpty(EndTime))
                    return DateTime.MaxValue;

                return DateTime.MinValue;

            }
        }

        public int IntervalInteger
        {
            get 
            { 
                int iInterval;
                
                if (Int32.TryParse((string)base["interval"], out iInterval))
                    return iInterval;

                return 1;
            }
        }

        public int TaskStartupDelay
        {
            get
            {
                int taskStartupDelay;
                int.TryParse(StartupDelay, out taskStartupDelay);
                return taskStartupDelay;
            }
        }
    }
}

