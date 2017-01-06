using System;
using System.Collections.Generic;
using System.Text;

namespace DiffgramAnalyzer
{
    public class Table
    {
        public string name;
        public List<Row> rows;

        public Table(string name)
        {
            this.name = name;
            rows = new List<Row>();
        }

        public void AddRow(Row row)
        {
            rows.Add(row);
        }
        
        public void AddRows(List<Row> row)
        {
            rows.AddRange(row);
        }

        public Row GetRowBefore(string rowName)
        {
            foreach (Row row in rows)
            {
                if(row.status == RowStatus.before
                    && row.name == rowName)
                {
                    return row;
                }
            }
            return null;
        }
    }
}
