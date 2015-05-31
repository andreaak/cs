using System;
using System.Collections.Generic;
using SharpSvn;
using System.IO;
using System.Linq;
using WorkWithSvn;
using System.Diagnostics;
using System.Windows.Forms;
using WorkWithSvn.DiskHierarchy;

namespace Utils
{
    public class UTILS
    {
        public const string EMPTY = "";

        public static bool IsIgnoredEntity(string fullPath)
        {
            return IsDirectory(fullPath) && !Options.GetInstance.IsDisplayDir 
                    || IsIgnored(fullPath);
        }

        public static bool IsIgnored(string fullPath)
        {
            bool isIgnored;
            if (IsDirectory(fullPath))
            {
                isIgnored = IsIgnoredDirectory(fullPath);
            }
            else
            {
                isIgnored = IsIgnoredFile(fullPath);
            }
            return isIgnored || IsIgnoredDirectoryInPath(fullPath);
        }

        public static bool IsDirectory(string fullPath)
        {
            return new DirectoryInfo(fullPath).Exists;
        }

        public static bool IsIgnoredFile(string fullPath)
        {
            string fileExtension = Path.GetExtension(fullPath);
            return Options.GetInstance.IgnoredFilesExtension.Contains(fileExtension);
        }

        public static bool IsIgnoredDirectory(string fullPath)
        {
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            return Options.GetInstance.IgnoredDirectories.Contains(dir.Name);
        }

        public static bool IsIgnoredDirectoryInPath(string fullPath)
        {
            DirectoryInfo dir = GetDirectory(fullPath);
            List<string> dirs = new List<string>( dir.FullName.Split(new char[] {Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries));
            return dirs.Exists(dirName => Options.GetInstance.IgnoredDirectories.Contains(dirName));
        }

        public static bool IsIgnoredExtension(IEnumerable<string> selectedExtensions, RepositoryItem entity)
        {
            return (selectedExtensions != null 
                && !selectedExtensions.Contains(Path.GetExtension(entity.Name)));
        }

        public static bool IsIgnoredChangeList(string changeListItem, RepositoryItem entity)
        {
            return (!changeListItem.Equals(Constants.ALL_ITEM)
                && (string.IsNullOrEmpty(entity.ChangeList) || !changeListItem.Equals(entity.ChangeList)));
        }
        
        private static DirectoryInfo GetDirectory(string fullPath)
        {
            return IsDirectory(fullPath) ? new DirectoryInfo(fullPath) : (new FileInfo(fullPath)).Directory;
        }

        #region PROCESS

        public static void OpenDirectory(string directory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "explorer";
            process.StartInfo.Arguments = directory;
            process.Start();
        }

        public static void OpenAs(string file)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo("rundll32.exe");
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;
            pi.Arguments = "shell32.dll,OpenAs_RunDLL " + file;
            p.StartInfo = pi;
            p.Start();
        }

        public static void AddFilesToZipArchive(string filePath, string folder)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"zip";
            process.StartInfo.Arguments = string.Format("-r {0} {1}", filePath, folder);
            process.Start();
            while (!process.HasExited)
            {

            }
            Directory.Delete(folder, true);
        }

        public static void OpenFileInNotepad(string fullPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"notepad++.exe";
            process.StartInfo.Arguments = fullPath;
            process.Start();
        }

        public static void OpenLog(string fullPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"TortoiseProc.exe";
            process.StartInfo.Arguments = string.Format("/command:log /path:\"{0}\"", fullPath);
            process.Start();
        }

        public static void OpenDiff(string fullPath, string fileName, string baseFilePath, bool isSvnDiff)
        {
            Process process = new Process();
            if (!isSvnDiff)
            {
                process.StartInfo.FileName = Application.StartupPath + @"\DiffDotNet.exe";
                string baseFile = string.Format("\"{0}\" \"{1}\" /f:gta", baseFilePath, fullPath);
                process.StartInfo.Arguments = baseFile;
            }
            else
            {
                process.StartInfo.FileName = @"TortoiseMerge.exe";
                string baseFile = string.Format("/base:\"{0}\" /readonly", baseFilePath);
                string theirsFile = string.Format("/theirs:\"{0}\" /readonly", baseFilePath);
                string myFile = string.Format("/mine:\"{0}\"", fullPath);
                //string description = string.Format("/basename:\"{0}\" /minename:\"{1}\"", "Base", fileName);
                //string description = string.Format("/theirsname:\"{0}\" /minename:\"{1}\"", "Base", fileName);
                string description = string.Format("/minename:\"{1}\"", "Base", fileName);
                //process.StartInfo.Arguments = baseFile + " " + theirsFile + " " + myFile + " " + description;
                process.StartInfo.Arguments = baseFile + " " + myFile + " " + description;

            }
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Options.ResManager.GetString("ERROR", Options.Culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        public static void BackUpFile(string workingCopyPath, string workingCopyName, string fullPath)
        {
            string path = fullPath.Replace(workingCopyPath, Options.GetInstance.SwitchDir +
                Path.DirectorySeparatorChar + workingCopyName);
            if (File.Exists(fullPath))
            {
                string destDirectory = Path.GetDirectoryName(path);
                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
                File.Copy(fullPath, path, true);
            }
        }

        public static void RestoreFile(string workingCopyPath, string workingCopyName, string fullPath)
        {
            string path = fullPath.Replace(workingCopyPath, Options.GetInstance.SwitchDir
                + Path.DirectorySeparatorChar + workingCopyName);
            if (File.Exists(path))
            {
                File.Copy(path, fullPath, true);
            }
            else
            {
                MessageBox.Show(string.Format(Options.ResManager.GetString("BACKUP_FILE_ERROR", Options.Culture), path), 
                    Options.ResManager.GetString("ERROR", Options.Culture), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
