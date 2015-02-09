using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Utils
{
    public static class XMLSerialization
    {
        //
        static SettingsData setData;
        public static SettingsData SetData
        {
          get { return XMLSerialization.setData; }
          set { XMLSerialization.setData = value; }
        }
        //
        static string filePathSt;
        public static string FilePath
        {
            get { return XMLSerialization.filePathSt; }
            set { XMLSerialization.filePathSt = value; }
        }
        //
        public static SettingsData ReadSettings(string filePath, List<Type> types, ref string error)
        {
            if (filePath == null || filePath == string.Empty || !File.Exists(filePath))
            {
                return null;
            }

            setData = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Open);
                XmlSerializer sr = null;
                if (types != null && types.Count > 0)
                {
                    sr = new XmlSerializer(typeof(SettingsData), types.ToArray<Type>());

                }
                else
                {
                    sr = new XmlSerializer(typeof(SettingsData));
                }
                setData = (SettingsData)sr.Deserialize(stream);
                filePathSt = filePath;
            }
            catch (Exception ex)
            {
                setData = null;
                filePathSt = string.Empty;
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
        //
        public static bool SaveSettings(string filePath, SettingsData data, List<Type> types, ref string error)
        {
            bool isOk = false;
            if (
                (filePathSt == null || filePathSt == string.Empty) &&
                (filePath == null || filePath == string.Empty)
                )
            {
                return isOk;
            }
            if (filePath == null || filePath == string.Empty)
            {
                filePath = filePathSt;
            }
            if (data != null)
            {
                setData = data;
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
                    sr = new XmlSerializer(data.GetType(), types.ToArray<Type>());

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
    }    
    //
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

        //private void test()
        //{
        //    SettingsData myset = new SettingsData();
        //    myset.AddSetting("FirstName", "FirstValue");
        //    myset.AddSetting("SecondName", "SecondValue");
        //    myset.AddSetting("ThirdName", "ThirdValue");
        //    XMLSerialization.SaveSettings(@"C:\testSet.xml", myset);

        //    SettingsData newSet = XMLSerialization.ReadSettings(@"C:\testSet.xml", null);
        //    string val = (string)newSet.GetSetting("SecondName");
        //    string val2 = (string)newSet.GetSetting("FFFSecondName");

        //    myset = new SettingsData();
        //    DataForTest testData = new DataForTest();
        //    myset.AddSetting("FirstName", testData);
        //    DataForTest testData2 = new DataForTest("ggg", 5, 10.0);
        //    myset.AddSetting("SecondName", testData2);
        //    XMLSerialization.SaveSettings(@"C:\testSet_.xml", myset);

        //    List<Type> temp = new List<Type>();
        //    temp.Add(typeof(DataForTest));
        //    newSet = XMLSerialization.ReadSettings(@"C:\testSet_.xml", temp);
        //    DataForTest val3 = (DataForTest)newSet.GetSetting("FirstName");
        //    DataForTest val4 = (DataForTest)newSet.GetSetting("SecondName");
        //}
    }

    //[Serializable]
    //public class DataForTest
    //{
    //    private string tempStr = "StringValue";

    //    public string TempStr
    //    {
    //        get { return tempStr; }
    //        set { tempStr = value; }
    //    }
    //    public int tempInt = 9;
    //    public double tempDouble = 20.0;
    //    public DataForTest(string str, int Int, double DDouble)
    //    {
    //        tempStr = str;
    //        tempInt = Int;
    //        tempDouble = DDouble;

    //    }
    //    public DataForTest()
    //    {

    //    }
    //}
}
