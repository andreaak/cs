using System;
using System.Windows.Forms;
using WorkWithSvn.RepositoryProviders;

namespace WorkWithSvn
{
    public partial class SwitchLocation : Form
    {
        public string TargetLocation
        {
            get;
            private set;
        }
 
        public bool IsRestoreFile
        {
            get;
            private set;
        }

        public bool IsBackupFile
        {
            get;
            private set;
        }

        public SwitchLocation(string path, IProvider provider)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            textBoxLocation.Text = provider.GetLocation(path);
            textBoxRepository.Text = Convert.ToString(provider.GetRepository(path));
        }

        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            TargetLocation = textBoxTargetLocation.Text;
            IsRestoreFile = checkBoxRestoreFile.Checked;
            IsBackupFile = checkBoxBackUpFile.Checked;
        }
    }
}