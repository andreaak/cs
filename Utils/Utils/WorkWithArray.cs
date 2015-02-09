using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{ 
    public static class WorkWithArray
    {
        public static void CopyArray(byte[] from, byte[] to, uint fromStartIndex)
        {
            int fromLength = from.GetLength(0);
            int toLength = to.GetLength(0);
            uint j = 0;
            uint i = fromStartIndex;
            for (; i < fromLength && j < toLength; i++, j++)
            {
                to[j] = from[i];
            }
        }
        //
        public static void CopyArray(byte[] from, uint fromArrayStartIndex, byte[] to, uint toArrayStartIndex)
        {

            int fromLength = from.GetLength(0);
            int toLength = to.GetLength(0);
            uint j = toArrayStartIndex;
            uint i = fromArrayStartIndex;
            for (; i < fromLength && j < toLength; i++, j++)
            {
                to[j] = from[i];
            }
        }
        //
        public static byte[] ConcatArray(byte[] buffer, byte[] buffer2)
        {
            int fromLength1 = buffer.GetLength(0);
            int fromLength2 = buffer2.GetLength(0);

            byte[] to = new byte[fromLength1 + fromLength2];

            buffer.CopyTo(to, 0);
            buffer2.CopyTo(to, fromLength1);

            return to;
        }

        //
        public static UInt32 ConvertToInt(byte[] bytes)
        {
            byte[] array = new byte[sizeof(UInt32)];
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            bytes.CopyTo(array, 0);
            UInt32 temp = BitConverter.ToUInt32(array, 0); ;
            return temp;
        }
        //
        public static UInt16 ConvertToShort(byte[] bytes)
        {
            byte[] array = new byte[sizeof(UInt16)];
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            bytes.CopyTo(array, 0);
            UInt16 temp = BitConverter.ToUInt16(array, 0); ;
            return temp;
        }
        //
        public static string ConvertToString(byte[] bytes)
        {
            string name = Encoding.Unicode.GetString(bytes);
            return name;
        }
        //
        public static string ConvertToStringASCII(byte[] bytes)
        {
            Encoding enc = Encoding.GetEncoding(866);
            string name = enc.GetString(bytes);
            return name.Trim();
        }
        //
        public static char[] ConvertToCharArray(byte[] bytes)
        {
            Encoding enc = Encoding.GetEncoding(866);
            char[] name = enc.GetChars(bytes);
            return name;
        }
    }
}
