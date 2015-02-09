using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Utils;
using DBLinks;
using System.Xml.Linq;
using Utils.ActionWindow;

namespace GrabDatabase.Links
{
    public partial class WorkWithKeys : Form
    {
        Dictionary<string, Table> tables;
        public Dictionary<string, Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }
        string path = "data.bin";

        public WorkWithKeys()
        {
            InitializeComponent();
        }

        public WorkWithKeys(Dictionary<string, Table> tables)
        {
            InitializeComponent();
            this.tables = tables;
        }

        private void buttonIndexColumns_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };
            string title = "Data Load";
            string filePath = string.Empty;
            if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                if (!File.Exists(filePath))
                {
                    DisplayMessage.ShowWarning("Wrong file");
                    return;
                }
                textBoxIndexColumns.Text = filePath;
            }
        }

        private void buttonFK_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };
            string title = "Data Load";
            string filePath = string.Empty;
            if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                if (!File.Exists(filePath))
                {
                    DisplayMessage.ShowWarning("Wrong file");
                    return;
                }
                textBoxFK.Text = filePath;
            }
        }

        private void buttonFKColumns_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
            };
            string title = "Data Load";
            string filePath = string.Empty;
            if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                if (!File.Exists(filePath))
                {
                    DisplayMessage.ShowWarning("Wrong file");
                    return;
                }
                textBoxFKColumns.Text = filePath;
            }
        }

        private void buttonCreateLinks_Click(object sender, EventArgs e)
        {
            WorkWithXML.OpenFile(textBoxIndexColumns.Text);
            IEnumerable<XElement> indexColumns = WorkWithXML.ReadElement("IndexColumns");
            WorkWithXML.OpenFile(textBoxFK.Text);
            IEnumerable<XElement> fks = WorkWithXML.ReadElement("ForeignKeys");
            WorkWithXML.OpenFile(textBoxFKColumns.Text);
            IEnumerable<XElement> fkColumns = WorkWithXML.ReadElement("ForeignKeyColumns");
            foreach (XElement indexColumn in indexColumns)
            {
                string tableName1 = indexColumn.Element("TABLE_NAME").Value;
                string columnName1 = indexColumn.Element("COLUMN_NAME").Value;
                string pk = indexColumn.Element("INDEX_NAME").Value;
                
                foreach (XElement fkItem in fks)
                {
                    if (fkItem.Element("PRIMARY_KEY_CONSTRAINT_NAME").Value == pk)
                    {
                        string fk = fkItem.Element("FOREIGN_KEY_CONSTRAINT_NAME").Value;
                        foreach (XElement fkItemColumn in fkColumns)
                        {
                            if (fkItemColumn.Element("CONSTRAINT_NAME").Value == fk)
                            {
                                string tableName2 = fkItemColumn.Element("TABLE_NAME").Value;
                                string columnName2 = fkItemColumn.Element("COLUMN_NAME").Value;
                                AddLink(tables, tableName1, columnName1, tableName2, columnName2, true, false);
                                AddLink(tables, tableName2, columnName2, tableName1, columnName1, false, true);
                            }
                        }

                    }
                }
            }
            DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
        }

        private void AddLink(Dictionary<string, Table> tables, string table1, string field1, string table2, string field2, bool isPrim, bool isForeign)
        {
            if (tables.ContainsKey(table1))
            {
                Table tempTable = tables[table1];
                if (tempTable.Fields.ContainsKey(field1))
                {
                    Field tempField = tempTable.Fields[field1];
                    tempField.IsPrimaryKey = isPrim;
                    tempField.IsForeignKey = isForeign;
                    Link tempLink = CreateLink(table2, field2);
                    if (!tempField.Contains(tempLink))
                    {
                        tempField.AddLink(tempLink);
                    }
                }
                else
                {
                    tempTable.AddField(CreateField(field1, CreateLink(table2, field2), isPrim, isForeign));
                }
            }
            else
            {
                tables.Add(table1, CreateTable(table1, CreateField(field1, CreateLink(table2, field2), isPrim, isForeign)));
            }
        }

        private static Table CreateTable(string table1, Field newField)
        {
            Table newTable = new Table(table1);
            newTable.AddField(newField);
            return newTable;
        }

        private static Field CreateField(string field1, Link newLink, bool isPrim, bool isForeign)
        {
            Field newField = new Field(field1);
            newField.AddLink(newLink);
            newField.IsPrimaryKey = isPrim;
            newField.IsForeignKey = isForeign;
            return newField;
        }

        private Link CreateLink(string table2, string field2)
        {
            Link newLink = new Link(table2, field2);
            return newLink;
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
             "BIN Files (*.bin)|*.bin|",
             "All Files (*.*)|*.*"
            };
            string title = "Save Data";
            string filePath = string.Empty;
            if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                GrabDatabase.UTILS.SaveData(path, tables);
            }
        }
    }
}