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
    }
}
