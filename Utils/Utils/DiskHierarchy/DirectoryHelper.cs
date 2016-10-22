using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils.DiskHierarchy
{
    public class DirectoryHelper
    {
        public static IDirectory GetDirectory(IDirectory currentDir, string dirPath)
        {
            string pathWithoutWC = dirPath.Replace(currentDir.FullName, "");
            char[] sep = new char[] { '\\' };
            List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            return GetDirectory(currentDir, dirs);
        }

        public static IDirectory GetDirectoryOrCreate(IDirectory currentDir, string dirPath)
        {
            string pathWithoutWC = dirPath.Replace(currentDir.FullName, "");
            char[] sep = new char[] { '\\' };
            List<string> dirs = new List<string>(pathWithoutWC.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            return CreateDirectory(currentDir, dirs);
        }

        public static void DeleteDirectory(IDirectory currentDir, string dirPath)
        {
            string parentDirPath = Path.GetDirectoryName(dirPath);
            IDirectory dir = GetDirectory(currentDir, parentDirPath);
            if (dir == null)
            {
                return;
            }
            dir.RemoveDirectory(dirPath);
        }

        private static IDirectory CreateDirectory(IDirectory currentDir, List<string> dirs)
        {
            IDirectory res = GetDirectory(currentDir, new List<string>(dirs));
            if (res == null)
            {
                res = CreateDirectoryForce(currentDir, dirs);
            }
            return res;
        }

        private static IDirectory GetDirectory(IDirectory currentDir, List<string> dirs)
        {
            if (dirs.Count == 0)
            {
                return currentDir;
            }

            foreach (IDirectory dir in currentDir.Directories)
            {
                if (dir.Name == dirs[0])
                {
                    dirs.RemoveAt(0);

                    return GetDirectory(dir, dirs);
                }
            }
            return null;
        }

        private static IDirectory CreateDirectoryForce(IDirectory currentDir, List<string> dirs)
        {
            if (dirs.Count == 0)
            {
                return currentDir;
            }

            IDirectory newDir = currentDir.CreateAndAddDirectory(dirs[0]);
            dirs.RemoveAt(0);
            return CreateDirectoryForce(newDir, dirs);
        }


        public static IFile GetFile(IDirectory currentDir, string fileFullName)
        {
            string dirPath = Path.GetDirectoryName(fileFullName);
            IDirectory dir = GetDirectory(currentDir, dirPath);
            if (dir != null)
            {
                return dir.Files.FirstOrDefault(item => item.FullName == fileFullName);
            }
            return null;
        }

        public static IFile GetFileOrCreate(IDirectory currentDir, string fileFullName)
        {
            string dirPath = Path.GetDirectoryName(fileFullName);
            IDirectory dir = GetDirectoryOrCreate(currentDir, dirPath);

            IFile file = dir.Files.FirstOrDefault(item => item.FullName == fileFullName);
            if (file == null)
            {
                file = dir.CreateAndAddFile(fileFullName);
            }
            return file;
        }

        public static IDirectory GetFileDirectory(IDirectory currentDir, string fileFullName)
        {
            string dirPath = Path.GetDirectoryName(fileFullName);
            return GetDirectory(currentDir, dirPath);
        }

        public static void DeleteFile(IDirectory currentDir, string fileFullName)
        {
            string dirPath = Path.GetDirectoryName(fileFullName);
            IDirectory dir = GetDirectory(currentDir, dirPath);
            if (dir == null)
            {
                return;
            }

            dir.RemoveFile(fileFullName);
        }
    }
}
