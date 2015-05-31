using System.Collections.ObjectModel;
using System.IO;
using SharpSvn;
using Utils;
using Display;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using WorkWithSvn.DiskHierarchy;
using SharpSvn.Implementation;
using WorkWithSvn.Utils;

namespace WorkWithSvn.Providers
{
    public class SvnProvider : AProvider
    {
        private SvnDirectory workingCopy;
        private delegate void InvokeMethod(ControlsData ctrlData);
        Collection<SvnStatusEventArgs> list;
        public event Action ProcessEnded;
        public event Action Increment;

        public RepositoryDirectory WorkingCopy
        {
            get { return workingCopy; }
        }
        
        private int SelItemsCount
        {
            get { return ListView.ListViewCtrl.SelectedItems.Count; }
        }

        public Exception Error
        {
            get;
            private set;
        }

        #region AProvider Members

        public void GetDirectories()
        {
            DirectoryInfo dir = new DirectoryInfo(Options.GetInstance.WorkingCopyPath);
            workingCopy = new SvnDirectory(dir);
            OnProcessEnded();
        }

        public WorkWithTree Tree
        {
            get; set;
        }

        public WorkWithListView ListView
        {
            get; set;
        }

        public void FillTree()
        {
            Tree.Fill(workingCopy);
            ListView.Clear();
        }

        public void FillListView(ControlsData ctrlData)
        {
            if (ListView.ListViewCtrl.InvokeRequired)
            {
                InvokeMethod d = new InvokeMethod(FillListView);
                ListView.ListViewCtrl.Invoke(d, ctrlData);
            }
            else
            {
                string cl = ctrlData.ChangeList != null ? ctrlData.ChangeList : Constants.ALL_ITEM;
                ListView.ListViewCtrl.BeginUpdate();
                ListView.Clear();
                ListView.Fill(workingCopy.GetDirectory(Tree.SelNode.FullPath) as SvnDirectory,
                    GetFilesTypes(ctrlData), cl, ctrlData.SelectedExtensions);
                ListView.ListViewCtrl.EndUpdate();
            }
        }

