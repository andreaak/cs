using DBServices;
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
    public partial class ItemsList: Form
    {
        public ItemsList()
        {
            InitializeComponent();
        }

        public void LoadData(IEnumerable<Entity> items)
        {
            ClearTreeList();
            treeList1.DataSource = GetTableForBinding(items);
            SortTreeList();
        }

        private static DBServiceDataset.EntityDataTable GetTableForBinding(IEnumerable<Entity> items)
        {
            DBServiceDataset.EntityDataTable table = new DBServiceDataset.EntityDataTable();
            foreach (var item in items)
            {
                table.AddEntityRow(
                    item.ID,
                    (long)item.ParentID,
                    item.Description,
                    (byte)item.Type,
                    (long)item.OrderPosition,
                    item.ModDate
                    );
            }
            return table;
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
    }
}
