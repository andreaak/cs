using System;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class AddChangeList : Form
    {
        public string ChangeListName
        {
            get;
            private set;
        }

        public AddChangeList()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ChangeListName = textBoxChangeList.Text;
        }
    }
}