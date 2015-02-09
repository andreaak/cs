using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Utils;
using GrabDatabase;

namespace DBLinks
{
    public partial class AddLinkForm : Form
    {
        Dictionary<string, Table> tables;
        string path = "data.bin";
        ComboBox tempCombo1;
        ComboBox tempCombo2;
        bool isChooseTable1;
        bool isChooseField1;
        bool isChooseTable2;
        bool isChooseField2;

        public AddLinkForm()
        {
            InitializeComponent();
            tempCombo1 = new ComboBox();
            tempCombo2 = new ComboBox();
            tables = UTILS.ReadData(path);
        }

        public AddLinkForm(Dictionary<string, Table> tables)
        {
            InitializeComponent();
            tempCombo1 = new ComboBox();
            tempCombo2 = new ComboBox();
            this.tables = tables;
        }

        private void AddLink_Click(object sender, EventArgs e)
        {
            string table1 = textBoxTable1.Text;
            string field1 = textBoxField1.Text;
            
            string table2 = textBoxTable2.Text;
            string field2 = textBoxField2.Text;


            AddLink_(table1, field1, table2, field2);
            AddLink_(table2, field2, table1, field1);
            
            textBoxTable1.Text = textBoxField1.Text = textBoxTable2.Text = textBoxField2.Text = string.Empty;
        }

        private void AddLink_(string table1, string field1, string table2, string field2)
        {
            if (tables.ContainsKey(table1))
            {
                Table tempTable = tables[table1];
                if (tempTable.Fields.ContainsKey(field1))
                {
                    Field tempField = tempTable.Fields[field1];
                    Link tempLink = CreateLink(table2, field2);
                    if (!tempField.Contains(tempLink))
                    {
                        tempField.AddLink(tempLink);
                    }
                }
                else
                {
                    tempTable.AddField(CreateField(field1, CreateLink(table2, field2)));
                }
            }
            else
            {
                tables.Add(table1, CreateTable(table1, CreateField(field1, CreateLink(table2, field2))));
            }
        }

        private static Table CreateTable(string table1, Field newField)
        {
            Table newTable = new Table(table1);
            newTable.AddField(newField);
            return newTable;
        }

        private static Field CreateField(string field1, Link newLink)
        {
            Field newField = new Field(field1);
            newField.AddLink(newLink);
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
            string title = "Сохранение данных";
            string filePath = string.Empty;
            if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                UTILS.SaveData(path, tables);
            }
        }

        private void buttonSelectTable1_Click(object sender, EventArgs e)
        {
            if (isChooseField1)
            {
                return;
            } 
            TextBox tempTextBox = textBoxTable1;
            GroupBox tempGroupBox = groupBox1;
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(tables.Values);
            selectData(ref tempCombo1, tempGroupBox, tempTextBox, textBoxField1, ref isChooseTable1, tmpList, "table1");
        }

        private void buttonSelectField1_Click(object sender, EventArgs e)
        {
            if (textBoxTable1.Text == null || !tables.ContainsKey(textBoxTable1.Text) || isChooseTable1)
            {
                return;
            }
            TextBox tempTextBox = textBoxField1;
            GroupBox tempGroupBox = groupBox1;
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(tables[textBoxTable1.Text].Fields.Values);
            tmpList.Sort();
            selectData(ref tempCombo1, tempGroupBox, tempTextBox, null, ref isChooseField1, tmpList, "field1");
        }


        private void buttonSelectTable2_Click(object sender, EventArgs e)
        {
            if (isChooseField2)
            {
                return;
            } 
            TextBox tempTextBox = textBoxTable2;
            GroupBox tempGroupBox = groupBox2;
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(tables.Values);
            selectData(ref tempCombo2, tempGroupBox, tempTextBox, textBoxField2, ref isChooseTable2, tmpList, "table2");
        }

        private void buttonSelectField2_Click(object sender, EventArgs e)
        {
            if (textBoxTable2.Text == null || !tables.ContainsKey(textBoxTable2.Text) || isChooseTable2)
            {
                return;
            }
            TextBox tempTextBox = textBoxField2;
            GroupBox tempGroupBox = groupBox2;
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(tables[textBoxTable2.Text].Fields.Values);
            tmpList.Sort();
            selectData(ref tempCombo2, tempGroupBox, tempTextBox, null, ref isChooseField2, tmpList, "field2");

        }

        private void selectData(ref ComboBox tempCombo, GroupBox tempGroupBox, TextBox tempTextBox, TextBox clearTextBox, ref bool isChecked, ArrayList tmpList, string name)
        {
            if (!isChecked)
            {

                tempCombo = new ComboBox();
                tempCombo.Location = tempTextBox.Location;
                tempCombo.Name = name;
                tempCombo.Size = tempTextBox.Size;
                tempCombo.TabIndex = 1;

                tempTextBox.Visible = isChecked;
                tempGroupBox.Controls.Add(tempCombo);
                
                Bind(tempCombo, tmpList);
            }
            else
            {
                
                tempTextBox.Text = tempCombo.Text;
                tempGroupBox.Controls.Remove(tempCombo);
                tempTextBox.Visible = isChecked;
                if (clearTextBox != null)
                {
                    clearTextBox.Text = string.Empty;
                }
            }
            isChecked = !isChecked;
        }

        private void Bind(ComboBox combo, ArrayList arr)
        {
            combo.DataSource = arr;
            combo.SelectedIndex = -1;
        }

    }
}