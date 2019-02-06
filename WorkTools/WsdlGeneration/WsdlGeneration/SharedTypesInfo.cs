using System;
using System.Collections.Generic;
using System.Text;

namespace WsdlGeneration
{
    internal class SharedTypesInfo
    {
        // Fields
        private string commonTypeNamespace = string.Empty;
        private List<string> commonTypes = new List<string>();
        private string commonTypesFile = string.Empty;
        private List<string> commonTypesReferences = new List<string>();
        private List<string> wsdlFiles = new List<string>();

        // Methods
        public bool IsValid()
        {
            return ((((this.WsdlFiles.Count > 0) && (this.CommonTypes.Count > 0)) && !string.IsNullOrEmpty(this.CommonTypeFile)) && !string.IsNullOrEmpty(this.CommonTypeNamespace));
        }

        // Properties
        public string CommonTypeFile
        {
            get
            {
                return this.commonTypesFile;
            }
            set
            {
                this.commonTypesFile = value;
            }
        }

        public string CommonTypeNamespace
        {
            get
            {
                return this.commonTypeNamespace;
            }
            set
            {
                this.commonTypeNamespace = value;
            }
        }

        public List<string> CommonTypes
        {
            get
            {
                return this.commonTypes;
            }
        }

        public List<string> CommonTypesReferences
        {
            get
            {
                return this.commonTypesReferences;
            }
        }

        public List<string> WsdlFiles
        {
            get
            {
                return this.wsdlFiles;
            }
        }
    }
}
