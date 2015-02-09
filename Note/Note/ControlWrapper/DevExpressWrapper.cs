using Note.ControlWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Utils;
using DevExpress.XtraRichEdit.API.Native;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using System.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraBars;
using DBServices;
using ControlWrapper;

namespace Note
{
    class DevExpressWrapper : IWrapper
    {
        DBService dbService;
        private TreeList treeList1;
        private MyRichEditControl richEditControl;
        private ExportHelper export;
        
        BarItem[] controls;

        public DevExpressWrapper(TreeList treeList1, MyRichEditControl richEditControl, BarItem[] controls)
        {
            this.treeList1 = treeList1;
            this.richEditControl = richEditControl;
            this.controls = controls;
        }
        
        public long SelectedNodeId
        {
            get
            {
                TreeListNode node = treeList1.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            }
        }

        public long Position
        {
            get
            {
                TreeListNode node = treeList1.Selection[0];
                return (long)node.GetValue(DBConstants.ENTITY_TABLE_POSITION); ;
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
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.FocusedNodeChanged);
        }

        public void DisableFocusing()
        {
            this.treeList1.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.FocusedNodeChanged);
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

        public string Data
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

        public string TextData
        {
            get
            {
                return richEditControl.Text;
            }
        }

        public string RtfText
        {
            get
            {
                return richEditControl.RtfText;
            }
            set
            {
                richEditControl.RtfText = value;
            }
        }

        public string GetConvertedData(string data, DocTypes inType, DocTypes outType)
        {
            return richEditControl.GetConvertedData(data, inType, outType);
        }

        public void SaveNodesDataToFile()
        {
            export.SaveNodesDataToFile();
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
            this.treeList1.CompareNodeValues += new DevExpress.XtraTreeList.CompareNodeValuesEventHandler(CompareNodeValues);
        }

        public void LoadEntityFromDB()
        {
            DisableFocusing();
            ClearTreeList();
            ClearRtfControl();
            dbService.InitEntity();
            treeList1.DataSource = dbService.BindEntity;
            SortTreeList();
            EnableFocusing();
        }

        public DBService DBService
        {
            set 
            {
                this.dbService = value; 
                export = new ExportHelper(treeList1, richEditControl, dbService);
            }
        }

        #region RTF

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
            if (!IsEmptyNode(node) && (byte)DataTypes.NOTE == (byte)node.GetValue(DBConstants.ENTITY_TABLE_TYPE))
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

        #endregion

        private void FocusedNodeChanged(object sender, EventArgs e)
        {
            DisableFocusing();
            UpdateNode(e);
            SetRTFData(e);
            EnableFocusing();
        }

        private void UpdateNode(EventArgs arg)
        {
            if (arg is FocusedNodeChangedEventArgs)
            {
                FocusedNodeChangedEventArgs argum = (FocusedNodeChangedEventArgs)arg;

                TreeListNode node = argum.OldNode;
                if (IsEmptyNode(node))
                {
                    return;
                }
                if (IsChangedDescription(node))
                {
                    UpdateNodeDescription(node);
                }
                
                if (IsNoteNode(node) && richEditControl.Modified)
                {
                    UpdateNodeData(node);
                }
            }
        }

        private bool IsChangedDescription(TreeListNode node)
        {
            DataTable table = treeList1.DataSource as DataTable;
            if (table != null )
            {
                DataRow row = table.AsEnumerable()
                                .FirstOrDefault(rw => UTILS.IsActiveRow(rw) && rw.Field<long>(DBConstants.ENTITY_TABLE_ID) == 
                                    (long)node.GetValue(DBConstants.ENTITY_TABLE_ID));
                return row != null && row.RowState == DataRowState.Modified;
            }
            return false;
        }

        private void UpdateNodeData(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            dbService.UpdateNodeData(id, Data, TextData);
        }

        private void UpdateNodeDescription(TreeListNode node)
        {
            long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
            dbService.UpdateNodeDescription(id);
        }

        private void CompareNodeValues(object sender, EventArgs arg)
        {
            CompareNodeValuesEventArgs e = arg as CompareNodeValuesEventArgs;
            if (e != null)
            {
                e.Result = ((long)e.Node1.GetValue(DBConstants.ENTITY_TABLE_POSITION)).CompareTo(
                    (long)e.Node2.GetValue(DBConstants.ENTITY_TABLE_POSITION));
            }
        }        

        private void SetRTFData(EventArgs arg)
        {
            if (arg is NodeEventArgs)
            {
                SetRTFData(((NodeEventArgs)arg).Node);
            }
        }
        
        private void SetRTFData(TreeListNode node, bool changeControlState = true)
        {
            bool isNoteNode = IsNoteNode(node);
            if (isNoteNode)
            {
                long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                Data = dbService.GetEntityData(id); //selRow.Data;
            }

            if (changeControlState)
            {
                ChangeState(isNoteNode);
            }
        }


        public void FocusParentNode()
        {
            treeList1.FocusedNode = treeList1.Selection[0].ParentNode;
        }
    }
}
