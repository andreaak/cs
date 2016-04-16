using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Patterns._02_Structural.Adapter._008_AdaptersForTreeDisplay
{
    class DirectoryTreeDisplay : TreeDisplay
    {
        public override void Display(object tree)
        {
            if (tree is DirectoryInfo)
                DisplaySubdirectories(tree as DirectoryInfo);
            else
                throw new ArgumentException();
        }

        void DisplaySubdirectories(DirectoryInfo directoryInfo)
        {
            this.Nodes.Clear();

            IEnumerable<DirectoryInfo> enumerable = directoryInfo.EnumerateDirectories();
            List<DirectoryInfo> directories = enumerable.ToList<DirectoryInfo>();

            foreach (DirectoryInfo directory in directories)
            {
                this.Nodes.Add(new TreeNode(directory.Name));

                foreach (FileInfo file in directory.GetFiles())
                    this.Nodes[directories.IndexOf(directory)].Nodes.Add(new TreeNode(file.Name));

                // TODO: ... некоторая логика по работе с файловой системой ...
            }
        }
    }
}
