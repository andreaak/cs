using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Note
{
    public partial class DBDataForm : Form
    {
        public DBDataForm()
        {
            InitializeComponent();
            textBoxProvider.Text = OptionsUtils.Provider;
            textBoxConStr.Text = OptionsUtils.ConnectionString;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            OptionsUtils.Provider = textBoxProvider.Text;
            OptionsUtils.ConnectionString = textBoxConStr.Text;
        }
    }
}
