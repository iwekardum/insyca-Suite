using log4net.Core;
using System;

namespace inSyca.foundation.framework.diagnostics
{
    public abstract class LogBase<T>
    {
 
        internal static bool logInitialized = false;

        private static log4net.ILog inSycaLogger
        {
            get
            {
                if (!logInitialized)
                {
                    LogSourceAttribute sa = typeof(T).GetCustomAttributes(typeof(LogSourceAttribute), false)[0] as LogSourceAttribute;

                    return sa.InitLogger(typeof(T), ref logInitialized);
                }

                return log4net.LogManager.GetLogger(typeof(T).Module.Name, typeof(T));
            }
        }

        //public static bool Debug(LogEntry logEntry)
        //{
        //    inSycaLogger.Debug(logEntry);
        //    return true;
        //}

        //public static bool Info(LogEntry logEntry)
        //{
        //    inSycaLogger.Info(logEntry);
        //    return true;
        //}

        //public static bool Warn(LogEntry logEntry)
        //{
        //    inSycaLogger.Warn(logEntry);
        //    return true;
        //}

        //public static bool Error(LogEntry logEntry)
        //{
        //    inSycaLogger.Error(logEntry);
        //    return true;
        //}

        //public static bool Fatal(LogEntry logEntry)
        //{
        //    inSycaLogger.Error(logEntry);
        //    return true;
        //}

        #region ILog Implementation
        public static bool IsDebugEnabled
        {
            get
            {
                return inSycaLogger.IsDebugEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return inSycaLogger.IsErrorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return inSycaLogger.IsFatalEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return inSycaLogger.IsInfoEnabled;
            }
        }

        public static bool IsWarnEnabled
        {
            get
            {
                return inSycaLogger.IsWarnEnabled;
            }
        }

        public static ILogger Logger
        {
            get
            {
                return inSycaLogger.Logger;
            }
        }

        public static void Debug(object message)
        {
            inSycaLogger.Debug(message);
        }

        public static void Debug(object message, Exception exception)
        {
            inSycaLogger.Debug(message, exception);
        }

        public static void DebugFormat(string format, object arg0)
        {
            inSycaLogger.DebugFormat(format, arg0);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            inSycaLogger.DebugFormat(format, args);
        }

        public static void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            inSycaLogger.DebugFormat(provider, format, args);
        }

        public static void DebugFormat(string format, object arg0, object arg1)
        {
            inSycaLogger.DebugFormat(format, arg0, arg1);
        }

        public static void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            inSycaLogger.DebugFormat(format, arg0, arg1, arg2);
        }

        public static void Error(object message)
        {
            inSycaLogger.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            inSycaLogger.Error(message, exception);
        }

        public static void ErrorFormat(string format, object arg0)
        {
            inSycaLogger.ErrorFormat(format, arg0);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            inSycaLogger.ErrorFormat(format, args);
        }

        public static void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            inSycaLogger.ErrorFormat(provider, format, args);
        }

        public static void ErrorFormat(string format, object arg0, object arg1)
        {
            inSycaLogger.ErrorFormat(format, arg0, arg1);
        }

        public static void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            inSycaLogger.ErrorFormat(format, arg0, arg1, arg2);
        }

        public static void Fatal(object message)
        {
            inSycaLogger.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            inSycaLogger.Fatal(message, exception);
        }

        public static void FatalFormat(string format, object arg0)
        {
            inSycaLogger.FatalFormat(format, arg0);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            inSycaLogger.FatalFormat(format, args);
        }

        public static void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            inSycaLogger.FatalFormat(provider, format, args);
        }

        public static void FatalFormat(string format, object arg0, object arg1)
        {
            inSycaLogger.FatalFormat(format, arg0, arg1);
        }

        public static void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            inSycaLogger.FatalFormat(format, arg0, arg1, arg2);
        }

        public static void Info(object message)
        {
            inSycaLogger.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            inSycaLogger.Info(message, exception);
        }

        public static void InfoFormat(string format, object arg0)
        {
            inSycaLogger.InfoFormat(format, arg0);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            inSycaLogger.InfoFormat(format, args);
        }

        public static void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            inSycaLogger.InfoFormat(provider, format, args);
        }

        public static void InfoFormat(string format, object arg0, object arg1)
        {
            inSycaLogger.InfoFormat(format, arg0, arg1);
        }

        public static void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            inSycaLogger.InfoFormat(format, arg0, arg1, arg2);
        }

        public static void Warn(object message)
        {
            inSycaLogger.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            inSycaLogger.Warn(message, exception);
        }

        public static void WarnFormat(string format, object arg0)
        {
            inSycaLogger.WarnFormat(format, arg0);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            inSycaLogger.WarnFormat(format, args);
        }

        public static void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            inSycaLogger.WarnFormat(provider, format, args);
        }

        public static void WarnFormat(string format, object arg0, object arg1)
        {
            inSycaLogger.WarnFormat(format, arg0, arg1);
        }

        public static void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            inSycaLogger.WarnFormat(format, arg0, arg1, arg2);
        }
#endregion
    }
}
