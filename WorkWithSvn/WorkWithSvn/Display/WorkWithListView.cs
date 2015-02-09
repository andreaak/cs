using System.Collections.Generic;
using System.Windows.Forms;
using Utils;
using WorkWithSvn;
using System.Drawing;
using System.IO;
using WorkWithSvn.DiskHierarchy;

namespace Display
{
    public class WorkWithListView
    {
        private delegate void delegateUpdate(RepoEntityData entity);
        private delegate void delegateUpdateItems(List<RepoEntityData> selCollection, RepoFileTypes list);
        private delegate void delegateRename(RepoEntityData entity, string oldName);
        private delegate List<string> delegateGet();
        private ListViewColumnSorter lvwColumnSorter;
        ContextMenuStrip contextMenuStripListView;

        public ListViewColumnSorter LvwColumnSorter
        {
            get { return lvwColumnSorter; }
        }

        ListView listView;
        public ListView ListViewCtrl
        {
            get { return listView; }
        }

        public WorkWithListView(ListView listView, ContextMenuStrip contextMenuStripListView)
        {
            this.listView = listView;
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = lvwColumnSorter;
            this.contextMenuStripListView = contextMenuStripListView;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(listView1_ColumnClick);
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(listView1_MouseClick);
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

        public void Clear()
        {
            listView.Items.Clear();
        }

        #region FILL

        public void Fill(RepoDirectoryData currentDir, RepoFileTypes fileStatus, string changeList, List<string> selectedExtensions)
        {
            if (currentDir == null)
            {
                return;
            }
            listView.ListViewItemSorter = null;
            Fill(currentDir, changeList, selectedExtensions, fileStatus);
            listView.ListViewItemSorter = lvwColumnSorter;
        }

        private void Fill(RepoDirectoryData baseDir, string changeList, List<string> selectedExtensions, RepoFileTypes fileStatus)
        {
            foreach (RepoFileData file in baseDir.FilesList)
            {
                if (file.GetData().IsIgnoredEntity(fileStatus)
                    || UTILS.IsIgnoredChangeList(changeList, file.GetData())
                    || UTILS.IsIgnoredExtension(selectedExtensions, file.GetData()))
                {
                    continue;
                }
                AddItem(file.GetData());
            }

            foreach (RepoDirectoryData dir in baseDir.DirectoriesList)
            {
                if (!(dir.GetData().IsIgnoredEntity(fileStatus)
                    || UTILS.IsIgnoredChangeList(changeList, dir.GetData())
                    || UTILS.IsIgnoredEntity(dir.Data.FullPath)))
                {
                    AddItem(dir.GetData()); 
                }
                Fill(dir, changeList, selectedExtensions, fileStatus);
            }
        }
        
        private void AddItem(RepoEntityData entity)
        {
            if (GetListViewItem(entity.Data.FullPath) != null)
            {
                return;
            }
            ListViewItem listviewitem = new ListViewItem(entity.Data.Name);
            listviewitem.UseItemStyleForSubItems = false;
            FillSubItem(listviewitem, entity);
            Color color = GetColor(entity);
            if (!color.IsEmpty)
            {
                SetColor(listviewitem, color);
            }            
            listView.Items.Add(listviewitem);
        }

        private static Color GetColor(RepoEntityData repoData)
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
            else if (repoData.IsDeleted)
            {
                color = Color.Red;
            }
            else if (repoData.IsConflicted)
            {
                color = Color.Goldenrod;
            }

            return color;
        }

        private static void FillSubItem(ListViewItem listviewitem, RepoEntityData file)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

            lvsi.Name = Constants.REVISION_COLUMN;
            lvsi.Text = file.Revision;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.LOCAL_STATUS_COLUMN;
            lvsi.Tag = file.LocalStatus;
            lvsi.Text = file.LocalStatusDesc;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.REMOTE_STATUS_COLUMN;
            lvsi.Tag = file.RemoteStatus;
            lvsi.Text = file.RemoteStatusDesc;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.FULL_PATH_COLUMN;
            lvsi.Text = file.Data.FullPath;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.CHANGE_LIST_COLUMN;
            lvsi.Text = file.ChangeList;
            listviewitem.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Name = Constants.LOCATION_COLUMN;
            lvsi.Text = file.Location != null ? file.Location.ToString() : null;
            listviewitem.SubItems.Add(lvsi);
        }

