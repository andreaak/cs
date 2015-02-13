using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Algoritms
{
    public partial class EditMethod : Form
    {
        List<BaseItem> items;
        DataType type;
        bool changed = false;
        bool blocksChanged = false;
        public bool Changed
        {
            get { return changed; }
        }

        List<BaseItem> blocks = null;

        BaseItem currentItem = null;
        public List<BaseItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public EditMethod(TreeNode selectedNode, List<BaseItem> methods)
        {
            InitializeComponent();
            this.items = methods;
            currentItem = GetCurrentItem(selectedNode, this.items);
            type = currentItem.Type_;
            Init();
        }

        private void Init()
        {
            foreach (string ln in BaseItem.langCollection.Values)
            {
                comboBoxLanguage.Items.Add(ln);
            }

            textBoxNamespace.Text = currentItem.NamespaceStr;
            textBoxClass.Text = currentItem.Class_;
            textBoxName.Text = currentItem.Name;
            comboBoxLanguage.SelectedItem = currentItem.Language;
            
            switch (type)
            {
                case DataType.branch:
                    this.Text = "Edit Branch";
                    buttonEditBranchBlock.Visible = true;
                    textBoxParameters.Enabled = false;
                    InitNonMethod();
                    break;
                case DataType.cycle:
                    this.Text = "Edit Cycle";
                    labelParameters.Text = "Condition:";
                    textBoxParameters.Text = currentItem.GetParameters();
                    InitNonMethod();
                    break;
                default:
                    textBoxParameters.Text = currentItem.GetParameters();
                    checkBoxStatic.Checked = currentItem.Static_;
                break;
            }

        }

        private void InitNonMethod()
        {
            checkBoxStatic.Enabled = false;
            textBoxNamespace.Enabled = textBoxClass.Enabled = textBoxName.Enabled = comboBoxLanguage.Enabled = false;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (!currentItem.IsRoot() && !textBoxClass.Text.Equals("") && !textBoxName.Text.Equals(""))
            {

                currentItem.NamespaceStr=textBoxNamespace.Text;
                currentItem.Class_=textBoxClass.Text;
                currentItem.Name=textBoxName.Text;
                currentItem.Static_=checkBoxStatic.Checked;
                currentItem.Language=comboBoxLanguage.Text;
                currentItem.SetParam(textBoxParameters.Text);
                if (currentItem.Type_ == DataType.branch && blocksChanged)
                {
                    currentItem.SetItems(blocks);
                }
                changed = true;
                this.Close();
            }
        }

        private BaseItem GetCurrentItem(TreeNode selectedNode, List<BaseItem> methods)
        {
            foreach (BaseItem method in methods)
            {
                BaseItem temp = method.GetItemById(selectedNode.Name);
                if (temp != null)
                {
                    return temp;
                }
            }
            throw new Exception("GetCurrentMethod Error");
        }

        private void buttonEditBranchBlock_Click(object sender, EventArgs e)
        {
            AddBranchBlocks form = new AddBranchBlocks(currentItem);
            if (form.ShowDialog() == DialogResult.OK)
            {
                blocks = form.Blocks;
                blocksChanged = true;
            }

        }

        private void EditMethod_Shown(object sender, EventArgs e)
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
