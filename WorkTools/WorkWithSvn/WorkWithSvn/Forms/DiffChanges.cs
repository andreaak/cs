using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace WorkWithSvn
{
    public partial class DiffChanges : Form
    {
        public DiffChanges()
        {
            InitializeComponent();
            textBoxWorkingCopy.Text = Options.GetInstance.WorkingCopyPath;
        }

        private void buttonDiff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRemoteRepository.Text) 
                || string.IsNullOrEmpty(textBoxFiles.Text)
                || string.IsNullOrEmpty(textBoxWorkingCopy.Text))
            {
                return;
            }
            string[] files = textBoxFiles.Text.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in files)
            {
                string file = item.TrimEnd('\r', '\t');
                string workingCopyPath = textBoxWorkingCopy.Text + Path.DirectorySeparatorChar + file;
                string remotePath = textBoxRemoteRepository.Text + Path.DirectorySeparatorChar + file;
                if (!File.Exists(remotePath))
                {
                    DeleteFile(workingCopyPath, remotePath);
                } 
                else if (!File.Exists(workingCopyPath))
                {
                    CopyFile(workingCopyPath, remotePath);
                }
                else
                {
                    UTILS.OpenDiff(workingCopyPath, Path.GetFileName(workingCopyPath), remotePath, true);
                    if (MessageBox.Show("Next File", "Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        break;
                    }
                }
            }
        }

        private static void DeleteFile(string workingCopyPath, string remotePath)
        {
            string message =  string.Format("File doesn't exist in remote copy.\r\n{0}.\r\nDo you want to delete?", remotePath);
            if (File.Exists(workingCopyPath) 
                && MessageBox.Show(message, "Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                File.Delete(workingCopyPath);
            }
        }

        private static void CopyFile(string workingCopyPath, string remotePath)
        {
            string message = string.Format("File doesn't exist in working copy.\r\n{0}.\r\nDo you want to copy?", remotePath);
            if (MessageBox.Show(message, "Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string dir = Path.GetDirectoryName(workingCopyPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Copy(remotePath, workingCopyPath, true);
            }
        }
    }
}
