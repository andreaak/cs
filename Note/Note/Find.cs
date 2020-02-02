using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Note.Domain;
using Note.Domain.Entities;

namespace Note
{
    public partial class Find : Form
    {
        private Domain.DatabaseManager databaseManager;
        private Action<int> focus;

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
            IEnumerable<DescriptionWithText> items = databaseManager.Find(textEdit1.Text);
            treeList.DataSource = GetDataSource(items);
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
            var id = (int)selectedNode.GetValue(colId.FieldName);
            var type = (int)selectedNode.GetValue(treeList.ImageIndexFieldName);
            if (type == 1)
            {
                focus(id);
            }
        }
    }
}
