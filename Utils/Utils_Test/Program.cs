using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using System.Data;
using Utils.ActionWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    class Program
    {
        public static event Action ProcessEnded;
        public static event Action Increment;
        private static int count = 250;

        static void Main(string[] args)
        {
            //Test();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
            DBTest.Test7();
            //DBTest.Test8();
            //DBTest.Test9();
            //TestOpenFile();
            //TestLogger();
            //Indication2();
        }

        private static void TestLogger()
        {
            WriteInfo.Open(Application.StartupPath + @"\log.log");
            WriteInfo.WriteLine("test1.1");
            WriteInfo.WriteLine("test1.2");
            WriteInfo.Open(Application.StartupPath + @"\log2.log");
            WriteInfo.WriteLine("test2.1");
            WriteInfo.WriteLine("test2.2");
            WriteInfo.Clear();
        }
        //
        
        //
        static void Test6()
        {
            List<string> keys = new List<string>();
            keys.Add("First");
            keys.Add("Second");
            keys.Add("Third");

            Dictionary<string, object> value = new Dictionary<string, object>();
            value.Add("stringValue", "FirstString");
            value.Add("intValue", 10);
            value.Add("byteValue", new byte[] { 0x11, 0x22 });

            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software", true);
            WorkWithRegistry.WriteToRegistry(softwareKey, keys, value);
            //
            Dictionary<string, object> value2 = new Dictionary<string, object>();
            List<string> subKeys = new List<string>();
            WorkWithRegistry.ReadFromRegistry(softwareKey, keys, value2, subKeys);

        }
        static void TestOpenFile()
        {
            string[] extensions = new string[]
            {
            "XML Files (*.xml)|*.xml|",
            "All Files (*.*)|*.*"
            };
            string title = "Загрузка файла данных оборудования";
            string filePath = "222";
            if (SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {

            }

        }        
        
        public static void Test5()//settings
        {
            SettingsData myset = new SettingsData();
            myset.AddSetting("FirstName", "FirstValue");
            myset.AddSetting("SecondName", "SecondValue");
            myset.AddSetting("ThirdName", "ThirdValue");
            string error = null;
            XMLSerialization.SaveSettings(@"C:\testSet.xml", myset, null, ref error);

            SettingsData newSet = XMLSerialization.ReadSettings(@"C:\testSet.xml", null, ref error);
            string val = (string)newSet.GetSetting("SecondName");
            string val2 = (string)newSet.GetSetting("FFFSecondName");

            myset = new SettingsData();
            DataForTest testData = new DataForTest();
            myset.AddSetting("FirstName", testData);
            DataForTest testData2 = new DataForTest("ggg", 5, 10.0);
            myset.AddSetting("SecondName", testData2);
            XMLSerialization.SaveSettings(@"C:\testSet_.xml", myset, null, ref error);

            List<Type> temp = new List<Type>();
            temp.Add(typeof(DataForTest));
            newSet = XMLSerialization.ReadSettings(@"C:\testSet_.xml", temp, ref error);
            DataForTest val3 = (DataForTest)newSet.GetSetting("FirstName");
            DataForTest val4 = (DataForTest)newSet.GetSetting("SecondName");

        }
        static void Test4()
        {
            WorkWithXML.OpenFile(@"C:\test.xml");
            List<XElement> temp = WorkWithXML.ReadElement("Module").ToList();
            List<XElement> temp2 = WorkWithXML.ReadElement("Module", "OrderNumber").ToList();
            List<XElement> temp3 = WorkWithXML.ReadElement("Module", "OrderNumber", "6ES7331–7KF02–0AB0").ToList();
            int temp4 = WorkWithXML.DeleteElement("Module", "OrderNumber", "6ES7331–7KF02–0AB0");
            WorkWithXML.SaveFile();
            WorkWithXML.CloseFile();

            XMLElement first = new XMLElement("First");
            first.AddAttribute("firstAttrName1", "firstAttrVAlue1");
            first.AddAttribute("firstAttrName2", "firstAttrVAlue2");
            XMLElement second = new XMLElement("Second");
            second.AddAttribute("secondAttrName1", "secondAttrVAlue1");
            second.AddAttribute("secondAttrName2", "secondAttrVAlue2");
            XMLElement third = new XMLElement("Third", "ThirdValue");
            third.AddAttribute("thirdAttrName1", "thirdAttrVAlue1");
            third.AddAttribute("thirdAttrName2", "thirdAttrVAlue2");

            first.AddElement(second);
            second.AddElement(third);
            WorkWithXML.OpenWriter(@"C:\test2.xml");
            WorkWithXML.WriteElement(first);
            WorkWithXML.CloseWriter();
        }
        //
        static void Test3()
        {
            //WorkWithExcel.OpenApplication();
            //WorkWithExcel.ShowApplication();
            //WorkWithExcel.OpenWorkBook(@"C:\test.xls");

            //WorkWithExcel.SetCurrentSheet(0);
            //WorkWithExcel.SetCurrentSheet(3);
            //WorkWithExcel.SetCurrentSheet(5);
            //int rows = WorkWithExcel.RowsCount;
            //int columns = WorkWithExcel.RowsCount;
            //List<List<string>> data = WorkWithExcel.ReadData();
            //WorkWithExcel.SetCurrentSheet(10);
            //WorkWithExcel.SetCurrentSheet("Test");
            //WorkWithExcel.SetCurrentSheet("TTest");
            //WorkWithExcel.CloseWorkBook(false);
            //WorkWithExcel.CloseApplication();
        }
        
       // [TestMethod]
        public bool Indication()
        {
            string headerText = "Data Export";
            Thread workThread = new Thread(new ThreadStart(delegate
            {
                WorkMethod();
                ThreadVisualization.OnProcessEnded();
            }));

            CancelForm form = new CancelForm(headerText);
            ThreadVisualization.ProcessEnded += form.CloseForm;
            if (!ThreadVisualization.StartWorkThread(form, workThread))
            {
                return false;
            }
            return true;

        }

        static void WorkMethod()
        {
            Thread.Sleep(30000);
            //....
        }
        
        static Boolean Indication2()
        {
            string headerText = "Data Export";
            Thread workThread = new Thread(new ThreadStart(delegate
            {
                WorkMethod2();
                ThreadVisualization.OnProcessEnded();
            }));

            CancelForm form = new CancelForm(headerText);
            ThreadVisualization.ProcessEnded += form.CloseForm;
            if (!ThreadVisualization.StartWorkThread(form, workThread))
            {
                return false;
            }
            return true;

        }

        static void WorkMethod2()
        {
            for (int i = 0; i < count; i++)
            {
                ThreadVisualization.OnIncrement();
                Thread.Sleep(100);
            }
        } 
    }
    //For serialization all must be public
    [Serializable]
    public class DataForTest
    {
        private string tempStr = "StringVAlue";

        public string TempStr
        {
            get { return tempStr; }
            set { tempStr = value; }
        }
        public int tempInt = 9;
        public double tempDouble = 20.0;
        public DataForTest(string str, int Int, double DDouble)
        {
            tempStr = str;
            tempInt = Int;
            tempDouble = DDouble;

        }
        public DataForTest()
        {

        }
    }

}
