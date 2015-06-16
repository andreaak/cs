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

        public List<RepositoryDirectory> Directories
        {
            get;
            set;
        }

        public List<RepositoryFile> Files
        {
            get;
            set;
        }


        public RepositoryDirectory(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)
        {
            diskDirectory = new DirectoryItem(fullName, parentId);
            Directories = new List<RepositoryDirectory>();
            Files = new List<RepositoryFile>();
        }

        public void AddDirectory(RepositoryDirectory dir)
        {
            Directories.Add(dir);
        }

        public void AddFile(RepositoryFile file)
        {
            Files.Add(file);
        }

        #region OVERRIDE
        
        protected bool IsIgnoredDirectory(string fullName)
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

        public void RemoveFilesFromChanged(List<string> items)
        {
            foreach (RepositoryDirectory dir in Directories.ToList())
            {
                dir.RemoveFilesFromChanged(items);
                if (IsVersioned(items, dir))
                {
                    CleanData(dir);
                }
                else
                {
                    Directories.Remove(dir);
                }
            }

            foreach (RepositoryFile file in Files.ToList())
	        {
                if (IsVersioned(items, file))
                {
                    CleanData(file);
                }
                else
                {
                    Files.Remove(file);
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
            return diskDirectory.GetFilesExtensions(this);
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
            foreach (RepositoryDirectory dir in Directories)
            {
                if (UTILS.IsIgnoredDirectory(dir.FullName))
                {
                    continue;
                }
                dir.GetChangeLists(changeLists);
            }
            foreach (RepositoryFile file in Files)
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

        IEnumerable<IDirectory> IDirectory.Directories
        {
            get { return Directories; }
        }

        IEnumerable<IFile> IDirectory.Files
        {
            get { return Files; }
        }

        public IDirectory CreateAndAddDirectory(string fullName)
        {
            RepositoryDirectory dir = GetDirectory(fullName);
            if (dir == null)
            {
                dir = CreateDirectory(fullName);
                AddDirectory(dir);
            }
            return dir;
        }

        public void RemoveDirectory(string fullName)
        {
            RepositoryDirectory dir = Directories.FirstOrDefault(item => item.FullName == fullName);
            if (dir != null)
            {
                Directories.Remove(dir);
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
            RepositoryFile file = Files.FirstOrDefault(item => item.FullName == fileFullName);
            if (file != null)
            {
                Files.Remove(file);
            }
        }

        #endregion
    }
}
