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
    public partial class AddMethod : Form
    {
        DataType type;

        BaseItem created = null;
        List<BaseItem> blocks = null;
        bool changed = false;
        public bool Changed
        {
            get { return changed; }
        }

        BaseItem currentItem = null;
        
        List<BaseItem> methods;
        public List<BaseItem> Methods
        {
            get { return methods; }
            set { methods = value; }
        }

        public AddMethod(TreeNode selectedNode, List<BaseItem> methods, DataType type)
        {
            InitializeComponent();
            this.methods = methods;
            this.type = type;
            currentItem = WorkWithTree.GetCurrentItem(selectedNode, this.methods);
            Init();
        }

        private void Init()
        {
            foreach (string ln in BaseItem.langCollection.Values)
            {
                comboBoxLanguage.Items.Add(ln);
            }            
            switch (type)
            {
                case DataType.branch:
                    this.Text = "Add Branch";
                    buttonAddBranchBlock.Visible = true;
                    textBoxParameters.Enabled = false;
                    InitNonMethod();
                    break;
                case DataType.cycle:
                    this.Text = "Add Cycle";
                    labelParameters.Text = "Condition:";
                    InitNonMethod();
                    break;
            }

        }

        private void InitNonMethod()
        {
            textBoxNamespace.Text = currentItem.NamespaceStr;
            textBoxClass.Text = currentItem.Class_;
            textBoxName.Text = currentItem.Name;
            comboBoxLanguage.SelectedItem = currentItem.Language;
            checkBoxStatic.Enabled = false;
            textBoxNamespace.Enabled = textBoxClass.Enabled = textBoxName.Enabled = comboBoxLanguage.Enabled = false;
            buttonParse.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxClass.Text) && !string.IsNullOrEmpty(textBoxName.Text))
            {

                switch (type)
                {
                    case DataType.method:
                        created = new MethodItem(textBoxNamespace.Text, textBoxClass.Text, textBoxName.Text, textBoxParameters.Text, checkBoxStatic.Checked, comboBoxLanguage.Text, currentItem);
                        break;
                    case DataType.branch:
                        if (blocks == null || blocks.Count == 0)
                        {
                            MessageBox.Show("Add condition");
                            return;
                        }
                        BranchItem tempBranch = new BranchItem(textBoxNamespace.Text, textBoxClass.Text, textBoxName.Text, textBoxParameters.Text, checkBoxStatic.Checked, comboBoxLanguage.Text
                            , 0, 0, 0, "", currentItem);
                        tempBranch.AddBlocks(blocks);
                        created = tempBranch;
                        break;
                    case DataType.cycle:
                        created = new CycleItem(textBoxNamespace.Text, textBoxClass.Text, textBoxName.Text, textBoxParameters.Text, checkBoxStatic.Checked, comboBoxLanguage.Text, currentItem);
                        break;
                }

                currentItem.Items.Add(created);
                if (type != DataType.cycle && type != DataType.branch)
                {
                    ClearData();
                }
                changed = true;
            }
        }

        private void ClearData()
        {
            textBoxNamespace.Text = textBoxClass.Text = textBoxName.Text = textBoxParameters.Text = string.Empty;
            checkBoxStatic.Checked = false;
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            if (textBoxNamespace.Text == string.Empty)
            {
                return;
            }
                
            try
            {
                MethodItem temp = new MethodItem(textBoxNamespace.Text);
                textBoxNamespace.Text = temp.NamespaceStr;
                textBoxClass.Text = temp.Class_;
                textBoxName.Text = temp.Name;
                textBoxParameters.Text = temp.GetParameters();

            }
            catch (Exception)
            {
            }
        }

        private void buttonAddBranchBlock_Click(object sender, EventArgs e)
        {
            AddBranchBlocks form = new AddBranchBlocks(new BranchItem());
            if (form.ShowDialog() == DialogResult.OK)
            {
                blocks = form.Blocks;
            }
        }

        private void AddMethod_Shown(object sender, EventArgs e)
        {
            if (currentItem.Type_ == DataType.branch)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
