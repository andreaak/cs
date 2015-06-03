using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using WorkWithSvn;
using WorkWithSvn.DiskHierarchy.Base;

namespace VisualProviders
{
    public class ListViewProvider
    {
        private ListViewColumnSorter lvwColumnSorter;
        ContextMenuStrip contextMenuStripListView;
        ListView listView;

        public IEnumerable<RepositoryItem> DataSource
        {
            set
            {
                listView.BeginUpdate();
                listView.ListViewItemSorter = null;
                Clear();
                foreach (var item in value)
                {
                    AddItem(item);
                }
                listView.ListViewItemSorter = lvwColumnSorter;
                listView.EndUpdate();
            }
        }

        public bool IsListItemSelected
        {
            get
            {
                return listView.SelectedItems.Count != 0;
            }
        }

        public ListViewProvider(ListView listView, ContextMenuStrip contextMenuStripListView)
        {
            this.listView = listView;
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = lvwColumnSorter;
            this.contextMenuStripListView = contextMenuStripListView;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(listView1_ColumnClick);
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(listView1_MouseClick);
        }

        public List<string> GetSelectedItemsFullName()
        {
            if (listView.InvokeRequired)
            {
                Func<List<string>> d = GetSelectedItemsFullName;
                return (List<string>)listView.Invoke(d);
            }
            else
            {
                List<string> list = new List<string>();
                foreach (ListViewItem lvi in listView.SelectedItems)
                {
                    list.Add(lvi.SubItems[Constants.FULL_PATH_COLUMN].Text);
                }
                return list;
            }
        }

        public void Clear()
        {
            listView.Items.Clear();
        }

