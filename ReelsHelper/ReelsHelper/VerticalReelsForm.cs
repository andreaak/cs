using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class VerticalReelsForm : Form
    {
        public VerticalReelsForm(IList<Reel> reels)
        {
            InitializeComponent();
            dataGridView1.DataSource = CreateVerticalReelDataTable(reels);
        }

        private static DataTable CreateVerticalReelDataTable(IList<Reel> reels)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < reels.Count; i++)
            {
                DataColumn column;
                column = new DataColumn();
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
    }
}
