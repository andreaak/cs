using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DataManager.Domain;
using ExportData;
using Note.ControlWrapper;
using Utils;
using Utils.ActionWindow;
using Note.Properties;

namespace Note.ExportData
{
    public class ExportHelper
    {
        private const char Separator = '_';
        private const char PaddingSymbol = '0';
        private const char FileExtensionSaparator = '.';

        private static int nodeIndex = 0;
        private static int charCount = 0;

        private readonly ITreeWrapper treeWrapper;
        private readonly MainPresenter presenter;
        
        private bool isCreateFolders;
        private Func<Node, string> GetPrefixDelegate;

        public ExportHelper(ITreeWrapper treeWrapper, MainPresenter presenter)
        {
            this.treeWrapper = treeWrapper;
            this.presenter = presenter;
        }

        public void Export()
        {

            string path = null;
            if (!SelectFolder.Select(Resources.SelectFolder, ref path))
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
                SetPrefixAlgoritmth(form, treeWrapper.Nodes);
                Exporter exp = GetExporter(form.Type);
                SaveNodesData(path, treeWrapper.Nodes, exp);
                ThreadVisualization.OnProcessEnded();
            }));

            string headerText = Resources.ExportCaption;
            CancelForm visForm = new CancelForm(headerText);
            ThreadVisualization.ProcessEnded += visForm.CloseForm;
            workThread.SetApartmentState(ApartmentState.STA);
            if (!ThreadVisualization.StartWorkThread(visForm, workThread))
            {
                return;
            }
        }

        private void SetPrefixAlgoritmth(ExportOptions form, IList<Node> nodes)
        {
            if (!form.IndexNumeration)
            {
                GetPrefixDelegate = GetEmptyPrefix;
            }
            else if (form.ThroughNumeration)
            {
                nodeIndex = 0;
                int count = nodes.SelectMany(item => item.Nodes).Union(nodes).Count();
                charCount = count.ToString().Length + 1;
                GetPrefixDelegate = GetIndexPrefix;
            }
            else
            {
                GetPrefixDelegate = GetPrefix;
            }
        }

        private void SaveNodesData(string path, IList<Node> nodes, Exporter exp)
        {
            try
            {
                foreach (Node node in nodes)
                {
                    SaveNodeData(path, node, exp);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        private void SaveNodeData(string path, Node node, Exporter exp)
        {
            if (node.IsNote)
            {
                string fileName = path + 
                    Path.DirectorySeparatorChar + 
                    GetPrefixDelegate(node) + 
                    Utils.GetValidFileName(node.EditValue) + 
                    FileExtensionSaparator + 
                    exp.GetExtension();
                string data = presenter.GetTextData(node.ID);
                exp.Export(fileName, data);
            }
            else
            {
                if (isCreateFolders)
                {
                    string folder = path + Path.DirectorySeparatorChar + GetPrefixDelegate(node) + Utils.GetValidFileName(node.EditValue);
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

        private string GetEmptyPrefix(Node node)
        {
            return null;
        }

        private string GetPrefix(Node node)
        {
            if (node == null
                || node.Index == DBConstants.BASE_LEVEL)
            {
                return string.Empty;
            }

            int charCount = node.SiblingsCount.ToString().Length + 1;
            return node.Index.ToString().PadLeft(charCount, PaddingSymbol) + Separator;
        }

        private string GetIndexPrefix(Node node)
        {
            return nodeIndex++.ToString().PadLeft(charCount, PaddingSymbol) + Separator;
        }
    }
}
