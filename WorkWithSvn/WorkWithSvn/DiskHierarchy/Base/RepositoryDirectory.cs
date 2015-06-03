using System;
using System.Collections.Generic;
using System.IO;
using Utils;
using System.Linq;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy.Base
{

    [Serializable]
    public class RepositoryDirectory : RepositoryItem, IDirectory
    {
        private DirectoryItem diskDirectory;

        public List<RepositoryDirectory> DirectoriesList
        {
            get;
            set;
        }

        public List<RepositoryFile> FilesList
        {
            get;
            set;
        }


        public RepositoryDirectory(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)
        {
            diskDirectory = new DirectoryItem(fullName, parentId);
            DirectoriesList = new List<RepositoryDirectory>();
            FilesList = new List<RepositoryFile>();
        }

        public void AddDirectory(RepositoryDirectory dir)
        {
            DirectoriesList.Add(dir);
        }

        public void AddFile(RepositoryFile fileData)
        {
            FilesList.Add(fileData);
        }

        #region OVERRIDE
        
        protected bool IsIgnoredDirectories(string fullName)
        {
            return UTILS.IsIgnoredDirectory(fullName);
        }

        public RepositoryFile GetFile(string filePath)
        {
            return (RepositoryFile)DirectoryHelper.GetFile(this, filePath);
        }

        public RepositoryFile GetFileOrCreate(string filePath)
        {
            return (RepositoryFile)DirectoryHelper.GetFileOrCreate(this, filePath);
        }

        public RepositoryDirectory GetDirectoryOrCreate(string dirPath)
        {
            return (RepositoryDirectory)DirectoryHelper.GetDirectoryOrCreate(this, dirPath);
        }

        public RepositoryDirectory GetDirectory(string dirPath)
        {
            return (RepositoryDirectory)DirectoryHelper.GetDirectory(this, dirPath);
        }

        public RepositoryDirectory GetFileDirectory(string fileFullName)
        {
            return (RepositoryDirectory)DirectoryHelper.GetFileDirectory(this, fileFullName);
        }


        protected virtual RepositoryDirectory CreateDirectory(string dirName)
        {
            return new RepositoryDirectory(FullName + "\\" + dirName, -1);
        }

        protected virtual RepositoryFile CreateFile(string filePath)
        {
            return new RepositoryFile(filePath, -1);
        }

        public virtual void RemoveFilesFromChanged(List<string> files)
        {
            foreach (RepositoryDirectory dir in DirectoriesList)
            {
                if (IsVersioned(files, dir))
                {
                    CleanData(dir);
                }
                dir.RemoveFilesFromChanged(files);
            }

            foreach (RepositoryFile file in FilesList)
	        {
                if (IsVersioned(files, file))
                {
                    CleanData(file);
                }
	        }
        }

        protected virtual void CleanData(RepositoryItem repoData)
        {
            repoData.Revision = null;
        }

        protected static bool IsVersioned(List<string> files, RepositoryItem repoData)
        {
            return !files.Contains(repoData.FullName);
        }

        public ISet<string> GetFilesExtensions()
        {
            return diskDirectory.GetFilesExtensions();
        }

        #endregion

        public ISet<String> GetChangeList()
        {
            ISet<String> changeLists = new HashSet<string>();
            GetChangeLists(changeLists);
            return changeLists;
        }

        public void GetChangeLists(ISet<String> changeLists)
        {
            foreach (RepositoryDirectory dir in DirectoriesList)
            {
                if (UTILS.IsIgnoredDirectory(dir.FullName))
                {
                    continue;
                }
                dir.GetChangeLists(changeLists);
            }
            foreach (RepositoryFile file in FilesList)
            {
                if (string.IsNullOrEmpty(file.ChangeList) || changeLists.Contains(file.ChangeList))
                {
                    continue;
                }
                changeLists.Add(file.ChangeList);
            }
        }

        public virtual void DeleteItem(RepositoryItem item)
        {
            if (UTILS.IsIgnoredEntity(item.FullName))
            {
                return;
            }
            DirectoryHelper.DeleteFile(this, item.FullName);
            DirectoryHelper.DeleteDirectory(this, item.FullName);
        }

        #region IDirectory

        IEnumerable<IDirectory> IDirectory.DirectoriesList
        {
            get { return DirectoriesList; }
        }

        IEnumerable<IFile> IDirectory.FilesList
        {
            get { return FilesList; }
        }

        public IDirectory CreateAndAddDirectory(string fullName)
        {
            RepositoryDirectory dir = CreateDirectory(fullName);
            AddDirectory(dir);
            return dir;
        }

        public void RemoveDirectory(string fullName)
        {
            RepositoryDirectory dir = DirectoriesList.FirstOrDefault(item => item.FullName == fullName);
            if (dir != null)
            {
                DirectoriesList.Remove(dir);
            }
        }

        public IFile CreateAndAddFile(string fileFullName)
        {
            RepositoryFile file = CreateFile(fileFullName);
            AddFile(file);
            return file;
        }

        public void RemoveFile(string fileFullName)
        {
            RepositoryFile file = FilesList.FirstOrDefault(item => item.FullName == fileFullName);
            if (file != null)
            {
                FilesList.Remove(file);
            }
        }

        #endregion
    }
}
