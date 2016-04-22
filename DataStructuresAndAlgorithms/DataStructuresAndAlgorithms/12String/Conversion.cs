using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms._12String
{
    [TestClass]
    public class Conversion
    {
        [TestMethod]
        public void TestStringToInt()
        {
            Debug.WriteLine(StringToInt("256"));
        }

        private int StringToInt(string str)
        {
            int i = 0, num = 0;
            bool isNeg = false;
            int len = str.Length;
            if (str[0] == '-')
            {
                isNeg = true;
                i = 1;
            }
            while (i < len)
            {
                num *= 10;
                num += (str[i++] - '0');
            }

            if (isNeg)
            {
                num = -num;
            }
            return num;
        }

        [TestMethod]
        public void TestIntToString()
        {
            Debug.WriteLine(IntToString(256));
            Debug.WriteLine(IntToString(0));
        }

        public const int MAX_DIGITS = 10;
        private string IntToString(int num)
        {
            int i = 0;
            bool isNeg = false;

            /* Буфер достаточно велик для самого большого int и знака - */
            char[] temp = new char[MAX_DIGITS + 1];

            /* Проверяем, не является ли число отрицательным */
            if (num < 0)
            {
                num = -num;
                isNeg = true;
            }

            /* Заполняем буфер цифровыми символами в обратном порядке */
            do
            {
                temp[i++] = (char)((num % 10) + '0');
                num /= 10;
            } while (num != 0);

            StringBuilder b = new StringBuilder();
            if (isNeg)
            {
                b.Append('-');
            }

            while (i > 0)
            {
                b.Append(temp[--i]);
            }
            return b.ToString();
        }

    }
}
