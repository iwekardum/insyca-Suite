using BizUnit.Xaml;
using inSyca.foundation.framework;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.integration.biztalk.diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace inSyca.foundation.integration.biztalk.test
{
    public class BizTalkTestEnvironment
    {
        public bool restartHostInstances { get; set; }
        public bool fullInfrastructureDirectory { get; set; }
        public string baseTestProjectPath { get; set; }
        public string baseTestFileDirectory { get; set; }
        public string baseInfrastructureDirectory { get; set; }
        public string[] outputFileExtensions { get; set; }
        public string[] applicationDirectories { get; set; }
        public string[] receiveDirectories { get; set; }
        public string[] sendDirectories { get; set; }
        public string[] hostInstanceNames { get; set; }
        public string[] biztalkServers { get; set; }

        internal TestCase testCase { get; set; }

        private Settings _settings = null;
        public Settings settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new Settings();
                }

                return _settings;
            }
        }

        public BizTalkTestEnvironment()
        {
            testCase = new TestCase();
            fullInfrastructureDirectory = true;

            try
            {
                string Location = Directory.GetParent(Assembly.GetCallingAssembly().Location).FullName;
                int index = Location.IndexOf("bin");

                if (index > 0)
                    baseTestProjectPath = Location.Remove(index);
                else
                    baseTestProjectPath = Location;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));
            }

            Log.DebugFormat("BizTalkTestEnvironment() baseTestProjectPath {0}", baseTestProjectPath);
        }

        public Dictionary<string, string> infrastructureApplicationDirectories
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                foreach (string applicationDirectory in applicationDirectories)
                {
                    string directory;

                    if (fullInfrastructureDirectory)
                        directory = Path.Combine(baseInfrastructureDirectory, applicationDirectory);
                    else
                        directory = Path.Combine(baseTestProjectPath, baseInfrastructureDirectory, applicationDirectory);

                    if (!Directory.Exists(directory))
                    { 
                        try
                        {
                            Directory.CreateDirectory(directory);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));
                        }
                    }

                    dictionary.Add(applicationDirectory, directory);

                    Log.DebugFormat("infrastructureApplicationDirectories added {0} - {1}", applicationDirectory, directory);
                }

                return dictionary;
            }
        }

        public Dictionary<string, string> testfileApplicationDirectories
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                foreach (string applicationDirectory in applicationDirectories)
                { 
                    string directory = Path.Combine(baseTestProjectPath, baseTestFileDirectory, applicationDirectory);

                    if (!Directory.Exists(directory))
                    {
                        try
                        {
                            Directory.CreateDirectory(directory);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                        }
                    }
                    dictionary.Add(applicationDirectory, directory);

                    Log.DebugFormat("testfileApplicationDirectories added {0} - {1}", applicationDirectory, directory);

                }
                return dictionary;
            }
        }
    }
}
