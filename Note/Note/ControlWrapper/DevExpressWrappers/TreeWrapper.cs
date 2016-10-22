using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Note.Domain.Concrete;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Note.ControlWrapper.Binding;
using Note.Domain.Common;

namespace Note.ControlWrapper.DevExpressWrappers
{
    public class TreeWrapper : ITreeWrapper
    {
        private readonly TreeList control;
        private readonly IRtfWrapper rtfWrapper;
        private readonly MainPresenter presenter;

        public long SelectedNodeId
        {
            get
            {
                TreeListNode node = control.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            }
        }

        private long ParentId
        {
            get
            {
                TreeListNode node = control.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_PARENTID);
            }
        }

        private int Position
        {
            get
            {
                TreeListNode node = control.Selection[0];
                return (int)node.GetValue(DBConstants.ENTITY_TABLE_POSITION);
            }
        }

        public bool Enabled
        {
            set
            {
                control.Enabled = value;
            }
        }

        public IList<Node> Nodes
        {
            get
            {
                return Convert(control.Nodes);
            }
        }

        public TreeWrapper(TreeList control, IRtfWrapper rtfWrapper, MainPresenter presenter)
        {
            this.control = control;
            this.rtfWrapper = rtfWrapper;
            this.presenter = presenter;
            SubscribeEvents();
        }

        #region PUBLIC

        public void DataSource(BindingDataset.DescriptionDataTable table)
        {
            DisableFocusing();
            control.DataSource = table;
            EnableFocusing();
        }

        public void SetDataSource()
        {
            DisableFocusing();
            control.DataSource = presenter.GetDataSource();
            EnableFocusing();
        }

        public void ChangeNodeLocation(Func<int, long, Direction, bool> IsValidAction, Func<int, long, long, Direction, bool> PerformAction, Direction dir)
        {
            DisableFocusing();
            if (!IsNodeSelect())
            {
                EnableFocusing();
                return;
            }

            int position = Position;
            long parentId = ParentId;
            if (!IsValidAction(position, parentId, dir))
            {
                EnableFocusing();
                return;
            }

            long id = SelectedNodeId;
            if (PerformAction(position, parentId, id, dir))
            {
                control.DataSource = presenter.GetDataSource();
                FocusNode(id);
            }
            EnableFocusing();
        }

        public bool IsNodeSelect()
        {
            return control.Selection.Count != 0;
        }

        public bool IsNoteSelect()
        {
            return IsNodeSelect() && IsNoteNode(control.Selection[0]);
        }

        public void EnableFocusing()
        {
            this.control.FocusedNodeChanged += new FocusedNodeChangedEventHandler(FocusedNodeChanged);
        }

        public void DisableFocusing()
        {
            this.control.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(FocusedNodeChanged);
        }

        public long GetParentId(bool isChildNode)
        {
            long parentId = DBConstants.BASE_LEVEL;
            if (IsNodeSelect())
            {
                TreeListNode selNode = control.Selection[0];
                if (isChildNode)
                {
                    parentId = (long)selNode.GetValue(DBConstants.ENTITY_TABLE_ID);
                }
                else if (selNode.ParentNode != null)
                {
                    parentId = (long)selNode.ParentNode.GetValue(DBConstants.ENTITY_TABLE_ID);
                }
            }
            return parentId;
        }

        public void Insert(long id, long parentId, string description, DataTypes type)
        {
            BindingDataset.DescriptionDataTable table = control.DataSource as BindingDataset.DescriptionDataTable;
            if (table == null)
            {
                return;
            }

            var items = table.Where(itm => Utils.IsActiveRow(itm) && itm.ParentID == parentId).ToArray();
            int position;
            if (items.Length == 0)
            {
                position = DBConstants.START_POSITION;
            }
            else
            {
                position = items.Max(itm => itm.OrderPosition) + 1;
            }
            
            table.AddDescriptionRow(
                    id,
                    parentId,
                    description,
                    (byte)type,
                    position
                    );
            table.AcceptChanges();
            FocusSelectedNode();
        }

        public void Delete(long id)
        {
            BindingDataset.DescriptionDataTable table = control.DataSource as BindingDataset.DescriptionDataTable;
            if (table == null)
            {
                return;
            }

            TreeListNode selNode = (control.Selection[0].NextNode 
                                    ?? control.Selection[0].PrevNode) 
                                    ?? control.Selection[0].ParentNode;
            DisableFocusing();
            Delete(id, table);
            table.AcceptChanges();
            control.FocusedNode = selNode;
            EnableFocusing();
            SetRTFData(selNode);
        }

        public void FocusParentNode()
        {
            control.FocusedNode = control.Selection[0].ParentNode;
        }
        
        #endregion

        #region EVENTS

