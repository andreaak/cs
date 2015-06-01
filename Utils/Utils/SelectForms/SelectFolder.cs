using System.Windows.Forms;

namespace Utils
{
    public static class SelectFolder
    {
        private static string selectedPath;

        public static bool Select(string description, ref string folderName)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = description;
                dlg.SelectedPath = string.IsNullOrEmpty(folderName) ? selectedPath : folderName;
                dlg.ShowNewFolderButton = false;
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK
                    && dlg.SelectedPath.Length > 0)
                {
                    folderName = selectedPath = dlg.SelectedPath;
                    return true;
                }
                return false;
            }
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
