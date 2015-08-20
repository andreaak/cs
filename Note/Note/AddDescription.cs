using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Note
{
    public partial class AddDescription : DevExpress.XtraEditors.XtraForm
    {
        string description;
        public string Description
        {
            get { return description; }
        }
        
        public bool IsChildNode
        {
            get { return checkEditInsertChild.Checked; }
        }

        public AddDescription()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            description = textEditDescription.Text;
        }
    }
}