using System;
using System.Collections.Generic;
using System.IO;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public class DirectoryData
    {
        public EntityData Data
        {
            get;
            private set;
        }
        
        static FileSystem.EntityDataTable dt;
        private List<DirectoryData> directoriesList;
        public List<DirectoryData> DirectoriesList
        {
            get { return directoriesList; }
            set { directoriesList = value; }
        }

        private List<FileData> filesList;
        public List<FileData> FilesList
        {
            get { return filesList; }
            set { filesList = value; }
        }

        private static List<String> filesExtensions = null;

        public DirectoryData(string fullPath, long parentId)
        {
            directoriesList = new List<DirectoryData>();
            filesList = new List<FileData>();
            Data = new EntityData(fullPath, Path.GetFileName(fullPath), Path.GetDirectoryName(fullPath),
            parentId);
        }

        public DirectoryData(DirectoryInfo directory, long parentId)
            : this(directory.FullName, parentId)
        {
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (dir.Exists && !IsIgnoredDirectories(dir.Name))
                {
                    directoriesList.Add(new DirectoryData(dir, Data.Id));
                }
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                filesList.Add(new FileData(file.FullName, Data.Id));
            }
        }

        public void AddDirectory(DirectoryInfo directoryData)
        {
            AddDirectory(new DirectoryData(directoryData, Data.Id));
        }

        public void AddDirectory(DirectoryData dir)
        {
            directoriesList.Add(dir);
        }

        public void AddFile(FileData fileData)
        {
            filesList.Add(fileData);
        }

        #region VIRTUAL
        
        protected virtual bool IsIgnoredDirectories(string dirName)
        {
            return false;
        }

        public virtual FileData GetFileOrCreate(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            DirectoryData dir = GetDirectoryOrCreate(dirPath);

            foreach (FileData file in dir.filesList)
            {
                if (file.Data.FullPath.Equals(filePath))
                {
                    return file;
                }
            }
            
            FileData fileOut = GetNewFile(filePath);
            dir.AddFile(fileOut);

            return fileOut;
        }

        public virtual FileData GetFile(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            DirectoryData dir = GetDirectory(dirPath);
            if (dir == null)
            {
                return null;
            }

            foreach (FileData file in dir.filesList)
            {
                if (file.Data.FullPath.Equals(filePath))
                {
                    return file;
                }
            }

            return null;
        }

        public virtual DirectoryData GetDirectory(string dirPath)
        {
            string pathWithoutWC = dirPath.Replace(Data.FullPath, "");
            char[] sep = new char[] { '\\' };
            List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            return GetDirectory(dirs);
        }

        public virtual DirectoryData GetDirectoryOrCreate(string dirPath)
        {
            string pathWithoutWC = dirPath.Replace(Data.FullPath, "");
            char[] sep = new char[] { '\\' };
            List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            return CreateDirs(dirs);
        }

        protected virtual DirectoryData CreateDirs(List<string> dirs)
        {
            if (dirs.Count == 0)
            {
                return this;
            }

            foreach (DirectoryData dir in directoriesList)
            {
                if (dir.Data.Name.Equals(dirs[0]))
                {
                    dirs.RemoveAt(0);
                    if (dirs.Count == 0)
                    {
                        return dir;
                    }
                    return dir.CreateDirs(dirs);
                }
            }

            DirectoryData newDir = GetNewDirectory(dirs[0]);
            directoriesList.Add(newDir);
            dirs.RemoveAt(0);
            if (dirs.Count == 0)
            {
                return newDir;
            }
            return newDir.CreateDirs(dirs);
        }

        public virtual DirectoryData GetNewDirectory(string dirName)
        {
            return new DirectoryData(Data.FullPath + "\\" + dirName, Data.Id);
        }

        public virtual FileData GetNewFile(string filePath)
        {
            return new FileData(filePath, Data.Id);
        }

        public virtual DirectoryData GetFileDirectory(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            return GetDirectory(dirPath);
        }

        #endregion

        private DirectoryData GetDirectory(List<string> dirs)
        {
            if (dirs.Count == 0)
            {
                return this;
            }

            foreach (DirectoryData dir in directoriesList)
            {
                if (dir.Data.Name.Equals(dirs[0]))
                {
                    dirs.RemoveAt(0);
                    if (dirs.Count == 0)
                    {
                        return dir;
                    }
                    return dir.GetDirectory(dirs);
                }
            }
            return null;
        }

        public void DeleteFile(string fullPath)
        {
            string dirPath = Path.GetDirectoryName(fullPath);
            DirectoryData dir = GetDirectory(dirPath);
            if (dir == null)
            {
                return;
            }
            for (int i = 0; i < dir.filesList.Count; i++)
            {
                FileData file = dir.filesList[i];
                if (file.Data.FullPath == fullPath)
                {
                    dir.filesList.Remove(file);
                    break;
                }
            }
        }

        public void DeleteDirectory(string fullPath)
        {
            string dirPath = Path.GetDirectoryName(fullPath);
            DirectoryData dir = GetDirectory(dirPath);
            if (dir == null)
            {
                return;
            }
            for (int i = 0; i < dir.directoriesList.Count; i++)
            {
                DirectoryData subDir = dir.DirectoriesList[i];
                if (subDir.Data.FullPath == fullPath)
                {
                    dir.directoriesList.Remove(subDir);
                    break;
                }
            }
        }

        public List<string> GetFilesExtensions()
        {
            filesExtensions = new List<string>();
            GetFilesExtensions2();
            return filesExtensions;
        }

        private void GetFilesExtensions2()
        {
            foreach (DirectoryData dir in directoriesList)
            {
                if (IsIgnoredDirectories(dir.Data.FullPath))
                {
                    continue;
                }
                dir.GetFilesExtensions2();
            }
            foreach (FileData file in filesList)
            {
                if (string.IsNullOrEmpty(file.Data.Name))
                {
                    continue;
                }
                string extension = Path.GetExtension(file.Data.Name);
                if (string.IsNullOrEmpty(extension) || filesExtensions.Contains(extension))
                {
                    continue;
                }
                filesExtensions.Add(extension);
            }
        }

        public FileSystem StartReadToDataset()
        {
            dt = new FileSystem.EntityDataTable();
            FileSystem ds = new FileSystem();
            dt = ds.Entity;
            ReadToDataset();
            return ds;
        }

        private void ReadToDataset()
        {
            AddDirectoryToDataset(this);
            foreach (FileData file in filesList)
            {
                AddFileToDataset(file);
            }
            foreach (DirectoryData dir in directoriesList)
            {
                dir.ReadToDataset();
            }
        }

        private void AddFileToDataset(FileData file)
        {
            dt.AddEntityRow(file.Data.Id, file.Data.ParentId, file.Data.Name, file.Data.Path_, EntityData.FILE_TYPE);
        }

        private void AddDirectoryToDataset(DirectoryData dir)
        {
            dt.AddEntityRow(dir.Data.Id, dir.Data.ParentId, dir.Data.Name, dir.Data.Path_, EntityData.DIR_TYPE);
        }
    }
}
