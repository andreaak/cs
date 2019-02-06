using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonVerticalReel_Click(object sender, EventArgs e)
        {
            IList<Reel> reels = JSonParser.ParseReels(textBox1.Text);
            Form form = new VerticalReelsForm(reels);
            form.Show();
        }

        private void buttonGorizontalReel_Click(object sender, EventArgs e)
        {
            IList<Reel> reels = JSonParser.ParseReels(textBox1.Text);
            Form form = new HorizontalReelsForm(reels);
            form.Show();
        }

        private void buttonRemoveIndex_Click(object sender, EventArgs e)
        {
            IList<Reel> reels = JSonParser.ParseReels(textBox1.Text);
            Form form = new RemoveIndexForm(reels);
            form.Show();
        }

        private void buttonCheckShiftPreparer_Click(object sender, EventArgs e)
        {
            IList<Reel> reels = JSonParser.ParseReels(textBox1.Text);
            Form form = new CheckShiftReelsPrepareForm(reels);
            form.Show();
        }
    }
}
