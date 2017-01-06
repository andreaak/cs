using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DiffgramAnalyzer
{
    class WorkWithListView
    {
        ListView listView;
        public WorkWithListView(ListView listView)
        {
            this.listView = listView;
        }

        public void ClearListView()
        {
            listView.Items.Clear();
        }

        //private void FillTree(DirectoryData directoryData, TreeNode treeNode,ref  List<string> ignoredDirectories)
        //{
        //    foreach (DirectoryData directory in directoryData.directoriesList)
        //    {
        //        if (ignoredDirectories.Contains(directory.directoryName))
        //        {
        //            continue;
        //        }
        //        treeNode.Nodes.Add(directory.directoryName);
        //        treeNode.Nodes[treeNode.Nodes.Count - 1].ImageIndex = 0;
        //        treeNode.Nodes[treeNode.Nodes.Count - 1].SelectedImageIndex = 1;
        //        treeNode.Nodes[treeNode.Nodes.Count - 1].StateImageIndex = 0;
        //        FillTree(directory, treeNode.Nodes[treeNode.Nodes.Count - 1], ref ignoredDirectories);
        //    }
        //}

        //public void FillTree(DirectoryData directoryData, List<string> ignoredDirectories)
        //{
        //    if (directoryData == null)
        //        return;
        //    ClearTree();
        //    treeView.Nodes.Add(directoryData.fullDirectoryName);
        //    treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 0;
        //    treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
        //    treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 0;
        //    FillTree(directoryData, treeView.Nodes[treeView.Nodes.Count - 1], ref ignoredDirectories);
        //}

        public void FillListView(List<Table> tables)
        {
            if (tables == null)
            {
                return;
            }
            listView.Items.Clear();

            foreach (Table tbl in tables)
            {
                FillListView(tbl, null);
            }
        }

        public void FillListView(Table table, string rowName)
        {
            ListViewItem listviewitem = new ListViewItem("Table: " + table.name);
            listviewitem.UseItemStyleForSubItems = false;
            listviewitem.ForeColor = Color.BlueViolet;
            listView.Items.Add(listviewitem);
            
            foreach (Row row in table.rows)
            {
                if (row.status != RowStatus.before
                    && (rowName == null || (row.name == rowName))
                    )
                {

                    listviewitem = new ListViewItem("Row: " + row.name + "-" + row.status.ToString());
                    listviewitem.UseItemStyleForSubItems = false;
                    listviewitem.ForeColor = Color.DarkBlue;
                    listView.Items.Add(listviewitem);
                    
                    Row beforeRow = table.GetRowBefore(row.name);
                    foreach (Cell cell in row.cells)
	                {
                        Cell temp = null;
                        if (beforeRow != null)
                        {
                            temp = beforeRow.GetCell(cell.name);
                        }
                        
                        listviewitem = new ListViewItem();
                        listviewitem.UseItemStyleForSubItems = false;
                        Color color = GetColor(cell, temp);
                        if (color != Color.Empty)
                        {
                            listviewitem.SubItems[0].BackColor = color;
                        }
                        
                        FillSubItem(listviewitem, cell, temp, color);
                        
                        listView.Items.Add(listviewitem);
	                }
                }
            }
        }

        public Color GetColor(Cell cellAfter, Cell cellBefore)
        {
            Color color = Color.Empty;
            if (cellBefore == null && cellAfter == null)
            {
                return color;
            }
            if ((cellBefore != null && cellAfter == null)
                || (cellBefore == null && cellAfter != null) 
                || !cellAfter.value.Equals(cellBefore.value))
            {
                color = Color.LightPink;
            }
            return color;
        }

        public void FillSubItem(ListViewItem listviewitem, Cell cellAfter, Cell cellBefore, Color color)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

            lvsi.Name = "ColumnName";
            lvsi.Text = cellAfter.name;
            lvsi.BackColor = color;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = "OldValue";
            lvsi.Text = cellBefore != null ? cellBefore.value : null;
            lvsi.BackColor = color;
            listviewitem.SubItems.Add(lvsi); 
            
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = "NewValue";
            lvsi.Text = cellAfter.value;
            lvsi.BackColor = color;
            listviewitem.SubItems.Add(lvsi);

        }
    }
}
