using System;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class AddChangeList : Form
    {
        private string changeListName;
        public string ChangeListName
        {
            get { return changeListName; }
        }
        public AddChangeList()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            changeListName = textBoxChangeList.Text;
        }
    }
}