using System.Windows.Forms;
using Utils;
using WorkWithSvn.DiskHierarchy;

namespace Display
{
    public class TreeProvider
    {
        TreeView treeView;
        ContextMenuStrip contextMenuStrip;
        private delegate TreeNode IncrementCallbackUpdate();


        public TreeProvider(TreeView treeView, ContextMenuStrip contextMenuStrip)
        {
            this.treeView = treeView;
            this.contextMenuStrip = contextMenuStrip;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
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

        public void Clear()
        {
            treeView.Nodes.Clear();
        }

        public void Fill(RepositoryDirectory directoryData)
        {
            if (directoryData == null)
                return;
            Clear();
            treeView.Nodes.Add(directoryData.FullName);
            treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 0;
            treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
            treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 0;
            Fill(directoryData, treeView.Nodes[treeView.Nodes.Count - 1]);
        }

        private void Fill(RepositoryDirectory directoryData, TreeNode treeNode)
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
                Fill(directory, treeNode.Nodes[treeNode.Nodes.Count - 1]);
            }
        }

        public TreeNode SelNode 
        { 
            get
            {
                return GetSelNode();
            }
        }

        private TreeNode GetSelNode()
        {
            if (treeView.InvokeRequired)
            {
                IncrementCallbackUpdate d = new IncrementCallbackUpdate(GetSelNode);
                return (TreeNode)treeView.Invoke(d);
            }
            else
            {
                return treeView.SelectedNode;
            }
        }

        public void FillTree(RepositoryDirectory repositoryDirectory)
        {
            throw new System.NotImplementedException();
        }
    }
}
