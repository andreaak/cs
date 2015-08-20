using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ControlWrapper;
using ControlWrapper.Binding;
using DataManager;
using DataManager.Domain;
using DataManager.Domain.LinqToSql;
using DataManager.Repository;
using DevExpress.XtraBars;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ExportData;
using Note.ControlWrapper;

namespace Note
{
    class DevExpressWrapper : IWrapper
    {
        DatabaseManager dataManager_;
        private TreeList treeList1;
        private MyRichEditControl richEditControl;
        private ExportHelper exportHelper;
        BindingDataset.DescriptionDataTable table; 
        BarItem[] controls;

        public DatabaseManager DataManager
        {
            set
            {
                this.dataManager_ = value;
                exportHelper = new ExportHelper(treeList1, richEditControl, dataManager_);
            }
            get
            {
                return dataManager_;
            }
        }

        public long SelectedNodeId
        {
            get
            {
                TreeListNode node = treeList1.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            }
        }

        public int Position
        {
            get
            {
                TreeListNode node = treeList1.Selection[0];
                return (int)node.GetValue(DBConstants.ENTITY_TABLE_POSITION); ;
            }
        }

        public long ParentId
        {
            get
            {
                TreeListNode node = treeList1.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_PARENTID);
            }
        }

        public string EditValue
        {
            get
            {
                return richEditControl.Data;
            }
            set
            {
                richEditControl.Data = value;
            }
        }

        public string PlainText
        {
            get
            {
                return richEditControl.Text;
            }
        }

        public DevExpressWrapper(TreeList treeList1, MyRichEditControl richEditControl, BarItem[] controls)
        {
            this.treeList1 = treeList1;
            this.richEditControl = richEditControl;
            this.controls = controls;
            table = new BindingDataset.DescriptionDataTable();
        }

        public void EnableControls(bool isEnable)
        {
            foreach (BarItem item in controls)
            {
                item.Enabled = isEnable;
            }
            treeList1.Enabled = isEnable;
            richEditControl.Enabled = isEnable;
        }

        public void Export()
        {
            exportHelper.Export();
        }

        public void LoadEntityFromDB()
        {
            DisableFocusing();
            ClearTreeList();
            ClearRtfControl();
            DataManager.InitEntity();
            UpdateBinding();
            treeList1.DataSource = table;
            SortTreeList();
            EnableFocusing();
        }

        #region RTF

        public void BeginUpdateRtfControl()
        {
            richEditControl.BeginUpdate();
        }

        public void EndUpdateRtfControl()
        {
            richEditControl.EndUpdate();
        }

