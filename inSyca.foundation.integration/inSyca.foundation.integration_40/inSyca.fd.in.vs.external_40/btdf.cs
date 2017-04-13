using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace inSyca.foundation.integration.visualstudio.external
{
    public class btdf
    {
        public event EventHandler<OutputWindowEventArgs> eventActivateOutputWindow;
        public event EventHandler<OutputWindowEventArgs> eventWriteToOutputWindow;

        public string solutionFileName { get; set; }
        public string configurationName { get; set; }

        public btdf()
        {
        }

        internal enum VSVersion
        {
            Vs2005,
            Vs2008,
            Vs2010,
            Vs2012,
            Vs2013,
            Vs2015,
        };

        internal int IsBusy = 0;
        private delegate void RunHandler(string exePath, string arguments);

        public void Execute()
        {
            string projectPath = GetDeploymentProjectPath(solutionFileName);
            string arguments = string.Format("\"{0}\" /nologo /t:Installer /p:Configuration={1}", projectPath, configurationName);
            ExecuteBuild(GetMsBuildPath(VSVersion.Vs2015), arguments);
        }
        internal static string GetMsBuildPath(VSVersion ideVersion)
        {
            const string VS2015KEY = @"SOFTWARE\Microsoft\MSBuild\ToolsVersions\14.0";
            //const string VS2008KEY = @"SOFTWARE\Microsoft\MSBuild\ToolsVersions\3.5";
            //const string VS2010KEY = @"SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0";
            //const string VS2013KEY = @"SOFTWARE\Microsoft\MSBuild\ToolsVersions\12.0";

            //if (ideVersion == VSVersion.Vs2015)
            //{
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(VS2015KEY, false))
            {
                return string.Format("{0}MSBuild.exe", (string)rk.GetValue("MSBuildToolsPath"));
            }
            //}
            //else if (ideVersion == VSVersion.Vs2010 || ideVersion == VSVersion.Vs2012 || ideVersion == VSVersion.Vs2013)
            //{
            //    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(VS2010KEY, false))
            //    {
            //        return string.Format("{0}MSBuild.exe", (string)rk.GetValue("MSBuildToolsPath"));
            //    }
            //}
            //else if (ideVersion == VSVersion.Vs2013)
            //{
            //    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(VS2013KEY, false))
            //    {
            //        return string.Format("{0}MSBuild.exe", (string)rk.GetValue("MSBuildToolsPath"));
            //    }
            //}
            //else
            //{
            //    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(VS2005KEY, false))
            //    {
            //        return string.Format("{0}MSBuild.exe", (string)rk.GetValue("MSBuildBinPath"));
            //    }
            //}
        }

        internal static string GetDeploymentProjectPath(string solutionPath)
        {
            //string solutionPath = _applicationObject.Solution.FileName;
            string solutionFilenameNoExt = Path.GetFileNameWithoutExtension(solutionPath);

            // First look for <solutionNameNoExtension>.Deployment\<solutionNameNoExtension>.Deployment.btdfproj
            string projectPath = Path.Combine(Path.GetDirectoryName(solutionPath), solutionFilenameNoExt + ".Deployment");
            projectPath = Path.Combine(projectPath, solutionFilenameNoExt + ".Deployment.btdfproj");

            if (!File.Exists(projectPath))
            {
                // Next look for <solutionNameNoExtension>.Deployment\Deployment.btdfproj
                projectPath = Path.Combine(Path.GetDirectoryName(solutionPath), solutionFilenameNoExt + ".Deployment");
                projectPath = Path.Combine(projectPath, "Deployment.btdfproj");

                if (!File.Exists(projectPath))
                {
                    // Next look for Deployment\<solutionNameNoExtension>.Deployment.btdfproj
                    projectPath = Path.Combine(Path.GetDirectoryName(solutionPath), "Deployment");
                    projectPath = Path.Combine(projectPath, solutionFilenameNoExt + ".Deployment.btdfproj");

                    if (!File.Exists(projectPath))
                    {
                        // Next look for Deployment\Deployment.btdfproj
                        projectPath = Path.Combine(Path.GetDirectoryName(solutionPath), "Deployment");
                        projectPath = Path.Combine(projectPath, "Deployment.btdfproj");

                        if (!File.Exists(projectPath))
                        {
                            System.Windows.Forms.MessageBox.Show(
                                "Could not find a .btdfproj file for this solution. Valid locations relative to the solution root are: <solutionNameNoExtension>.Deployment\\<solutionNameNoExtension>.Deployment.btdfproj, <solutionNameNoExtension>.Deployment\\Deployment.btdfproj, Deployment\\<solutionNameNoExtension>.Deployment.btdfproj or Deployment\\Deployment.btdfproj.",
                                "Deployment Framework for BizTalk",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }

            return projectPath;
        }

        internal void ExecuteBuild(string exePath, string arguments)
        {
            SetBusy();

            eventActivateOutputWindow?.Invoke(this, new OutputWindowEventArgs() { Name = "Deployment Framework for BizTalk" });

            RunHandler rh = new RunHandler(Run);
            AsyncCallback callback = new AsyncCallback(RunCallback);
            rh.BeginInvoke(exePath, arguments, callback, rh);
        }

        private void SetBusy()
        {
            Interlocked.CompareExchange(ref IsBusy, 1, 0);
        }

        private void SetFree()
        {
            Interlocked.CompareExchange(ref IsBusy, 0, 1);
        }

        private void Run(string exePath, string arguments)
        {
            RunProcess(exePath, arguments);
        }
        private void RunCallback(IAsyncResult result)
        {
            RunHandler rh = result.AsyncState as RunHandler;

            try
            {
                rh.EndInvoke(result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Deployment Framework for BizTalk: Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetFree();
            }
        }


        private void RunProcess(string exePath, string arguments)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            if (eventWriteToOutputWindow != null)
            {
                eventWriteToOutputWindow(this, new OutputWindowEventArgs() { Name= "Deployment Framework for BizTalk", Text = "Starting build..." });
                eventWriteToOutputWindow(this, new OutputWindowEventArgs() { Name = "Deployment Framework for BizTalk", Text = exePath + " " + arguments });
                eventWriteToOutputWindow(this, new OutputWindowEventArgs() { Name = "Deployment Framework for BizTalk", Text = string.Empty });
            }

            proc.StartInfo.FileName = exePath;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;

            proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_OutputDataReceived);
            proc.Start();

            proc.BeginOutputReadLine();
            proc.WaitForExit();
        }

        private void proc_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            eventWriteToOutputWindow?.Invoke(this, new OutputWindowEventArgs() { Name = "Deployment Framework for BizTalk", Text = e.Data });
        }
    }
}
