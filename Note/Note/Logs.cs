using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Note.Domain.Repository;

namespace Note
{
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
        }

        public void LoadData(IEnumerable<LogData> items)
        {
            gridControl1.DataSource = items;
        }
    }
}
