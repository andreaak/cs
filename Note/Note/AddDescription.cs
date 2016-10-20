using System;

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