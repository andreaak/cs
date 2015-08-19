using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DataManager;
using DataManager.Domain;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Note;
using Note.ControlWrapper;
using Utils;
using Utils.ActionWindow;

namespace ExportData
{
    public class ExportHelper
    {
        private static int nodeIndex = 0;
        private static int charCount = 0;
        
        private TreeList treeList;
        private MyRichEditControl richEditControl;
        private NoteDataManager dataManager;
        private bool isCreateFolders;

        private Func<TreeListNode, string> GetPrefixDelegate;

        public ExportHelper(TreeList treeList, MyRichEditControl richEditControl, NoteDataManager dataManager)
        {
            this.treeList = treeList;
            this.richEditControl = richEditControl;
            this.dataManager = dataManager;
        }

        public void SaveNodesDataToFile()
        {

            string path = null;
            if (!Utils.SelectFolder.Select("Select destination folder", ref path))
            {
                return;
            }
            
            ExportOptions form = new ExportOptions();
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }            
            
            Thread workThread = new Thread(new ThreadStart(delegate
            {
                isCreateFolders = form.IsCreateFolders;
                SetPrefixAlgoritmth(form);
                Exporter exp = GetExporter(form.Type);
                richEditControl.BackupData();
                SaveNodesData(path, treeList.Nodes, exp);
                richEditControl.RestoreData();
                ThreadVisualization.OnProcessEnded();
            }));

            string headerText = "Data Export";
            CancelForm visForm = new CancelForm(headerText);
            ThreadVisualization.ProcessEnded += visForm.CloseForm;
            workThread.SetApartmentState(ApartmentState.STA);
            if (!ThreadVisualization.StartWorkThread(visForm, workThread))
            {
                return;
            }
        }

        private void SetPrefixAlgoritmth(ExportOptions form)
        {
            if (!form.IndexNumeration)
            {
                GetPrefixDelegate = GetEmptyPrefix;
            }
            else if (form.ThroughNumeration)
            {
                nodeIndex = 0;
                DataTable table = treeList.DataSource as DataTable;
                if (table != null)
                {
                    charCount = table.Rows.Count.ToString().Length + 1;
                }
                GetPrefixDelegate = GetIndexPrefix;
            }
            else
            {
                GetPrefixDelegate = GetPrefix;
            }
        }

        private void SaveNodesData(string path, TreeListNodes nodes, Exporter exp)
        {
            try
            {
                foreach (TreeListNode node in nodes)
                {
                    SaveNodeData(path, node, exp);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        private void SaveNodeData(string path, TreeListNode node, Exporter exp)
        {
            string nodeText = node.GetDisplayText(0);
            if (DevExpressWrapper.IsNoteNode(node))
            {
                string fileName = path + Path.DirectorySeparatorChar + GetPrefixDelegate(node) + Note.Utils.GetValidFileName(nodeText) + "." + exp.GetExtension();
                long id = (long)node.GetValue(DBConstants.ENTITY_TABLE_ID);
                string data = dataManager.GetData(id);
                exp.Export(fileName, data);
            }
            else
            {
                if (isCreateFolders)
                {
                    string folder = path + Path.DirectorySeparatorChar + GetPrefixDelegate(node) + Note.Utils.GetValidFileName(nodeText);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    SaveNodesData(folder, node.Nodes, exp);
                }
                else
                {
                    SaveNodesData(path, node.Nodes, exp);
                }
            }
        }

        private Exporter GetExporter(ExportDocTypes format)
        {
            switch (format)
            {
                case ExportDocTypes.Pdf:
                    return new PdfExporter();
                case ExportDocTypes.Html:
                case ExportDocTypes.Doc:
                case ExportDocTypes.Rtf:
                default:
                    return new MultiFormatExporter(format);
            }
        }

        private string GetEmptyPrefix(TreeListNode node)
        {
            return null;
        }

        private string GetPrefix(TreeListNode node)
        {
            if (node == null)
            {
                return "";
            }

            TreeListNodes nodes = node.ParentNode == null ? node.TreeList.Nodes : node.ParentNode.Nodes;
            int nodeIndex = nodes.IndexOf(node);
            int charCount = nodes.Count.ToString().Length + 1;
            if (nodeIndex == DBConstants.BASE_LEVEL)
            {
                return "";
            }
            return nodeIndex.ToString().PadLeft(charCount, '0') + '_';
        }

        private string GetIndexPrefix(TreeListNode node)
        {
            return nodeIndex++.ToString().PadLeft(charCount, '0') + '_';
        }
    }
}
