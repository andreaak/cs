using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class SelectExtensions : Form
    {
        List<string> extensions;
        List<string> selectedExtensions;
        public List<string> SelectedExtensions
        {
            get { return selectedExtensions; }
        }

        public SelectExtensions(List<string> extensions)
        {
            this.extensions = extensions;
            InitializeComponent();
            listBox1.DataSource = extensions;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            selectedExtensions = new List<string>();
            foreach (string item in listBox1.SelectedItems)
            {
                selectedExtensions.Add(item);
            }
        }
    }
}
