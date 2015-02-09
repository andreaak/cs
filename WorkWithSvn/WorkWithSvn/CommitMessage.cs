using System;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class CommitMessage : Form
    {
        private string commitMessage;
        public string Message
        {
            get { return commitMessage; }
        }

        public CommitMessage()
        {
            InitializeComponent();
            richTextBoxCommitMessage.Text = Constants.COMMIT_MESSAGE;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            commitMessage = richTextBoxCommitMessage.Text;
        }
    }
}