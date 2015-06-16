using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace WorkWithSvn
{
    public partial class SelectExtensions : Form
    {
        public ISet<string> SelectedExtensions
        {
            get;
            private set;
        }

        public SelectExtensions(ISet<string> extensions)
        {
            InitializeComponent();
            listBox1.DataSource = new List<string>(extensions);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SelectedExtensions = new HashSet<string>();
            foreach (string item in listBox1.SelectedItems)
            {
                SelectedExtensions.Add(item);
            }
        }
    }
}
