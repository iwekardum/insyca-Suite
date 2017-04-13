using BizUnit.TestSteps.BizTalk.Host;
using BizUnit.TestSteps.DataLoaders.File;
using BizUnit.TestSteps.DataLoaders.Xml;
using BizUnit.TestSteps.File;
using inSyca.foundation.framework;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.integration.biztalk.diagnostics;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace inSyca.foundation.integration.biztalk.test
{
    public static class BizTalkTestCase
    {
        private static Settings _settings = null;
        public static Settings settings
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        public static void prepareDefaultTest(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("prepareDefaultTest(BizTalkTestEnvironment environment {0})", environment);

            emptyAllTestCasesFolders(environment);
            startHostInstances(environment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static DeleteStep emptyAllTestCasesFolders(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("emptyAllTestCasesFolders(BizTalkTestEnvironment environment {0})", environment);

            DeleteStep deletestep;
            // empty all test cases folders

            deletestep = new DeleteStep();
            Collection<string> filepathtodelete = new Collection<string>();

            foreach (string infrastructureApplicationDirectory in environment.infrastructureApplicationDirectories.Values)
            {
                foreach (string outputFileExtension in environment.outputFileExtensions)
                {
                    if (Directory.Exists(infrastructureApplicationDirectory))
                        filepathtodelete.Add(Path.Combine(infrastructureApplicationDirectory, outputFileExtension));

                    foreach (string sendDirectory in environment.sendDirectories)
                    {
                        if (Directory.Exists(Path.Combine(infrastructureApplicationDirectory, sendDirectory)))
                            filepathtodelete.Add(Path.Combine(infrastructureApplicationDirectory, sendDirectory, outputFileExtension));
                    }
                }
            }

            deletestep.FilePathsToDelete = filepathtodelete;
            environment.testCase.SetupSteps.Add(deletestep);

            return deletestep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        public static void startHostInstances(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("startHostInstances(BizTalkTestEnvironment environment {0})", environment);

            HostConductorStep hostconductorstep;
            //check if host instances are started

            foreach (string bizTalkserver in environment.biztalkServers)
            {
                foreach (var hostInstanceNames in environment.hostInstanceNames)
                {
                    if(environment.restartHostInstances)
                    {
                        hostconductorstep = new HostConductorStep();
                        hostconductorstep.Servers = bizTalkserver;
                        hostconductorstep.HostInstanceName = hostInstanceNames;
                        hostconductorstep.Action = "Stop";

                        environment.testCase.SetupSteps.Add(hostconductorstep);
                    }

                    hostconductorstep = new HostConductorStep();
                    hostconductorstep.Servers = bizTalkserver;
                    hostconductorstep.HostInstanceName = hostInstanceNames;
                    hostconductorstep.Action = "Start";

                    environment.testCase.SetupSteps.Add(hostconductorstep);
                }
            }
        }

        public static void stopHostInstances(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("stopHostInstances(BizTalkTestEnvironment environment {0})", environment);

            HostConductorStep hostconductorstep;
            //check if host instances are started

            foreach (string bizTalkserver in environment.biztalkServers)
            {
                foreach (var hostInstanceNames in environment.hostInstanceNames)
                {
                    hostconductorstep = new HostConductorStep();
                    hostconductorstep.Servers = bizTalkserver;
                    hostconductorstep.HostInstanceName = hostInstanceNames;
                    hostconductorstep.Action = "Stop";

                    environment.testCase.SetupSteps.Add(hostconductorstep);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="applicationName"></param>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void createXmlTestFile(BizTalkTestEnvironment environment, string applicationName, string sourceFile, string destinationFile)
        {
            Log.DebugFormat("createXmlTestFile(BizTalkTestEnvironment environment {0}, string applicationName {1}, string sourceFile {2}, string destinationFile {3})", environment, applicationName, sourceFile, destinationFile);

            CreateStep createstep;

            createstep = new CreateStep();

            //loading the test file
            XmlDataLoader xmldataloader = new XmlDataLoader();

            try
            {
                xmldataloader.FilePath = Path.Combine(environment.testfileApplicationDirectories[applicationName], sourceFile);
                createstep.CreationPath = Path.Combine(environment.infrastructureApplicationDirectories[applicationName], destinationFile);
                createstep.DataSource = xmldataloader;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { environment, applicationName, sourceFile, destinationFile }, ex ));
            }

            Log.DebugFormat("xmldataloader.FilePath {0}", xmldataloader.FilePath);
            Log.DebugFormat("createstep.CreationPath {0}", createstep.CreationPath);

            environment.testCase.SetupSteps.Add(createstep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="applicationName"></param>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void createTxtTestFile(BizTalkTestEnvironment environment, string applicationName, string sourceFile, string destinationFile)
        {
            Log.DebugFormat("createTxtTestFile(BizTalkTestEnvironment environment {0}, string applicationName {1}, string sourceFile {2}, string destinationFile {3})", environment, applicationName, sourceFile, destinationFile);

            CreateStep createstep;

            createstep = new CreateStep();

            //loading the test file
            FileDataLoader txtdataloader = new FileDataLoader();

            try
            {
                txtdataloader.FilePath = Path.Combine(environment.testfileApplicationDirectories[applicationName], sourceFile);
                createstep.CreationPath = Path.Combine(environment.infrastructureApplicationDirectories[applicationName], destinationFile);
                createstep.DataSource = txtdataloader;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { environment, applicationName, sourceFile, destinationFile }, ex ));
            }

            Log.DebugFormat("txtdataloader.FilePath {0}", txtdataloader.FilePath);
            Log.DebugFormat("createstep.CreationPath {0}", createstep.CreationPath);

            environment.testCase.SetupSteps.Add(createstep);
        }

        public static void fileRead(BizTalkTestEnvironment environment, string applicationName, string searchPattern, int expectedNumberOfFiles, int timeout)
        {
            Log.DebugFormat("fileRead(BizTalkTestEnvironment environment {0}, string applicationName {1}, string searchPattern {2}, int expectedNumberOfFiles {3}, int timeout {4})", environment, applicationName, searchPattern, expectedNumberOfFiles, timeout);

            prepareFileRead(environment, environment.infrastructureApplicationDirectories[applicationName], searchPattern, expectedNumberOfFiles, timeout);
        }

        public static void fileRead(BizTalkTestEnvironment environment, string applicationName, string subDirectory, string searchPattern, int expectedNumberOfFiles, int timeout)
        {
            Log.DebugFormat("fileRead(BizTalkTestEnvironment environment {0}, string applicationName {1}, string subDirectory {2}, string searchPattern {3}, int expectedNumberOfFiles {4}, int timeout {5})", environment, applicationName, subDirectory, searchPattern, expectedNumberOfFiles, timeout);

            prepareFileRead(environment, Path.Combine(environment.infrastructureApplicationDirectories[applicationName], subDirectory), searchPattern, expectedNumberOfFiles, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        public static void runTest(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("runTest(BizTalkTestEnvironment environment {0})", environment);

            Log.DebugFormat("environment.baseInfrastructureDirectory {0}", environment.baseInfrastructureDirectory);
            Log.DebugFormat("environment.baseTestFileDirectory {0}", environment.baseTestFileDirectory);
            Log.DebugFormat("environment.baseTestProjectPath {0}", environment.baseTestProjectPath);

            BizUnit.BizUnit bizUnit = new BizUnit.BizUnit(environment.testCase);

            bizUnit.RunTest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        public static void cleanTestSteps(BizTalkTestEnvironment environment)
        {
            Log.DebugFormat("cleanTestSteps(BizTalkTestEnvironment environment {0})", environment);

            environment.testCase.SetupSteps.Clear();
        }

        private static void prepareFileRead(BizTalkTestEnvironment environment, string directoryPath, string searchPattern, int expectedNumberOfFiles, int timeout)
        {
            Log.DebugFormat("prepareFileRead(BizTalkTestEnvironment environment {0}, string directoryPath {1}, string searchPattern {2}, int expectedNumberOfFiles {3}, int timeout {4})", environment, directoryPath, searchPattern, expectedNumberOfFiles, timeout);

            FileReadMultipleStep filereadmultiplestep;

            filereadmultiplestep = new FileReadMultipleStep();
            filereadmultiplestep.DirectoryPath = directoryPath;
            filereadmultiplestep.SearchPattern = searchPattern;
            filereadmultiplestep.ExpectedNumberOfFiles = expectedNumberOfFiles;
            filereadmultiplestep.Timeout = timeout;

            environment.testCase.SetupSteps.Add(filereadmultiplestep);
        }
    }
}
