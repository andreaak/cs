using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Algoritms
{
    class Serialization
    {

        #region XML
        
        private static List<Type> SetType()
        {
            List<Type> ls = new List<Type>();
            ls.Add(typeof(BaseItem));
            ls.Add(typeof(Parameter));
            ls.Add(typeof(DataType));
            ls.Add(typeof(Language));
            ls.Add(typeof(MethodItem));
            ls.Add(typeof(PropertyItem));
            ls.Add(typeof(BranchBlockItem));
            ls.Add(typeof(BranchItem));
            ls.Add(typeof(CycleItem));
            return ls;
        }
        
        public static bool SaveDataToXML(string filePath, List<BaseItem> itemsList, ref string error)
        {
            if (itemsList == null)
            {
                return true;
            }

            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
                };
                string title = "Сохранение данных";
                if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
                {
                    return true;
                }
            }
            itemsList[0].ClearParent();
            SettingsData myset = new SettingsData();
            myset.AddSetting("MethodList", itemsList);

            List<Type> ls = SetType();

            bool ok = SaveSettings(filePath, myset, ls, ref error);
            itemsList[0].SetParent(null);
            return ok;
        }

        public static bool SaveSettings(string filePath, SettingsData data, List<Type> types, ref string error)
        {
            bool isOk = false;
            if (
                
                (filePath == null || filePath == string.Empty)
                )
            {
                return isOk;
            }

            FileStream stream = null;
            try
            {

                if (types == null)
                {
                    SetTypes(data, ref types);
                }

                string fullPath = Path.GetFullPath(filePath);
                string rootDirPath = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(rootDirPath))
                {
                    Directory.CreateDirectory(rootDirPath);
                }
                stream = new FileStream(filePath, FileMode.Create);


                XmlSerializer sr = null;
                if (types.Count > 0)
                {
                    sr = new XmlSerializer(data.GetType(), types.ToArray());

                }
                else
                {
                    sr = new XmlSerializer(data.GetType());
                }
                sr.Serialize(stream, data);
                isOk = true;
            }
            catch (Exception ex)
            {
                isOk = false;
                error = ex.Message;
                if (ex.InnerException != null)
                {
                    error = " Inner exception " + ex.InnerException.Message;
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return isOk;
        }

        private static void SetTypes(SettingsData data, ref List<Type> types)
        {
            foreach (object ob in data.Values)
            {
                Type temp = ob.GetType();
                if (!types.Contains(temp) && temp.Namespace != "System")
                {
                    types.Add(temp);
                }
            }
        }

        public static List<BaseItem> ReadDataFromXML(string filePath, ref string error)
        {

            List<BaseItem> itemsList = null;
            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "XML Files (*.xml)|*.xml|",
                "All Files (*.*)|*.*"
                };
                string title = "Загрузка данных";
                if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
                {
                    return new List<BaseItem>();
                }
            }
            if (File.Exists(filePath))
            {
                List<Type> temp = SetType();
                SettingsData newSet = ReadSettings(filePath, temp, ref error);
                if (newSet == null)
                {
                    MessageBox.Show(string.Format("Error open file "));
                    itemsList = new List<BaseItem>();
                    return itemsList;
                }
                itemsList = newSet.GetSetting("MethodList") as List<BaseItem>;
                if (itemsList == null)
                {
                    MessageBox.Show(string.Format("Error open file "));
                    itemsList = new List<BaseItem>();
                }
                else
                {
                    BaseItem.idCount = BaseItem.GetMaxId(itemsList);
                    itemsList[0].SetParent(null);
                }
            }

            return itemsList;
        }

        public static SettingsData ReadSettings(string filePath, List<Type> types, ref string error)
        {
            if (filePath == null || filePath == string.Empty || !File.Exists(filePath))
            {
                return null;
            }

            SettingsData setData = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Open);
                XmlSerializer sr = null;
                if (types != null && types.Count > 0)
                {
                    sr = new XmlSerializer(typeof(SettingsData), types.ToArray());

                }
                else
                {
                    sr = new XmlSerializer(typeof(SettingsData));
                }
                setData = (SettingsData)sr.Deserialize(stream);
            }
            catch (Exception ex)
            {
                setData = null;
                error = ex.Message;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return setData;
        }
        
        #endregion

        #region BIN
        
        public static string SaveData(string filePath, List<BaseItem> itemsList)
        {
            if (itemsList == null)
            {
                return null;
            }

            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "BIN Files (*.bin)|*.bin|",
                "All Files (*.*)|*.*"
                };
                string title = "Сохранение данных";
                if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
                {
                    return null;
                }
            }
            string error = string.Empty;
            bool code = Utils.SerializeBin.SerializeTo(filePath, itemsList, ref error);
            return error;
        }

        public static List<BaseItem> ReadData(string filePath)
        {

            List<BaseItem> itemsList = null;
            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "BIN Files (*.bin)|*.bin|",
                "All Files (*.*)|*.*"
                };
                string title = "Загрузка данных";
                if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
                {
                    return new List<BaseItem>();
                }
            }
            if (File.Exists(filePath))
            {
                string error = string.Empty;
                bool code = true;
                itemsList = Utils.SerializeBin.SerializeFrom(filePath, ref code, ref error) as List<BaseItem>;
                if (!code || itemsList == null)
                {
                    MessageBox.Show(string.Format("Error open file {0}", error));
                    itemsList = new List<BaseItem>();
                }
                else
                {
                    BaseItem.idCount = BaseItem.GetMaxId(itemsList);
                }
            }
            return itemsList;
        }
        
        #endregion
    }

    [Serializable]
    public class SettingsData
    {
        List<string> names;
        public List<string> Names
        {
            get { return names; }
        }
        List<object> values;
        public List<object> Values
        {
            get { return values; }
        }
        //
        public SettingsData()
        {
            names = new List<string>();
            values = new List<object>();
        }
        //
        public void AddSetting(string setName, object setValue)
        {
            if (setName != null && setName != string.Empty)
            {
                names.Add(setName);
                if (setValue != null)
                {
                    values.Add(setValue);
                    return;
                }
                values.Add(string.Empty);
            }
        }
        //
        public object GetSetting(string setName)
        {
            if (setName != null && setName != string.Empty && names.Contains(setName))
            {
                return values[names.IndexOf(setName)];
            }
            return null;
        }
    }
}