        public RepoFileTypes GetFilesTypes(ControlsData ctrlData)
        {
            SvnFileTypes fileTypes = new SvnFileTypes();
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

        public void ClearWorkingCopy()
        {
            ListView.Clear();
            Tree.Clear();
            workingCopy = null;
        }

        public void SetChangedEntitysData(string fullPath, ControlsData ctrlData)
        {
            System.Collections.ObjectModel.Collection<SvnStatusEventArgs> list = null;
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnStatusArgs args = new SvnStatusArgs();
                    if (ctrlData.UseServerData)
                    {
                        args.RetrieveRemoteStatus = ctrlData.UseServerData;
                        args.RetrieveAllEntries = ctrlData.ShowUnchanged;
                    }
                    client.GetStatus(fullPath, args, out list);

                    if (!ctrlData.FastScan)
                    {
                        RemoveFilesFromChanged(list);
                    }

                    foreach (SvnStatusEventArgs arg in list)
                    {
                        if (!SvnRepositoryHelper.IsIgnoredItem(arg))
                        {
                            SetEntityData(arg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }

        public void RefreshFileStatus(ControlsData ctrlData)
        {
            foreach (RepositoryItem repItem in GetSelectedItems())
            {
                SetEntityData(repItem);
                OnIncrement();
            }
            OnProcessEnded();
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

        public void ShowDiff(ControlsData controlsData)
        {
            List<string> pathes = ListView.GetSelectedItemsPath();
            if (pathes.Count == 0)
            {
                OnProcessEnded();
                return;
            }

            string fullPath = pathes[0];
            string fileName = Path.GetFileName(fullPath);
            string baseFilePath = Options.GetInstance.DiffDir + Path.DirectorySeparatorChar + Constants.DIFF_FILE;

            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = true;
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
                    UTILS.OpenDiff(fullPath, fileName, baseFilePath, controlsData.IsSvnDif);
                }
                OnProcessEnded();
            }
        }

        public void SetEntityData(string fullPath,  ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnStatusArgs args = new SvnStatusArgs();
                    args.RetrieveRemoteStatus = ctrlData.UseServerData;
                    client.Status(fullPath, args,
                    delegate(object lSender, SvnStatusEventArgs entityArgs)
                    {
                        SetEntityData(entityArgs);
                    });
                }
                catch (Exception)
                {

                }
            }
        }

        public void Resolved(ControlsData ctrlData)
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetSelectedItems())
                {
                    client.Resolved(repItem.FullName);
                    SetEntityData(repItem);
                    OnIncrement();
                }
                OnProcessEnded();
            }
        }

        public void Switch(ControlsData ctrlData,
            bool backup, bool restore, string targetLocation)
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    bool ok = false;
                    foreach (RepositoryItem repItem in GetSelectedItems())
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

                        SetEntityData(repItem);
                        OnIncrement();
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }

        public void Update(ControlsData ctrlData)
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    List<RepositoryItem> selCollection = GetSelectedItems();
                    List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                    SvnUpdateArgs argsU = new SvnUpdateArgs();
                    SvnUpdateResult res;
                    if (!client.Update(pathes, out res))
                    {
                        throw new Exception("Update Failed");
                    }

                    SetEntitysData(selCollection);
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }

        public void Commit(List<string> files, string message)
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    if (files.Count == 0)
                    {
                        throw new Exception("File's List Is Empty");
                    }

                    SvnCommitArgs args = new SvnCommitArgs();
                    args.LogMessage = message;
                    if (!client.Commit(files, args))
                    {
                        throw new Exception(Options.ResManager.GetString("COMMIT_ERROR", Options.Culture));
                    }

                    foreach (string path in files)
                    {
                        RepositoryItem file = workingCopy.GetFile(path);
                        file.LocalStatus = SvnStatus.Normal;
                        file.RemoteStatus = SvnStatus.None;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }

        public void Revert()
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    foreach (RepositoryItem repItem in GetSelectedItems())
                    {
                        client.Revert(repItem.FullName);
                        SetEntityData(repItem);
                        OnIncrement();
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }

        public StringBuilder CreatePatch()
        {
            using (SvnClient client = new SvnClient())
            {
                StringBuilder all = new StringBuilder();
                foreach (RepositoryItem repItem in GetSelectedItems())
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
                    OnIncrement();
                }
                all.Append(" /\n");
                OnProcessEnded();
                return all;
            }
        }

        public bool MoveToChangeList(string changeList)
        {
            bool ret = false;
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetSelectedItems())
                {
                    if (string.IsNullOrEmpty(repItem.ChangeList))
                    {
                        string changeListName = changeList;
                        client.AddToChangeList(repItem.FullName, changeListName);
                        repItem.ChangeList = changeListName;
                    }
                }
            }
            return ret;
        }

        public void RemoveFromChangeList()
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (RepositoryItem repItem in GetSelectedItems())
                {
                    client.RemoveFromChangeList(repItem.FullName);
                    repItem.ChangeList = null;
                }
            }
        }

        public CommitData GetLogs(string author)
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
                finally
                {
                    OnProcessEnded();
                }
            }
            return logs;
        }

        public RepositoryItem GetEntity(string fullPath)
        {
            if (fullPath == WorkingCopy.FullName
                || UTILS.IsIgnoredEntity(fullPath))
            {
                return null;
            }

            if (!UTILS.IsDirectory(fullPath))
            {
                return workingCopy.GetFile(fullPath);
            }
            else
            {
                return workingCopy.GetDirectory(fullPath);
            }
        }

        public RepositoryItem GetDeletedEntity(string fullPath)
        {
            if (fullPath == WorkingCopy.FullName
                || UTILS.IsIgnoredEntity(fullPath))
            {
                return null;
            }
            RepositoryItem file = workingCopy.GetFile(fullPath);
            if (file != null)
            {
                return file;
            }
            RepositoryItem dir = workingCopy.GetDirectory(fullPath);
            if (dir != null)
            {
                return dir;
            }
            return null;
        }

        public bool IsUnchanged(RepositoryItem repItem)
        {
            return repItem.IsUnchanged;
        }

        public List<RepositoryItem> GetSelectedItems()
        {
            List<string> filesPath = ListView.GetSelectedItemsPath();
            List<RepositoryItem> list = new List<RepositoryItem>();
            foreach (String path in filesPath)
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

        public void Add(ControlsData ctrlData)
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    List<RepositoryItem> selCollection = GetSelectedItems();

                    List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                    foreach (string itemPath in pathes)
                    {
                        client.Add(itemPath, SvnDepth.Empty);
                        OnIncrement();
                    }

                    SetEntitysData(selCollection);
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
        }
        
        public void Delete(ControlsData ctrlData)
        {
            Error = null;
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    List<RepositoryItem> selCollection = GetSelectedItems();
                    List<string> pathes = selCollection.Select(item => item.FullName).ToList();
                    SvnDeleteArgs args = new SvnDeleteArgs();
                    args.Force = true;
                    args.ThrowOnError = false;
                    client.Delete(pathes, args);
                    SetEntitysData(selCollection);
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    OnProcessEnded();
                }
            }
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

        public void UnsubscribeEvents()
        {
            if (ProcessEnded != null)
            {
                ProcessEnded = null;
            }
            if (Increment != null)
            {
                Increment = null;
            }        
        }

        #endregion

        private static string GetLocation_(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            using (SvnClient client = new SvnClient())
            {
                string uri = client.GetUriFromWorkingCopy(path) != null ? client.GetUriFromWorkingCopy(path).ToString() : null;
                string repositoryRoot = client.GetRepositoryRoot(path) != null ? client.GetRepositoryRoot(path).ToString() : null;
                Uri ur1 = client.GetRepositoryRoot(path);
                string workingRoot = client.GetWorkingCopyRoot(path);
                if (uri == null || repositoryRoot == null)
                {
                    return null;
                }
                string serverPart = uri.Replace(repositoryRoot, UTILS.EMPTY);
                string forReplace = path.Replace(workingRoot, UTILS.EMPTY).Replace(Path.DirectorySeparatorChar, '/');
                if (string.IsNullOrEmpty(forReplace))
                {
                    return null;
                }
                string location = serverPart.Replace(forReplace, UTILS.EMPTY);
                return location;
            }
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
                string location = path.Replace(workingRoot, UTILS.EMPTY).Replace(Path.DirectorySeparatorChar, '/');
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
        
        private void RemoveFilesFromChanged(Collection<SvnStatusEventArgs> list)
        {
            List<string> files = new List<string>();
            foreach (SvnStatusEventArgs file in list)
            {
                if (SvnRepositoryHelper.IsIgnoredItem(file))
                {
                    continue;
                }
                files.Add(file.FullPath);
            }
            WorkingCopy.RemoveFilesFromChanged(files);
        }

        private void SetEntitysData(List<RepositoryItem> selCollection)
        {
            foreach (RepositoryItem entity in selCollection)
            {
                SetEntityData(entity);
            }
        }

        private void SetEntityData(RepositoryItem repItem)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = true;
                args.RetrieveAllEntries = true;
                client.GetStatus(repItem.FullName, args, out list);

                if (list.Count > 0)
                {
                    SetEntityData(repItem, list[0]);
                }
                else
                {
                    repItem.LocalStatus = SvnStatus.Normal;
                    repItem.RemoteStatus = SvnStatus.None;
                    repItem.IsSwitched = false;
                }
            }
        }

        private void SetEntityData(SvnStatusEventArgs arg)
        {
            if (arg.FullPath == WorkingCopy.FullName
                || SvnRepositoryHelper.IsIgnoredItem(arg))
            {
                return;
            }

            if (!SvnRepositoryHelper.IsDirectory(arg))
            {
                RepositoryItem file = workingCopy.GetFileOrCreate(arg.FullPath);
                SetEntityData(file, arg);
            }
            else
            {
                RepositoryItem dir = workingCopy.GetDirectoryOrCreate(arg.FullPath);
                SetEntityData(dir, arg);
            }
        }

        private static void SetEntityData(RepositoryItem repItem, SvnStatusEventArgs arg)
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

        //private static void SetFileData(RepositoryItem file, SvnStatusEventArgs arg)
        //{
        //    if (file == null)
        //    {
        //        return;
        //    }
        //    file.Revision = arg.Revision != -1 ? arg.Revision.ToString() : null;
        //    file.LocalStatus = arg.LocalContentStatus;
        //    file.RemoteStatus = arg.RemoteContentStatus;
        //    file.ChangeList = arg.ChangeList;
        //    file.Location = GetLocation_(file.FullName);
        //    file.IsSwitched = arg.Switched;
        //}

        //private static void SetDirectoryData(RepositoryItem dir, SvnStatusEventArgs le)
        //{
        //    if (dir == null)
        //    {
        //        return;
        //    }

        //    dir.Revision = le.Revision != -1 ? le.Revision.ToString() : null;
        //    dir.LocalStatus = le.LocalContentStatus;
        //    dir.RemoteStatus = le.RemoteContentStatus;
        //    dir.ChangeList = le.ChangeList;
        //    dir.Location = GetLocation_(dir.FullName);
        //    dir.IsSwitched = le.Switched;
        //}

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

        private void OnProcessEnded()
        {
            if (ProcessEnded != null)
            {
                ProcessEnded();
            }
        }

        private void OnIncrement()
        {
            if (Increment != null)
            {
                Increment();
            }
        }
    }
}
