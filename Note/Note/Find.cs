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

        public Find()
        {
            InitializeComponent();
        }

        public Find(DatabaseManager databaseManager)
        {
            this.databaseManager = databaseManager;
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
    }
}
