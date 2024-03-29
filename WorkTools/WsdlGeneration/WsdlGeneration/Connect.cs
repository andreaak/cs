using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WsdlGeneration
{
    public delegate void AddedEventHandler();
    public delegate void InsertEventHandler(int index);

    internal enum InfoLevel
    {
        Info,
        Warning,
        Error,
        Canceled
    }

    public enum ParseMessageType
    {
        Error = 2,
        Info = 0,
        None = -1,
        Question = 3,
        Warning = 1
    }

    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        // Fields
        private AddIn _addInInstance;
        private DTE2 _applicationObject;
        private const string ConfigFileName = "WsdlGeneration.config";
        private InfoMessagesListBox infoMessagesListBox;
        private Window m_toolWin;
        private const string ToolName = "WsdlGeneration";
        private const string WsdlFileExtension = "wsdl";
        private bool generateSelectedFile = true;
        private bool checkModificationTime = false;
        private Window activeWindow;


        #region CONNECT
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        public void OnBeginShutdown(ref Array custom)
        {
        }

        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            System.Threading.Thread.Sleep(0x3ed);
            this._applicationObject = (DTE2)application;
            this._addInInstance = (AddIn)addInInst;
            if ((connectMode == ext_ConnectMode.ext_cm_AfterStartup) || (connectMode == ext_ConnectMode.ext_cm_Startup))
            {
                try
                {
                    activeWindow = _applicationObject.ActiveWindow;
                    object controlObject = null;
                    string str = "WsdlGeneration.InfoMessagesListBox";
                    string guidPosition = "{A30BE595-67D8-4204-B6C1-728A604884A6}";
                    Assembly executingAssembly = Assembly.GetExecutingAssembly();
                    this.m_toolWin = ((Windows2)this._applicationObject.Windows).CreateToolWindow2(this._addInInstance, executingAssembly.Location, str, "Wsdl Generation Info", guidPosition, ref controlObject);
                    this.infoMessagesListBox = (InfoMessagesListBox)controlObject;
                    this.m_toolWin.Visible = true;
                    this.m_toolWin.Height = 300;
                    this.m_toolWin.Width = 500;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                    Debug.WriteLine(exception.StackTrace);
                }
            }
            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                Microsoft.VisualStudio.CommandBars.CommandBars commandBars = (Microsoft.VisualStudio.CommandBars.CommandBars)this._applicationObject.CommandBars;
                CommandBar bar = commandBars["MenuBar"];
                CommandBarPopup popup = (CommandBarPopup)bar.Controls["Tools"];
                this.AddPopupCommand(popup, "WsdlGeneration", "WsdlGeneration", "Run WsdlGeneration", 0);
            }
        }

        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        public void OnStartupComplete(ref Array custom)
        {
        }

        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                ReferenceGenerator generator = null;
                if ((commandName == "WsdlGeneration.Connect.WsdlGeneration") || (commandName == "WsdlGeneration.Connect.WsdlGenerationButton"))
                {
                    if (((this._applicationObject.SelectedItems.Count <= 0) || (this._applicationObject.SelectedItems.SelectionContainer == null)) || (this._applicationObject.SelectedItems.SelectionContainer.Count <= 0))
                    {
                        MessageBox.Show("Select any item in project with wsdl files.", "WsdlGeneration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Project project = this.GetProject();
                        if (project == null)
                        {
                            MessageBox.Show("Select any item in project with wsdl files.", "WsdlGeneration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (project.FileName.Length == 0)
                        {
                            MessageBox.Show("Cannot generate code for items outside of a project.", "WsdlGeneration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            string filePath = this.FindConfigurationFile(project);
                            if (filePath.Length == 0)
                            {
                                MessageBox.Show("There is no WsdlGeneration.config file in the project.", "WsdlGeneration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                GenerationConfiguration configuration = new GenerationConfiguration(filePath);
                                List<ProjectItemEx> projectWsdlFiles = this.GetProjectWsdlFiles(project, configuration, generateSelectedFile);
                                if (!((this.m_toolWin == null) || this.m_toolWin.Visible))
                                {
                                    this.m_toolWin.Visible = true;
                                }
                                if (this.infoMessagesListBox != null)
                                {
                                    InfoMessages messages = new InfoMessages();
                                    messages.AddInfoMessage(InfoLevel.Info, string.Empty, "References generation in progress...");
                                    this.infoMessagesListBox.Messages = messages;
                                }
                                generator = new ReferenceGenerator(projectWsdlFiles, configuration, checkModificationTime);
                                generator.Generate();
                                if ((this.infoMessagesListBox != null) && (generator != null))
                                {
                                    this.infoMessagesListBox.Messages = generator.InfoMessages;
                                }
                                handled = true;
                            }
                        }
                    }
                }
            }
        }

        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if ((neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone) && ((commandName == "WsdlGeneration.Connect.WsdlGeneration") || (commandName == "WsdlGeneration.Connect.WsdlGenerationButton")))
            {
                status = vsCommandStatus.vsCommandStatusEnabled | vsCommandStatus.vsCommandStatusSupported;
            }
        }

        private CommandBar AddCommandBar(string name, MsoBarPosition position)
        {
            Microsoft.VisualStudio.CommandBars.CommandBars commandBars = (Microsoft.VisualStudio.CommandBars.CommandBars)this._applicationObject.CommandBars;
            CommandBar bar = null;
            try
            {
                try
                {
                    bar = commandBars.Add(name, position, false, false);
                }
                catch (ArgumentException)
                {
                    bar = commandBars[name];
                }
            }
            catch
            {
            }
            return bar;
        }

        private CommandBar AddCommandMenu(string name)
        {
            Microsoft.VisualStudio.CommandBars.CommandBars commandBars = (Microsoft.VisualStudio.CommandBars.CommandBars)this._applicationObject.CommandBars;
            CommandBar bar = null;
            try
            {
                try
                {
                    bar = commandBars.Add(name, MsoBarPosition.msoBarPopup, false, false);
                }
                catch (ArgumentException)
                {
                    bar = commandBars[name];
                }
            }
            catch
            {
            }
            return bar;
        }

        private void AddPopupCommand(CommandBarPopup popup, string name, string label, string ttip, int iconIdx)
        {
            if (popup != null)
            {
                Commands2 commands = (Commands2)this._applicationObject.Commands;
                object[] contextUIGUIDs = new object[0];
                try
                {
                    Command command = commands.AddNamedCommand2(this._addInInstance, name, label, ttip, false, null, ref contextUIGUIDs, 3, 2, vsCommandControlType.vsCommandControlTypeButton);
                    if ((command != null) && (popup != null))
                    {
                        command.AddControl(popup.CommandBar, 1);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private void AddToolbarCommand(CommandBar bar, string name, string label, string ttip, int iconIdx)
        {
            if (bar != null)
            {
                Commands2 commands = (Commands2)this._applicationObject.Commands;
                object[] contextUIGUIDs = new object[0];
                try
                {
                    Command command = commands.AddNamedCommand2(this._addInInstance, name, label, ttip, false, null, ref contextUIGUIDs, 3, 2, vsCommandControlType.vsCommandControlTypeButton);
                    if ((command != null) && (bar != null))
                    {
                        command.AddControl(bar, 1);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }

        #endregion
        
        
        private string CreateNamespaceFragment(string parent, string fragment)
        {
            if (fragment == null)
            {
                return parent;
            }
            fragment = fragment.ToLower();
            string str = fragment;
            if (((fragment.StartsWith("gcm") || fragment.StartsWith("ctg")) || (fragment.StartsWith("hla") || fragment.StartsWith("flw"))) || fragment.StartsWith("mol"))
            {
                str = fragment.Substring(3);
                fragment = fragment.Substring(0, 3);
                fragment = char.ToUpper(fragment[0]).ToString() + fragment.Substring(1);
            }
            else
            {
                fragment = string.Empty;
            }
            if (str.Length > 1)
            {
                str = char.ToUpper(str[0]).ToString() + str.Substring(1);
            }
            if ((parent == null) || (parent.Length == 0))
            {
                fragment = fragment + str + "WebReferences";
            }
            else
            {
                fragment = fragment + str;
            }
            if ((parent != null) && (parent.Length > 0))
            {
                return (parent + "." + fragment);
            }
            return fragment;
        }

        private string FindConfigurationFile(Project project)
        {
            foreach (ProjectItem item in project.ProjectItems)
            {
                if (item.Name == "WsdlGeneration.config")
                {
                    return item.get_FileNames(1);
                }
            }
            return string.Empty;
        }

        private Project GetProject()
        {
            Project containingProject = this._applicationObject.SelectedItems.Item(1).Project;
            if (containingProject == null)
            {
                ProjectItem projectItem = this._applicationObject.SelectedItems.Item(1).ProjectItem;
                if (projectItem != null)
                {
                    containingProject = projectItem.ContainingProject;
                }
            }
            return containingProject;
        }

        private List<ProjectItemEx> GetProjectWsdlFiles(Project project, GenerationConfiguration configuration, bool generateSelectedFile)
        {
            List<ProjectItemEx> list = new List<ProjectItemEx>();
            if (project != null)
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.Name == configuration.WsdlRootItem)
                    {
                        if (!generateSelectedFile)
                        {
                            list.AddRange(this.GetWsdlSubItems(item, string.Empty));
                        }
                        else
                        {
                            ProjectItem file = activeWindow.ProjectItem;
                            if (file == null)
                            {
                                continue;
                            }
                            string filePath = file.get_FileNames(1);
                            if (filePath.EndsWith(".wsdl"))
                            {
                                list.AddRange(this.GetCurrentItem(item, string.Empty, filePath));
                            }
                        }
                    }
                }
            }
            return list;
        }

        private List<ProjectItemEx> GetWsdlSubItems(ProjectItem item, string parentItemName)
        {
            List<ProjectItemEx> list = new List<ProjectItemEx>();
            if (item.ProjectItems.Count != 0)
            {
                string str = parentItemName;
                foreach (ProjectItem item2 in item.ProjectItems)
                {
                    parentItemName = str;
                    if (!item2.Name.EndsWith(".wsdl"))
                    {
                        if (parentItemName.Length > 0)
                        {
                            parentItemName = parentItemName + ".";
                        }
                        parentItemName = parentItemName + item2.Name;
                    }
                    list.AddRange(this.GetWsdlSubItems(item2, parentItemName));
                    if (item2.Name.EndsWith(".wsdl"))
                    {
                        ProjectItemEx ex = new ProjectItemEx(item2, parentItemName);
                        list.Add(ex);
                    }
                }
            }
            return list;
        }

        private List<ProjectItemEx> GetCurrentItem(ProjectItem item, string parentItemName, string fileName)
        {
            List<ProjectItemEx> list = new List<ProjectItemEx>();
            if (item.ProjectItems.Count != 0)
            {
                string str = parentItemName;
                foreach (ProjectItem item2 in item.ProjectItems)
                {
                    parentItemName = str;
                    if (!item2.Name.EndsWith(".wsdl"))
                    {
                        if (parentItemName.Length > 0)
                        {
                            parentItemName = parentItemName + ".";
                        }
                        parentItemName = parentItemName + item2.Name;
                    }
                    list.AddRange(this.GetCurrentItem(item2, parentItemName, fileName));
                    if (item2.Name.EndsWith(".wsdl") && item2.get_FileNames(1).Equals(fileName))
                    {
                        ProjectItemEx ex = new ProjectItemEx(item2, parentItemName);
                        list.Add(ex);
                    }
                }
            }
            return list;
        }

        private void RecursiveWsdlGeneration(ProjectItem item, GenerationConfiguration configuration, string namespaceFragment)
        {
            if (item.ProjectItems.Count != 0)
            {
                foreach (ProjectItem item2 in item.ProjectItems)
                {
                    this.RecursiveWsdlGeneration(item2, configuration, this.CreateNamespaceFragment(namespaceFragment, item2.Name));
                    if (item2.Name.EndsWith(".wsdl"))
                    {
                        string path = item2.get_FileNames(1);
                        WsdlExeRunner runner = new WsdlExeRunner();
                        runner.Namespace = namespaceFragment + "." + Path.GetFileNameWithoutExtension(path);
                        runner.InputFilePath = path;
                        runner.OutputFilePath = Path.Combine(configuration.OutputDirectory, Path.GetFileNameWithoutExtension(path) + ".cs");
                        runner.Run();
                    }
                }
            }
        }
    }
}