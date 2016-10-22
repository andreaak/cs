using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace Utils.WorkWithDB
{
    public partial class SelectConnection : Form
    {
        public string ConnectionName
        {
            get
            {
                return Convert.ToString(comboBoxConnectionNames.SelectedValue);
            }
        }
        
        public SelectConnection(Dictionary<string, ConnectionStringSettings> connections)
        {
            InitializeComponent();
            comboBoxConnectionNames.DataSource = connections.Keys.ToList();
        }
    }
}
