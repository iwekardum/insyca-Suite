using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TemplateWizard;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using System.IO;

namespace inSyca.foundation.communication.visualstudio.wizard_40
{
    public class MessagebrokerWizard : IWizard
    {
        private DTE _dte;
        private string templateDirectory;

        SolutionFolder solutionFolder;

        Project deploymentFolder;
 
        // This method is called before opening any item that 
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
            Solution2 solution = ((Solution2)_dte.Solution);

            deploymentFolder = solution.AddSolutionFolder("_deployment");

            solutionFolder = (SolutionFolder)deploymentFolder.Object;

            // Get the project template
            templateDirectory = solution.GetProjectTemplate("Messagebroker.csharp.zip", "CSharp");

            foreach (Project project in solution.Projects)
            {
                if (project.Name.Contains(".deployment"))
                {
                    RebuildDeployment(project);
                    solution.Remove(project);
                }
            }

            _dte.Documents.CloseAll();
        }

        private void RebuildDeployment(Project project)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(project.FileName);
            DirectoryInfo dirInfoSource = dirInfo.Parent;
            DirectoryInfo dirInfoParent = dirInfoSource.Parent;
            string strDirectoryTarget = string.Format("{0}.deployment", dirInfoParent.FullName);

            Directory.Move(dirInfoSource.FullName, strDirectoryTarget);

            DirectoryInfo dirInfoTarget = new DirectoryInfo(strDirectoryTarget);

            foreach (var file in dirInfoTarget.EnumerateFiles("*.csproj", SearchOption.TopDirectoryOnly))
                file.Delete();

            foreach (var file in dirInfoTarget.EnumerateFiles("*.wixproj", SearchOption.TopDirectoryOnly))
                solutionFolder.AddFromFile(file.FullName);
            //    deploymentFolder.ProjectItems.AddFromFile(file.FullName);

            //foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("custom", SearchOption.TopDirectoryOnly))
            //{
            //    foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            //        customFolder.ProjectItems.AddFromFile(file.FullName);
            //}

            //foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("documentation", SearchOption.TopDirectoryOnly))
            //{
            //    foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            //        documentationFolder.ProjectItems.AddFromFile(file.FullName);
            //}

            //foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("batch", SearchOption.TopDirectoryOnly))
            //{
            //    foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            //        batchFolder.ProjectItems.AddFromFile(file.FullName);
            //}

            //foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("ressources", SearchOption.TopDirectoryOnly))
            //{
            //    foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            //        ressourcesFolder.ProjectItems.AddFromFile(file.FullName);
            //}
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = (DTE)automationObject;
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
