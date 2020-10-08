using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.API.Word;
using Note.Domain;
using Note.Domain.Entities;

namespace Note
{
    public partial class Find : Form
    {
        private Domain.DatabaseManager databaseManager;
        private Action<int> focus;
        const string RegPreffix = "Reg_ ";

        public Find()
        {
            InitializeComponent();
        }

        public Find(DatabaseManager databaseManager, Action<int> focus)
        {
            this.databaseManager = databaseManager;
            this.focus = focus;
            InitializeComponent();
        }

        private void simpleButtonFind_Click(object sender, EventArgs e)
        {
            ClearTreeList();

            var request = textEditFind.Text;
            bool isRegExpression = request.StartsWith(RegPreffix);
            var entities = isRegExpression ?
                databaseManager.Find("") :
                databaseManager.Find(request);

            if (isRegExpression)
            {
                var list = new List<DescriptionWithText>();
                string pattern = request.Replace(RegPreffix, "");
                RichEditControl control = new RichEditControl();
                foreach (var entity in entities)
                {
                    if (entity.Rtf != null)
                    {
                        control.RtfText = entity.Rtf;
                        Regex myRegEx = new Regex(pattern);
                        var items = control.Document.FindAll(myRegEx);


                        if (items.Length != 0)
                        {
                            var i = items.Select(item => control.Document.GetText(item)).Distinct().ToArray();
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        //list.Add(entity);
                    }
                }

                entities = list;
            }

            if (entities.Any())
            {
                treeList.DataSource = GetDataSource(entities);
            }
            else
            {
               MessageBox.Show("Not found");
            }
        }

        private void simpleButtonReplace_Click(object sender, EventArgs e)
        {
            var request = textEditFind.Text;
            bool isRegExpression = request.StartsWith(RegPreffix);
            var entities = isRegExpression ? 
                databaseManager.FindEntities("") : 
                databaseManager.FindEntities(request);

            RichEditControl control = new RichEditControl();
            foreach (var entity in entities)
            {
                control.RtfText = entity.Data;

                if (!isRegExpression)
                {
                    int i = control.Document.ReplaceAll(request, textEditReplace.Text, SearchOptions.None);
                    if (i == 0)
                    {
                        control.RtfText = control.RtfText.Replace(request, textEditReplace.Text);
                    }
                }
                else
                {
                    //string pattern = request.Replace(RegPreffix, "");//@"\p{Cc}";
                    //Regex myRegEx = new Regex(pattern);
                    //control.Document.ReplaceAll(myRegEx, textEditReplace.Text);
                }

                databaseManager.UpdateTextData(entity.ID, control.RtfText, control.Text, control.HtmlText);
            }
            MessageBox.Show("Done");
        }

        private void ClearTreeList()
        {
            treeList.Nodes.Clear();
        }

        private static object GetDataSource(IEnumerable<DescriptionWithText> items)
        {
            List<object> list = new List<object>();
            foreach (var item in items)
            {
                list.Add(new
                {
                    ID = item.ID,
                    ParentID = item.ParentID,
                    Description = item.EditValue,
                    Type = item.Type,
                    OrderPosition = item.OrderPosition,
                    Text = item.Text,
                });
            }
            return list;
        }

        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var selectedNode = treeList.FocusedNode;
            if (selectedNode == null)
            {
                return;
            }
            var id = (int)selectedNode.GetValue(colId.FieldName);
            var type = (int)selectedNode.GetValue(treeList.ImageIndexFieldName);
            if (type == 1)
            {
                focus(id);
            }
        }
    }
}
