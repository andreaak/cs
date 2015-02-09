using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WorkWithSvn
{
    public partial class Location : Form
    {
        private string baseLocation;
        private Uri repository;
        private string targetLocation;
        public string TargetLocation
        {
            get { return targetLocation; }
            set { targetLocation = value; }
        }

        public Location(string path)
        {
            InitializeComponent();
            baseLocation = UTILS.GetLocation(path);
            textBoxLocation.Text = baseLocation;
            repository = UTILS.GetRepository(path);
            textBoxRepository.Text = repository.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            targetLocation = textBoxTargetLocation.Text;
            this.Close();
        }

        private void buttonGetRepository_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetLocation_Click(object sender, EventArgs e)
        {

        }

        private void textBoxRepository_TextChanged(object sender, EventArgs e)
        {
        }
    }
}