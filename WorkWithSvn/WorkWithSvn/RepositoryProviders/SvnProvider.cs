using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpSvn;
using SharpSvn.Implementation;
using Utils;
using WorkWithSvn.DiskHierarchy.Base;
using WorkWithSvn.DiskHierarchy.Svn;
using WorkWithSvn.Utils;

namespace WorkWithSvn.RepositoryProviders
{
    public class SvnProvider : IProvider
    {
        public RepositoryDirectory WorkingCopy
        {
            get;
            private set;
        }
        
        #region PUBLIC

        public void ReadDirectories(string fullName)
        {
            DirectoryInfo dir = new DirectoryInfo(fullName);
            WorkingCopy = new SvnDirectory(dir);
        }

        public void Clear()
        {
            WorkingCopy = null;
        }

        public void ScanDirectory(string fullName, ControlsData ctrlData)
        {
            System.Collections.ObjectModel.Collection<SvnStatusEventArgs> list = null;
            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                if (ctrlData.UseServerData)
                {
                    args.RetrieveRemoteStatus = ctrlData.UseServerData;
                    args.RetrieveAllEntries = ctrlData.ShowUnchanged;
                }
                client.GetStatus(fullName, args, out list);

                if (!ctrlData.FastScan)
                {
                    RemoveFilesFromChanged(list);
                }

                foreach (SvnStatusEventArgs arg in list)
                {
                    if (!SvnRepositoryHelper.IsIgnoredItem(arg))
                    {
                        SetItemData(this, arg);
                    }
                }
            }
        }

        public void RefreshFilesStatus(List<string> fullNames, ControlsData ctrlData)
        {
            foreach (RepositoryItem repItem in GetItems(fullNames))
            {
                SetItemData(repItem);
            }
        }

