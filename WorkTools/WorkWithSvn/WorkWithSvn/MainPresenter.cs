using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using Utils.WorkWithFiles;
using WorkWithSvn.DiskHierarchy.Base;
using WorkWithSvn.RepositoryProviders;
using WorkWithSvn.Utils;

namespace WorkWithSvn
{
    public class MainPresenter
    {
        IMainView view;
        private ResourceManager rm;
        private CultureInfo culture;
        private IProvider repositoryProvider;
        private FileSystemWatcher watcher;

        public RepositoryDirectory WorkingCopy
        {
            get
            {
                return repositoryProvider.WorkingCopy;
            }
        }

        private bool isWatcherActive;
        public bool IsWatcherActive
        {
            get
            {
                return isWatcherActive;
            }
            set
            {
                isWatcherActive = value;
                if (watcher != null)
                {
                    watcher.EnableRaisingEvents = value;
                }
            }
        }

        public MainPresenter(IMainView view)
        {
            this.view = view;
            rm = Options.ResManager;
            culture = OptionsUtils.Culture = Options.Culture;
            repositoryProvider = Options.Provider;
        }

        #region PUBLIC

        public bool OpenWorkingCopy()
        {
            string folder = string.Empty;
            string description = rm.GetString("CHOOSE_WORKING_COPY", culture);
            if (!SelectFolder.Select(description, ref folder))
            {
                return false;
            }
            Options.GetInstance.WorkingCopyPath = folder;
            return true;
        }

        public void Clear()
        {
            repositoryProvider.Clear();
        }

        public bool ReadDirectories(string fullName)
        {
            string headerText = rm.GetString("CATALOGS_SCAN_HEADER", culture);
            string infoText = rm.GetString("CATALOGS_SCAN_TEXT", culture);
            Action action = () => repositoryProvider.ReadDirectories(fullName);

            if (CancelFormEx.ShowProgressWindow(action, headerText))
            {
                InitWatcher();
                return true;
            }
            return false;
        }

