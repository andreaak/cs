﻿//using System.Configuration;
//using System.Diagnostics;
//using System.Linq;
//using NUnit.Framework;

//namespace CSTest._22_AppConfig
//{

//    [TestFixture]
//    public class Test
//    {
//        [Test]
//        public void TestReadAll()
//        {
//            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
//            {
//                Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key]));
//            }
//            /*
//            Key: dbFile Value: data.db
//            Key: dbFormatType Value: rtf
//            Key: inType Value: doc
//            Key: outType Value: rtf
//            Key: alphabetPath Value: RussianAlphabet.txt
//            Key: dictionaryPath Value: russian.dic
//            */
//        }

//        [Test]
//        public void TestReadNotExisting()
//        {
//            string key = "dbFile_";
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//            /*
//            Key: dbFile_ Value: null
//            */
//        }

//        [Test]
//        public void TestDeleteKey()
//        {
//            string key = "dbFile_";

//            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
//            {
//                Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//                config.AppSettings.Settings.Remove(key);
//                config.Save(ConfigurationSaveMode.Modified, true);
//                ConfigurationManager.RefreshSection("appSettings");
//                Debug.WriteLine("After delete");
//                foreach (var k in ConfigurationManager.AppSettings.AllKeys)
//                {
//                    Debug.WriteLine(string.Format("Key: {0} Value: {1}", k, ConfigurationManager.AppSettings[k]));
//                }
//            }
//            else
//            {
//                Debug.WriteLine("Key not found");
//            }
//            /*
//            Key: dbFile_ Value: Test,Test
//            After delete
//            Key: dbFile Value: data.db
//            Key: dbFormatType Value: rtf
//            Key: inType Value: doc
//            Key: outType Value: rtf
//            Key: alphabetPath Value: RussianAlphabet.txt
//            Key: dictionaryPath Value: russian.dic
//            */
//        }

//        [Test]
//        public void TestAddKey()
//        {
//            string key = "dbFile_";
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            config.AppSettings.Settings.Add(key, "Test1");
//            config.Save(ConfigurationSaveMode.Modified, true);
//            ConfigurationManager.RefreshSection("appSettings");
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//            /*
//            Key: dbFile_ Value: null
//            Key: dbFile_ Value: Test
//            */

//            /* SecondTime
//            Key: dbFile_ Value: Test
//            Key: dbFile_ Value: Test,Test
//            */
//        }

//        [Test]
//        public void TestSetKey()
//        {
//            string key = "dbFile_";
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            config.AppSettings.Settings[key].Value = "Test2";
//            config.Save(ConfigurationSaveMode.Modified, true);
//            ConfigurationManager.RefreshSection("appSettings");
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key] ?? "null"));
//            /*
//            Key: dbFile_ Value: Test1,Test1
//            Key: dbFile_ Value: Test2
//            */
//        }

//        [Test]
//        public void TestReadApplicationSettings()
//        {
//            Debug.WriteLine(string.Format("Key: {0} Value: {1}", "TestString", Properties.Settings.Default.TestString));
//            /*
//            Key: TestString Value: TestStringValue
//            */
//        }
//    }
//}
