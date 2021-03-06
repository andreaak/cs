using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VisualProviders;
using System.Threading;
using Utils;
using System.Resources;
using System.Globalization;
using WorkWithSvn.RepositoryProviders;
using WorkWithSvn.DiskHierarchy.Base;
using WorkWithSvn.Utils;
using Utils.ActionWindow;
using Utils.WorkWithFiles;

namespace WorkWithSvn
{
    public partial class MainView : Form, IMainView
    {
        private TreeProvider treeProvider;
        private ListViewProvider listViewProvider;

        private ISet<string> changeLists;
        private ISet<string> selectedExtensions = new HashSet<string>();
        SynchronizationContext uiSyncContext;
        
        private ResourceManager rm;
        private CultureInfo culture;
        private MainPresenter presenter;

        private bool IsListItemSelected
        {
            get
            {
                return listViewProvider.IsListItemSelected;
            }
        }

        public ControlsData ControlsData
        {
            get
            {
                return GetControlData();
            }
        }

        public MainView()
        {
            InitializeComponent();
            rm = Options.ResManager;
            culture = OptionsUtils.Culture = Options.Culture;
            presenter = new MainPresenter(this);
            presenter.IsWatcherActive = toolStripButtonHandleFilesChanges.Checked;
            listViewProvider = new ListViewProvider(listViewFiles, contextMenuStripListView);
            treeProvider = new TreeProvider(treeView1, contextMenuStripTreeView);
            SetFormText();
            uiSyncContext = SynchronizationContext.Current;
        }

        #region BUTTON_HANDLERS

        private void toolStripButtonOpenWorkingCopy_Click(object sender, EventArgs e)
        {
            if (presenter.OpenWorkingCopy())
            {
                presenter.Clear();
                treeProvider.Clear();
                listViewProvider.Clear();
                SetFormText();
            }
        }

