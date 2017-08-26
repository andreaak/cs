using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Utils;
using GrabDatabase;

namespace DBLinks
{
    public partial class RemoveLinkForm : Form
    {
        Dictionary<string, Table> tables;
        string path = "data.bin";

        public RemoveLinkForm()
        {
            InitializeComponent();
            tables = UTILS.ReadData(path);
        }


        public RemoveLinkForm(Dictionary<string, Table> tables)
        {
            InitializeComponent();
            this.tables = tables;
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == -1)
            {
                return;
            }

            Table item = (Table)combo.SelectedItem;
            BindFields(item);
        }

        private void comboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == -1)
            {
                return;
            }


            Field item = (Field)combo.SelectedItem;
            BindLinks(item);
        }

        private void RemoveLink_Click(object sender, EventArgs e)
        {

            if (comboBoxTables.SelectedIndex == -1 || comboBoxFields.SelectedIndex == -1 || comboBoxLinks.SelectedIndex == -1)
            {
                return;
            }
            
            Table table = (Table)comboBoxTables.SelectedItem;
            Field field = (Field)comboBoxFields.SelectedItem;
            Link link = (Link)comboBoxLinks.SelectedItem;
            
            string tableName = link.TableName;
            string fieldName = link.FieldName;
            string linkTableName = table.Name;
            string linkFieldName = field.Name;

            RemoveCrossReference(tableName, fieldName, linkTableName, linkFieldName);
            field.RemoveLink(link);
            BindLinks(field);
            BindFields(table);
            
            if (table.Fields.Count == 0)
            {
                tables.Remove(table.Name);
                BindTables();
            }  
        }

        private void RemoveCrossReference(string table, string field, string linkTable, string linkField)
        {
            if (tables.ContainsKey(table) && tables[table].Fields.ContainsKey(field) && tables[table].Fields[field].Contains(new Link(linkTable, linkField)))
            {
                tables[table].Fields[field].RemoveLink(new Link(linkTable, linkField));
                if (tables[table].Fields.Count == 0)
                {
                    tables.Remove(table);
                    BindTables();
                } 
            }
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            string[] extensions = new string[]
            {
             "BIN Files (*.bin)|*.bin|",
             "All Files (*.*)|*.*"
            };
            string title = "���������� ������";
            string filePath = string.Empty;
            if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                UTILS.SaveData(path, tables);
            }
        }

        private void BindTables()
        {
            Table[] arrTable = new Table[tables.Values.Count];
            tables.Values.CopyTo(arrTable, 0);
            comboBoxTables.DataSource = arrTable;
            comboBoxTables.SelectedIndex = -1;
        }

        private void BindFields(Table item)
        {
            Field[] arrItem = new Field[item.Fields.Count];
            item.Fields.Values.CopyTo(arrItem, 0);
            comboBoxFields.DataSource = arrItem;
            comboBoxFields.SelectedIndex = -1;
        }

        private void BindLinks(Field item)
        {
            comboBoxLinks.DataSource = item.Links;
            comboBoxLinks.SelectedIndex = -1;
        }
    }
}