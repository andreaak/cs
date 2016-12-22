using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace WsdlGeneration
{
    internal class GenerationConfiguration
    {
        // Fields
        private const string CommonType = "commonType";
        private const string CommonTypeFile = "commonTypesFile";
        private const string CommonTypeNamespace = "commonTypesNamespace";
        private const string CommonTypeReferences = "commonTypesReference";
        private string filePath = string.Empty;
        private string namespaceRootSuffix = string.Empty;
        private const string NamespaceRootSuffixParam = "namespaceRootSuffix";
        private string outputDirectory = string.Empty;
        private const string OutputDirectoryParam = "outputDirectory";
        private List<SharedTypesInfo> sharedTypes = new List<SharedTypesInfo>();
        private bool validateServiceName = true;
        private const string ValidateServiceNameParam = "validateServiceName";
        private const string WsdlFile = "wsdl";
        private string wsdlRootItem = string.Empty;
        private const string WsdlRootItemParam = "wsdlRootItem";
        private const string WsdlsWithSharedTypes = "wsdlsWithSharedTypes";

        // Methods
        internal GenerationConfiguration(string filePath)
        {
            if (File.Exists(filePath))
            {
                SharedTypesInfo item = new SharedTypesInfo();
                this.filePath = filePath;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.LocalName == "outputDirectory")
                                {
                                    this.OutputDirectory = reader.ReadElementString();
                                }
                                if (reader.LocalName == "namespaceRootSuffix")
                                {
                                    this.NamespaceRootSuffix = reader.ReadElementString();
                                }
                                if (reader.LocalName == "wsdlRootItem")
                                {
                                    this.WsdlRootItem = reader.ReadElementString();
                                }
                                if (reader.LocalName == "validateServiceName")
                                {
                                    string str = this.ExpandFromEnvironment(reader.ReadElementString());
                                    bool result = true;
                                    bool.TryParse(str, out result);
                                    this.ValidateServiceName = result;
                                }
                                if (reader.LocalName == "wsdlsWithSharedTypes")
                                {
                                    if (item.IsValid())
                                    {
                                        this.SharedTypes.Add(item);
                                    }
                                    item = new SharedTypesInfo();
                                }
                                if (reader.LocalName == "wsdl")
                                {
                                    item.WsdlFiles.Add(reader.ReadElementString().Trim());
                                }
                                if (reader.LocalName == "commonType")
                                {
                                    item.CommonTypes.Add(reader.ReadElementString().Trim());
                                }
                                if (reader.LocalName == "commonTypesFile")
                                {
                                    item.CommonTypeFile = reader.ReadElementString().Trim();
                                }
                                if (reader.LocalName == "commonTypesNamespace")
                                {
                                    item.CommonTypeNamespace = reader.ReadElementString().Trim();
                                }
                                if (reader.LocalName == "commonTypesReference")
                                {
                                    item.CommonTypesReferences.Add(reader.ReadElementString().Trim());
                                }
                            }
                        }
                        if (item.IsValid())
                        {
                            this.SharedTypes.Add(item);
                        }
                    }
                }
            }
        }

        private string ExpandFromEnvironment(string paramValue)
        {
            if (paramValue != null)
            {
                paramValue = paramValue.Trim();
                string str = "%";
                int length = -1;
                int index = -1;
                length = paramValue.IndexOf(str);
                if (length != -1)
                {
                    index = paramValue.IndexOf(str, (int)(length + 1));
                }
                while ((length != -1) && (index != -1))
                {
                    string environmentVariable = Environment.GetEnvironmentVariable(paramValue.Substring(length + 1, (index - 1) - length));
                    paramValue = paramValue.Substring(0, length) + environmentVariable + paramValue.Substring(index + 1);
                    length = paramValue.IndexOf(str);
                    if (length != -1)
                    {
                        index = paramValue.IndexOf(str, (int)(length + 1));
                    }
                }
            }
            return paramValue;
        }

        // Properties
        public string NamespaceRootSuffix
        {
            get
            {
                return this.namespaceRootSuffix;
            }
            private set
            {
                this.namespaceRootSuffix = this.ExpandFromEnvironment(value);
            }
        }

        public string OutputDirectory
        {
            get
            {
                return this.outputDirectory;
            }
            private set
            {
                this.outputDirectory = Path.Combine(Path.GetDirectoryName(this.filePath), this.ExpandFromEnvironment(value));
            }
        }

        public List<SharedTypesInfo> SharedTypes
        {
            get
            {
                return this.sharedTypes;
            }
            private set
            {
                this.sharedTypes = value;
            }
        }

        public bool ValidateServiceName
        {
            get
            {
                return this.validateServiceName;
            }
            set
            {
                this.validateServiceName = value;
            }
        }

        public string WsdlRootItem
        {
            get
            {
                return this.wsdlRootItem;
            }
            private set
            {
                this.wsdlRootItem = this.ExpandFromEnvironment(value);
            }
        }
    }
}
