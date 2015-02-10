using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Algoritms
{
    public partial class InsertMethods : Form
    {
        List<BaseItem> items;
        BaseItem currentItem = null;
        BaseItem parrentItem = null;

        bool changed = false;
        public bool Changed
        {
            get { return changed; }
        }

        public List<BaseItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public InsertMethods(TreeNode selectedNode, List<BaseItem> items)
        {
            InitializeComponent();
            this.items = items;
            currentItem = WorkWithTree.GetCurrentItem(selectedNode, this.items);
            parrentItem = currentItem.Parent;
            listBox1.Items.Clear();

            foreach (BaseItem item in parrentItem.Items)
            {
                if (item.Equals(currentItem))
                {
                    continue;
                }
                if (item.Type_ == DataType.branch)
                {
                    foreach (BaseItem subItem in item.Items)
                    {
                        listBox1.Items.Add(subItem);
                    }
                }
                else
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            BaseItem selectedItem = (BaseItem)listBox1.SelectedItem;
            selectedItem.AddItem(currentItem);
            parrentItem.Items.Remove(currentItem);

            changed = true;

            this.Close();
        }

        private void InsertMethods_Shown(object sender, EventArgs e)
        {
            if (currentItem.Type_ == DataType.branchCondition ||
                currentItem.Type_ == DataType.branchElse)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
