using System;
using System.IO;

namespace Utils
{
    public static class ReadInfo
    {
        static StreamReader reader;

        public static bool Open(string filePath)
        {
            Close();
            if (filePath == "")
            {
                reader = null;
                return false;
            }
            try
            {
                reader = File.OpenText(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //
        public static void Close()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            } 
        }
        //
        public static string ReadLine(string filePath)
        {
            Open(filePath);
            return ReadLine();
        }        
        //
        public static string ReadLine()
        {
            if (reader == null)
            {
                return null;
            }
            return reader.ReadLine();
        }        
        //
        public static char[] Read(string filePath, int index, int count)
        {
            Open(filePath);
            return Read(index, count);
        }        
        //
        public static char[] Read(int index, int count)
        {
            if (reader == null)
            {
                return null;
            }
            char[] buffer = new char[count];
            reader.Read(buffer, index, count);
            return buffer;
        }
    }
}