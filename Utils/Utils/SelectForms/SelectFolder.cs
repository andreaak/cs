using System.Windows.Forms;

namespace Utils
{
    public static class SelectFolder
    {
        public static bool Select(string description, ref string folderName)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = description;
            folderDialog.ShowNewFolderButton = false;
            if (folderName != null && folderName != "")
            {
                folderDialog.SelectedPath = folderName;
            }
            if (folderDialog.ShowDialog() == DialogResult.OK && folderDialog.SelectedPath.Length > 0)
            {
                folderName = folderDialog.SelectedPath;
                return true;
            }
            return false;
        }

        /*
        string folder = string.Empty;
        string description = "Choose tables directory...";
        if(!SelectFolder.Select(description, ref folder))
        {
            return;
        }
        */
    }
}
