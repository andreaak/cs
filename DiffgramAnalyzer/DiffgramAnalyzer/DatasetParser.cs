using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DiffgramAnalyzer
{
    class DatasetParser : IParser
    {
        int count = 0;

        public List<Table> Parse(List<Table> tables, XmlNode node, bool isBefore)
        {
            if (node != null)
            {
                foreach (XmlNode tableXml in node.ChildNodes)
                {
                    Table table = GetTable(tableXml, isBefore);
                    MergeTables(tables, table);
                }
            }
            return tables;
        }

        private Table GetTable(XmlNode table, bool isBefore)
        {
            List<Cell> cells = new List<Cell>();
            XmlElement elem = table as XmlElement;
            string tableName = elem.Name;
            Table tbl = new Table(tableName);
            string rowName = count++.ToString();
            RowStatus status = RowStatus.inserted;
            Row row = new Row(rowName, status);

            foreach (XmlNode cell in table.ChildNodes)
            {
                string name = cell.Name;
                string value = cell.InnerText;
                Cell temp = new Cell(name, value);
                row.AddCell(temp);
            }

            tbl.AddRow(row);
            return tbl;
        }

        private void MergeTables(List<Table> tables, Table table)
        {
            foreach (Table tbl in tables)
            {
                if (tbl.name.Equals(table.name))
                {
                    tbl.AddRows(table.rows);
                    return;
                }
            }
            tables.Add(table);
        }
    }
}
