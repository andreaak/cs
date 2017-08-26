using System.Collections.Generic;using System.Windows.Forms;
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
