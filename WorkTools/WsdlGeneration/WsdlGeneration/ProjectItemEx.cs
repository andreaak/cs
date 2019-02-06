using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;

namespace WsdlGeneration
{
    internal class ProjectItemEx
    {
        // Fields
        private string itemPath;
        private ProjectItem projectItem;

        // Methods
        public ProjectItemEx(ProjectItem projectItem, string itemPath)
        {
            this.projectItem = projectItem;
            this.itemPath = itemPath;
        }

        // Properties
        public string ItemPath
        {
            get
            {
                return this.itemPath;
            }
            set
            {
                this.itemPath = value;
            }
        }

        public ProjectItem ProjectItem
        {
            get
            {
                return this.projectItem;
            }
            set
            {
                this.projectItem = value;
            }
        }
    }
}
