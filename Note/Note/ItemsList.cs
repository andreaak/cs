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

        public void LoadData(IEnumerable<Tuple<Entity, DataStatus>> items)
        {
            ClearTreeList();
            treeList1.DataSource = GetTableForBinding(items);
            SortTreeList();
        }

        //private static DBServiceDataset.EntityDataTable GetTableForBinding(IEnumerable<Tuple<Entity, DataStatus>> items)
        //{
        //    List<object> list = new List<object>();
            
            
        //    DBServiceDataset.EntityDataTable table = new DBServiceDataset.EntityDataTable();
            
        //    foreach (var item in items)
        //    {
        //        table.AddEntityRow(
        //            item.Item1.ID,
        //            (long)item.Item1.ParentID,
        //            item.Item1.Description,
        //            (byte)item.Item1.Type,
        //            (long)item.Item1.OrderPosition,
        //            item.Item1.ModDate
        //            );
        //    }
        //    return table;
        //}

        private static List<object> GetTableForBinding(IEnumerable<Tuple<Entity, DataStatus>> items)
        {
            List<object> list = new List<object>();
            foreach (var item in items)
            {
                list.Add(new 
                {
                    ID = item.Item1.ID,
                    ParentID = (long)item.Item1.ParentID,
                    Description = item.Item1.Description,
                    Type = (byte)item.Item1.Type,
                    OrderPosition = (long)item.Item1.OrderPosition,
                    DataStatus = item.Item2 == DataStatus.Parent ? string.Empty: item.Item2.ToString() ,
                });
            }
            return list;
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