        public static void ColorSubItems(ListViewItem lvi, RepoEntityData entity)
        {
            Color color = GetColor(entity);

            foreach (System.Windows.Forms.ListViewItem.ListViewSubItem lvsi in lvi.SubItems)
            {
                lvsi.BackColor = color;
            }
        }

        #endregion

        public List<string> GetSelectedItemsPath()
        {
            if (listView.InvokeRequired)
            {
                delegateGet d = new delegateGet(GetSelectedItemsPath);
                return (List<string>)listView.Invoke(d);
            }
            else
            {
                List<string> list = new List<string>();
                foreach (ListViewItem lvi in listView.SelectedItems)
                {
                    list.Add(WorkWithListView.GetFullPath(lvi));
                }
                return list;
            }
        }
        
        private ListViewItem GetListViewItem(string fullPath)
        {
            foreach (ListViewItem item in listView.Items)
            {
                if (item.SubItems[Constants.FULL_PATH_COLUMN].Text.Equals(fullPath))
                {
                    return item;
                }
            }
            return null;
        }

        public static string GetFullPath(ListViewItem lvi)
        {
            return lvi.SubItems[Constants.FULL_PATH_COLUMN].Text;
        }

        public void UpdateItemsInListView(List<RepoEntityData> selCollection, RepoFileTypes list)
        {
            if (listView.InvokeRequired)
            {
                delegateUpdateItems d = new delegateUpdateItems(UpdateItemsInListView);
                listView.Invoke(d, selCollection, list);
            }
            else
            {
                foreach (RepoEntityData entity in selCollection)
                {
                    if (entity.IsIgnoredEntity(list))
                    {
                        Remove(entity);
                    }
                    else if (entity.IsAdded)
                    {
                        if (GetListViewItem(entity.Data.FullPath) == null)
                        {
                            Add(entity);
                        }
                        else
                        {
                            Update(entity);
                        }
                    }
                    else if (entity.IsConflicted
                        || entity.IsModified
                        || entity.IsSwitched)
                    {
                        Update(entity);
                    }
                }
            }
        }

        public void Add(RepoEntityData entity)
        {
            if (listView.InvokeRequired)
            {
                delegateUpdate d = new delegateUpdate(Add);
                listView.Invoke(d, entity);
            }
            else
            {
                AddItem(entity);
            }
        }

        public void Remove(RepoEntityData entity)
        {
            if (listView.InvokeRequired)
            {
                delegateUpdate d = new delegateUpdate(Remove);
                listView.Invoke(d, entity);
            }
            else
            {
                ListViewItem item = GetListViewItem(entity.Data.FullPath);
                if (item != null)
                {
                    listView.Items.Remove(item);
                }
            }
        }

        private void Update(RepoEntityData entity)
        {
            if (listView.InvokeRequired)
            {
                delegateUpdate d = new delegateUpdate(Update);
                listView.Invoke(d, entity);
            }
            else
            {
                ListViewItem item = GetListViewItem(entity.Data.FullPath);
                if (item == null)
                {
                    AddItem(entity);
                    return;
                }
                item.SubItems[Constants.REVISION_COLUMN].Text = entity.Revision;
                item.SubItems[Constants.LOCATION_COLUMN].Text = entity.Location;
                item.SubItems[Constants.CHANGE_LIST_COLUMN].Text = entity.ChangeList;
                item.SubItems[Constants.LOCAL_STATUS_COLUMN].Tag = entity.LocalStatus;
                item.SubItems[Constants.LOCAL_STATUS_COLUMN].Text = entity.LocalStatusDesc;
                item.SubItems[Constants.REMOTE_STATUS_COLUMN].Tag = entity.RemoteStatus;
                item.SubItems[Constants.REMOTE_STATUS_COLUMN].Text = entity.RemoteStatusDesc;


                Color color = GetColor(entity);
                if (!color.IsEmpty)
                {
                    SetColor(item, color);
                }
            }
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

        public void RenameItemsInListView(RepoEntityData entity, string newPath)
        {
            if (listView.InvokeRequired)
            {
                delegateRename d = new delegateRename(RenameItemsInListView);
                listView.Invoke(d, entity, newPath);
            }
            else
            {
                ListViewItem item = GetListViewItem(entity.Data.FullPath);
                if (item == null)
                {
                    return;
                }
                item.SubItems[Constants.FULL_PATH_COLUMN].Text = newPath;
                item.Text = Path.GetFileName(newPath);
            }
        }

    }
}
