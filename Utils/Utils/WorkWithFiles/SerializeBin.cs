using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utils
{
    public static class SerializeBin
    {
        //
        public static bool SerializeTo(string filePath, object sn, ref string error)
        {
            bool returnCode = true;
            FileStream saveFile = null;
            try
            {
                // Получить сериализатор. 
                IFormatter serializer = new BinaryFormatter();
                // Сериализовать продукты. 
                saveFile =
                new FileStream(filePath, FileMode.Create, FileAccess.Write);
                if (sn != null)
                    serializer.Serialize(saveFile, sn);
            }
            catch (SerializationException ex)
            {
                error = "Ошибка сериализации " + ex.Message;
                returnCode = false;
            }
            catch (IOException ex)
            {
                error = "Ошибка ввода/вывода " + ex.Message;
                returnCode = false;
            }
            finally
            {
                if (saveFile != null)
                {
                    saveFile.Close();
                }
            }
            return returnCode;
        }
        //
        public static object SerializeFrom(string filePath, ref bool returnCode, ref string error)
        {
            FileStream loadFile = null;
            object sn = null; 
            try
            {
                // Получить сериализатор. 
                IFormatter serializer = new BinaryFormatter();
                loadFile =
                new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //Десериализация сигналов
                sn = serializer.Deserialize(loadFile);
                if (sn == null)
                {
                    returnCode = false;
                }
            }
            catch (SerializationException ex)
            {
                error = "Ошибка сериализации " + ex.Message;
                returnCode = false;
            }
            catch (IOException ex)
            {
                error = "Ошибка ввода/вывода " + ex.Message;
                returnCode = false;
            }
            finally
            {
                if (loadFile != null)
                {
                    loadFile.Close();
                }
            }
            return sn;
        }

        //public static Dictionary<string, Table> ReadData(string filePath)
        //{

        //    Dictionary<string, Table> tables = null;
        //    if (filePath == null)
        //    {
        //        string[] extensions = new string[]
        //        {
        //        "BIN Files (*.bin)|*.bin|",
        //        "All Files (*.*)|*.*"
        //        };
        //        string title = "Загрузка данных";
        //        if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
        //        {
        //            return new Dictionary<string, Table>();
        //        }
        //    }
        //    if (File.Exists(filePath))
        //    {
        //        string error = string.Empty;
        //        bool code = true;
        //        tables = (Dictionary<string, Table>)UTILS.SerializeBin.SerializeFrom(filePath, ref code, ref error);
        //        if (!code)
        //        {
        //            MessageBox.Show(string.Format("Error open file {0}", error));
        //            tables = new Dictionary<string, Table>();
        //        }
        //    }
        //    else
        //    {
        //        tables = new Dictionary<string, Table>();
        //    }
        //    return tables;
        //}

        //public static bool SaveData(string filePath, Dictionary<string, Table> tables)
        //{
        //    if (tables == null)
        //    {
        //        return false;
        //    }

        //    if (filePath == null)
        //    {
        //        string[] extensions = new string[]
        //        {
        //        "BIN Files (*.bin)|*.bin|",
        //        "All Files (*.*)|*.*"
        //        };
        //        string title = "Сохранение данных";
        //        if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
        //        {
        //            return false;
        //        }
        //    }
        //    string error = string.Empty;
        //    bool code = UTILS.SerializeBin.SerializeTo(filePath, tables, ref error);
        //    return code;
        //}
    }
}
