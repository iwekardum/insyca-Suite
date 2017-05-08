using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework.security;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace inSyca.foundation.framework.configuration
{
    public abstract class ConfigurationBase<T>
    {
        private static string registryKeySource = @"ConfigurationBase";
        private static string RegistryKeySource
        {
            get
            {
                try
                {
                    if (registryKeySource == "ConfigurationBase")
                    {
                        RegistryKeySourceAttribute sa = typeof(T).GetCustomAttributes(typeof(RegistryKeySourceAttribute), false)[0] as RegistryKeySourceAttribute;
                        registryKeySource = sa._name;
                    }

                    return registryKeySource;
                }
                catch (Exception)
                {
                    return registryKeySource;
                }
            }
        }

        private static Settings settings;
        public static Settings Settings
        {
            get
            {
                if (settings == null)
                    settings = new Settings();

                return settings;
            }
        }

        private static string configFileInfo;
        public static string ConfigFileInfo
        {
            get
            {
                if (configFileInfo == null)
                    configFileInfo = GetConfigurationFile();

                return configFileInfo;
            }
        }

        internal static System.Configuration.Configuration _configuration;
        internal static System.Configuration.Configuration configuration
        {
            get
            {
                if (_configuration == null)
                    LoadConfigFile();

                return _configuration;
            }
        }

        internal static bool LoadConfigFile()
        {
            if (_configuration != null)
                return true;

            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = ConfigFileInfo;

                _configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Init configuration: {0}\n\rError: {1}", ConfigFileInfo, ex.Message), System.Diagnostics.EventLogEntryType.Error);

                throw ex;
            }

            if (!_configuration.HasFile)
            {
                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Init configuration: {0}\n\rError: {1}", ConfigFileInfo, "Cannot find file specified" ), System.Diagnostics.EventLogEntryType.Error);
                throw new Exception(string.Format("Init configuration: {0}\n\rError: {1}", ConfigFileInfo, "Cannot find file specified"));
            }

            return true;
        }

        private static string GetConfigurationFile()
        {
            RegistryKey regKey = null;
            string configDir = string.Empty;
            string configFile = string.Empty;

            regKey = Registry.LocalMachine.OpenSubKey(RegistryKeySource, RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);

            if (regKey == null)
            {
                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Cannot open RegistryKey\nRegistryKeySource-> {0}", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                throw new Exception(string.Format("regKey is null for RegistryKeySource: {0}", RegistryKeySource));
            }

            try
            {
                configDir = (string)regKey.GetValue("ConfigDir");

                if (string.IsNullOrEmpty(configDir))
                {
                    System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Cannot open RegistryKey\nRegistryKeySource-> {0} - ConfigDir", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                    throw new Exception(string.Format("Value is null for registry value: {0} - {1}", RegistryKeySource, "ConfigDir"));
                }
            }
            catch (System.Security.SecurityException ex)
            {
                if (regKey != null)
                    regKey.Close();

                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("SecurityException: {0}", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
            catch (Exception ex)
            {
                if (regKey != null)
                    regKey.Close();

                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Exception: {0}", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }

            try
            {
                configFile = (string)regKey.GetValue("ConfigFile");

                if (string.IsNullOrEmpty(configFile))
                {
                    System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Cannot open RegistryKey\nRegistryKeySource-> {0} - ConfigFile", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                    throw new Exception(string.Format("Value is null for registry value: {0} - {1}", RegistryKeySource, "ConfigFile"));
                }
            }
            catch (System.Security.SecurityException ex)
            {
                if (regKey != null)
                    regKey.Close();

                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("SecurityException: {0}", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
            catch (Exception ex)
            {
                if (regKey != null)
                    regKey.Close();

                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Exception: {0}", RegistryKeySource), System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }

            if (regKey != null)
                regKey.Close();

            string configFileFull = Path.Combine(configDir, configFile);

            System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("Configfile: {0} loaded", configFileFull), System.Diagnostics.EventLogEntryType.Information);

            return configFileFull;
        }

        public static bool Encrypt_ConnectionStrings()
        {
            bool bChanged = false;

            for (int i = 0; i < configuration.ConnectionStrings.ConnectionStrings.Count; i++)
            {
                if (configuration.ConnectionStrings.ConnectionStrings[i].Name != "LocalSqlServer")
                {
                    if (!IsStringEncrypted(configuration.ConnectionStrings.ConnectionStrings[i].ConnectionString))
                    {
                        configuration.ConnectionStrings.ConnectionStrings[i].ConnectionString = Security.Encrypt(configuration.ConnectionStrings.ConnectionStrings[i].ConnectionString);
                        bChanged = true;
                    }
                }
            }

            if (bChanged)
            {
                configuration.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            }

            return true;
        }

        private static string Decrypt_ConnectionString(string strConnectionString)
        {
            if (IsStringEncrypted(strConnectionString))
                return Security.Decrypt(strConnectionString);

            return strConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string GetConnectionStringsValue(string KeyName)
        {
            Log.DebugFormat("GetConnectionStringsValue(string KeyName {0})", KeyName );

            string sValue = string.Empty;

            try
            {
                sValue = configuration.ConnectionStrings.ConnectionStrings[KeyName].ConnectionString;

                if (IsStringEncrypted(sValue))
                    sValue = Decrypt_ConnectionString(sValue);

                Log.DebugFormat("GetConnectionStringsValue(string KeyName {0})\nExeConfigFilename -> {1}\nsValue -> {2}", KeyName, configuration.FilePath, ReplacePasswordCharacters(sValue));

                return sValue;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\nsValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, sValue, ex.Message }));
                return string.Empty;
            }
        }

        public static AppSchedules GetAppSchedules()
        {
            Log.Debug("GetAppSchedules()");

            AppSchedules oAppSchedules = null;

            try
            {
                oAppSchedules = configuration.GetSection("appSchedules") as AppSchedules;

                return oAppSchedules;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "ExeConfigFilename -> {0}\nError\n{1}", new object[] { configuration.FilePath, ex.Message }));
                return null;
            }
        }


        public static object GetAppSettingsValue(string KeyName)
        {
            Log.DebugFormat("GetAppSettingsValue(string KeyName {0})", KeyName );

            object oValue = string.Empty;

            try
            {
                oValue = configuration.AppSettings.Settings[KeyName].Value;

                Log.DebugFormat("GetAppSettingsValue(string KeyName {0})\nExeConfigFilename -> {1}\noValue -> {2}", KeyName, configuration.FilePath, oValue );

                return oValue;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\noValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, oValue, ex.Message }));
                return null;
            }
        }

        public static decimal GetNumericAppSettingsValue(string KeyName)
        {
            Log.DebugFormat("GetNumericAppSettingsValue(string KeyName {0})", KeyName);

            string sValue = string.Empty;

            try
            {
                sValue = configuration.AppSettings.Settings[KeyName].Value;

                Log.DebugFormat("GetNumericAppSettingsValue(string KeyName {0})\nExeConfigFilename -> {0}\nsValue -> {1}", KeyName, configuration.FilePath, sValue );

                return Convert.ToDecimal(sValue);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\nsValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, sValue, ex.Message }));
                return decimal.MinValue;
            }
        }

        public static string GetTextAppSettingsValue(string KeyName)
        {
            Log.DebugFormat("GetTextAppSettingsValue(string KeyName {0})", KeyName);

            string sValue = string.Empty;

            try
            {
                sValue = configuration.AppSettings.Settings[KeyName].Value;

                Log.DebugFormat("GetTextAppSettingsValue(string KeyName {0})\nExeConfigFilename -> {1}\nsValue -> {2}", KeyName, configuration.FilePath, sValue );

                return sValue;
            }
            catch (Exception ex)
            {
                Log.Warn(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\nsValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, sValue, ex.Message }));
                return string.Empty;
            }
        }

        public static bool SetAppSettingsValue(string KeyName, string sValue)
        {
            Log.DebugFormat("SetAppSettingsValue(string KeyName {0}, string sValue {1})", KeyName, sValue);

            try
            {
                configuration.AppSettings.Settings[KeyName].Value = sValue;
                configuration.Save();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\nsValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, sValue, ex.Message }));
                return false;
            }

            return true;
        }

        public static bool GetBooleanAppSettingsValue(string KeyName)
        {
            Log.DebugFormat("GetBooleanAppSettingsValue(string KeyName {0})", KeyName);

            string sValue = string.Empty;

            try
            {
                sValue = configuration.AppSettings.Settings[KeyName].Value;

                Log.DebugFormat("GetBooleanAppSettingsValue(string KeyName {0})\nExeConfigFilename -> {0}\nsValue -> {1}", KeyName, configuration.FilePath, sValue );

                return Convert.ToBoolean(sValue);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { KeyName }, "ExeConfigFilename -> {0}\nsValue -> {1}\n\rError\n{2}", new object[] { configuration.FilePath, sValue, ex.Message }));
                return false;
            }
        }

        private static bool IsStringEncrypted(string strValue)
        {
            Log.DebugFormat("IsStringEncrypted(string strValue {0})", strValue);

            if (strValue.ToLower().IndexOf("provider") >= 0 || strValue.ToLower().IndexOf("data source=") >= 0 || strValue.ToLower().IndexOf("password") >= 0 || strValue.ToLower().IndexOf("user id") >= 0)
                return false;

            return true;
        }

        static private string ReplacePasswordCharacters(string strConnectionString)
        {
            Log.DebugFormat("ReplacePasswordCharacters(string strConnectionString {0})", strConnectionString);

            if (string.IsNullOrEmpty(strConnectionString))
                return strConnectionString;

            int iStart;
            int iStartPassword;
            int iEndPassword;

            StringBuilder strBuilder = new StringBuilder(strConnectionString.ToLower());

            iStart = strBuilder.ToString().IndexOf("password=");

            if (iStart < 0)
                return strConnectionString;

            iStartPassword = strBuilder.ToString().IndexOf("=", iStart) + 1;
            iEndPassword = strBuilder.ToString().IndexOf(";", iStart);

            try
            {
                strBuilder.Remove(iStartPassword, iEndPassword - iStartPassword);
                strBuilder.Insert(iStartPassword, "*****");
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { "strConnectionString" }, "Error\n{0}", new object[] { ex.Message }));
                return strConnectionString;
            }

            return strBuilder.ToString();
        }
    }
}
