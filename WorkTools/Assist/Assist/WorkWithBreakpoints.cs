using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using EnvDTE;
using Utils;
using System.IO;

namespace Assist
{
    public class WorkWithBreakpoints
    {
        DTE2 applicationObject = null;

        public WorkWithBreakpoints(DTE2 applicationObject)
        {
            this.applicationObject = applicationObject;
        }
        public void SaveBreakpoints()
        {
            List<string> lstStr = new List<string>();
            foreach (Breakpoint brk in applicationObject.Debugger.Breakpoints)
            {
                lstStr.Add(string.Format("File: {0}\n Function: {1}", brk.File, brk.FunctionName));
            }
            if (lstStr.Count == 0)
            {
                return;
            }
            string error = null;
            SaveDataToFile(null, lstStr, ref error);

        }

        public bool SaveDataToFile(string filePath, List<string> itemsList, ref string error)
        {
            if (itemsList == null)
            {
                return true;
            }

            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "TXT Files (*.txt)|*.txt|",
                "All Files (*.*)|*.*"
                };
                string title = "Сохранение данных";
                if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
                {
                    return true;
                }
            }
            bool ok = new bool();
            StreamWriter sw = null;
            try
            {
                sw = File.CreateText(filePath);
                foreach (string str in itemsList)
                {
                    sw.WriteLine(str);
                }
                ok = true;
            }
            catch (System.Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            return ok;
        }
    }
}
