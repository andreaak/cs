using System;
using System.Collections.Generic;
using System.IO;
using SharpSvn;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class SvnDirectoryData : RepoDirectoryData
    {
        public SvnEntityData SvnData
        {
            get;
            private set;
        }
        
        public SvnDirectoryData(DirectoryInfo directoryData)
        : base(directoryData.FullName)
        {
            SvnData = new SvnEntityData(RepoData);
            foreach (DirectoryInfo dir in directoryData.GetDirectories())
            {
                if (dir.Exists && !IsIgnoredDirectories(dir.FullName))
                {
                    DirectoriesList.Add(new SvnDirectoryData(dir));
                }
            }
        }

        public SvnDirectoryData(string fullDirectoryName)
            : base(fullDirectoryName)
        {
            SvnData = new SvnEntityData(RepoData);
        }

        #region OVERRIDE

        private static SvnDirectoryData GetDirectory(DirectoryData dir)
        {
            if (dir == null)
            {
                return null;
            }
            return dir as SvnDirectoryData;
        }

        public new SvnFileData GetFile(string filePath)
        {
            return SvnFileData.GetFile(base.GetFile(filePath));
        }

        public new SvnFileData GetFileOrCreate(string filePath)
        {
            return SvnFileData.GetFile(base.GetFileOrCreate(filePath));
        }

        public new SvnDirectoryData GetDirectoryOrCreate(string path)
        {
            return SvnDirectoryData.GetDirectory(base.GetDirectoryOrCreate(path));
        }

        public new SvnDirectoryData GetDirectory(string path)
        {
            return SvnDirectoryData.GetDirectory(base.GetDirectory(path));
        }

        public override DirectoryData GetNewDirectory(string dirName)
        {
            return new SvnDirectoryData(Data.FullPath + "\\" + dirName);
        }

        public override FileData GetNewFile(string filePath)
        {
            return new SvnFileData(filePath);
        }

        public new SvnDirectoryData GetFileDirectory(string filePath)
        {
            return GetDirectory(base.GetFileDirectory(filePath));
        }

        #endregion
        
        public override void RemoveFilesFromChanged(List<string> files)
        {
            foreach (SvnDirectoryData dir in DirectoriesList)
            {
                if (IsVersioned(files, dir.SvnData))
                {
                    CleanData(dir.SvnData);
                    dir.SvnData.LocalContentStatus = SvnStatus.Zero;
                }
                dir.RemoveFilesFromChanged(files);
            }

            foreach (SvnFileData file in FilesList)
	        {
                if (IsVersioned(files, file.SvnData))
                {
                    CleanData(file.SvnData);
                    file.SvnData.LocalContentStatus = SvnStatus.Zero;
                }
	        }
        }

        public override RepoEntityData GetData()
        {
            return SvnData;
        }
    }
}
