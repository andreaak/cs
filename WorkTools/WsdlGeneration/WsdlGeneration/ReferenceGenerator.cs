using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using System.IO;
using System.Xml;

namespace WsdlGeneration
{
    internal class ReferenceGenerator
    {
        // Fields
        private GenerationConfiguration configuration;
        private InfoMessages info;
        private bool checkModificationTime;
        private const string ServiceNamXmlAttribute = "name";
        private const string ServiceXmlTag = "service";
        private List<ProjectItemEx> wsdlFiles;

        // Methods
        public ReferenceGenerator(List<ProjectItemEx> wsdlFiles, GenerationConfiguration configuration, bool checkModificationTime)
        {
            this.wsdlFiles = wsdlFiles;
            this.configuration = configuration;
            this.checkModificationTime = checkModificationTime;
            this.info = new InfoMessages();
        }

        private string CreateNamespace(ProjectItemEx projectItem)
        {
            string str = string.Empty;
            string itemPath = projectItem.ItemPath;
            string[] strArray = itemPath.Split(new char[] { '.' });
            if (strArray != null)
            {
                if (strArray.Length > 0)
                {
                    str = strArray[0] + this.configuration.NamespaceRootSuffix;
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        str = str + "." + strArray[i];
                    }
                }
                else
                {
                    str = itemPath;
                }
            }
            string errorMessage = string.Empty;
            return (str + "." + this.GetServiceName(projectItem.ProjectItem.get_FileNames(1), out errorMessage));
        }

        private bool cutCommonTypeContents(ref string fileTxt, string commonTypeContents)
        {
            bool flag = false;
            if (fileTxt.Contains(commonTypeContents))
            {
                int index = fileTxt.IndexOf(commonTypeContents);
                fileTxt = fileTxt.Substring(0, index) + fileTxt.Substring(index + commonTypeContents.Length);
                flag = true;
            }
            return flag;
        }

