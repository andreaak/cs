using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class VerticalReelsForm : Form
    {
        private IList<Reel> _reels;
        private BindingSource bindingSource1 = new BindingSource();

        public VerticalReelsForm(IList<Reel> reels)
        {
            InitializeComponent();
            _reels = reels;
            bindingSource1.DataSource = CreateVerticalReelDataTable(reels);
            dataGridView1.DataSource = bindingSource1;
        }

        private static DataTable CreateVerticalReelDataTable(IList<Reel> reels)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < reels.Count; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Reel " + i;
                table.Columns.Add(column);
            }

            for (int i = 0; i < reels.Max(reel => reel.Indexes.Count); i++)
            {
                DataRow row = table.NewRow();
                for (int j = 0; j < reels.Count; j++)
                {
                    row.SetField(j, reels[j][i]);
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private void buttonGetReels_Click(object sender, System.EventArgs e)
        {
            string json = GetReelsJson();
            var form = new ShowData();
            form.LoadData(json);
            form.Show();
        }

        private string GetReelsJson()
        {
            var table = (DataTable)dataGridView1.DataSource;

            int reelCount = table.Columns.Count;

            IList<Reel> newReels = new List<Reel>();
            for (int i = 0; i < reelCount; i++)
            {
                newReels.Add(new Reel());
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < reelCount; i++)
                {
                    newReels[i].Add(row.Field<int>(i));
                }
            }

            return $"{new string(' ', 8)}\"reels\": " +
                JSonParser.CreateJson(newReels.Select(item => item.Indexes.ToArray()).ToArray())
                .Replace("],[", $"],\r\n{new string(' ', 12)}[")
                .Replace("[[", $"[\r\n{new string(' ', 12)}[")
                .Replace("]]", $"]\r\n{new string(' ', 8)}]")
                .Replace(",", ", ");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int maxIndex = (int)numericUpDown1.Value;
            labelError.Visible = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ErrorText = string.Empty;
                    int index;
                    if (maxIndex > 0 && int.TryParse(Convert.ToString(cell.Value), out index) && maxIndex < index)
                    {
                        cell.ErrorText = "Wrong Index";
                        labelError.Visible = true;
                    }
                }
            }
        }
    }
}
