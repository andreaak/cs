using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FindWord
{
    public partial class Form1 : Form
    {
        private IList<string> lines;
        private System.Timers.Timer aTimer = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();
            //lines = File.ReadAllLines("D:\\Projects\\My\\cs\\Note\\Note\\bin\\Debug\\russian.dic");


            var files = Directory.GetFiles("D:\\", "russian-nouns*.txt");
            
            var set = new HashSet<string>();
            foreach (var file in files)
            {
                set.UnionWith(File.ReadAllLines(file).Select(i => i.Trim()));
            }
            
            //set.Add((File.ReadAllLines("D:\\russian-nouns.txt")).Concat(File.ReadAllLines("D:\\russian-nouns.txt")))

            lines = set.OrderBy(i => i).ToArray();// File.ReadAllLines("D:\\russian-nouns.txt");
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 2000;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Text = "";
                timer1.Stop();
                textBox2.Text = label1.Text = "-";
                return;
            }

            //aTimer.Stop();

            //aTimer.Start();
            timer1.Stop();
            timer1.Start();

            //aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            aTimer.Stop();
            //string st = textBox1.Text;
            //var find = lines.Where(i => i.StartsWith(textBox1.Text)).ToArray();
            //textBox2.Text = "Test";// string.Join("\r\n", find);
            aTimer.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            string st = textBox1.Text;
            var find = lines.Where(i => i.StartsWith(st)).ToArray();

            if (find.Length == 0)
            {
                textBox2.Text = "-";
                label1.Text = find.Length.ToString();
            }
            else
            {
                textBox2.Text = string.Join("\r\n", find);
                label1.Text = find.Length.ToString();
            }
            
        }
    }
}
