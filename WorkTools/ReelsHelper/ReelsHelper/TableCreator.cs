using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ReelsHelper
{
    public static class TableCreator
    {
        public static DataTable CreateHorizontalReelDataTable(List<Reel> reels)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < reels.Max(reel => reel.Indexes.Count); i++)
            {
                DataColumn column;
                column = new DataColumn();
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

        public static DataTable CreateVerticalReelDataTable(List<Reel> reels)
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
                for(int j = 0; j < reels.Count; j++)
                {
                    row.SetField(j, reels[j][i]);
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
