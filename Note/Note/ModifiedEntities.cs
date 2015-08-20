using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataManager.Repository;

namespace Note
{
    public partial class ItemsList: Form
    {
        public ItemsList()
        {
            InitializeComponent();
        }

        public void LoadData(IEnumerable<Tuple<Description, DataStatus>> items)
        {
            ClearTreeList();
            treeList1.DataSource = GetTableForBinding(items);
            SortTreeList();
        }

        private static List<object> GetTableForBinding(IEnumerable<Tuple<Description, DataStatus>> items)
        {
            List<object> list = new List<object>();
            foreach (var item in items)
            {
                list.Add(new 
                {
                    ID = item.Item1.ID,
                    ParentID = item.Item1.ParentID,
                    Description = item.Item1.EditValue,
                    Type = item.Item1.Type,
                    OrderPosition = item.Item1.OrderPosition,
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
