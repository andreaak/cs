using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utils;
using System.IO;

namespace Algoritms
{
    public partial class Main : Form
    {
        List<BaseItem> methods;
        WorkWithTree wt = null;
        public Main()
        {
            InitializeComponent();
            Init();
            InitTree();
        }

        private void Init()
        {
            methods = new List<BaseItem>();
            methods.Add(MethodItem.GetRootItem());
        }

        private void InitTree()
        {
            wt = new WorkWithTree(treeView1);
            wt.fillTree(methods);
        }

        #region BUTTON

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            Init();
            InitTree();
        }

        private void toolStripButtonOpenData_Click(object sender, EventArgs e)
        {
            methods = Serialization.ReadData(null);
            if (methods == null)
            {
                Init();
            }
            wt.fillTree(methods);
        }

        private void toolStripButtonImportFromXML_Click(object sender, EventArgs e)
        {
            string error = "";
            methods = Serialization.ReadDataFromXML(null, ref error);
            if (methods == null)
            {
                MessageBox.Show(error);
                Init();
            }
            wt.fillTree(methods);
        }

        private void toolStripButtonSaveData_Click(object sender, EventArgs e)
        {
            string error = null;
            if (!string.IsNullOrEmpty(error = Serialization.SaveData(null, methods)))
            {
                MessageBox.Show("Error save file " + error);
            }
        }

        private void toolStripButtonExportToXML_Click(object sender, EventArgs e)
        {
            string error = "";
            bool ok = Serialization.SaveDataToXML(null, methods, ref error);
            if (!ok)
            {
                MessageBox.Show(error);
            }
        }

        private void toolStripButtonAddMethod_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            AddMethod form = new AddMethod(node, methods, DataType.method);

            form.ShowDialog();
            if (form.Changed)
            {
                wt.fillTree(methods);
            }
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonAddBranch_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            AddMethod form = new AddMethod(node, methods, DataType.branch);

            form.ShowDialog();
            if (form.Changed)
            {
                wt.fillTree(methods);
            }
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonAddCycle_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            AddMethod form = new AddMethod(node, methods, DataType.cycle);

            form.ShowDialog();
            if (form.Changed)
            { 
                wt.fillTree(methods);
            }
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();

        }

        private void toolStripButtonDeleteMethod_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            string parentNodeId = node.Parent.Name;
            for (int i = 0; i < methods.Count; i++)
            {
                if (methods[i].RemoveItemById(nodeId))
                {
                    break;
                }
            }

            wt.fillTree(methods);
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, parentNodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            EditMethod form = new EditMethod(node, methods);
            form.ShowDialog();
            if (form.Changed)
            {
                wt.fillTree(methods);
            }
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonUpToHierarchy_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            string nodeId = selectedNode.Name;
            BaseItem currentItem = WorkWithTree.GetCurrentItem(selectedNode, this.methods);
            BaseItem parrentItem = currentItem.Parent;
            if (parrentItem.IsRoot()
                || currentItem.Type_ == DataType.branchCondition
                || currentItem.Type_ == DataType.branchElse)
            {
                return;
            }

            BaseItem grandParrentItem = parrentItem.Parent;
            while (grandParrentItem.Type_ == DataType.branch)
            {
                grandParrentItem = grandParrentItem.Parent;
            }

            grandParrentItem.AddItem(currentItem);
            parrentItem.RemoveItem(currentItem);

            wt.fillTree(methods);
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonInsertToMethod_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;
            InsertMethods form = new InsertMethods(node, methods);
            form.ShowDialog();
            if (form.Changed)
            {
                wt.fillTree(methods);
            }
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;

            MoveMethod(node, true);
            
            wt.fillTree(methods);
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();

        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            string nodeId = node.Name;

            MoveMethod(node, false);
            
            wt.fillTree(methods);
            treeView1.SelectedNode = GetNodeById(treeView1.Nodes, nodeId);
            treeView1.SelectedNode.Expand();
        }

        private void toolStripButtonShowDiagramm_Click(object sender, EventArgs e)
        {
            Diagramm form = new Diagramm(methods);
            form.Show();
        }

        #endregion
        
        #region HANDLERS

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            toolStripButtonEdit_Click(null, null);
        }

        #endregion

        #region UTILS

        private TreeNode GetNodeById(TreeNodeCollection treeNodeCollection, string nodeId)
        {
            foreach (TreeNode node in treeNodeCollection)
            {
                if (node.Name.Equals(nodeId))
                {
                    return node;
                }
                if (node.Nodes == null)
                {
                    continue;
                }
                TreeNode temp = GetNodeById(node.Nodes, nodeId);
                if (temp != null)
                {
                    return temp;
                }
            }
            return null;
        }

        private void MoveMethod(TreeNode selectedNode, bool isUp)
        {

            BaseItem currentItem = WorkWithTree.GetCurrentItem(selectedNode, this.methods);
            BaseItem parentItem = WorkWithTree.GetCurrentItem(selectedNode.Parent, this.methods);
            
            UTILS.MoveMethod(parentItem, currentItem, isUp);
        }

        #endregion

        #region DISPLAY
        //
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }
        //
        private void Form_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }
        #endregion
    }
}
