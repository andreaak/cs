using System;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class CommitMessage : Form
    {
        public string Message
        {
            get;
            private set;
        }

        public CommitMessage()
        {
            InitializeComponent();
            richTextBoxCommitMessage.Text = Constants.COMMIT_MESSAGE;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Message = richTextBoxCommitMessage.Text;
        }
    }
}