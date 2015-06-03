using System;
using System.Windows.Forms;
using WorkWithSvn.RepositoryProviders;

namespace WorkWithSvn
{
    public partial class SwitchLocation : Form
    {
        private string baseLocation;
        private Uri repository;
        private string targetLocation;
        public string TargetLocation
        {
            get { return targetLocation; }
        }
 
        private bool restoreFile;
        public bool RestoreFile
        {
            get { return restoreFile; }
        }

        private bool backupFile;
        public bool BackupFile
        {
            get { return backupFile; }
        }


        public SwitchLocation(string path, AProvider provider)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            baseLocation = provider.GetLocation(path);
            textBoxLocation.Text = baseLocation;
            repository = provider.GetRepository(path);
            textBoxRepository.Text = repository.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            targetLocation = textBoxTargetLocation.Text;
            restoreFile = checkBoxRestoreFile.Checked;
            backupFile = checkBoxBackUpFile.Checked;
            this.Close();
        }
    }
}