        private void toolStripButtonReadDirectories_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Options.GetInstance.WorkingCopyPath) 
                || !Directory.Exists(Options.GetInstance.WorkingCopyPath))
            {
                return;
            }
            try
            {
                if (presenter.ReadDirectories(Options.GetInstance.WorkingCopyPath))
                {
                    treeProvider.DataSource = presenter.WorkingCopy;
                    listViewProvider.Clear();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonScan_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null
                || string.IsNullOrEmpty(treeView1.SelectedNode.FullPath)
                || !Directory.Exists(treeView1.SelectedNode.FullPath))
            {
                return;
            }            
            
            try
            {
                string fullName = treeProvider.SelectedNode.FullPath;
                if(presenter.ScanDirectory(fullName, ControlsData))
                {
                    changeLists = presenter.GetChangeList();
                    FillChangeListCheckbox();
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void toolStripButtonRefreshFileStatus_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.RefreshFilesStatus(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void toolStripButtonHandleFilesChanges_CheckedChanged(object sender, EventArgs e)
        {
            presenter.IsWatcherActive = toolStripButtonHandleFilesChanges.Checked;
        }

        private void toolStripButtonShowDiff_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            if (!Directory.Exists(Options.GetInstance.DiffDir))
            {
                Directory.CreateDirectory(Options.GetInstance.DiffDir);
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                presenter.ShowDiff(fullNames, ControlsData);
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonOpenWith_Click(object sender, EventArgs e)
        {
            List<string> pathes = listViewProvider.GetSelectedItemsFullName();
            if (pathes.Count == 0 ||
                !File.Exists(pathes[0]))
            {
                return;
            }
            try 
	        {
                UTILS.OpenAs(pathes[0]);
	        }
	        catch (Exception ex) 
	        {
                MessageBox.Show(this, ex.Message, rm.GetString("FILE_ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
	        }
        }        
        
        private void toolStripButtonShowLog_Click(object sender, EventArgs e)
        {
            foreach (string path in listViewProvider.GetSelectedItemsFullName())
            {
                UTILS.OpenLog(path);
            }
        }        

        private void toolStripButtonMarkResolved_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.Resolved(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonSwitch_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.Switch(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonAddItems_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.AddItems(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonDeleteItem_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected
                || MessageBox.Show(this, rm.GetString("DELETE_ITEM_QUESTION", culture), rm.GetString("WARNING", culture),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.DeleteItems(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonDeleteFromDisk_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected
                || MessageBox.Show(this, rm.GetString("DELETE_ITEM_QUESTION", culture), rm.GetString("WARNING", culture),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }
            try
            {
                
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                presenter.DeleteFromDisk(fullNames);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }        
        
        private void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            presenter.IsWatcherActive = false;
            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.Update(fullNames, ControlsData))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                presenter.IsWatcherActive = toolStripButtonHandleFilesChanges.Checked;
            }
        }
        
        private void toolStripButtonCommit_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }
            
            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.Commit(fullNames))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonRevert_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.Revert(fullNames))
                {
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonCreatePatch_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                presenter.CreatePatch(fullNames);
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonMoveToChangeList_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }
            AddChangeList form = new AddChangeList();
            if (form.ShowDialog() != DialogResult.OK 
                || string.IsNullOrEmpty(form.ChangeListName))
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                presenter.MoveToChangeList(form.ChangeListName, fullNames);
                changeLists.Add(form.ChangeListName);
                FillChangeListCheckbox();
                UpdateItemsList();
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonRemoveFromChangeList_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                if (presenter.RemoveFromChangeList(fullNames))
                {
                    changeLists = presenter.GetChangeList();
                    FillChangeListCheckbox();
                    UpdateItemsList();
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonOpenDirectory_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }
            List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
            presenter.OpenDirectory(fullNames);
        }

        private void toolStripButtonBackUp_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }

            try
            {
                List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
                presenter.BackUp(fullNames);
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonShowModified_CheckedChanged(object sender, EventArgs e)
        {
            UpdateItemsList();
        }

        private void toolStripComboBoxChangeListToShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemsList();
        }

        private void toolStripButtonFilterFiles_Click(object sender, EventArgs e)
        {
            if (presenter.FilterFiles(selectedExtensions))
            {
                UpdateItemsList();
            }
        }

        private void toolStripButtonPrintProperties_Click(object sender, EventArgs e)
        {
            if (!IsListItemSelected)
            {
                return;
            }
            List<string> fullNames = listViewProvider.GetSelectedItemsFullName();
            presenter.PrintProperties(fullNames);
        }

        private void toolStripButtonLogByAuthor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBoxAuthor.Text))
            {
                MessageBox.Show(rm.GetString("AUTHOR_OF_REVISION_REQUEST", culture));
                return;
            }

            try
            {
                string author = toolStripTextBoxAuthor.Text;
                presenter.LogByAuthor(author);
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.InnerException.Message, rm.GetString("ERROR", culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonDiffChangesBetweenBranches_Click(object sender, EventArgs e)
        {
            DiffChanges form = new DiffChanges();
            form.Show();
        }

        #endregion

        #region HANDLERS

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateItemsList();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(Options.GetInstance.SwitchDir))
            {
                Directory.Delete(Options.GetInstance.SwitchDir, true);
            }
            if (Directory.Exists(Options.GetInstance.DiffDir))
            {
                Directory.Delete(Options.GetInstance.DiffDir, true);
            }
        }
        
        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            toolStripButtonShowDiff_Click(sender, e);
        }

        #endregion

        #region UTILS

        private void SetFormText()
        {
            this.Text = Constants.FORM_PREFIX + "-" + Options.GetInstance.WorkingCopyPath;
        }

        private ControlsData GetControlData()
        {
            ControlsData data = new ControlsData();
            uiSyncContext.Send( _ =>
            {
                data.ChangeList = toolStripComboBoxChangeListToShow.SelectedItem != null ?
                    toolStripComboBoxChangeListToShow.SelectedItem.ToString()
                    : Constants.ALL_ITEM;
                data.SelectedExtensions = selectedExtensions;
                data.ShowModified = toolStripButtonShowModified.Checked;
                data.ShowModifiedOnServer = toolStripButtonShowModifiedOnServer.Checked;
                data.ShowUnversioned = toolStripButtonShowUnversioned.Checked;
                data.ShowConflicted = toolStripButtonShowConflicted.Checked;
                data.ShowSwitched = toolStripButtonShowSwitched.Checked;
                data.ShowUnchanged = toolStripButtonShowUnchanged.Checked;
                data.UseServerData = toolStripButtonUseServerData.Checked;
                data.FastScan = toolStripButtonFastScan.Checked;
                data.IsSvnDif = toolStripButtonIsSVNDiff.Checked;
            }, null);
            return data; 
        }

        private void FillChangeListCheckbox()
        {
            this.toolStripComboBoxChangeListToShow.SelectedIndexChanged -= new System.EventHandler(this.toolStripComboBoxChangeListToShow_SelectedIndexChanged);
            changeLists.Remove(Constants.ALL_ITEM);
            List<string> dataSource = new List<string>(changeLists);
            dataSource.Sort();
            dataSource.Insert(0, Constants.ALL_ITEM);
            this.toolStripComboBoxChangeListToShow.ComboBox.DataSource = null;
            this.toolStripComboBoxChangeListToShow.ComboBox.DataSource = dataSource;
            toolStripComboBoxChangeListToShow.SelectedIndex = 0;
            this.toolStripComboBoxChangeListToShow.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxChangeListToShow_SelectedIndexChanged);
        }

        public void UpdateItemsList()
        {
            if (treeProvider.SelectedNode == null)
            {
                return;
            }
            uiSyncContext.Post(_ => listViewProvider.DataSource = presenter.GetActiveItems(treeProvider.SelectedNode.FullPath, ControlsData), null);
            //listView.UpdateItemsInListView(selCollection, provider.GetFilesTypes(GetControlData()));
        }

        private void ShowException(Exception exception)
        {
            MessageBox.Show(this, exception.Message,
                Options.ResManager.GetString("ERROR", Options.Culture),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region DISPLAY

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            int maxSize = 63;
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
                notifyIcon1.Text = this.Text.Length <= maxSize ? this.Text : this.Text.Substring(0, maxSize);
            }
        }
        
        #endregion
    }
}