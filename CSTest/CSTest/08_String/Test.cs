﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CSTest._08_String
{
    [TestClass]
    public class Test
    {
        const string consts1 = "Hello1";
        const string consts2 = "Hello1";
        
        [TestMethod]
        public void TestString1()
        {
            // Задание пути к приложению
            string file = "C:\\Windows\\System32\\Notepad.exe";
            // Задание пути к приложению с помощью буквальной строки
            file = @"C:\Windows\System32\Notepad.exe";

            string test1 = "Test";
            string test2 = "Test";
            Debug.WriteLine("test1 == test2 : " + (test1 == test2));
            Debug.WriteLine("test1 equal test2 : " + test1.Equals(test2, StringComparison.Ordinal));
            /*
            test1 == test2 : True
            test1 equal test2 : True
            */
        }

        [TestMethod]
        public void TestString2Intern()
        {
            // Конкатенация трех литеральных строк образует одну литеральную строку
            string sc1 = "Hi" + " " + "there.";
            string sc2 = "Hi there.";
            Debug.WriteLine("sc1 ReferenceEquals sc2 : " + object.ReferenceEquals(sc1, sc2)); //True Зависит от аттрибута NoStringInterning
            
            string sc3 = " ";
            string sc4 = "Hi" + sc3 +  "there.";
            Debug.WriteLine("sc1 ReferenceEquals sc3 : " + object.ReferenceEquals(sc1, sc3)); // 'False'

            Debug.WriteLine("consts1 ReferenceEquals consts2 : " + object.ReferenceEquals(consts1, consts2)); //True Зависит от аттрибута NoStringInterning
            string s1 = "Hello";
            string s2 = "Hello";
            Debug.WriteLine("s1 ReferenceEquals s2 : " + object.ReferenceEquals(s1, s2)); //True Зависит от аттрибута NoStringInterning
            /*Однако если выполнить этот код в CLR 
            версии 4.5, будет выведено значение True. Дело в том, что эта версия CLR игно-
            рирует атрибут/флаг, созданный компилятором C#, и интернирует литеральную 
            строку "Hello" при загрузке сборки в домен приложений. Это означает, что s1 и s2 
            ссылаются на одну строку в куче. Однако, как уже отмечалось, никогда не стоит 
            писать код с расчетом на такое поведение, потому что в последующих версиях этот 
            атрибут/флаг может приниматься во внимание, и строка "Hello" интернироваться 
            не будет. */
            s1 = string.Intern(s1);
            s2 = string.Intern(s2);
            Debug.WriteLine("s1 ReferenceEquals s2 : " + object.ReferenceEquals(s1, s2)); // 'True'

            Debug.WriteLine("s1 String ReferenceEquals s2 : " + StringEquals("Hello", "Hello")); //True Зависит от аттрибута NoStringInterning
        }

        [TestMethod]
        public void TestString3Format()
        {
            Debug.WriteLine("C format: {0:C}", 99.9);      // Вывод в денежном формате.
            Debug.WriteLine("F format: {0:##}", 99.935);   // Вывод значений с фиксированой точностью.
            Debug.WriteLine("N format: {0:N}", 99999);     // Стандартное числовое форматироваание.
            Debug.WriteLine("X format: {0:X}", 255);       // Вывод в шеснадцатиричном формате.
            Debug.WriteLine("D format: {0:D}", 0xFF);      // Вывод в десятичном формате.
            Debug.WriteLine("E format: {0:E}", 9999);      // Вывод в экспоненциальном формате.
            Debug.WriteLine("G format: {0:G}", 99.9);      // Вывод в общем формате.
            Debug.WriteLine("P format: {0:P}", 99.9);      // Вывод в процентном формате.
            /*
            C format: ¤99.90
            F format: 100
            N format: 99,999.00
            X format: FF
            D format: 255
            E format: 9.999000E+003
            G format: 99.9
            P format: 9,990.00 %
            */
        }
        private bool StringEquals(string s1, string s2)
        {
            return object.ReferenceEquals(s1, s2);
        }
    }
}