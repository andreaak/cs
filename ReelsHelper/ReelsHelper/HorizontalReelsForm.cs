using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class HorizontalReelsForm : Form
    {
        public HorizontalReelsForm(IList<Reel> reels)
        {
            InitializeComponent();
            dataGridView1.DataSource = CreateHorizontalReelDataTable(reels);
        }

        private static DataTable CreateHorizontalReelDataTable(IList<Reel> reels)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < reels.Max(reel => reel.Indexes.Count); i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Index " + i;
                table.Columns.Add(column);
            }

            foreach (Reel reel in reels)
            {
                DataRow row = table.NewRow();
                for (int i = 0; i < reel.Indexes.Count; i++)
                {
                    row[i] = reel[i];
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

            int reelCount = table.Rows.Count;

            IList<Reel> newReels = new List<Reel>();
            for (int i = 0; i < reelCount; i++)
            {
                newReels.Add(new Reel());
            }

            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                Reel reel = newReels[rowIndex];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    reel.Add(row.Field<int>(i));
                }
                rowIndex++;
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