        #region HANDLERS

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            listView.Sort();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && contextMenuStripListView != null)
            {
                contextMenuStripListView.Show(listView, e.X, e.Y);
            }
        }

        #endregion

        #region FILL

        private void AddItem(RepositoryItem item)
        {
            if (GetListViewItem(item.FullName) != null)
            {
                return;
            }
            ListViewItem listviewitem = new ListViewItem(item.Name);
            listviewitem.UseItemStyleForSubItems = false;
            FillSubItem(listviewitem, item);
            Color color = GetColor(item);
            if (!color.IsEmpty)
            {
                SetColor(listviewitem, color);
            }            
            listView.Items.Add(listviewitem);
        }

        private ListViewItem GetListViewItem(string fullPath)
        {
            foreach (ListViewItem item in listView.Items)
            {
                if (item.SubItems[Constants.FULL_PATH_COLUMN].Text == fullPath)
                {
                    return item;
                }
            }
            return null;
        }

        private static void FillSubItem(ListViewItem listviewitem, RepositoryItem item)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

            lvsi.Name = Constants.REVISION_COLUMN;
            lvsi.Text = item.Revision;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.LOCAL_STATUS_COLUMN;
            lvsi.Tag = item.LocalStatus;
            lvsi.Text = item.LocalStatusDesc;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.REMOTE_STATUS_COLUMN;
            lvsi.Tag = item.RemoteStatus;
            lvsi.Text = item.RemoteStatusDesc;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.FULL_PATH_COLUMN;
            lvsi.Text = item.FullName;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.CHANGE_LIST_COLUMN;
            lvsi.Text = item.ChangeList;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.LOCATION_COLUMN;
            lvsi.Text = item.Location != null ? item.Location.ToString() : null;
            listviewitem.SubItems.Add(lvsi);
        }

        #endregion

        //public static void ColorSubItems(ListViewItem lvi, RepositoryItem item)
        //{
        //    Color color = GetColor(item);

        //    foreach (System.Windows.Forms.ListViewItem.ListViewSubItem lvsi in lvi.SubItems)
        //    {
        //        lvsi.BackColor = color;
        //    }
        //}

        //private string GetFullPath(ListViewItem lvi)
        //{
        //    return lvi.SubItems[Constants.FULL_PATH_COLUMN].Text;
        //}

        //public void UpdateItemsInListView(List<RepositoryItem> selCollection, RepoFileTypes list)
        //{
        //    MethodInvoker invoker = delegate
        //    {
        //        foreach (RepositoryItem entity in selCollection)
        //        {
        //            if (entity.IsIgnoredItem(list))
        //            {
        //                Remove(entity);
        //            }
        //            else if (entity.IsAdded)
        //            {
        //                if (GetListViewItem(entity.FullName) == null)
        //                {
        //                    Add(entity);
        //                }
        //                else
        //                {
        //                    Update(entity);
        //                }
        //            }
        //            else if (entity.IsConflicted
        //                || entity.IsModified
        //                || entity.IsSwitched)
        //            {
        //                Update(entity);
        //            }
        //        }
        //    };
        //    InvokeMethod(invoker);
        //}

        //public void AddItem(RepositoryItem entity)
        //{
        //    MethodInvoker invoker = delegate
        //    {
        //        AddItem(entity);
        //    };
        //    InvokeMethod(invoker);
        //}

        //public void RemoveItem(RepositoryItem entity)
        //{
        //    MethodInvoker invoker = delegate
        //    {
        //        ListViewItem item = GetListViewItem(entity.FullName);
        //        if (item != null)
        //        {
        //            listView.Items.Remove(item);
        //        }
        //    };
        //    InvokeMethod(invoker);
        //}

        //private void UpdateItem(RepositoryItem entity)
        //{
        //    MethodInvoker invoker = delegate
        //    {
        //        ListViewItem item = GetListViewItem(entity.FullName);
        //        if (item == null)
        //        {
        //            AddItem(entity);
        //            return;
        //        }
        //        item.SubItems[Constants.REVISION_COLUMN].Text = entity.Revision;
        //        item.SubItems[Constants.LOCATION_COLUMN].Text = entity.Location;
        //        item.SubItems[Constants.CHANGE_LIST_COLUMN].Text = entity.ChangeList;
        //        item.SubItems[Constants.LOCAL_STATUS_COLUMN].Tag = entity.LocalStatus;
        //        item.SubItems[Constants.LOCAL_STATUS_COLUMN].Text = entity.LocalStatusDesc;
        //        item.SubItems[Constants.REMOTE_STATUS_COLUMN].Tag = entity.RemoteStatus;
        //        item.SubItems[Constants.REMOTE_STATUS_COLUMN].Text = entity.RemoteStatusDesc;


        //        Color color = GetColor(entity);
        //        if (!color.IsEmpty)
        //        {
        //            SetColor(item, color);
        //        }
        //    };
        //    InvokeMethod(invoker);
        //}

        //public void RenameItem(RepositoryItem entity, string newPath)
        //{
        //    MethodInvoker invoker = delegate
        //    {
        //        ListViewItem item = GetListViewItem(entity.FullName);
        //        if (item == null)
        //        {
        //            return;
        //        }
        //        item.SubItems[Constants.FULL_PATH_COLUMN].Text = newPath;
        //        item.Text = Path.GetFileName(newPath);
        //    };
        //    InvokeMethod(invoker);
        //}

        private static Color GetColor(RepositoryItem repoData)
        {
            Color color = Color.Empty;

            if (repoData.IsSwitched)
            {
                color = Color.Beige;
            }
            else if (repoData.IsModified)
            {
                color = Color.LightPink;
            }
            else if (repoData.IsAdded)
            {
                color = Color.Aqua;
            }
            else if (repoData.IsDeletedLocal)
            {
                color = Color.Red;
            }
            else if (repoData.IsConflicted)
            {
                color = Color.Goldenrod;
            }

            return color;
        }

        private static void SetColor(ListViewItem item, Color color)
        {
            item.SubItems[0].BackColor = color;
            item.SubItems[Constants.CHANGE_LIST_COLUMN].BackColor = color;
            item.SubItems[Constants.FULL_PATH_COLUMN].BackColor = color;
            item.SubItems[Constants.REVISION_COLUMN].BackColor = color;
            item.SubItems[Constants.LOCATION_COLUMN].BackColor = color;
            item.SubItems[Constants.LOCAL_STATUS_COLUMN].BackColor = color;
            item.SubItems[Constants.REMOTE_STATUS_COLUMN].BackColor = color;
        }

        //private void InvokeMethod(MethodInvoker invoker)
        //{
        //    if (listView.InvokeRequired)
        //    {
        //        listView.BeginInvoke(invoker);
        //    }
        //    else
        //    {
        //        invoker();
        //    }
        //}


    }
}
