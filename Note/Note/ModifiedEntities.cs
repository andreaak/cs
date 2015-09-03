using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Note.Domain.Common;
using Note.Domain.Repository;
using Note.Domain.Entities;

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
            treeList.DataSource = GetTableForBinding(items);
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
