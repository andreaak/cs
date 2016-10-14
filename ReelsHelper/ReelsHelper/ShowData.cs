using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class ShowData : Form
    {
        public ShowData()
        {
            InitializeComponent();
        }

        public void LoadData(string data)
        {
            textBox1.Text = data;
        }
    }
}
