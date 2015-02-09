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
        private SvnDirectoryData workingCopy;
        private delegate void InvokeMethod(ControlsData ctrlData);
        Collection<SvnStatusEventArgs> list;
        public event Action ProcessEnded;
        public event Action Increment;

        public RepoDirectoryData WorkingCopy
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
            workingCopy = new SvnDirectoryData(dir);
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
                ListView.Fill(workingCopy.GetDirectory(Tree.SelNode.FullPath) as SvnDirectoryData,
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

                    foreach (SvnStatusEventArgs entityArgs in list)
                    {
                        if (!UTILS.IsIgnoredEntity(entityArgs))
                        {
                            SetEntityData(entityArgs);
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
            foreach (SvnEntityData entity in GetSelectedItems())
            {
                SetEntityData(entity);
                OnIncrement();
            }
            OnProcessEnded();
        }

        public bool IsNotVersioned(RepoEntityData entity)
        {
            return (entity as SvnEntityData).RemoteContentStatus == SvnStatus.NotVersioned;
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

        public bool IsDeletedEntity(RepoEntityData entity)
        {
            SvnEntityData svnEntity = entity as SvnEntityData;
            return svnEntity.LocalContentStatus != SvnStatus.Deleted
                    && svnEntity.RemoteContentStatus != SvnStatus.Deleted;
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
                foreach (SvnEntityData entity in GetSelectedItems())
                {
                    client.Resolved(entity.Data.FullPath);
                    SetEntityData(entity);
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
                    foreach (SvnEntityData entity in GetSelectedItems())
                    {

                        if (backup)
                        {
                            UTILS.BackUpFile(WorkingCopy.Data.FullPath,
                                WorkingCopy.Data.Name, entity.Data.FullPath);
                        }

                        ok = client.Switch(entity.Data.FullPath, GetTargetLocationUri(entity.Data.FullPath, targetLocation));

                        if (restore)
                        {
                            UTILS.RestoreFile(WorkingCopy.Data.FullPath,
                                WorkingCopy.Data.Name, entity.Data.FullPath);
                        }

                        SetEntityData(entity);
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
                    List<RepoEntityData> selCollection = GetSelectedItems();
                    List<string> pathes = selCollection.Select(item => item.Data.FullPath).ToList();
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
                        SvnFileData file = workingCopy.GetFile(path);
                        file.SvnData.LocalContentStatus = SvnStatus.Normal;
                        file.SvnData.RemoteContentStatus = SvnStatus.None;
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
                    foreach (SvnEntityData entity in GetSelectedItems())
                    {
                        client.Revert(entity.Data.FullPath);
                        SetEntityData(entity);
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
                foreach (SvnEntityData entity in GetSelectedItems())
                {
                    SvnTarget from = SvnTarget.FromString(entity.Data.FullPath);
                    SvnTarget to = SvnTarget.FromUri(client.GetUriFromWorkingCopy(entity.Data.FullPath));
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
                foreach (SvnEntityData entity in GetSelectedItems())
                {
                    if (string.IsNullOrEmpty(entity.ChangeList))
                    {
                        string changeListName = changeList;
                        client.AddToChangeList(entity.Data.FullPath, changeListName);
                        entity.ChangeList = changeListName;
                    }
                }
            }
            return ret;
        }

        public void RemoveFromChangeList()
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (SvnEntityData entity in GetSelectedItems())
                {
                    client.RemoveFromChangeList(entity.Data.FullPath);
                    entity.ChangeList = null;
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

        public RepoEntityData GetEntity(string fullPath)
        {
            if (fullPath == WorkingCopy.Data.FullPath
                || UTILS.IsIgnoredEntity(fullPath))
            {
                return null;
            }

            if (!UTILS.IsDirectory(fullPath))
            {
                SvnFileData file = workingCopy.GetFile(fullPath);
                if (file != null)
                {
                    return file.SvnData;
                }
            }
            else
            {
                SvnDirectoryData dir = workingCopy.GetDirectory(fullPath);
                if (dir != null)
                {
                    return dir.SvnData;
                }
            }
            return null;
        }

        public RepoEntityData GetDeletedEntity(string fullPath)
        {
            if (fullPath == WorkingCopy.Data.FullPath
                || UTILS.IsIgnoredEntity(fullPath))
            {
                return null;
            }
            SvnFileData file = workingCopy.GetFile(fullPath);
            if (file != null)
            {
                return file.SvnData;
            }
            SvnDirectoryData dir = workingCopy.GetDirectory(fullPath);
            if (dir != null)
            {
                return dir.SvnData;
            }
            return null;
        }

        public bool IsUnchanged(RepoEntityData entity)
        {
            SvnEntityData svnEntity = entity as SvnEntityData;
            return svnEntity.IsUnchanged;
        }

        public List<RepoEntityData> GetSelectedItems()
        {
            List<string> filesPath = ListView.GetSelectedItemsPath();
            List<RepoEntityData> list = new List<RepoEntityData>();
            foreach (String path in filesPath)
            {
                if (UTILS.IsDirectory(path))
                {
                    RepoDirectoryData dir = WorkingCopy.GetDirectory(path);
                    if (dir != null)
                    {
                        list.Add(dir.GetData());
                    }
                }
                else
                {
                    RepoFileData file = WorkingCopy.GetFile(path);
                    if (file != null)
                    {
                        list.Add(file.GetData());
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
                    List<RepoEntityData> selCollection = GetSelectedItems();

                    List<string> pathes = selCollection.Select(item => item.Data.FullPath).ToList();
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
                    List<RepoEntityData> selCollection = GetSelectedItems();
                    List<string> pathes = selCollection.Select(item => item.Data.FullPath).ToList();
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
                if (UTILS.IsIgnoredEntity(file))
                {
                    continue;
                }
                files.Add(file.FullPath);
            }
            WorkingCopy.RemoveFilesFromChanged(files);
        }

        private void SetEntitysData(List<RepoEntityData> selCollection)
        {
            foreach (SvnEntityData entity in selCollection)
            {
                SetEntityData(entity);
            }
        }

        private void SetEntityData(SvnEntityData entity)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnStatusArgs args = new SvnStatusArgs();
                args.RetrieveRemoteStatus = true;
                args.RetrieveAllEntries = true;
                client.GetStatus(entity.Data.FullPath, args, out list);

                if (list.Count > 0)
                {
                    SetEntityData(entity, list[0]);
                }
                else
                {
                    entity.LocalContentStatus = SvnStatus.Normal;
                    entity.RemoteContentStatus = SvnStatus.None;
                    entity.IsSwitched = false;
                }
            }
        }

        private void SetEntityData(SvnStatusEventArgs entityArgs)
        {
            if (entityArgs.FullPath == WorkingCopy.Data.FullPath
                || UTILS.IsIgnoredEntity(entityArgs))
            {
                return;
            }

            if (!UTILS.IsDirectory(entityArgs))
            {
                SvnFileData file = workingCopy.GetFileOrCreate(entityArgs.FullPath);
                SetFileData(file, entityArgs);
            }
            else
            {
                SvnDirectoryData dir = workingCopy.GetDirectoryOrCreate(entityArgs.FullPath);
                SetDirectoryData(dir, entityArgs);
            }
        }

        private static void SetEntityData(SvnEntityData entity, SvnStatusEventArgs entityArgs)
        {
            if (entity == null)
            {
                return;
            }
            entity.Revision = entityArgs.Revision != -1 ? entityArgs.Revision.ToString() : null;
            entity.LocalContentStatus = entityArgs.LocalContentStatus;
            entity.RemoteContentStatus = entityArgs.RemoteContentStatus;
            entity.ChangeList = entityArgs.ChangeList;
            entity.Location = GetLocation_(entity.Data.FullPath);
            entity.IsSwitched = entityArgs.Switched;
        }

        private static void SetFileData(SvnFileData file, SvnStatusEventArgs le)
        {
            if (file == null)
            {
                return;
            }
            file.SvnData.Revision = le.Revision != -1 ? le.Revision.ToString() : null;
            file.SvnData.LocalContentStatus = le.LocalContentStatus;
            file.SvnData.RemoteContentStatus = le.RemoteContentStatus;
            file.SvnData.ChangeList = le.ChangeList;
            file.SvnData.Location = GetLocation_(file.Data.FullPath);
            file.SvnData.IsSwitched = le.Switched;
        }

        private static void SetDirectoryData(SvnDirectoryData dir, SvnStatusEventArgs le)
        {
            if (dir == null)
            {
                return;
            }

            dir.SvnData.Revision = le.Revision != -1 ? le.Revision.ToString() : null;
            dir.SvnData.LocalContentStatus = le.LocalContentStatus;
            dir.SvnData.RemoteContentStatus = le.RemoteContentStatus;
            dir.SvnData.ChangeList = le.ChangeList;
            dir.SvnData.Location = GetLocation_(dir.Data.FullPath);
            dir.SvnData.IsSwitched = le.Switched;
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
                    outStr.Append(temp.Replace(WorkingCopy.Data.FullPath.Replace(@"\", "/") + "/", "") + "\n");
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
                .Replace(WorkingCopy.Data.FullPath.Replace(@"\", "/") + "/", "")
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
