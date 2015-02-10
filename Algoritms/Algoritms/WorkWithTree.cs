using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Algoritms;

namespace Utils
{
    class WorkWithTree
    {
        TreeView treeView;
        public WorkWithTree(TreeView treeView_)
        {
            treeView = treeView_;
        }

        public void clearTree()
        {
            treeView.Nodes.Clear();
        }

        public void fillTree(List<BaseItem> methods)
        {
            if (methods == null)
                return;
            clearTree();
            foreach (BaseItem method in methods)
            {
                TreeNode node = fillTree(method, null);
                if (node == null)
                {
                    continue;
                }
            }
            
        }

        public TreeNode fillTree(BaseItem method, TreeNode node)
        {
            if (method == null)
                return null;
            TreeNode tempNode = null;
            if (node == null)
            {
                tempNode = treeView.Nodes.Add(method.GetKey(), method.GetDescription());
            }
            else
            {
                tempNode = node.Nodes.Add(method.GetKey(), method.GetDescription());
            }
            
            int shift = GetShift(method);
            
            tempNode.ImageIndex = shift;
            tempNode.StateImageIndex = shift;
            tempNode.SelectedImageIndex = shift;

            foreach (BaseItem method_ in method.Items)
            {
                fillTree(method_, tempNode);
            }
            return tempNode;
        }

        private static int GetShift(BaseItem method)
        {
            int shift2 = 0;
            switch (method.Type_)
            {
                case DataType.method:
                case DataType.property:
                    int shift1 = 0;
                    if (method.Static_)
                    {
                        shift1 = 1;
                    } 
                    switch (method.Language)
                    {
                        case "C#":
                            shift2 = 2;
                            break;
                        case "Java":
                            shift2 = 4;
                            break;
                        case "C++":
                            shift2 = 6;
                            break;
                    }
                    shift2 += shift1;
                    break;
                case DataType.branch:
                    shift2 = 8;
                    break;
                case DataType.branchCondition:
                    shift2 = 9;
                    break;
                case DataType.branchElse:
                    shift2 = 10;
                    break;
                case DataType.cycle:
                    shift2 = 11;
                    break;
            }
            return shift2;
        }


        public static BaseItem GetCurrentItem(TreeNode selectedNode, List<BaseItem> items)
        {
            if (selectedNode == null)
            {
                return null;
            }
            foreach (BaseItem method in items)
            {
                BaseItem temp = method.GetItemById(selectedNode.Name);
                if (temp != null)
                {
                    return temp;
                }
            }
            throw new Exception("GetCurrentMethod Error");
        }
    }
}
