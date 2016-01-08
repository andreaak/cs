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

        public void LoadData(IEnumerable<DescriptionWithStatus> items)
        {
            ClearTreeList();
            treeList.DataSource = GetDataSource(items);
        }

        private void ClearTreeList()
        {
            treeList.Nodes.Clear();
        }

        private static object GetDataSource(IEnumerable<DescriptionWithStatus> items)
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
                    DataStatus = item.Status == DataStatus.Parent ? string.Empty : item.Status.ToString(),
                });
            }
            return list;
        }
    }
}
