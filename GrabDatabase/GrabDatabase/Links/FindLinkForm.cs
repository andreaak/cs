using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using Utils;
using GrabDatabase;

namespace DBLinks
{
    public partial class FindLinkForm : Form
    {
        Dictionary<string, Table> tables;
        string path = "data.bin";

        public FindLinkForm()
        {
            InitializeComponent();
            tables = UTILS.ReadData(path);
            Init();
        }

        public FindLinkForm(Dictionary<string, Table> tables)
        {
            InitializeComponent();
            this.tables = tables;
            Init();
        }

        private void Init()
        {
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(tables.Values);
            Bind(comboBoxTable1, tmpList);
            ArrayList tmpList2 = new ArrayList();
            tmpList2.AddRange(tables.Values);
            Bind(comboBoxTable2, tmpList2);

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
            string title = "���������� ������";
            string filePath = string.Empty;
            if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                UTILS.SaveData(filePath, tables);
            }
        }
        private void Bind(ComboBox combo, ArrayList arr)
        {
            combo.DataSource = arr;
            combo.SelectedIndex = -1;
        }

        private void comboBoxTable1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTable1.SelectedIndex == -1)
            {
                return;
            }
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(((Table)comboBoxTable1.SelectedItem).Fields.Values);
            Bind(comboBoxField1, tmpList);
        }

        private void comboBoxTable2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTable2.SelectedIndex == -1)
            {
                return;
            } 
            ArrayList tmpList = new ArrayList();
            tmpList.AddRange(((Table)comboBoxTable2.SelectedItem).Fields.Values);
            Bind(comboBoxField2, tmpList);
        }

        private void FindLink_Click(object sender, EventArgs e)
        {
            string table1 = ((Table)comboBoxTable1.SelectedItem).Name;
            string table2 = ((Table)comboBoxTable2.SelectedItem).Name;
            
            string field1 = ((Field)comboBoxField1.SelectedItem).Name;
            string field2 = ((Field)comboBoxField2.SelectedItem).Name;

            string path = "";
            List<string> paths = new List<string>();
            List<string> tablesFields= new List<string>();
            bool ret = FindLinks(string.Empty, string.Empty, table1, field1, table2, field2, ref path, paths, tablesFields);
        }

        private bool FindLinks(string prevTable, string prevField, string startTable, string startField, string endTable, string endField, ref string path, List<string> paths, List<string> tablesFields)
        {
            string tf = string.Format("{0}.{1}", startTable, startField);
            if(tablesFields.Contains(tf))
            {
                return false;
            }
            bool ret = false;
            if (!(
                tables.ContainsKey(startTable) && tables[startTable].Fields.ContainsKey(startField)
                && tables.ContainsKey(endTable) && tables[endTable].Fields.ContainsKey(endField)
                ))
            {
                return false;
            }

            Table tableStart = tables[startTable];
            Field fieldStart = tableStart.Fields[startField];
            
            //������ ���� � �������
            if (startTable.Equals(endTable))
            {
                path += string.Format("({0}.{1}:{2})", startTable, startField, endField);
                paths.Add(path);
                ret = true;
            }

            //������ ���� � ����
            if (fieldStart.Contains(new Link(endTable, endField)))
            {
                
                path += string.Format("({0}.{1}-->{2}.{3})", startTable, startField, endTable, endField);
                paths.Add(path);
                ret = true;
            }

            //����� � ����� ������� ������ ������
            foreach (Field field in tableStart.Fields.Values)
            {
                if (field.Equals(startField))
                {
                    continue;
                }
                if(field.Contains(new Link(endTable, endField)))
                {
                    string tempPath = path;
                    tempPath += string.Format("({0}.{1}:{4}-->{2}.{3})", startTable, startField, endTable, endField, field);
                    paths.Add(tempPath);
                    ret = true;
                }
            }

            tablesFields.Add(tf);
            FindLink_(prevTable, prevField, startTable, startField, endTable, endField, ref path, paths, tablesFields);
            return ret;
        }

        private void FindLink_(string prevTable, string prevField, string startTable, string startField, string endTable, string endField, ref string path, List<string> paths, List<string> tablesFields)
        {
            Table tableStart = tables[startTable];
            Field fieldStart = tableStart.Fields[startField];
            
            //����� � ������� �������� ����
            foreach (Link link in fieldStart.Links)
            {
                if(prevTable == link.TableName && prevField == link.FieldName)
                {
                    continue;
                }
                string tempPath = path;
                tempPath += string.Format("{0}.{1}-->{2}.{3}\r", startTable, startField, link.TableName, link.FieldName);
                FindLinks(tableStart.Name, fieldStart.Name, link.TableName, link.FieldName, endTable, endField, ref tempPath, paths, tablesFields);
            }

            //����� � ����� �������
            foreach (Field field in tableStart.Fields.Values)
            {
                if (field.Equals(startField))
                {
                    continue;
                }
                foreach (Link link in field.Links)
                {
                    string tempPath = path;
                    tempPath += string.Format("{0}.{1}:{2}-->{3}.{4}\r", startTable, startField, field.Name, link.TableName, link.FieldName);
                    FindLinks(tableStart.Name, field.Name, link.TableName, link.FieldName, endTable, endField, ref tempPath, paths, tablesFields);
                }
            }
        }
    }
}