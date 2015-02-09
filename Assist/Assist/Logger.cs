using System;
using System.Text;
using System.IO;
using Utils;


namespace Assist
{
    public static class Logger
    {
        static bool isLoggerActive;

        public static bool IsLoggerActive
        {
            get { return isLoggerActive; }
        }

        public static void Open(string filePath)
        {
            isLoggerActive = WriteInfo.Open(filePath);
        }
        
        public static void Close()
        {
            WriteInfo.Close();
            isLoggerActive = false;
        }

        public static void Clear()
        {
            isLoggerActive = WriteInfo.Clear();
        }

        public static void WriteLine(string StringData)
        {
            if (!isLoggerActive)
            {
                return;
            }
            WriteInfo.WriteLine(StringData);
        }

        public static void WriteLine(int TabCount, string StringData)
        {
            if (!isLoggerActive)
            {
                return;
            }
            WriteInfo.WriteLine(TabCount, StringData);
        }
        
    }
}
