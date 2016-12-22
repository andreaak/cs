using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DiffgramAnalyzer
{
    class DiffgramParser : IParser
    {
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
            XmlAttributeCollection attrCollection = table.Attributes;
            XmlElement elem = table as XmlElement;
            string tableName = elem.Name;
            Table tbl = new Table(tableName);
            string rowName = elem.GetAttribute("diffgr:id");
            RowStatus status;
            if (isBefore)
            {
                status = RowStatus.before;
            }
            else
            {
                string rowStatus = elem.GetAttribute("diffgr:hasChanges");
                if (!string.IsNullOrEmpty(rowStatus))
                {
                    status = (RowStatus)Enum.Parse(typeof(RowStatus), rowStatus, true);
                }
                else
                {
                    status = RowStatus.unchanged;
                }
            }
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