        private void CompareNodeValues(object sender, EventArgs arg)
        {
            CompareNodeValuesEventArgs e = arg as CompareNodeValuesEventArgs;
            if (e != null)
            {
                e.Result = ((int)e.Node1.GetValue(DBConstants.ENTITY_TABLE_POSITION)).CompareTo(
                    (int)e.Node2.GetValue(DBConstants.ENTITY_TABLE_POSITION));
            }
        }
        
        private void FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            DisableFocusing();
            UpdatePreviousNode(e.OldNode);
            SetRTFData(e.Node);
            EnableFocusing();
        }

        #endregion

        private void SubscribeEvents()
        {
            this.control.CompareNodeValues += CompareNodeValues;
        }

        private void Delete(long id, BindingDataset.DescriptionDataTable table)
        {
            var subItems = table.Where(itm => Utils.IsActiveRow(itm) && itm.ParentID == id).ToArray();
            foreach (var subItem in subItems)
            {
                Delete(subItem.ID, table);
                table.RemoveDescriptionRow(subItem);
            }
            var item = table.FirstOrDefault(itm => Utils.IsActiveRow(itm) && itm.ID == id);
            if(item != null)
            {
                table.RemoveDescriptionRow(item);
            }
        }

        private static bool IsEmptyNode(TreeListNode node)
        {
            if (node == null || System.Convert.IsDBNull(node.GetValue(DBConstants.ENTITY_TABLE_ID)))
            {
                return true;
            }
            return false;
        }

        private static bool IsNoteNode(TreeListNode node)
        {
            if (!IsEmptyNode(node) && DataTypes.NOTE == (DataTypes)node.GetValue(DBConstants.ENTITY_TABLE_TYPE))
            {
                return true;
            }
            return false;
        }

        private void UpdatePreviousNode(TreeListNode node)
        {
            if (IsEmptyNode(node))
            {
                return;
            }
            if (IsChangedDescription(node))
            {
                UpdateDescription(node);
            }

            if (IsNoteNode(node) && rtfWrapper.IsModified)
            {
                UpdateTextData(node);
            }
        }

        private bool IsChangedDescription(TreeListNode node)
        {
            BindingDataset.DescriptionDataTable table = control.DataSource as BindingDataset.DescriptionDataTable;
            if (table == null)
            {
                return false;
            }
            long id = (long) node.GetValue(DBConstants.ENTITY_TABLE_ID);
            DataRow row = table.FirstOrDefault(rw => Utils.IsActiveRow(rw) && rw.ID == id);
            return row != null && row.RowState == DataRowState.Modified;
        }

        #region DATA CHANGING

        private void UpdateTextData(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            presenter.UpdateTextData(id, rtfWrapper.EditValue, rtfWrapper.PlainText);
        }

        private void UpdateDescription(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            string description = (string)node.GetValue(DBConstants.ENTITY_TABLE_DESC);
            presenter.UpdateDescription(id, description);
        }

        #endregion

        private void SetRTFData(TreeListNode node)
        {
            bool isNoteNode = IsNoteNode(node);
            if (isNoteNode)
            {
                long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                rtfWrapper.EditValue = presenter.GetTextData(id); //selRow.Data;
            }
            rtfWrapper.ChangeState(isNoteNode);
        }

        private IList<Node> Convert(TreeListNodes treeListNodes)
        {
            List<Node> nodes = new List<Node>();
            foreach (TreeListNode node in treeListNodes)
            {
                int nodeIndex = treeListNodes.IndexOf(node);
                Node res = new Node();
                res.ID = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                res.EditValue = node.GetDisplayText(0);
                res.IsNote = TreeWrapper.IsNoteNode(node);
                res.Index = nodeIndex;
                res.SiblingsCount = treeListNodes.Count;
                if (node.HasChildren)
                {
                    res.Nodes = new List<Node>();
                    res.Nodes.AddRange(Convert(node.Nodes));
                }
                nodes.Add(res);
            }
            return nodes;
        }

        private void FocusSelectedNode()
        {
            if (IsNodeSelect())
            {
                control.Selection[0].Expanded = true;
                control.FocusedNode = control.Selection[0].LastNode;
            }
        }

        private void FocusNode(long id)
        {
            TreeListNode node = null;
            if (control.Nodes.Count != 0 && TryGetNodeById(id, control.Nodes, ref node))
            {
                if (node.ParentNode != null)
                {
                    node.ParentNode.Expanded = true;
                }
                control.FocusedNode = node;
            }
        }

        private bool TryGetNodeById(long id, TreeListNodes nodes, ref TreeListNode resNode)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Nodes.Count != 0 && TryGetNodeById(id, node.Nodes, ref resNode))
                {
                    return true;
                }
                
                long itemId = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                if (itemId == id)
                {
                    resNode = node;
                    return true;
                }
            }
            return false;
        }
    }
}
