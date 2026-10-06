using System;
using System.Windows.Forms;

namespace Note.ExportData
{
    public partial class ExportOptions : Form
    {
        public bool IndexNumeration
        {
            get { return checkBoxIndexNumeration.Checked; }
        }

        public bool ThroughNumeration
        {
            get { return checkBoxThroughNumeration.Checked; }
        }

        public ExportDocTypes Type
        {
            get { return (ExportDocTypes)comboBoxExportType.SelectedValue; }
        }

        public bool IsCreateFolders
        {
            get { return checkBoxCreateFolders.Checked; }
        }

        public bool IsOneFile
        {
            get { return checkBoxOneFile.Checked; }
        }

        public bool IsSelected
        {
            get { return checkBoxOnlySelected.Checked; }
        }

        public ExportOptions()
        {
            InitializeComponent();
            comboBoxExportType.DataSource = Enum.GetValues(typeof(ExportDocTypes));
            comboBoxExportType.SelectedIndex = 0;
        }

        private void checkBoxIndexNumeration_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxThroughNumeration.Enabled = checkBoxIndexNumeration.Checked;
        }

        private void Export_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (comboBoxExportType.SelectedValue == null)
            {
                e.Cancel = true;
            }
        }

    }
}
