using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public class DirectoryItem : DiskItem, IDirectory
    {
        public override int Type
        {
            get
            {
                return DiskItem.DIR_TYPE;
            }
        }
        
        public IList<DirectoryItem> DirectoriesList
        {
            get;
            set;
        }

        public List<FileItem> FilesList
        {
            get;
            set;
        }


        public DirectoryItem(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)
        {
            DirectoriesList = new List<DirectoryItem>();
            FilesList = new List<FileItem>();
        }

        public DirectoryItem(DirectoryInfo directory, long parentId)
            : this(directory.FullName, parentId)
        {
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (dir.Exists && !IsIgnoredDirectory(dir.Name))
                {
                    DirectoriesList.Add(new DirectoryItem(dir, Id));
                }
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                FilesList.Add(new FileItem(file.FullName, Id));
            }
        }

        public void AddDirectory(DirectoryInfo directoryData)
        {
            AddDirectory(new DirectoryItem(directoryData, Id));
        }

        public void AddDirectory(DirectoryItem dir)
        {
            DirectoriesList.Add(dir);
        }

        public void AddFile(FileItem fileData)
        {
            FilesList.Add(fileData);
        }

        #region VIRTUAL
        ////
        //public virtual DirectoryItem GetDirectory(string dirPath)
        //{
        //    string pathWithoutWC = dirPath.Replace(FullName, "");
        //    char[] sep = new char[] { '\\' };
        //    List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
        //    return GetDirectory(dirs);
        //}
        ////
        //public virtual DirectoryItem GetDirectoryOrCreate(string dirPath)
        //{
        //    string pathWithoutWC = dirPath.Replace(FullName, "");
        //    char[] sep = new char[] { '\\' };
        //    List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
        //    return CreateDirectory(dirs);
        //}
        ////
        //protected virtual DirectoryItem CreateDirectory(List<string> dirs)
        //{
        //    DirectoryItem res = GetDirectory(dirs);
        //    if(res == null)
        //    {
        //        res = CreateDirectoryForce(dirs);
        //    }
        //    return res;
        //}

        public DirectoryItem CreateDirectory(string dirName)
        {
            return new DirectoryItem(FullName + "\\" + dirName, Id);
        }

        //public void DeleteDirectory(string dirPath)
        //{
        //    string parentDirPath = Path.GetDirectoryName(dirPath);
        //    DirectoryItem dir = GetDirectory(parentDirPath);
        //    if (dir == null)
        //    {
        //        return;
        //    }
        //    DirectoryItem file = dir.DirectoriesList.FirstOrDefault(item => item.FullName == dirPath);
        //    if (file != null)
        //    {
        //        dir.DirectoriesList.Remove(file);
        //    }
        //}

        //public virtual FileItem GetFile(string fileFullName)
        //{
        //    string dirPath = Path.GetDirectoryName(fileFullName);
        //    DirectoryItem dir = GetDirectory(dirPath);
        //    if (dir != null)
        //    {
        //        foreach (FileItem file in dir.FilesList)
        //        {
        //            if (file.FullName == fileFullName)
        //            {
        //                return file;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public virtual FileItem GetFileOrCreate(string fileFullName)
        //{
        //    string dirPath = Path.GetDirectoryName(fileFullName);
        //    DirectoryItem dir = GetDirectoryOrCreate(dirPath);

        //    foreach (FileItem file in dir.FilesList)
        //    {
        //        if (file.FullName == fileFullName)
        //        {
        //            return file;
        //        }
        //    }
            
        //    FileItem fileOut = CreateFile(fileFullName);
        //    dir.AddFile(fileOut);

        //    return fileOut;
        //}

        //public virtual DirectoryItem GetFileDirectory(string fileFullName)
        //{
        //    string dirPath = Path.GetDirectoryName(fileFullName);
        //    return GetDirectory(dirPath);
        //}

        public FileItem CreateFile(string fileFullName)
        {
            return new FileItem(fileFullName, Id);
        }

        //public void DeleteFile(string fileFullName)
        //{
        //    string dirPath = Path.GetDirectoryName(fileFullName);
        //    DirectoryItem dir = GetDirectory(dirPath);
        //    if (dir == null)
        //    {
        //        return;
        //    }

        //    FileItem file = dir.FilesList.FirstOrDefault(item => item.FullName == fileFullName);
        //    if (file != null)
        //    {
        //        dir.FilesList.Remove(file);
        //    }
        //}

        public ISet<string> GetFilesExtensions()
        {
            ISet<String> filesExtensions = new HashSet<string>();
            GetFilesExtensions(filesExtensions);
            return filesExtensions;
        }

        protected virtual bool IsIgnoredDirectory(string dirName)
        {
            return false;
        }

        #endregion
        //
        //private DirectoryItem GetDirectory(List<string> dirs)
        //{
        //    if (dirs.Count == 0)
        //    {
        //        return this;
        //    }

        //    foreach (DirectoryItem dir in DirectoriesList)
        //    {
        //        if (dir.Name == dirs[0])
        //        {
        //            dirs.RemoveAt(0);
        //            return dir.GetDirectory(dirs);
        //        }
        //    }
        //    return null;
        //}
        ////
        //private DirectoryItem CreateDirectoryForce(List<string> dirs)
        //{
        //    if (dirs.Count == 0)
        //    {
        //        return this;
        //    } 
        //    DirectoryItem newDir = CreateDirectory(dirs[0]);
        //    DirectoriesList.Add(newDir);
        //    dirs.RemoveAt(0);
        //    return newDir.CreateDirectoryForce(dirs);
        //}

        private void GetFilesExtensions(ISet<String> filesExtensions)
        {
            foreach (DirectoryItem dir in DirectoriesList)
            {
                if (IsIgnoredDirectory(dir.FullName))
                {
                    continue;
                }
                dir.GetFilesExtensions(filesExtensions);
            }

            foreach (FileItem file in FilesList)
            {
                if (string.IsNullOrEmpty(file.Name))
                {
                    continue;
                }
                string extension = Path.GetExtension(file.Name);
                if (string.IsNullOrEmpty(extension) || filesExtensions.Contains(extension))
                {
                    continue;
                }
                filesExtensions.Add(extension);
            }
        }


        static FileSystemDataSet.EntityDataTable dt;
        public FileSystemDataSet StartReadToDataset()
        {
            dt = new FileSystemDataSet.EntityDataTable();
            FileSystemDataSet ds = new FileSystemDataSet();
            dt = ds.Entity;
            ReadToDataset();
            return ds;
        }

        private void ReadToDataset()
        {
            AddDiskItemRow(this);
            foreach (FileItem file in FilesList)
            {
                AddDiskItemRow(file);
            }
            foreach (DirectoryItem dir in DirectoriesList)
            {
                dir.ReadToDataset();
            }
        }

        private void AddDiskItemRow(DiskItem item)
        {
            dt.AddEntityRow(item.Id, item.ParentId, item.Name, item.DirectoryName, item.Type);
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
            DirectoryItem dir = CreateDirectory(fullName);
            AddDirectory(dir);
            return dir;
        }

        public void RemoveDirectory(string fullName)
        {
            DirectoryItem dir = DirectoriesList.FirstOrDefault(item => item.FullName == fullName);
            if (dir != null)
            {
                DirectoriesList.Remove(dir);
            }
        }

        public IFile CreateAndAddFile(string fileFullName)
        {
            FileItem file = CreateFile(fileFullName);
            AddFile(file);
            return file;
        }

        public void RemoveFile(string fileFullName)
        {
            FileItem file = FilesList.FirstOrDefault(item => item.FullName == fileFullName);
            if (file != null)
            {
                FilesList.Remove(file);
            }
        }

        #endregion
    }
}