        public bool ScanDirectory(string fullName, ControlsData ctrlData)
        {
            string headerText = rm.GetString("WORKING_COPY_SCAN_HEADER", culture);
            string infoText = rm.GetString("WORKING_COPY_SCAN_TEXT", culture);
            Action action = () => repositoryProvider.ScanDirectory(fullName, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }
        //COUNT
        public bool RefreshFilesStatus(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("FILE_INFO_UPDATE_HEADER", culture);
            string infoText = rm.GetString("FILE_INFO_UPDATE_TEXT", culture);
            Action action = () => repositoryProvider.RefreshFilesStatus(fullNames, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public void ShowDiff(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("GET_FILE_HEADER", culture);
            string infoText = rm.GetString("GET_FILE_TEXT", culture);
            Action action = () => repositoryProvider.ShowDiff(fullNames, ctrlData);
            CancelFormEx.ShowProgressWindow(action, headerText);
        }
        //COUNT
        public bool Resolved(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("RESOLVE_CONFLICT_HEADER", culture);
            string infoText = rm.GetString("RESOLVE_CONFLICT_TEXT", culture);
            Action action = () => repositoryProvider.Resolved(fullNames, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }
        //COUNT
        public bool Switch(List<string> fullNames, ControlsData ctrlData)
        {
            string targetLocation = null;
            bool restore = false;
            bool backup = false;

            
            if (fullNames.Count == 0
                || !GetSwitchData(fullNames[0], ref targetLocation, ref restore, ref backup))
            {
                return false;
            }
            
            string headerText = rm.GetString("SWITCH_FILE_HEADER", culture);
            string infoText = rm.GetString("SWITCH_FILE_TEXT", culture);
            Action action = () => repositoryProvider.Switch(fullNames, ctrlData, backup, restore, targetLocation);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }
        //COUNT
        public bool AddItems(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("ADD_ITEMS_HEADER", culture);
            string infoText = rm.GetString("ADD_ITEMS_TEXT", culture);
            Action action = () => repositoryProvider.AddItems(fullNames, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public bool DeleteItems(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("DELETE_ITEMS_HEADER", culture);
            string infoText = rm.GetString("DELETE_ITEMS_TEXT", culture);
            Action action = () => repositoryProvider.DeleteItems(fullNames, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public void DeleteFromDisk(List<string> fullNames)
        {
            foreach (string fullName in fullNames)
            {
                if (File.Exists(fullName))
                {
                    File.Delete(fullName);
                }
                else if (Directory.Exists(fullName))
                {
                    Directory.Delete(fullName, true);
                }
            }
        }

        public bool Update(List<string> fullNames, ControlsData ctrlData)
        {
            string headerText = rm.GetString("UPDATE_FILES_HEADER", culture);
            string infoText = rm.GetString("UPDATE_FILES_TEXT", culture);
            Action action = () => repositoryProvider.Update(fullNames, ctrlData);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public bool Commit(List<string> fullNames)
        {
            CommitMessage form = new CommitMessage();
            if (form.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(form.Message))
            {
                return false;
            }
            
            List<RepositoryItem> selCollection = repositoryProvider.GetItems(fullNames);
            List<string> files = GetFilesNamesForCommit(selCollection);
            string headerText = rm.GetString("COMMIT_FILES_HEADER", culture);
            string infoText = rm.GetString("COMMIT_FILES_TEXT", culture);
            Action action = () => repositoryProvider.Commit(files, form.Message);
            if (CancelFormEx.ShowProgressWindow(action, headerText))
            {
                OpenLog(selCollection);
                return true;
            }
            return false;
        }
        //COUNT
        public bool Revert(List<string> fullNames)
        {
            string headerText = rm.GetString("REVERT_FILES_HEADER", culture);
            string infoText = rm.GetString("REVERT_FILES_TEXT", culture);
            Action action = () => repositoryProvider.Revert(fullNames);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }
        //COUNT
        public void CreatePatch(List<string> fullNames)
        {
            string filePath = GetPatchFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            
            string headerText = rm.GetString("PATCH_CREATION_HEADER", culture);
            string infoText = rm.GetString("PATCH_CREATION_TEXT", culture);
            Action action = () =>
            {
                StringBuilder all = repositoryProvider.CreatePatch(fullNames);
                if (all != null)
                {
                    File.WriteAllText(filePath, all.ToString());
                }
            };
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public void MoveToChangeList(string changeListName, List<string> fullNames)
        {
            string headerText = rm.GetString("MOVE_TO_CHANGELIST_HEADER", culture);
            Action action = () => repositoryProvider.MoveToChangeList(changeListName, fullNames);
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public bool RemoveFromChangeList(List<string> fullNames)
        {
            string headerText = rm.GetString("REMOVE_FROM_CHANGELIST_HEADER", culture);
            Action action = () => repositoryProvider.RemoveFromChangeList(fullNames);
            return CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public ISet<String> GetChangeList()
        {
            return repositoryProvider.WorkingCopy.GetChangeList();
        }

        public void OpenDirectory(List<string> fullNames)
        {
            if (fullNames.Count == 0)
            {
                return;
            }

            string directory = Path.GetDirectoryName(fullNames[0]);
            UTILS.OpenDirectory(directory);
        }
        //COUNT
        public void BackUp(List<string> fullNames)
        {
            string filePath = GetZipFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            
            string folder = Path.GetDirectoryName(filePath) +
                     Path.DirectorySeparatorChar + Constants.BACKUP_DIR;
            
            string headerText = rm.GetString("COPY_FILES_HEADER", culture);
            string infoText = rm.GetString("COPY_FILES_TEXT", culture);

            Action action = () =>
            {
                foreach (string fullName in fullNames)
                {
                    if (File.Exists(fullName))
                    {
                        string path = fullName.Replace(repositoryProvider.WorkingCopy.FullName, folder);
                        string destDirectory = Path.GetDirectoryName(path);
                        if (!Directory.Exists(destDirectory))
                        {
                            Directory.CreateDirectory(destDirectory);
                        }
                        File.Copy(fullName, path, true);
                    }

                    //form.Increment();
                }
                UTILS.AddFilesToZipArchive(filePath, folder);
            };
            CancelFormEx.ShowProgressWindow(action, headerText);
        }

        public bool FilterFiles(ISet<string> selectedExtensions)
        {
            ISet<string> filesExtensions = repositoryProvider.WorkingCopy.GetFilesExtensions();
            SelectExtensions form = new SelectExtensions(filesExtensions);
            if (form.ShowDialog() == DialogResult.OK)
            {
                selectedExtensions.Clear();
                selectedExtensions.UnionWith(form.SelectedExtensions);
                return true;
            }
            return false;
        }

        public void LogByAuthor(string author)
        {
            string headerText = rm.GetString("GET_LOG_HEADER", culture);
            string infoText = rm.GetString("GET_LOG_TEXT", culture);

            CommitData logs = null;
            Action action = () =>
            {
                logs = repositoryProvider.LogByAuthor(author);
            };
            if (CancelFormEx.ShowProgressWindow(action, headerText))
            {
                GridForm logForm = new GridForm(logs);
                logForm.Show();
            }
        }

        public void PrintProperties(List<string> fullNames)
        {
            string filePath = GetTextFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            Logger logger = Logger.GetInstance();
            logger.Init(filePath);
            foreach (string path in fullNames)
            {
                logger.WriteLine(path);
            }
            logger.Close();
        }

        public IEnumerable<RepositoryItem> GetActiveItems(string fullName, ControlsData ctrlData)
        {
            return repositoryProvider.GetActiveItems(fullName, ctrlData);
        }

        public void SetItemData(string fullName, ControlsData ControlsData)
        {
            repositoryProvider.SetItemData(fullName, ControlsData);
        }

        public bool RenameItem(string oldFullName, string fullName)
        {
            RepositoryItem item = repositoryProvider.GetDeletedItem(oldFullName);
            if (item != null)
            {
                //if (treeProvider.SelectedNode != null
                //    && e.FullPath.Contains(treeProvider.SelectedNode.FullPath))
                //{
                //    //listView.RenameItemsInListView(entity, e.FullPath);
                //}
                item.Rename(Path.GetFileName(fullName));
                return true;
            }
            return false;
        }

        public bool DeletedItem(string fullName)
        {
            RepositoryItem item = repositoryProvider.GetDeletedItem(fullName);
            if (item != null)
            {
                //if (treeProvider.SelectedNode != null
                //    && e.FullPath.Contains(treeProvider.SelectedNode.FullPath))
                //{
                //    //listView.Remove(entity);
                //}
                repositoryProvider.WorkingCopy.DeleteItem(item);
                return true;
            }
            return false;
        }

        #endregion

        #region PRIVATE

        private bool GetSwitchData(string fullPath, ref string targetLocation, ref bool restore, ref bool backup)
        {
            SwitchLocation form = new SwitchLocation(fullPath, repositoryProvider);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            targetLocation = form.TargetLocation;
            restore = form.IsRestoreFile;
            backup = form.IsBackupFile;
            return true;
        }

        private List<string> GetFilesNamesForCommit(List<RepositoryItem> selCollection)
        {
            List<string> files = new List<string>();
            foreach (RepositoryItem entity in selCollection)
            {
                if (repositoryProvider.IsNotVersioned(entity))
                {
                    repositoryProvider.AddFile(entity.FullName);
                }
                files.Add(entity.FullName);
            }
            return files;
        }
        
        private void OpenLog(List<RepositoryItem> selCollection)
        {
            foreach (RepositoryItem repItem in selCollection)
            {
                if (!repItem.IsDeleted
                    && !string.IsNullOrEmpty(repItem.FullName))
                {
                    UTILS.OpenLog(repItem.FullName);
                    return;
                }
            }
        }

        #endregion

        #region FILE_PATHS

        private string GetPatchFilePath()
        {
            string[] extensions = new string[]
            {
            "Patch Files (*.patch)|*.patch|",
            "All Files (*.*)|*.*"
            };

            string title = rm.GetString("PATCH_SAVE", culture);
            return GetFilePath(extensions, title);
        }

        private string GetZipFilePath()
        {
            string[] extensions = new string[]
            {
            "ZIP Files (*.zip)|*.zip|",
            "All Files (*.*)|*.*"
            };
            return GetFilePath(extensions);
        }

        private string GetTextFilePath()
        {
            string[] extensions = new string[]
            {
            "TXT Files (*.txt)|*.txt|",
            "All Files (*.*)|*.*"
            };
            return GetFilePath(extensions);
        }

        private string GetFilePath(string[] extensions, string title = null)
        {
            string filePath = null;
            if (string.IsNullOrEmpty(title))
            {
                title = rm.GetString("SAVE_DATA_TEXT", culture);
            }
            SelectFile.SaveFile(title, string.Empty, ref filePath, extensions);
            return filePath;
        }

        #endregion

        #region WATCHER

        private void InitWatcher()
        {
            watcher = new FileSystemWatcher(Options.GetInstance.WorkingCopyPath);
            SubscribeWatcher();
            watcher.EnableRaisingEvents = IsWatcherActive;
            watcher.IncludeSubdirectories = true;
        }

        private void SubscribeWatcher()
        {
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (!watcher.EnableRaisingEvents || UTILS.IsIgnoredEntity(e.FullPath))
            {
                return;
            }
            try
            {
                SetItemData(e.FullPath, view.ControlsData);
                //RepositoryItem entity = repositoryProvider.GetEntity(e.FullPath);
                //if (entity != null
                //    && treeProvider.SelectedNode != null
                //    && e.FullPath.Contains(treeProvider.SelectedNode.FullPath))
                //{
                //    //listView.Add(entity);
                //}
                view.UpdateItemsList();
            }
            catch (Exception)
            {
            }
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!watcher.EnableRaisingEvents || UTILS.IsIgnoredEntity(e.FullPath))
            {
                return;
            }
            try
            {
                SetItemData(e.FullPath, view.ControlsData);
                //RepositoryItem entity = repositoryProvider.GetEntity(e.FullPath);
                //if (entity != null 
                //    && treeProvider.SelectedNode != null
                //    && e.FullPath.Contains(treeProvider.SelectedNode.FullPath))
                //{
                //    //listView.UpdateItemsInListView(new List<RepoEntityData> { entity }, provider.GetFilesTypes(GetControlData()));
                //}
                view.UpdateItemsList();
            }
            catch (Exception)
            {
            }
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (!watcher.EnableRaisingEvents || UTILS.IsIgnoredEntity(e.FullPath))
            {
                return;
            }
            try
            {
                if (RenameItem(e.OldFullPath, e.Name))
                {
                    view.UpdateItemsList();
                }
            }
            catch (Exception)
            {
            }
        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!watcher.EnableRaisingEvents || UTILS.IsIgnoredEntity(e.FullPath))
            {
                return;
            }
            try
            {
                if (DeletedItem(e.FullPath))
                {
                    view.UpdateItemsList();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
