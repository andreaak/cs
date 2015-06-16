using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Utils.DiskHierarchy
{
    public partial class ExplorerControl : UserControl
    {
        public ExplorerControl()
        {
            InitializeComponent();
        }

        public virtual void ClearTree()
        {
            treeView.Nodes.Clear();
        }

        public virtual void FillTree(DirectoryItem directoryData)
        {
            if (directoryData == null)
                return;
            ClearTree();
            treeView.Nodes.Add(directoryData.FullName);
            treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 0;
            treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
            treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 0;
            FillTree(directoryData, treeView.Nodes[treeView.Nodes.Count - 1]);
        }

        protected virtual void FillTree(DirectoryItem directoryData, TreeNode treeNode)
        {
            foreach (DirectoryItem directory in directoryData.Directories)
            {
                if (IsIgnoredDirectory(directory))
                {
                    continue;
                }
                treeNode.Nodes.Add(directory.Name);
                treeNode.Nodes[treeNode.Nodes.Count - 1].ImageIndex = 0;
                treeNode.Nodes[treeNode.Nodes.Count - 1].SelectedImageIndex = 1;
                treeNode.Nodes[treeNode.Nodes.Count - 1].StateImageIndex = 0;
                FillTree(directory, treeNode.Nodes[treeNode.Nodes.Count - 1]);
            }
        }

        public virtual bool IsIgnoredDirectory(DirectoryItem dir)
        {
            return false;
        }

        public virtual void ClearListView()
        {
            listView.Items.Clear();
        }

        public virtual void FillListView(DirectoryItem baseDir, List<string> selectedExtensions)
        {
            if (baseDir == null)
            {
                return;
            }
            foreach (FileItem file in baseDir.Files)
            {
                if (IsIgnoredFile(file))
                {
                    continue;
                }
                if (selectedExtensions != null 
                    && selectedExtensions.Count != 0 
                    && !selectedExtensions.Contains(Path.GetExtension(file.Name)))
                {
                    continue;
                }
                ListViewItem listviewitem = new ListViewItem(file.Name);
                listviewitem.UseItemStyleForSubItems = false;

                FillListViewSubItem(listviewitem, file, listviewitem.SubItems[0].BackColor);
                listView.Items.Add(listviewitem);

            }

            foreach (DirectoryItem dir in baseDir.Directories)
            {
                FillListView(dir, selectedExtensions);
            }
        }

        protected virtual void FillListViewSubItem(ListViewItem listviewitem, FileItem file, Color color)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = "FullPath";
            lvsi.Text = file.FullName;
            lvsi.BackColor = color;
            listviewitem.SubItems.Add(lvsi);
        }

        public virtual bool IsIgnoredFile(FileItem file)
        {
            return false;
        }

    }
}