        public void ShowDiff(List<string> fullNames, ControlsData ctrlData)
        {
            if (fullNames.Count == 0)
            {
                return;
            }

            string fullPath = fullNames[0];
            string fileName = Path.GetFileName(fullPath);
            string baseFilePath = Options.GetInstance.DiffDir + Path.DirectorySeparatorChar + Constants.DIFF_FILE;

            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = true;
                Collection<SvnStatusEventArgs> list;
                client.GetStatus(fullPath, args, out list);
                if (list.Count == 0)
                {
                    return;
                }
                if (list[0].Uri == null || list[0].LocalContentStatus == SvnStatus.Added)
                {
                    UTILS.OpenFileInNotepad(fullPath);
                }
                else
                {
                    SvnTarget target = SvnTarget.FromUri(list[0].Uri);
                    GetFileFromServer(baseFilePath, client, target);
                    UTILS.OpenDiff(fullPath, fileName, baseFilePath, ctrlData.IsSvnDif);
                }
            }
        }

        public void Resolved(List<string> fullNames, ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {
                    client.Resolved(repItem.FullName);
                    SetItemData(repItem);
                }
            }
        }

        public void Switch(List<string> fullNames, ControlsData ctrlData,
            bool backup, bool restore, string targetLocation)
        {
            using (SvnClient client = new SvnClient())
            {
                bool ok = false;
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {

                    if (backup)
                    {
                        UTILS.BackUpFile(WorkingCopy.FullName,
                            WorkingCopy.Name, repItem.FullName);
                    }

                    ok = client.Switch(repItem.FullName, GetTargetLocationUri(repItem.FullName, targetLocation));

                    if (restore)
                    {
                        UTILS.RestoreFile(WorkingCopy.FullName,
                            WorkingCopy.Name, repItem.FullName);
                    }

                    SetItemData(repItem);
                }
            }
        }

        public void AddItems(List<string> fullNames, ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                List<RepositoryItem> selCollection = GetItems(fullNames);

                List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                foreach (string itemPath in pathes)
                {
                    client.Add(itemPath, SvnDepth.Empty);
                }

                SetItemsData(selCollection);
            }
        }

        public void DeleteItems(List<string> fullNames, ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                List<RepositoryItem> selCollection = GetItems(fullNames);
                List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                SvnDeleteArgs args = new SvnDeleteArgs();
                args.Force = true;
                args.ThrowOnError = false;
                client.Delete(pathes, args);
                SetItemsData(selCollection);
            }
        }

        public void Update(List<string> fullNames, ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                List<RepositoryItem> selCollection = GetItems(fullNames);
                List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                SvnUpdateArgs argsU = new SvnUpdateArgs();
                SvnUpdateResult res;
                if (!client.Update(pathes, out res))
                {
                    throw new Exception("Update Failed");
                }

                SetItemsData(selCollection);
            }
        }

        public void Commit(List<string> fullNames, string message)
        {
            using (SvnClient client = new SvnClient())
            {
                if (fullNames.Count == 0)
                {
                    throw new Exception("File's List Is Empty");
                }

                SvnCommitArgs args = new SvnCommitArgs();
                args.LogMessage = message;
                if (!client.Commit(fullNames, args))
                {
                    throw new Exception(Options.ResManager.GetString("COMMIT_ERROR", Options.Culture));
                }

                foreach (string path in fullNames)
                {
                    RepositoryItem file = WorkingCopy.GetFile(path);
                    file.LocalStatus = SvnStatus.Normal;
                    file.RemoteStatus = SvnStatus.None;
                }
            }
        }
        
        public bool IsNotVersioned(RepositoryItem repItem)
        {
            return repItem.IsNotVersioned;
        }

        public void AddFile(string fullPath)
        {
            using (SvnClient client = new SvnClient())
            {
                client.Add(fullPath);
            }
        }

        public void Revert(List<string> fullNames)
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {
                    client.Revert(repItem.FullName);
                    SetItemData(repItem);
                }
            }
        }

        public StringBuilder CreatePatch(List<string> fullNames)
        {
            using (SvnClient client = new SvnClient())
            {
                StringBuilder all = new StringBuilder();
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {
                    SvnTarget from = SvnTarget.FromString(repItem.FullName);
                    SvnTarget to = SvnTarget.FromUri(client.GetUriFromWorkingCopy(repItem.FullName));
                    MemoryStream stream = new MemoryStream();
                    if (client.Diff(from, to, stream))
                    {
                        stream.Position = 0;
                        StreamReader strReader = new StreamReader(stream);
                        all.Append(FormatDiff(strReader));
                    }
                }
                all.Append(" /\n");
                return all;
            }
        }

        public void MoveToChangeList(string changeListName, List<string> fullNames)
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {
                    if (string.IsNullOrEmpty(repItem.ChangeList))
                    {
                        client.AddToChangeList(repItem.FullName, changeListName);
                        repItem.ChangeList = changeListName;
                    }
                }
            }
        }

        public void RemoveFromChangeList(List<string> fullNames)
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetItems(fullNames))
                {
                    client.RemoveFromChangeList(repItem.FullName);
                    repItem.ChangeList = null;
                }
            }
        }

        public CommitData LogByAuthor(string author)
        {
            CommitData logs = new CommitData();
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    System.Collections.ObjectModel.Collection<SvnLogEventArgs> outArgs;
                    SvnLogArgs args = new SvnLogArgs();
                    args.RetrieveAllProperties = true;
                    args.RetrieveAllProperties = true;
                    client.GetLog(Options.GetInstance.WorkingCopyPath, out outArgs);

                    foreach (SvnLogEventArgs item in outArgs)
                    {
                        if (item.Author.Equals(author))
                        {
                            logs.COMMIT.AddCOMMITRow(
                                item.Author,
                                item.Revision,
                                GetFilesList(item.ChangedPaths),
                                item.LogMessage,
                                item.Time
                                );
                        }
                    }
                }
                catch (Exception ex)
                {
                    logs.ERROR.AddERRORRow(ex.Message);
                }
            }
            return logs;
        }

        public void SetItemData(string fullName, ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = ctrlData.UseServerData;
                args.RetrieveAllEntries = true;
                client.Status(fullName, args, SetItemData);
            }
        }

        public RepositoryItem GetDeletedItem(string fullPath)
        {
            if (fullPath == WorkingCopy.FullName
                || UTILS.IsIgnoredEntity(fullPath))
            {
                return null;
            }
            RepositoryItem file = WorkingCopy.GetFile(fullPath);
            if (file != null)
            {
                return file;
            }
            RepositoryItem dir = WorkingCopy.GetDirectory(fullPath);
            if (dir != null)
            {
                return dir;
            }
            return null;
        }

        public List<RepositoryItem> GetItems(List<string> fullNames)
        {
            List<RepositoryItem> list = new List<RepositoryItem>();
            foreach (String path in fullNames)
            {
                if (UTILS.IsDirectory(path))
                {
                    RepositoryDirectory dir = WorkingCopy.GetDirectory(path);
                    if (dir != null)
                    {
                        list.Add(dir);
                    }
                }
                else
                {
                    RepositoryFile file = WorkingCopy.GetFile(path);
                    if (file != null)
                    {
                        list.Add(file);
                    }
                }
            }
            return list;
        }

        public IEnumerable<RepositoryItem> GetActiveItems(string fullName, ControlsData ctrlData)
        {
            RepositoryDirectory dir = WorkingCopy.GetDirectory(fullName);
            List<RepositoryItem> list = new List<RepositoryItem>();
            if (dir != null)
            {
                string changeList = ctrlData.ChangeList != null ? ctrlData.ChangeList : Constants.ALL_ITEM;
                RepositoryFileStatuses filesTypes = GetFilesTypes(ctrlData);
                GetActiveItems(dir, filesTypes, changeList, ctrlData.SelectedExtensions, list);
            }
            return list;
        }

        public string GetLocation(string path)
        {
            return GetLocation_(path);
        }

        public Uri GetRepository(string path)
        {
            using (SvnClient client = new SvnClient())
            {
                return client.GetRepositoryRoot(path);
            }
        }

        #endregion

        #region PRIVATE

        private void RemoveFilesFromChanged(Collection<SvnStatusEventArgs> list)
        {
            List<string> items = new List<string>();
            foreach (SvnStatusEventArgs file in list)
            {
                if (SvnRepositoryHelper.IsIgnoredItem(file))
                {
                    continue;
                }
                items.Add(file.FullPath);
            }
            WorkingCopy.RemoveFilesFromChanged(items);
        }

        private void SetItemsData(List<RepositoryItem> selCollection)
        {
            foreach (RepositoryItem repItem in selCollection)
            {
                SetItemData(repItem);
            }
        }

        private void SetItemData(RepositoryItem repItem)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = true;
                args.RetrieveAllEntries = true;
                Collection<SvnStatusEventArgs> list;
                client.GetStatus(repItem.FullName, args, out list);

                if (list.Count > 0)
                {
                    SetItemData(repItem, list[0]);
                }
                else
                {
                    repItem.LocalStatus = SvnStatus.Normal;
                    repItem.RemoteStatus = SvnStatus.None;
                    repItem.IsSwitched = false;
                }
            }
        }

        private void SetItemData(object sender, SvnStatusEventArgs arg)
        {
            if (arg.FullPath == WorkingCopy.FullName
                || SvnRepositoryHelper.IsIgnoredItem(arg))
            {
                return;
            }

            if (!SvnRepositoryHelper.IsDirectory(arg))
            {
                RepositoryItem file = WorkingCopy.GetFileOrCreate(arg.FullPath);
                SetItemData(file, arg);
            }
            else
            {
                RepositoryItem dir = WorkingCopy.GetDirectoryOrCreate(arg.FullPath);
                SetItemData(dir, arg);
            }
        }

        private void SetItemData(RepositoryItem repItem, SvnStatusEventArgs arg)
        {
            if (repItem == null)
            {
                return;
            }
            repItem.Revision = arg.Revision != -1 ? arg.Revision.ToString() : null;
            repItem.LocalStatus = arg.LocalContentStatus;
            repItem.RemoteStatus = arg.RemoteContentStatus;
            repItem.ChangeList = arg.ChangeList;
            repItem.Location = GetLocation_(repItem.FullName);
            repItem.IsSwitched = arg.Switched;
        }

        private static Uri GetTargetLocationUri(string path, string targetLocation)
        {
            using (SvnClient client = new SvnClient())
            {
                string repositoryRoot = client.GetRepositoryRoot(path) != null ? client.GetRepositoryRoot(path).ToString() : null;
                string workingRoot = client.GetWorkingCopyRoot(path);
                if (repositoryRoot == null)
                {
                    return null;
                }
                string location = path.Replace(workingRoot, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                Uri targetUri = new Uri(repositoryRoot + targetLocation + location);
                IsTargetLocationValid(path, targetUri);
                return targetUri;
            }
        }

        private static void IsTargetLocationValid(string path, Uri targetUri)
        {
            using (SvnClient client = new SvnClient())
            {
                string repositoryRoot1;
                string repositoryRoot2;
                SvnTarget tg = null;

                try
                {
                    repositoryRoot1 = client.GetRepositoryRoot(path) != null ? client.GetRepositoryRoot(path).ToString() : null;
                    repositoryRoot2 = client.GetRepositoryRoot(targetUri) != null ? client.GetRepositoryRoot(targetUri).ToString() : null;
                    tg = SvnTarget.FromUri(targetUri);
                    System.Collections.ObjectModel.Collection<SvnFileVersionEventArgs> lst;
                    bool ok = client.GetFileVersions(tg, out lst);
                }
                catch (System.Exception)
                {
                    repositoryRoot1 = repositoryRoot2 = null;
                }
                if (repositoryRoot1 == null
                    || repositoryRoot2 == null
                    || !repositoryRoot1.Equals(repositoryRoot2)
                    )
                {
                    throw new Exception(Options.ResManager.GetString("WRONG_LOCATION", Options.Culture));
                }
            }
        }

        private static string GetLocation_(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            using (SvnClient client = new SvnClient())
            {
                string uri = Convert.ToString(client.GetUriFromWorkingCopy(path));
                string repositoryRoot = Convert.ToString(client.GetRepositoryRoot(path));
                if (string.IsNullOrEmpty(uri) || string.IsNullOrEmpty(repositoryRoot))
                {
                    return null;
                }
                string workingRoot = client.GetWorkingCopyRoot(path);
                string forReplace = path.Replace(workingRoot, string.Empty).Replace(Path.DirectorySeparatorChar, '/');
                if (string.IsNullOrEmpty(forReplace))
                {
                    return null;
                }
                return uri.Replace(repositoryRoot, string.Empty).Replace(forReplace, string.Empty);
            }
        }

        private static void GetFileFromServer(string baseFilePath, SvnClient client, SvnTarget target)
        {
            if (File.Exists(baseFilePath))
            {
                File.Delete(baseFilePath);
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(baseFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                client.Write(target, fs);
                fs.Flush();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private string FormatDiff(StreamReader strReader)
        {
            StringBuilder outStr = new StringBuilder();
            bool isPlus = false, isMinus = false;
            string temp = null;
            while (!string.IsNullOrEmpty(temp = strReader.ReadLine().Trim()))
            {

                if (temp.StartsWith("---"))
                {
                    outStr.Append(FormatDiff(temp) + "\n");
                    isMinus = true;
                }

                else if (temp.StartsWith("+++"))
                {
                    outStr.Append(FormatDiff(temp) + "\n");
                    isPlus = true;
                }

                else
                {
                    outStr.Append(temp.Replace(WorkingCopy.FullName.Replace(@"\", "/") + "/", "") + "\n");
                }

                if (isPlus && isMinus)
                {
                    temp = strReader.ReadToEnd().TrimEnd('\n').TrimEnd('/').Trim();
                    if (!temp.EndsWith("\n"))
                    {
                        temp += '\n';
                    }
                    outStr.Append(temp);
                    break;
                }
            }
            return outStr.ToString();
        }

        private string FormatDiff(string temp)
        {
            int ind1 = temp.IndexOf("(", 0);
            int ind2 = temp.IndexOf(")", ind1);
            string temp2 = temp.Substring(0, ind1)
                .Replace(WorkingCopy.FullName.Replace(@"\", "/") + "/", "")
                + temp.Substring(ind2 + 1);
            return temp2.Replace("\t\t", "\t");
        }

        private static string GetFilesList(SvnChangeItemCollection files)
        {
            StringBuilder formatedFilesSb = new StringBuilder();
            foreach (SvnChangeItem item in files)
            {
                if (formatedFilesSb.Length > 0)
                {
                    formatedFilesSb.Append(Environment.NewLine);
                }
                formatedFilesSb.Append(new StringBuilder(item.Action.ToString()).Append(" ").Append(item.Path));
            }
            return formatedFilesSb.ToString();
        }

        private void GetActiveItems(RepositoryDirectory baseDir, RepositoryFileStatuses filesTypes, string changeList
            , ISet<string> selectedExtensions, List<RepositoryItem> list)
        {
            foreach (RepositoryFile file in baseDir.Files)
            {
                if (IsValidFile(filesTypes, changeList, selectedExtensions, file))
                {
                    list.Add(file);
                }
            }

            foreach (RepositoryDirectory dir in baseDir.Directories)
            {
                if (IsValidDirectory(filesTypes, changeList, dir))
                {
                    list.Add(dir);
                }
                GetActiveItems(dir, filesTypes, changeList, selectedExtensions, list);
            }
        }

        private static bool IsValidDirectory(RepositoryFileStatuses filesTypes, string changeList, RepositoryDirectory dir)
        {
            return !(dir.IsIgnoredItem(filesTypes)
                                || UTILS.IsIgnoredChangeList(changeList, dir)
                                || UTILS.IsIgnoredEntity(dir.FullName));
        }

        private static bool IsValidFile(RepositoryFileStatuses filesTypes, string changeList, ISet<string> selectedExtensions, RepositoryFile file)
        {
            return !(file.IsIgnoredItem(filesTypes)
                                || UTILS.IsIgnoredChangeList(changeList, file)
                                || UTILS.IsIgnoredExtension(selectedExtensions, file));
        }

        private RepositoryFileStatuses GetFilesTypes(ControlsData ctrlData)
        {
            SvnFileStatuses fileTypes = new SvnFileStatuses();
            if (ctrlData.ShowModified)
            {
                fileTypes.AddModified();
                fileTypes.isModifiedLocal = true;
            }
            if (ctrlData.ShowModifiedOnServer)
            {
                fileTypes.AddModified();
                fileTypes.isModifiedOnServer = true;
            }
            if (ctrlData.ShowUnversioned)
            {
                fileTypes.Add(SvnStatus.NotVersioned);
            }
            if (ctrlData.ShowConflicted)
            {
                fileTypes.Add(SvnStatus.Conflicted);
            }
            if (ctrlData.ShowSwitched)
            {
                fileTypes.isSwitched = true;
            }
            if (ctrlData.ShowUnchanged)
            {
                fileTypes.Add(SvnStatus.Normal);
            }
            return fileTypes;
        }

        #endregion
    }
}
