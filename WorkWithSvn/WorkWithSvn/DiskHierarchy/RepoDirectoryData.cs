using System;
using System.Collections.Generic;
using Utils;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class RepoDirectoryData : DirectoryData
    {
        public RepoEntityData RepoData
        {
            get;
            private set;
        }
        
        private static List<String> changeLists;

        public RepoDirectoryData(string fullDirectoryName)
            : base(fullDirectoryName, -1)
        {
            RepoData = new RepoEntityData(Data);
        }

        #region OVERRIDE
        
        protected override bool IsIgnoredDirectories(string fullName)
        {
            return UTILS.IsIgnoredDirectory(fullName);
        }

        private static RepoDirectoryData GetDirectory(DirectoryData dir)
        {
            if (dir == null)
            {
                return null;
            }
            return dir as RepoDirectoryData;
        }

        public new RepoFileData GetFile(string filePath)
        {
            return RepoFileData.GetFile(base.GetFile(filePath));
        }

        public new RepoFileData GetFileOrCreate(string filePath)
        {
            return RepoFileData.GetFile(base.GetFileOrCreate(filePath));
        }

        public new RepoDirectoryData GetDirectoryOrCreate(string path)
        {
            return RepoDirectoryData.GetDirectory(base.GetDirectoryOrCreate(path));
        }

        public new RepoDirectoryData GetDirectory(string path)
        {
            return RepoDirectoryData.GetDirectory(base.GetDirectory(path));
        }

        public override DirectoryData GetNewDirectory(string dirName)
        {
            return new RepoDirectoryData(Data.FullPath + "\\" + dirName);
        }

        public override FileData GetNewFile(string filePath)
        {
            return new RepoFileData(filePath);
        }

        public new RepoDirectoryData GetFileDirectory(string filePath)
        {
            return GetDirectory(base.GetFileDirectory(filePath));
        }
        
        public virtual void RemoveFilesFromChanged(List<string> files)
        {
            foreach (RepoDirectoryData dir in DirectoriesList)
            {
                if (IsVersioned(files, dir.RepoData))
                {
                    CleanData(dir.RepoData);
                }
                dir.RemoveFilesFromChanged(files);
            }

            foreach (RepoFileData file in FilesList)
	        {
                if (IsVersioned(files, file.RepoData))
                {
                    CleanData(file.RepoData);
                }
	        }
        }

        protected static void CleanData(RepoEntityData repoData)
        {
            repoData.Revision = null;
        }

        protected static bool IsVersioned(List<string> files, RepoEntityData repoData)
        {
            return !files.Contains(repoData.Data.FullPath);
        }

        #endregion

        public List<string> GetChangeLists()
        {
            changeLists = new List<string>();
            GetChangeLists2();
            return changeLists;
        }

        public void GetChangeLists2()
        {
            foreach (DirectoryData dir in DirectoriesList)
            {
                if (UTILS.IsIgnoredDirectory(dir.Data.FullPath))
                {
                    continue;
                }
                RepoDirectoryData dir_ = dir as RepoDirectoryData;
                dir_.GetChangeLists2();
            }
            foreach (FileData file_ in FilesList)
            {
                RepoEntityData data = (file_ as RepoFileData).GetData();
                if (string.IsNullOrEmpty(data.ChangeList) || changeLists.Contains(data.ChangeList))
                {
                    continue;
                }
                changeLists.Add(data.ChangeList);
            }
        }

        public virtual RepoEntityData GetData()
        {
            return RepoData;
        }

        public virtual void DeleteEntity(RepoEntityData entity)
        {
            if (UTILS.IsIgnoredEntity(entity.Data.FullPath))
            {
                return;
            }
            DeleteFile(entity.Data.FullPath);
            DeleteDirectory(entity.Data.FullPath);
        }
    }
}
