using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxItem1.Text))
            {
                try
                {
                    var res = Decryptor.Encrypt(textBoxItem1.Text, textBoxPassword.Text, textBoxSalt.Text);
                    textBoxItem2.Text = res;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, exception.Message, "Error", MessageBoxButtons.OK);
                }
            }
            else if (!string.IsNullOrEmpty(textBoxItem2.Text))
            {
                try
                {
                    var res = Decryptor.Decrypt(textBoxItem2.Text, textBoxPassword.Text, textBoxSalt.Text);
                    textBoxItem1.Text = res;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, exception.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = Cipher.DecryptFromAzure(textBoxItem1.Text, EncryptFor.DeliveryValue);
        }
    }
}
