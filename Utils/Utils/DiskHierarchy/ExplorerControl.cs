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

        public virtual void FillTree(DirectoryData directoryData)
        {
            if (directoryData == null)
                return;
            ClearTree();
            treeView.Nodes.Add(directoryData.Data.FullPath);
            treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 0;
            treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
            treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 0;
            FillTree(directoryData, treeView.Nodes[treeView.Nodes.Count - 1]);
        }

        protected virtual void FillTree(DirectoryData directoryData, TreeNode treeNode)
        {
            foreach (DirectoryData directory in directoryData.DirectoriesList)
            {
                if (IsIgnoredDirectory(directory))
                {
                    continue;
                }
                treeNode.Nodes.Add(directory.Data.Name);
                treeNode.Nodes[treeNode.Nodes.Count - 1].ImageIndex = 0;
                treeNode.Nodes[treeNode.Nodes.Count - 1].SelectedImageIndex = 1;
                treeNode.Nodes[treeNode.Nodes.Count - 1].StateImageIndex = 0;
                FillTree(directory, treeNode.Nodes[treeNode.Nodes.Count - 1]);
            }
        }

        public virtual bool IsIgnoredDirectory(DirectoryData dir)
        {
            return false;
        }

        public virtual void ClearListView()
        {
            listView.Items.Clear();
        }

        public virtual void FillListView(DirectoryData baseDir, List<string> selectedExtensions)
        {
            if (baseDir == null)
            {
                return;
            }
            foreach (FileData file in baseDir.FilesList)
            {
                if (IsIgnoredFile(file))
                {
                    continue;
                }
                if (
                    (selectedExtensions != null && selectedExtensions.Count > 0 && !selectedExtensions.Contains(Path.GetExtension(file.Data.Name)))
                    )
                {
                    continue;
                }
                ListViewItem listviewitem = new ListViewItem(file.Data.Name);
                listviewitem.UseItemStyleForSubItems = false;

                FillListViewSubItem(listviewitem, file, listviewitem.SubItems[0].BackColor);
                listView.Items.Add(listviewitem);

            }

            foreach (DirectoryData dir in baseDir.DirectoriesList)
            {
                FillListView(dir, selectedExtensions);
            }
        }

        protected virtual void FillListViewSubItem(ListViewItem listviewitem, FileData file, Color color)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = "FullPath";
            lvsi.Text = file.Data.FullPath;
            lvsi.BackColor = color;
            listviewitem.SubItems.Add(lvsi);
        }

        public virtual bool IsIgnoredFile(FileData file)
        {
            return false;
        }

    }
}
