using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Algoritms
{
    public partial class AddBranchBlocks : Form
    {
        public List<BaseItem> Blocks
        {
            get { return parent.Items; }
        }
        
        BaseItem parent;
        int prevIndex = -1;

        public AddBranchBlocks(BaseItem parent)
        {
            InitializeComponent();
            if (parent == null)
            {
                this.parent = new BranchItem();
                //blocks = new List<BaseItem>();
            }
            else
            {
                this.parent = new BranchItem();
                this.parent.Copy(parent.Items);
                //blocks = this.parent.Items;
                FillListBox();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string condition = "";
            DataType type = DataType.branchElse;
            if(radioButtonCondition.Checked)
            {
                condition = textBoxCondition.Text;
                if (string.IsNullOrEmpty(condition))
                {
                    return;
                }
                type = DataType.branchCondition;
            }
            else if (!radioButtonElse.Checked && string.IsNullOrEmpty(condition))
            {
                return;
            }
            BranchBlockItem temp = new BranchBlockItem(condition, type, 0, 0, 0, parent);
            if (!parent.Items.Contains(temp) && AddItem(temp))
            {
                FillListBox();
                textBoxCondition.Text = "";
            }
        }

        private bool AddItem(BranchBlockItem temp)
        {
            if (IsElseAdded(parent))
            {
                if(temp.Type_ == DataType.branchElse)
                {
                    return false;
                }
                parent.Items.Insert(parent.Items.Count - 1, temp);
            }
            else
            {
                if (parent.Items.Count == 0 && temp.Type_ == DataType.branchElse)
                {
                    return false;
                }
                parent.Items.Add(temp);
            }
            return true;
        }

        private bool IsElseAdded(BaseItem parent)
        {
            foreach (BaseItem item in parent.Items)
            {
                if (item.Type_ == DataType.branchElse)
                {
                    return true;
                }
            }
            return false;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxConditions.SelectedIndex == -1)
            {
                return;
            }
            parent.Items.RemoveAt(listBoxConditions.SelectedIndex);
            prevIndex = -1;
            textBoxCondition.Text = "";
            FillListBox();
        }

        private void FillListBox()
        {
            listBoxConditions.Items.Clear();
            foreach (BranchBlockItem block in parent.Items)
            {
                listBoxConditions.Items.Add(block.GetDescription("L"));
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (listBoxConditions.SelectedIndex == 0)
            {
                return;
            } 
            this.listBoxConditions.SelectedIndexChanged -= new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);

            if (MoveMethod(true))
            {
                FillListBox();
                listBoxConditions.SelectedIndex = --prevIndex;
            }
            this.listBoxConditions.SelectedIndexChanged += new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);

        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (listBoxConditions.SelectedIndex >= parent.Items.Count - 1)
            {
                return;
            } 
            this.listBoxConditions.SelectedIndexChanged -= new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);

            if(MoveMethod(false))
            {
                FillListBox();
                listBoxConditions.SelectedIndex = ++prevIndex;
            }
            this.listBoxConditions.SelectedIndexChanged += new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);

        }

        private bool MoveMethod(bool isUp)
        {
            if (listBoxConditions.SelectedIndex == -1)
            {
                return false;
            }
            BaseItem parentItem = parent;
            BaseItem currentItem = parentItem.Items[listBoxConditions.SelectedIndex];
            return UTILS.MoveMethod(parentItem, currentItem, isUp);
        }

        private void listBoxConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxConditions.SelectedIndex == -1)
            {
                return;
            }
            if (prevIndex != -1 && prevIndex != listBoxConditions.SelectedIndex
                && parent.Items[prevIndex].Type_ != DataType.branchElse)
            {
                (parent.Items[prevIndex] as BranchBlockItem).Condition = textBoxCondition.Text;
            }

            textBoxCondition.Text = (parent.Items[listBoxConditions.SelectedIndex] as BranchBlockItem).Condition;
            if (parent.Items[listBoxConditions.SelectedIndex].Type_ == DataType.branchElse)
            {
                radioButtonElse.Checked = true;
            }
            else
            {
                radioButtonCondition.Checked = true;
            }            
            prevIndex = listBoxConditions.SelectedIndex;

            
            this.listBoxConditions.SelectedIndexChanged -= new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);
            FillListBox();
            listBoxConditions.SelectedIndex = prevIndex;
            this.listBoxConditions.SelectedIndexChanged += new System.EventHandler(this.listBoxConditions_SelectedIndexChanged);

        }
    }
}