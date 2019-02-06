using System.Collections.Generic;
using Microsoft.Win32;

namespace Assist
{
    public static class WorkWithRegistry
    {
       
        public static void WriteToReg(Dictionary<string, object> value)
        {
            List<string> keys = new List<string>();
            keys.Add("AndreASoft");
            keys.Add("VSAssist");
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            Utils.WorkWithRegistry.WriteToRegistry(softwareKey, keys, value);
        }

        public static bool ReadFromReg(out Dictionary<string, object> value)
        {
            List<string> keys = new List<string>();
            keys.Add("AndreASoft");
            keys.Add("VSAssist");

            value = new Dictionary<string, object>();
            List<string> subKeys = new List<string>();

            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");
            Utils.WorkWithRegistry.ReadFromRegistry(softwareKey, keys, value, subKeys);
            if (value == null)
            {
                return false;
            }
            return true;
        }

        public static List<string> ReadGuidFromReg(out Dictionary<string, object> value)
        {
            List<string> keys = new List<string>();
            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\8.0\Text Editor\External Markers
            //HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\11.0_Config\Text Editor\External Markers
            //HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\11.0\Text Editor\External Markers
            //HKEY_USERS\.DEFAULT\Software\Microsoft\VisualStudio\11.0_Config\Text Editor\External Markers
            keys.Add("Microsoft");
            keys.Add("VisualStudio");
            keys.Add("11.0_Config");
            keys.Add("Text Editor");
            keys.Add("External Markers");

            value = new Dictionary<string, object>();
            List<string> subKeys = new List<string>();
            
            //RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software");
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");
            Utils.WorkWithRegistry.ReadFromRegistry(softwareKey, keys, value, subKeys);
            if (value == null)
            {
                return null;
            }
            return subKeys;
        }

    }

}

