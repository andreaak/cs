using System;
using System.Text;
using System.IO;
using Log = Utils.WorkWithFiles.Logger;

namespace Assist
{
    public static class Logger
    {
        static bool isLoggerActive;

        public static void Open(string filePath)
        {
            Log.GetInstance().Init(filePath);
            isLoggerActive = true;
        }
        
        public static void Close()
        {
            Log.GetInstance().Close();
            isLoggerActive = false;
        }

        public static void WriteLine(string StringData)
        {
            if (!isLoggerActive)
            {
                return;
            }
            Log.GetInstance().WriteLine(StringData);
        }

        public static void WriteLine(int TabCount, string StringData)
        {
            if (!isLoggerActive)
            {
                return;
            }
            Log.GetInstance().WriteLine(TabCount + " " + StringData);
        }
        
    }
}
