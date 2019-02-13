using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;

namespace inSyca.foundation.integration.visualstudio.wizard_40
{
    public class EEBWizard : IWizard
    {
        private DTE _dte;
        private string templateDirectory;

        SolutionFolder solutionFolder;
        //SolutionFolder unittestMainFolder;
        //SolutionFolder unittestEntityFolder;

        Project deploymentProject;
        Project customProject;
        Project documentationProject;
        Project hostsProject;
        Project hostsDevProject;
        Project hostsPrdProject;
        Project hostsTstProject;
        Project environmentsettingsProject;
        Project ressourcesProject;
        //Project testfilesMainProject;
        //Project testfilesEntityProject;

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

            deploymentProject = solution.AddSolutionFolder("_deployment");

            solutionFolder = (SolutionFolder)deploymentProject.Object;

            customProject = solutionFolder.AddSolutionFolder("custom");
            documentationProject = solutionFolder.AddSolutionFolder("documentation");
            hostsProject = solutionFolder.AddSolutionFolder("hosts");
            hostsDevProject = ((SolutionFolder)hostsProject.Object).AddSolutionFolder("dev");
            hostsPrdProject = ((SolutionFolder)hostsProject.Object).AddSolutionFolder("prd");
            hostsTstProject = ((SolutionFolder)hostsProject.Object).AddSolutionFolder("tst");
            environmentsettingsProject = solutionFolder.AddSolutionFolder("environmentsettings");
            ressourcesProject = solutionFolder.AddSolutionFolder("ressources");

            // Get the project template
            templateDirectory = solution.GetProjectTemplate("EEB.csharp.zip", "BizTalk");

            foreach (Project project in solution.Projects)
            {
                if (project.Name.Contains(".deployment"))
                {
                    RebuildDeployment(project);
                    solution.Remove(project);
                }
//                else if (project.Name.Contains("_unittest"))
//                {
//                    unittestMainFolder = (SolutionFolder)project.Object;
////                    testfilesMainProject = unittestMainFolder.AddSolutionFolder("biztalk.testfiles");
//                    unittestEntityFolder = (SolutionFolder)testfilesMainProject.Object;
//                    testfilesEntityProject = unittestEntityFolder.AddSolutionFolder("ci");
                    
//                    foreach (ProjectItem unittest in project.ProjectItems)
//                    {
//                        if(unittest.Name.Contains(".unittest"))
//                        {
//                            Project unittestProject = (Project)unittest.Object;
//                            //AddTestFileDirectories(unittestProject);
//                        }
//                    }
//                }
            }

            _dte.Documents.CloseAll();
        }

        //private void AddTestFileDirectories(Project project)
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo(project.FileName);
        //    DirectoryInfo testfilesDirectory = Directory.CreateDirectory(string.Format(@"{0}\biztalk.testfiles", dirInfo.Parent.FullName));
        //    DirectoryInfo entityfilesDirectory = Directory.CreateDirectory(string.Format(@"{0}\ci", dirInfo.Parent.FullName));

        //    string placeholderFile = string.Format(@"{0}\testfile.txt", entityfilesDirectory.FullName);
            
        //    File.Create(placeholderFile);

        //    for (int i = 0; i < 10; i++)
        //    {
        //        try
        //        {
        //            testfilesEntityProject.ProjectItems.AddFromFile(placeholderFile);
        //            break;
        //        }
        //        catch
        //        { 
        //        }
        //    }
        //}

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

            foreach (var file in dirInfoTarget.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (file.Extension == ".btdfproj")
                    AssembleDeploymentProjectFile(file);

                deploymentProject.ProjectItems.AddFromFile(file.FullName);
            }

            foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("custom", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                customProject.ProjectItems.AddFromFile(file.FullName);
            }

            foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("documentation", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                    documentationProject.ProjectItems.AddFromFile(file.FullName);
            }

            foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("hosts", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                    hostsProject.ProjectItems.AddFromFile(file.FullName);

                foreach (var dirDev in dirCustom.EnumerateDirectories("dev", SearchOption.TopDirectoryOnly))
                    foreach (var file in dirDev.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                        hostsDevProject.ProjectItems.AddFromFile(file.FullName);

                foreach (var dirPrd in dirCustom.EnumerateDirectories("prd", SearchOption.TopDirectoryOnly))
                    foreach (var file in dirPrd.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                        hostsPrdProject.ProjectItems.AddFromFile(file.FullName);

                foreach (var dirTst in dirCustom.EnumerateDirectories("tst", SearchOption.TopDirectoryOnly))
                    foreach (var file in dirTst.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                        hostsTstProject.ProjectItems.AddFromFile(file.FullName);
            }

            foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("environmentsettings", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                    environmentsettingsProject.ProjectItems.AddFromFile(file.FullName);
            }

            foreach (var dirCustom in dirInfoTarget.EnumerateDirectories("ressources", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in dirCustom.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
                    ressourcesProject.ProjectItems.AddFromFile(file.FullName);
            }
        }

        private void AssembleDeploymentProjectFile(FileInfo file)
        {
            string[] projectnameparts = file.Directory.Parent.Name.Split('.');

            string content = File.ReadAllText(file.FullName);

            content = content.Replace("${MANUFACTURER}", "inSyca IT Solutions GmbH");
            content = content.Replace("${PRODUCTID}", Guid.NewGuid().ToString());
            content = content.Replace("${PRODUCTUPGRADECODE}", Guid.NewGuid().ToString());

            if(projectnameparts.Length > 0)
                content = content.Replace("${COMPANY}", projectnameparts[0]);

            if (projectnameparts.Length > 2)
                content = content.Replace("${MAINPROCESS}", projectnameparts[2]);

            if (projectnameparts.Length > 3)
                content = content.Replace("${SUBPROCESS}", projectnameparts[3]);


            File.WriteAllText(file.FullName, content);
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