        public void SetDefinitionFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = false;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void SetMethodFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = true;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void SetClassFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = true;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetHeaderFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Courier New";
            cp.FontSize = 14;
            cp.Bold = true;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetInfoFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = false;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetCodeFormat()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Courier New";
            cp.FontSize = 10;
            cp.Bold = false;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void RemoveWhiteSpace()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            richEditControl.Document.ReplaceAll(" ", "", SearchOptions.None, range);
        }

        public void RemoveDoubleWhiteSpace()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            //Regex reg = new Regex("  ");
            richEditControl.Document.ReplaceAll("  ", " ", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll("( )", "()", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll(" () ", "() ", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll("—", "-", SearchOptions.None, range);
        }

        public void RemoveLineBreak()
        {
            Document doc = richEditControl.Document;
            DocumentRange range = richEditControl.Document.Selection;
            //Regex reg = new Regex(@"\r\n");
            char ch = '\u00AD';
            richEditControl.Document.ReplaceAll("\t", "", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll(ch.ToString(), "-", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll("- \r\n", "", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll("-\r\n", "", SearchOptions.None, range);
            richEditControl.Document.ReplaceAll("\r\n", " ", SearchOptions.None, range);
            RemoveDoubleWhiteSpace();
            Paragraph par = richEditControl.Document.GetParagraph(range.Start);
            par.Alignment = ParagraphAlignment.Justify;
            par.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            par.FirstLineIndent = 60.0f;
        }

        private void ChangeState(bool isNoteNode)
        {
            if (!isNoteNode)
            {
                ClearRtfControl();
            }
            else
            {
                Document doc = richEditControl.Document;
                DocumentPosition pos = doc.Selection.Start;
                DocumentRange range = doc.CreateRange(pos, 0);
                ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
                TabInfoCollection tbiColl = pp.BeginUpdateTabs(true);
                TabInfo tbi = new TabInfo();
                tbi.Alignment = TabAlignmentType.Left;
                tbi.Position = DevExpress.Office.Utils.Units.InchesToDocumentsF(0.2f);
                tbiColl.Add(tbi);
                pp.EndUpdateTabs(tbiColl);
                doc.EndUpdateParagraphs(pp);
            }
            richEditControl.Enabled = isNoteNode;
        }

        private void ClearRtfControl()
        {
            richEditControl.Text = "";
        }

        #endregion

        #region TREE

        public bool IsNodeSelect()
        {
            return treeList1.Selection.Count > 0;
        }

        public bool IsNoteNode()
        {
            return IsNoteNode(treeList1.Selection[0]);
        }

        public void EnableFocusing()
        {
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(FocusedNodeChanged);
        }

        public void DisableFocusing()
        {
            this.treeList1.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(FocusedNodeChanged);
        }

        public long GetParentId(bool isChildNode)
        {
            long parentId = DBConstants.BASE_LEVEL;
            if (IsNodeSelect())
            {
                TreeListNode selNode = treeList1.Selection[0];
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

        public void FocusSelectedNode()
        {
            treeList1.Selection[0].Expanded = true;
            treeList1.FocusedNode = treeList1.Selection[0].LastNode;
        }

        public void InitHandlers()
        {
            this.treeList1.CompareNodeValues += CompareNodeValues;
        }

        public void UpdateBinding()
        {
            IEnumerable<long> tableIds = table.Where(Utils.IsActiveRow).Select(row => row.ID).ToList();
            IEnumerable<long> entityIds = DataManager.Description.Select(item => item.ID).ToList();
            long[] idsForAdding = entityIds.Except(tableIds).ToArray();
            long[] idsForDeleting = tableIds.Except(entityIds).ToArray();

            //add
            foreach (var item in DataManager.Description.Where(item => idsForAdding.Contains(item.ID)))
            {
                table.AddDescriptionRow(
                    item.ID,
                    item.ParentID,
                    item.EditValue,
                    (byte)item.Type,
                    item.OrderPosition
                    );
            }

            //remove
            table.Where(row => Utils.IsActiveRow(row) && idsForDeleting.Contains(row.ID))
                .ToList()
                .ForEach(row => table.RemoveDescriptionRow(row));
            
            //update
            foreach (var item in DataManager.Updates)
            {
                Description itm = item as Description;
                if (itm != null)
                {
                    var rw = table.FirstOrDefault(row => row.ID == itm.ID);
                    if (rw != null)
                    {
                        rw.Description = itm.EditValue;
                        rw.OrderPosition = itm.OrderPosition;
                        rw.ParentID = itm.ParentID;
                    }
                }
            }
            table.AcceptChanges();
        }

        public void FocusParentNode()
        {
            treeList1.FocusedNode = treeList1.Selection[0].ParentNode;
        }

        public static bool IsEmptyNode(TreeListNode node)
        {
            if (node == null || Convert.IsDBNull(node.GetValue(DBConstants.ENTITY_TABLE_ID)))
            {
                return true;
            }
            return false;
        }

        public static bool IsNoteNode(TreeListNode node)
        {
            if (!IsEmptyNode(node) && DataTypes.NOTE == (DataTypes)node.GetValue(DBConstants.ENTITY_TABLE_TYPE))
            {
                return true;
            }
            return false;
        }

        private void SortTreeList()
        {
            this.treeList1.BeginSort();
            this.treeList1.Columns[0].SortOrder = SortOrder.Ascending;
            this.treeList1.EndSort();
        }

        private void ClearTreeList()
        {
            treeList1.Nodes.Clear();
        }

        private void CompareNodeValues(object sender, EventArgs arg)
        {
            CompareNodeValuesEventArgs e = arg as CompareNodeValuesEventArgs;
            if (e != null)
            {
                e.Result = ((int)e.Node1.GetValue(DBConstants.ENTITY_TABLE_POSITION)).CompareTo(
                    (int)e.Node2.GetValue(DBConstants.ENTITY_TABLE_POSITION));
            }
        } 
  
        #endregion

        private void FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            DisableFocusing();
            UpdatePreviousNode(e);
            SetRTFData(e);
            EnableFocusing();
        }

        private void UpdatePreviousNode(FocusedNodeChangedEventArgs e)
        {
            TreeListNode node = e.OldNode;
            if (IsEmptyNode(node))
            {
                return;
            }
            if (IsChangedDescription(node))
            {
                UpdateDescription(node);
            }
                
            if (IsNoteNode(node) && richEditControl.Modified)
            {
                UpdateTextData(node);
            }
        }

        private bool IsChangedDescription(TreeListNode node)
        {
            DataTable table = treeList1.DataSource as DataTable;
            if (table != null )
            {
                DataRow row = table.AsEnumerable()
                                .FirstOrDefault(rw => Utils.IsActiveRow(rw) && rw.Field<long>(DBConstants.ENTITY_TABLE_ID) == 
                                    (long)node.GetValue(DBConstants.ENTITY_TABLE_ID));
                return row != null && row.RowState == DataRowState.Modified;
            }
            return false;
        }

        #region DATA CHANGING

        private void UpdateTextData(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            DataManager.UpdateTextData(id, EditValue, PlainText);
        }

        private void UpdateDescription(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            string description = (string)node.GetValue(DBConstants.ENTITY_TABLE_DESC);
            DataManager.UpdateDescription(id, description);
        }

        #endregion

        private void SetRTFData(FocusedNodeChangedEventArgs e)
        {
            TreeListNode node = e.Node;
            bool isNoteNode = IsNoteNode(node);
            if (isNoteNode)
            {
                long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                EditValue = DataManager.GetTextData(id); //selRow.Data;
            }
            ChangeState(isNoteNode);
        }

    }
}
