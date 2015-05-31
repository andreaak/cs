using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class SelectExtensions : Form
    {
        ISet<string> extensions;
        ISet<string> selectedExtensions;
        public ISet<string> SelectedExtensions
        {
            get { return selectedExtensions; }
        }

        public SelectExtensions(ISet<string> extensions)
        {
            this.extensions = extensions;
            InitializeComponent();
            listBox1.DataSource = extensions;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            selectedExtensions = new HashSet<string>();
            foreach (string item in listBox1.SelectedItems)
            {
                selectedExtensions.Add(item);
            }
        }
    }
}
