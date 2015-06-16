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
        
        public IList<DirectoryItem> Directories
        {
            get;
            set;
        }

        public List<FileItem> Files
        {
            get;
            set;
        }


        public DirectoryItem(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)
        {
            Directories = new List<DirectoryItem>();
            Files = new List<FileItem>();
        }

        public DirectoryItem(DirectoryInfo directory, long parentId)
            : this(directory.FullName, parentId)
        {
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (dir.Exists && !IsIgnoredDirectory(dir.Name))
                {
                    Directories.Add(new DirectoryItem(dir, Id));
                }
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                Files.Add(new FileItem(file.FullName, Id));
            }
        }

        public void AddDirectoryWithSubItems(DirectoryInfo directoryData)
        {
            AddDirectory(new DirectoryItem(directoryData, Id));
        }

        public void AddDirectory(DirectoryInfo directoryData)
        {
            AddDirectory(new DirectoryItem(directoryData.FullName, Id));
        }

        public void AddDirectory(DirectoryItem dir)
        {
            Directories.Add(dir);
        }

        public void AddFile(FileInfo file)
        {
            Files.Add(new FileItem(file.FullName, Id));
        }

        public void AddFile(FileItem file)
        {
            Files.Add(file);
        }

        #region VIRTUAL

        public DirectoryItem CreateDirectory(string dirName)
        {
            return new DirectoryItem(FullName + "\\" + dirName, Id);
        }

        public FileItem CreateFile(string fileFullName)
        {
            return new FileItem(fileFullName, Id);
        }

        public ISet<string> GetFilesExtensions(IDirectory currentDir)
        {
            ISet<String> filesExtensions = new HashSet<string>();
            GetFilesExtensions(currentDir, filesExtensions);
            return filesExtensions;
        }

        protected virtual bool IsIgnoredDirectory(string dirName)
        {
            return false;
        }

        #endregion

        private void GetFilesExtensions(IDirectory currentDir, ISet<String> filesExtensions)
        {
            foreach (IDirectory dir in currentDir.Directories)
            {
                if (IsIgnoredDirectory(dir.FullName))
                {
                    continue;
                }
                GetFilesExtensions(dir, filesExtensions);
            }

            foreach (IFile file in currentDir.Files)
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
            foreach (FileItem file in Files)
            {
                AddDiskItemRow(file);
            }
            foreach (DirectoryItem dir in Directories)
            {
                dir.ReadToDataset();
            }
        }

        private void AddDiskItemRow(DiskItem item)
        {
            dt.AddEntityRow(item.Id, item.ParentId, item.Name, item.DirectoryName, item.Type);
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
            DirectoryItem dir = CreateDirectory(fullName);
            AddDirectory(dir);
            return dir;
        }

        public void RemoveDirectory(string fullName)
        {
            DirectoryItem dir = Directories.FirstOrDefault(item => item.FullName == fullName);
            if (dir != null)
            {
                Directories.Remove(dir);
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
            FileItem file = Files.FirstOrDefault(item => item.FullName == fileFullName);
            if (file != null)
            {
                Files.Remove(file);
            }
        }

        #endregion
    }
}
