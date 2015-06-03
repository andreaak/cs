using System;
using System.Windows.Forms;
using Utils;
using WorkWithSvn.DiskHierarchy.Base;

namespace VisualProviders
{
    public class TreeProvider
    {
        private TreeView treeView;
        private ContextMenuStrip contextMenuStrip;
        
        public TreeNode SelectedNode
        {
            get
            {
                return GetSelectedNode();
            }
        } 
       
        public RepositoryDirectory DataSource
        {
            set
            {
                Clear();
                if (value != null)
                {
                    treeView.Nodes.Add(value.FullName);
                    treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 0;
                    treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
                    treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 0;
                    FillTreeView(value, treeView.Nodes[treeView.Nodes.Count - 1]);
                }
            }
        }

        public TreeProvider(TreeView treeView, ContextMenuStrip contextMenuStrip)
        {
            this.treeView = treeView;
            this.contextMenuStrip = contextMenuStrip;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
        }

        public void Clear()
        {
            treeView.Nodes.Clear();
        }

        #region HANDLERS

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && contextMenuStrip != null)
            {
                contextMenuStrip.Show(treeView, e.X, e.Y);
            }

        }

        #endregion

        private void FillTreeView(RepositoryDirectory directoryData, TreeNode treeNode)
        {
            foreach (RepositoryDirectory directory in directoryData.DirectoriesList)
            {
                if (UTILS.IsIgnoredDirectory(directory.FullName))
                {
                    continue;
                }
                treeNode.Nodes.Add(directory.Name);
                treeNode.Nodes[treeNode.Nodes.Count - 1].ImageIndex = 0;
                treeNode.Nodes[treeNode.Nodes.Count - 1].SelectedImageIndex = 1;
                treeNode.Nodes[treeNode.Nodes.Count - 1].StateImageIndex = 0;
                FillTreeView(directory, treeNode.Nodes[treeNode.Nodes.Count - 1]);
            }
        }

        private TreeNode GetSelectedNode()
        {
            if (treeView.InvokeRequired)
            {
                Func<TreeNode> d = GetSelectedNode;
                return (TreeNode)treeView.Invoke(d);
            }
            else
            {
                return treeView.SelectedNode;
            }
        }
    }
}
