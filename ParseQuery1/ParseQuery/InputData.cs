using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParseQuery
{
    public partial class InputData : Form
    {
        Parameter data;

        public Parameter Data
        {
            get { return data; }
        }

        public InputData(object data)
        {
            this.data = data as Parameter;
            InitializeComponent();
            textBoxValue.DataBindings.Add("Text", this.data, "Value");
            comboBoxType.DataSource = Parameter.GetParameterTypes();
            comboBoxType.SelectedItem = this.data.Type.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedItem == null)
            {
                return;
            }
            data = new Parameter(comboBoxType.SelectedItem.ToString(), textBoxValue.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
