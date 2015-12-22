using System.IO;
using System.Windows.Forms;

namespace Utils
{
    public static class SelectFile
    {
        public static bool OpenFile(string title, string initialDirectory, ref string filePath, params string[] extensions)
        {
            //openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            
            OpenFileDialog oFileDlg = new OpenFileDialog();
            if(extensions.Length == 0)
            {
                return false;
            }

            string extension = "";
            foreach (string param in extensions)
            {
                extension += param;
            }
            oFileDlg.Filter = extension;

            if (initialDirectory != "")
            {
                oFileDlg.InitialDirectory = initialDirectory;
            }

            oFileDlg.FilterIndex = 1;
            oFileDlg.RestoreDirectory = true;
            oFileDlg.Title = title;
            if (!string.IsNullOrEmpty(filePath))
            {
                oFileDlg.FileName = Path.GetFileName(filePath);
            }
            if (oFileDlg.ShowDialog() == DialogResult.OK && oFileDlg.FileName.Length > 0)
            {
                filePath = oFileDlg.FileName;
                return true;
            }
            return false;
        }

        public static bool OpenFiles(string title, string initialDirectory, out string[] fileNames, params string[] extensions)
        {
            OpenFileDialog oFileDlg = new OpenFileDialog();
            if (extensions.Length == 0)
            {
                fileNames = null;
                return false;
            }

            string extension = "";
            foreach (string param in extensions)
            {
                extension += param;
            }
            oFileDlg.Filter = extension;

            if (initialDirectory != "")
            {
                oFileDlg.InitialDirectory = initialDirectory;
            }

            oFileDlg.FilterIndex = 1;
            oFileDlg.RestoreDirectory = true;
            oFileDlg.Title = title;
            oFileDlg.Multiselect = true;
            if (oFileDlg.ShowDialog() == DialogResult.OK && oFileDlg.FileNames.Length != 0)
            {
                fileNames = oFileDlg.FileNames;
                return true;
            }
            fileNames = null;
            return false;
        }
        /*
        string[] extensions = new string[]
        {
            "XML Files (*.xml)|*.xml|",
            "All Files (*.*)|*.*"
        };
        string title = "Загрузка файла данных оборудования";
        string filePath = string.Empty;
        if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
        {

        }
        */
        //
        public static bool SaveFile(string title, string initialDirectory, ref string filePath, params string[] params_)
        {
            SaveFileDialog sFileDlg = new SaveFileDialog();
            if(params_.Length == 0)
            {
                return false;
            }

            string extension = "";
            foreach (string param in params_)
            {
                extension += param;
            }
            sFileDlg.Filter = extension;

            if (initialDirectory != "")
            {
                sFileDlg.InitialDirectory = initialDirectory;
            }

            sFileDlg.FilterIndex = 1;
            sFileDlg.RestoreDirectory = true;
            sFileDlg.Title = title;
            sFileDlg.SupportMultiDottedExtensions = true;
            if (sFileDlg.ShowDialog() == DialogResult.OK && sFileDlg.FileName.Length > 0)
            {
                filePath = sFileDlg.FileName;
                return true;
            }
            return false;
        } 
        /*
        string[] extensions = new string[]
        {
         "CSV Files (*.csv)|*.csv|",
         "All Files (*.*)|*.*"
        };
        string title = "Сохранение данных";
        string filePath = string.Empty;
        if (SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
        {

        }
        */
    }
}
