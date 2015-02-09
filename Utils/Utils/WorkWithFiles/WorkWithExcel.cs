
using System;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

#if OFFICE
using Excel = Microsoft.Office.Interop.Excel;
#endif


namespace Utils
{
    public static class WorkWithExcel
    {
        #if OFFICE
        static Excel.Application ExcellApplication; 
        static Excel.Workbook CurrentWorkbook;
        static Excel.Worksheet CurrentSheet;
        static bool CloseExcel;
        //
        static ThreadParams threadParam = null;
        public static ThreadParams ThreadParam
        {
            get { return WorkWithExcel.threadParam; }
            set { WorkWithExcel.threadParam = value; }
        }
        //
        public static int WorkbookSheetCount
        {
            get
            {
                if (CurrentWorkbook != null)
                {
                    return CurrentWorkbook.Sheets.Count;
                }
                else
                {
                    return 0;
                }
            }
        } 
        //
        public static int RowsCount
        {
            get
            {
                if (CurrentSheet != null)
                {
                    return CurrentSheet.UsedRange.Row + CurrentSheet.UsedRange.Rows.Count - 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        //
        public static int ColumnsCount
        {
            get
            {
                if (CurrentSheet != null)
                {
                    return CurrentSheet.UsedRange.Column + CurrentSheet.UsedRange.Columns.Count - 1;
                }
                else
                {
                    return 0;
                }
            }
        }        
        //
        public static string CurrentSheetName
        {
            get
            {
                if (CurrentSheet != null)
                {
                    return CurrentSheet.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set 
            {
                if (CurrentSheet != null && value != null && value != string.Empty)
                {
                    CurrentSheet.Name = value;
                }
            
            }
        }       
        //
        public static bool OpenApplication()
        {
            try
            {
                ExcellApplication = (Excel.Application)Marshal.GetActiveObject("Excel.Application");
            }
            catch
            {
                ExcellApplication = new Excel.ApplicationClass();
                CloseExcel = true;
            }
            return true;
        } 
        //
        public static bool CloseApplication()
        {
            CloseWorkBook(false);
            if (CloseExcel)
            {
                ExcellApplication.Quit();
            }
            ExcellApplication = null;
            return true;
        }         
        //
        public static bool OpenWorkBook(string filePath)
        {
            CloseWorkBook(false);
            if (filePath == string.Empty || !File.Exists(filePath))
            {
                CurrentWorkbook = null;
                return false;
            }
            OpenApplication();
            try
            {
                //Открытие рабочей книги
                CurrentWorkbook = ExcellApplication.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);//Excel.XlWBATemplate.xlWBATWorksheet);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        //
        public static bool CreateWorkBook(int sheetsCount)
        {
            CloseWorkBook(false);
            
            OpenApplication();
            try
            {
                CurrentWorkbook = ExcellApplication.Workbooks.Add(Type.Missing);


                int sheetsToInsert = sheetsCount - CurrentWorkbook.Sheets.Count;


                
                if (sheetsToInsert < 0)
                {
                    CurrentWorkbook.Sheets.Delete();
                    sheetsToInsert = sheetsCount;
                }

                if(sheetsToInsert > 0)
                {
                    CurrentWorkbook.Sheets.Add(Type.Missing, Type.Missing, sheetsToInsert, Type.Missing);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        //
        public static void SaveWorkBook(string name)
        {
            if (CurrentWorkbook == null)
            {
                return;
            }
            if (name == null || name == string.Empty)
            {
                CurrentWorkbook.Save();
            }
            else
            {
                CurrentWorkbook.SaveAs(name, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlShared,
                                                           Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
        }        
        //
        public static void CloseWorkBook(bool save)
        {
            if (CurrentWorkbook == null)
            {
                return;
            }
            CurrentWorkbook.Close(save, Type.Missing, Type.Missing);
            CurrentWorkbook = null;
        }        
        //
        public static bool SetCurrentSheet(string SheetName)
        {
            try
            {
                CurrentSheet = (Excel.Worksheet)CurrentWorkbook.Sheets[SheetName];
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        //
        public static bool SetCurrentSheet(int SheetIndex)
        {
            if (SheetIndex < 1 || SheetIndex > CurrentWorkbook.Sheets.Count)
            {
                return false;
            }
            CurrentSheet = (Excel.Worksheet)CurrentWorkbook.Sheets[SheetIndex];
            return true;
        }
       
        //
        public static void ShowApplication()
        {
            if (ExcellApplication == null)
            {
                return;
            }
            ExcellApplication.ScreenUpdating = true;
            ExcellApplication.Visible = true;
        }
        //
        public static void HideApplication()
        {
            if (ExcellApplication == null)
            {
                return;
            }
            ExcellApplication.ScreenUpdating = true;
            ExcellApplication.Visible = false;
        }
        //
        public static List<List<string>> ReadData()
        {
            int maxRow = RowsCount;
            return ReadRange(1, maxRow);
        }
        //
        public static List<List<string>> ReadRange(int indexStart, int indexEnd)
        {
            int maxRow = RowsCount;
            if (indexStart < 1 || indexStart > indexEnd || indexStart > maxRow)
            {
                return null;
            }
            List<List<string>> tempReturn = new List<List<string>>();

            int VisualizationCount = 0;

            List<string> temp = null;
            for (int j = indexStart; j <= indexEnd && j <= maxRow; ++j)
            {
                temp = ReadRow(j);
                if(temp != null)
                {
                    tempReturn.Add(temp);
                }
                //
                if (threadParam != null)
                {
                    threadParam.Increment(++VisualizationCount);
                    if (threadParam.StopProcess)
                        return null;
                }
                //
            }
            return tempReturn;
        }          
        //
        public static List<string> ReadRow(int index)
        {
            int maxRow = RowsCount;
            int maxColumn = ColumnsCount;
            if(index < 1 || index > maxRow)
            {
                return null;
            }
            List<string> temp = new List<string>();
            for (int j = 1; j <= maxColumn; ++j)
            {
                temp.Add(((Excel.Range)CurrentSheet.Cells[index, j]).Value2 != null ? ((Excel.Range)CurrentSheet.Cells[index, j]).Value2.ToString() : string.Empty);

            }
            return temp;
        }  
        //
        public static bool WriteRange(int startIndex, List<List<string>> dataCollection)
        {
            if (startIndex < 1)
            {
                return false;
            }
            int VisualizationCount = 0;
            foreach (List<string> data in dataCollection)
            {
                if (!WriteRow(startIndex++, data))
                {
                    return false;
                }
                //
                if (threadParam != null)
                {
                    threadParam.Increment(++VisualizationCount);
                    if (threadParam.StopProcess)
                        return false;
                }
                //
            }
            return true;
        } 
        //
        public static bool WriteRow(int index, List<string> dataRow)
        {

            bool IsOk = false;
            if (index < 1)
            {
                return IsOk;
            }
            try
            {
                for (int i = 0; i < dataRow.Count; ++i)
                {
                    CurrentSheet.Cells[index, i + 1] = "'" + dataRow[i];
                }
                IsOk = true;
            }
            catch (System.Exception ex)
            {
            }

            return IsOk;
        }  
#endif
    }

}