        private void ExtractCommonTypes(ProjectItems projectItems, string projectPath, List<SharedTypesInfo> sharedTypesToProcess)
        {
            foreach (SharedTypesInfo info in sharedTypesToProcess)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string path = Path.Combine(projectPath, info.CommonTypeFile);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                string str2 = (string.Empty + "namespace " + info.CommonTypeNamespace + " {\n") + "\tusing System.Diagnostics;\n\tusing System.Web.Services;\n\tusing System.ComponentModel;\n\tusing System.Web.Services.Protocols;\n\tusing System;\n\tusing System.Xml.Serialization;\n";
                foreach (string str3 in info.CommonTypesReferences)
                {
                    str2 = str2 + "\tusing " + str3 + ";\n";
                }
                foreach (string str4 in info.WsdlFiles)
                {
                    string str5 = Path.Combine(projectPath, Path.GetFileNameWithoutExtension(str4) + ".cs");
                    StreamReader reader = new StreamReader(str5);
                    string fileTxt = reader.ReadToEnd();
                    reader.Close();
                    bool flag = false;
                    foreach (string str7 in info.CommonTypes)
                    {
                        string commonTypeContents = string.Empty;
                        if (this.getCommonTypeContents(fileTxt, str7, out commonTypeContents) != -1)
                        {
                            if (!dictionary.ContainsKey(str7))
                            {
                                str2 = str2 + "\n";
                                str2 = str2 + commonTypeContents;
                                dictionary.Add(str7, commonTypeContents);
                                this.cutCommonTypeContents(ref fileTxt, commonTypeContents);
                                flag = true;
                            }
                            else if (dictionary[str7] == commonTypeContents)
                            {
                                this.cutCommonTypeContents(ref fileTxt, commonTypeContents);
                                flag = true;
                            }
                            else
                            {
                                this.info.AddInfoMessage(InfoLevel.Warning, "Exception", "Different type '" + str7 + "' in WSDL files.");
                            }
                        }
                    }
                    if (flag)
                    {
                        int index = fileTxt.IndexOf("using");
                        if (index != -1)
                        {
                            string line = string.Empty;
                            index = this.getNextLine(fileTxt, index, out line);
                            for (line = line.Trim(); line.StartsWith("using"); line = line.Trim())
                            {
                                index = this.getNextLine(fileTxt, index, out line);
                            }
                            fileTxt = fileTxt.Substring(0, index) + "\tusing " + info.CommonTypeNamespace + ";\n" + fileTxt.Substring(index);
                        }
                        else
                        {
                            fileTxt = "\tusing " + info.CommonTypeNamespace + ";\n" + fileTxt;
                        }
                        File.Delete(str5);
                        StreamWriter writer = new StreamWriter(str5);
                        writer.Write(fileTxt);
                        writer.Close();
                    }
                }
                str2 = str2 + "}\n";
                StreamWriter writer2 = new StreamWriter(path);
                writer2.Write(str2);
                writer2.Close();
                if (!this.ProjectContainsFile(projectItems.ContainingProject, path))
                {
                    try
                    {
                        projectItems.AddFromFile(path);
                        this.info.AddInfoMessage(InfoLevel.Info, string.Empty, "File added to project\n" + path);
                    }
                    catch (Exception exception)
                    {
                        this.info.AddInfoMessage(InfoLevel.Error, "Exception", "Error while adding generated file to project.\nCan't add file " + path + " to project.\n" + exception.Message);
                    }
                }
            }
        }

        private bool FindFile(ProjectItem item, string generatedFile)
        {
            bool flag = false;
            foreach (ProjectItem item2 in item.ProjectItems)
            {
                if ((item2.get_FileNames(1) != null) && (item2.get_FileNames(1).ToLower() == generatedFile.ToLower()))
                {
                    return true;
                }
                flag = this.FindFile(item2, generatedFile);
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        private List<SharedTypesInfo> FindFileSharedTypesInfoList(ProjectItemEx wsdlFile)
        {
            List<SharedTypesInfo> list = new List<SharedTypesInfo>();
            foreach (SharedTypesInfo info in this.configuration.SharedTypes)
            {
                foreach (string str in info.WsdlFiles)
                {
                    if (wsdlFile.ProjectItem.get_FileNames(1).EndsWith(@"\" + str))
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }

        public void Generate()
        {
            Action<SharedTypesInfo> action = null;
            List<SharedTypesInfo> sharedTypesToProcess = new List<SharedTypesInfo>();
            foreach (ProjectItemEx ex in this.wsdlFiles)
            {
                string path = ex.ProjectItem.get_FileNames(1);
                string str2 = Path.Combine(this.configuration.OutputDirectory, Path.GetFileNameWithoutExtension(path) + ".cs");
                if (!checkModificationTime || File.GetLastWriteTime(path) >= File.GetLastWriteTime(str2))
                {
                    if (action == null)
                    {
                        action = delegate(SharedTypesInfo sharedType)
                        {
                            if (!sharedTypesToProcess.Contains(sharedType))
                            {
                                sharedTypesToProcess.Add(sharedType);
                            }
                        };
                    }
                    this.FindFileSharedTypesInfoList(ex).ForEach(action);
                }
            }
            bool flag = false;
            foreach (ProjectItemEx ex in this.wsdlFiles)
            {
                if (!this.configuration.ValidateServiceName || this.ValidateWsdlServiceName(ex.ProjectItem.get_FileNames(1)))
                {
                    string filePath = this.GenerateServiceReference(ex, this.ShareTypesInfoListContainsFile(sharedTypesToProcess, ex) || !checkModificationTime);
                    if (filePath.Length > 0)
                    {
                        Exception exception;
                        ProcessGeneratedFile file = new ProcessGeneratedFile(filePath);
                        try
                        {
                            file.RunProcessor();
                            this.info.AddInfoMessages(file.ProcessInfo);
                            if (!this.ProjectContainsFile(ex.ProjectItem.ContainingProject, filePath))
                            {
                                try
                                {
                                    ex.ProjectItem.Collection.AddFromFile(filePath);
                                    this.info.AddInfoMessage(InfoLevel.Info, string.Empty, "File added to project\n" + filePath);
                                }
                                catch (Exception exception1)
                                {
                                    exception = exception1;
                                    this.info.AddInfoMessage(InfoLevel.Error, "Exception", "Error while adding generated file to project.\nCan't add file " + filePath + " to project.\n" + exception.Message);
                                }
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            this.info.AddInfoMessage(InfoLevel.Error, "Exception", "Error while processing generated file. \n" + exception.Message);
                        }
                        flag |= this.ShareTypesInfoListContainsFile(this.configuration.SharedTypes, ex);
                    }
                }
            }
            if (flag)
            {
                this.ExtractCommonTypes(this.wsdlFiles[0].ProjectItem.ProjectItems, this.configuration.OutputDirectory, sharedTypesToProcess);
            }
        }

        private string GenerateServiceReference(ProjectItemEx wsdlFile, bool forceGenerate)
        {
            string outputFilePath = string.Empty;
            WsdlExeRunner runner = new WsdlExeRunner();
            try
            {
                runner.Namespace = this.CreateNamespace(wsdlFile);
                string path = wsdlFile.ProjectItem.get_FileNames(1);
                runner.InputFilePath = path;
                runner.OutputFilePath = Path.Combine(this.configuration.OutputDirectory, Path.GetFileNameWithoutExtension(path) + ".cs");
                runner.ForceGenerate = forceGenerate;
                if (runner.Run())
                {
                    outputFilePath = runner.OutputFilePath;
                }
            }
            catch (Exception exception)
            {
                this.info.AddInfoMessage(InfoLevel.Error, "Exception", "Exception while generating web reference: " + exception.Message);
            }
            this.info.AddInfoMessages(runner.Messages);
            return outputFilePath;
        }

        private int getCommonTypeContents(string fileTxt, string commonType, out string commonTypeContents)
        {
            string str;
            commonTypeContents = string.Empty;
            int num = -1;
            int index = fileTxt.IndexOf("public partial class " + commonType + " ");
            if (index == -1)
            {
                index = fileTxt.IndexOf("public abstract partial class " + commonType + " ");
            }
            if (index == -1)
            {
                index = fileTxt.IndexOf("public enum " + commonType + " ");
            }
            if (index == -1)
            {
                return num;
            }
            int currPos = this.getCurrLine(fileTxt, index, out str);
            int num4 = this.getPrevLine(fileTxt, currPos, out str);
            for (str = str.Trim(); (num4 != -1) && (str.StartsWith("[") || str.StartsWith("//")); str = str.Trim())
            {
                currPos = num4;
                num4 = this.getPrevLine(fileTxt, currPos, out str);
            }
            int startIndex = index;
            int num6 = 0;
            bool flag = false;
            char ch = fileTxt[startIndex];
            while ((startIndex < fileTxt.Length) && !flag)
            {
                startIndex++;
                switch (ch)
                {
                    case '{':
                        num6++;
                        break;

                    case '}':
                        num6--;
                        if (num6 == 0)
                        {
                            flag = true;
                        }
                        break;

                    case '/':
                        if (startIndex < fileTxt.Length)
                        {
                            switch (fileTxt[startIndex])
                            {
                                case '*':
                                    startIndex = fileTxt.IndexOf("*/", (int)(startIndex + 1));
                                    if (startIndex == -1)
                                    {
                                        startIndex = fileTxt.Length;
                                    }
                                    break;

                                case '/':
                                    startIndex = fileTxt.IndexOf("\n", (int)(startIndex + 1));
                                    if (startIndex == -1)
                                    {
                                        startIndex = fileTxt.Length;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                if (startIndex < fileTxt.Length)
                {
                    ch = fileTxt[startIndex];
                }
            }
            if (flag)
            {
                startIndex = fileTxt.IndexOf("\n", startIndex);
                if (startIndex == -1)
                {
                    startIndex = fileTxt.Length - 1;
                }
                commonTypeContents = fileTxt.Substring(currPos, (startIndex - currPos) + 1);
                return startIndex;
            }
            return -1;
        }

        private int getCurrLine(string text, int currPos, out string line)
        {
            line = string.Empty;
            int startIndex = text.LastIndexOf("\n", currPos) + 1;
            int index = text.IndexOf("\n", currPos);
            line = text.Substring(startIndex, (index - startIndex) + 1);
            return startIndex;
        }

        private int getNextLine(string text, int currPos, out string line)
        {
            line = string.Empty;
            int index = text.IndexOf("\n", currPos);
            if (index == -1)
            {
                return index;
            }
            int num2 = text.IndexOf("\n", (int)(index + 1));
            if (num2 == -1)
            {
                num2 = text.Length - 1;
            }
            line = text.Substring(index + 1, num2 - index);
            return (index + 1);
        }

        private int getPrevLine(string text, int currPos, out string line)
        {
            line = string.Empty;
            int num = text.LastIndexOf("\n", currPos);
            if (num == -1)
            {
                return num;
            }
            int num2 = -1;
            if (num > 0)
            {
                num2 = text.LastIndexOf("\n", (int)(num - 1));
            }
            line = text.Substring(num2 + 1, num - num2);
            return (num2 + 1);
        }

        private string GetServiceName(string wsdlFile, out string errorMessage)
        {
            string str = string.Empty;
            errorMessage = string.Empty;
            try
            {
                using (FileStream stream = new FileStream(wsdlFile, FileMode.Open, FileAccess.Read))
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        while (reader.Read())
                        {
                            if ((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == "service"))
                            {
                                return reader.GetAttribute("name");
                            }
                        }
                        return str;
                    }
                }
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
            return str;
        }

        private bool ProjectContainsFile(Project project, string generatedFile)
        {
            bool flag = false;
            if (project != null)
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    flag = this.FindFile(item, generatedFile);
                    if (flag)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        private bool ShareTypesInfoListContainsFile(List<SharedTypesInfo> sharedTypesInfoList, ProjectItemEx wsdlFile)
        {
            bool flag = false;
            foreach (SharedTypesInfo info in sharedTypesInfoList)
            {
                foreach (string str in info.WsdlFiles)
                {
                    if (wsdlFile.ProjectItem.get_FileNames(1).EndsWith(@"\" + str))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        private bool ValidateWsdlServiceName(string wsdlFile)
        {
            bool flag = false;
            if (File.Exists(wsdlFile))
            {
                string str3;
                string errorMessage = string.Empty;
                string serviceName = this.GetServiceName(wsdlFile, out errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    str3 = ("Wsdl file name: " + wsdlFile) + "\nError message: " + errorMessage;
                    this.info.AddInfoMessage(InfoLevel.Error, "Invalid wsdl file", str3);
                    return flag;
                }
                if (serviceName != Path.GetFileNameWithoutExtension(wsdlFile))
                {
                    str3 = "Service name different than name of wsdl file";
                    str3 = (str3 + "\nWsdl file name: " + Path.GetFileName(wsdlFile)) + "\nService name: " + serviceName;
                    this.info.AddInfoMessage(InfoLevel.Error, "Wrong service name", str3);
                    return flag;
                }
                return true;
            }
            this.info.AddInfoMessage(InfoLevel.Warning, "File not found", "Wsdl file " + wsdlFile + " not found!");
            return flag;
        }

        // Properties
        public InfoMessages InfoMessages
        {
            get
            {
                return this.info;
            }
        }
    }
}
