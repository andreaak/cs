using Note.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Note
{
    public partial class Find : Form
    {
        private Domain.DatabaseManager databaseManager;

        public Find()
        {
            InitializeComponent();
        }

        public Find(Domain.DatabaseManager databaseManager)
        {
            this.databaseManager = databaseManager;
            InitializeComponent();
        }

        private void simpleButtonFind_Click(object sender, EventArgs e)
        {
            ClearTreeList();
            IEnumerable<Description> items = databaseManager.Find(textEdit1.Text);
            treeList.DataSource = GetTableForBinding(items);
            SortTreeList();
        }

        private static List<object> GetTableForBinding(IEnumerable<Description> items)
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
                });
            }
            return list;
        }

        private void SortTreeList()
        {
            this.treeList.BeginSort();
            this.treeList.Columns[0].SortOrder = SortOrder.Ascending;
            this.treeList.EndSort();
        }

        private void ClearTreeList()
        {
            treeList.Nodes.Clear();
        }
    }
}
