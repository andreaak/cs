using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._08_String
{
    [TestFixture]
    public class Test
    {
        const string consts1 = "Hello1";
        const string consts2 = "Hello1";

        [Test]
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

        [Test]
        public void TestString2Intern()
        {
            // Конкатенация трех литеральных строк образует одну литеральную строку
            string sc1 = "Hi" + " " + "there.";
            string sc2 = "Hi there.";
            Debug.WriteLine("sc1 ReferenceEquals sc2 : " + object.ReferenceEquals(sc1, sc2)); //True Зависит от аттрибута NoStringInterning

            string sc3 = " ";
            string sc4 = "Hi" + sc3 + "there.";
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

        [Test]
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

            string temp1 = "One, Two";
            string temp2 = $"@{temp1}";
            Debug.WriteLine("Temp1 {0} Temp2 {1}", temp1, temp2);      // Вывод в процентном формате.
            /*
            C format: ¤99.90
            F format: 100
            N format: 99,999.00
            X format: FF
            D format: 255
            E format: 9.999000E+003
            G format: 99.9
            P format: 9,990.00 %
            Temp1 One, Two Temp2 @One, Two
            */
        }
        private bool StringEquals(string s1, string s2)
        {
            return object.ReferenceEquals(s1, s2);
        }

        [Test]
        public void TestString3Clone()
        {
            string first = "First";
            string second = (string)first.Clone();
            Debug.WriteLine("first.Equals(second): " + first.Equals(second));
            Debug.WriteLine("ReferenceEquals(first,second): " + ReferenceEquals(first, second));
            string third = string.Copy(first);
            Debug.WriteLine("first.Equals(third): " + first.Equals(third));
            Debug.WriteLine("ReferenceEquals(first,third): " + ReferenceEquals(first, third));

            StringTest test1;
            test1.First = "First";
            test1.Second = (string)test1.First.Clone();
            test1.Third = string.Copy(test1.First);
            Debug.WriteLine("ReferenceEquals(test1.First, test1.Second): " + ReferenceEquals(test1.First, test1.Second));
            Debug.WriteLine("ReferenceEquals(test1.Second, test1.Third): " + ReferenceEquals(test1.Second, test1.Third));

            StringTest test2 = test1;
            Debug.WriteLine("ReferenceEquals(test1.First, test2.First): " + ReferenceEquals(test1.First, test2.First));
            Debug.WriteLine("ReferenceEquals(test1.Second, test2.Second): " + ReferenceEquals(test1.Second, test2.Second));
            Debug.WriteLine("ReferenceEquals(test1.Third, test2.Third): " + ReferenceEquals(test1.Third, test2.Third));

            object temp = test1;
            StringTest test3 = (StringTest)temp;
            Debug.WriteLine("ReferenceEquals(test1.First, test3.First): " + ReferenceEquals(test1.First, test3.First));
            Debug.WriteLine("ReferenceEquals(test1.Second, test3.Second): " + ReferenceEquals(test1.Second, test3.Second));
            Debug.WriteLine("ReferenceEquals(test1.Third, test3.Third): " + ReferenceEquals(test1.Third, test3.Third));
            /*
            first.Equals(second): True
            ReferenceEquals(first,second): True
            first.Equals(third): True
            ReferenceEquals(first,third): False

            ReferenceEquals(test1.First, test1.Second): True
            ReferenceEquals(test1.Second, test1.Third): False

            ReferenceEquals(test1.First, test2.First): True
            ReferenceEquals(test1.Second, test2.Second): True
            ReferenceEquals(test1.Third, test2.Third): True

            ReferenceEquals(test1.First, test3.First): True
            ReferenceEquals(test1.Second, test3.Second): True
            ReferenceEquals(test1.Third, test3.Third): True
            */
        }

        [Test]
        public void TestString4StringConcat()
        {
            string temp = "";
            string temp2 = "FFFFF";
            string temp3 = temp2.Substring(1, 3);
            string res = temp + temp3;
            Debug.WriteLine("ReferenceEquals(temp3, res): " + StringEquals(temp3, res));

            string res2 = string.Concat(temp, temp3);
            Debug.WriteLine("ReferenceEquals(temp3, res2): " + StringEquals(temp3, res2));

            string ddd = string.IsInterned(temp3);
            Debug.WriteLine("IsInterbed temp3: " + (ddd != null ? "Yes" : "Not"));
            /*
            ReferenceEquals(temp3, res): True
            ReferenceEquals(temp3, res2): True
            IsInterbed temp3: Not
            */
        }

        struct StringTest
        {
            public string First;
            public string Second;
            public string Third;
        }

        class StringTestClass
        {
            public string First;

            internal void SetName(string v)
            {
                First = v;
            }
        }

        [Test]
        public void TestString7Shit()
        {
            var list = new List<StringTestClass>
            {
                new StringTestClass(),
                new StringTestClass(),
                new StringTestClass(),
            };

            SetButtonsText(list, "Prize");

            /*
            */
        }

        private void SetButtonsText(List<StringTestClass> listPercents, string rewardValue)
        {
            foreach (var text in listPercents)
            {
                text.First = rewardValue + "%";
            }
            Debug.WriteLine(ReferenceEquals(listPercents[0], listPercents[1]));
            Debug.WriteLine(ReferenceEquals(listPercents[0], listPercents[2]));
            Debug.WriteLine(ReferenceEquals(listPercents[1], listPercents[2]));
            /*
            False
            False
            False
            */
        }

        private const string StateOpen = "open";
        private const string StateClose = "close";
        private const string StateLose = "lose_";
        private const string StateWin = "win_";


        [Test]
        public void TestString8Shit()
        {
            var list = new List<StringTestClass>
            {
                new StringTestClass(),
                new StringTestClass(),
                new StringTestClass(),
            };

            SetButtonsText(list, true);

            /*
            */
        }

        private void SetButtonsText(List<StringTestClass> listPercents, bool open)
        {
            string openValue = open ? StateOpen : StateClose;

            foreach (var button in listPercents)
            {
                string res = StateLose + openValue;
                Debug.WriteLine(res.GetHashCode());
                button.SetName(res);
            }

            Debug.WriteLine(ReferenceEquals(listPercents[0], listPercents[1]));
            Debug.WriteLine(ReferenceEquals(listPercents[0], listPercents[2]));
            Debug.WriteLine(ReferenceEquals(listPercents[1], listPercents[2]));
            /*
            313926663
            313926663
            313926663
            False
            False
            False
            */
        }
    }
}
