using System.Collections.Generic;
using Microsoft.Win32;

namespace Utils
{
    public static class WorkWithRegistry
    {
        //Сохранение ключей
        public static bool WriteToRegistry(RegistryKey startKey, List<string> keys, Dictionary<string, object> value) 
        {
            if (startKey == null || value == null || keys == null)
            {
                return false;
            }
            //
            RegistryKey temKey = startKey; 
            //
            foreach (string key in keys)
	        {
                temKey = temKey.CreateSubKey(key);
	        }
            //
            foreach (string valName in value.Keys)
	        {
                if (value[valName] is int)
                {
                    temKey.SetValue(valName, (int)value[valName]);
                }
                else if (value[valName] is string)
                {
                    temKey.SetValue(valName, (string)value[valName]);
                }
                else if (value[valName] is byte[])
                {
                    temKey.SetValue(valName, (byte[])value[valName]);
                }
                else if (value[valName] is bool)
                {
                    temKey.SetValue(valName, value[valName].ToString());
                }
                else if (value[valName] is float)
                {
                    temKey.SetValue(valName, (float)value[valName]);
                }
                else if (value[valName] is double)
                {
                    temKey.SetValue(valName, (double)value[valName]);
                }
            }
            return true;
                
        }
        //Чтение ключей
        public static bool ReadFromRegistry(RegistryKey startKey, List<string> keys, Dictionary<string, object> values, List<string> subKeys)
        {
            if (startKey == null || values == null || subKeys == null)
            {
                return false;
            }
            //
            RegistryKey temKey = startKey;
            //
            foreach (string key in keys)
	        {
        		 temKey = temKey.OpenSubKey(key);
                 if (temKey == null)
                 {
                     return false;
                 }
	        }
            //
            if(temKey.ValueCount == 0)
            {
                return false;
            }
            //
            values.Clear();
            foreach (string valName in temKey.GetValueNames())
	        {
                values.Add(valName, temKey.GetValue(valName));
	        }
            //
            subKeys.Clear();
            foreach (string subKeysName in temKey.GetSubKeyNames())
	        {
                subKeys.Add(subKeysName);
	        }
            //
            return true;
        }
        ////Examples
        ////Сохранение ключей 
        //public void SaveSettings(Main main)
        //{
        //    List<string> keys = new List<string>();
        //    keys.Add("AndreASoft");
        //    keys.Add("EplanDatabaseAdmin");

        //    Dictionary<string, object> value = new Dictionary<string, object>();
        //    value.Add("Width", main.Width);
        //    value.Add("Height", main.Height);
        //    value.Add("X", main.DesktopLocation.X);
        //    value.Add("Y", main.DesktopLocation.Y);
        //    value.Add("WindowState", main.WindowState.ToString());

        //    RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software", true);
        //    Utils.WorkWithRegistry.WriteToRegistry(softwareKey, keys, value);



        //}
        ////Чтение ключей
        //internal bool ReadSettings(Main main)
        //{
        //    //
        //    List<string> keys = new List<string>();
        //    keys.Add("AndreASoft");
        //    keys.Add("EplanDatabaseAdmin");

        //    Dictionary<string, object> value = new Dictionary<string, object>();
        //    List<string> subKeys = new List<string>();

        //    RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software");
        //    Utils.WorkWithRegistry.ReadFromRegistry(softwareKey, keys, value, subKeys);
        //    if (value == null)
        //    {
        //        return false;
        //    }

        //    int X = (int)value["X"];
        //    int Y = (int)value["Y"];
        //    main.DesktopLocation = new Point(X, Y);
        //    main.Height = (int)value["Height"];
        //    main.Width = (int)value["Width"];
        //    string initialWindowState = (string)value["WindowState"];
        //    main.WindowState = (FormWindowState)FormWindowState.Parse(main.WindowState.GetType(), initialWindowState);
        //    return true;
        //}

        ////Вставить в конструктор окна
        //void Main()
        //{ 
        //        RegSettings = new WorkWithRegistry();
        //        try
        //        {
        //            if (RegSettings.ReadSettings(this) == false)
        //            {
        //                // "Информация в реестре не найдена"; 
        //            }
        //            else
        //            {
        //                StartPosition = FormStartPosition.Manual;
        //                // "Информация прочитана из реестра"; 
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message,"Внимание");
        //            this.Close();
        //        } 
        //}
        ////Вставить в деструктор окна
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    RegSettings.SaveSettings(this);
        //    base.Dispose(disposing);
        //}
   }

}

