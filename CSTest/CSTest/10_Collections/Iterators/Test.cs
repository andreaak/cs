using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections.Iterators
{
    [TestClass]
    public partial class Test
    {
        [TestMethod]
        public void TestEnumerator1()
        {
            _01_IEnumerable mc = new _01_IEnumerable();
            // Отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            // Вновь отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");

            /*
            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset

            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset
            */
        }

        [TestMethod]
        public void TestEnumerator2()
        {
            _02_IEnumerable mc = new _02_IEnumerable();
            // Отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            // Вновь отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");

            /*
            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset

            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset
            */
        }

        [TestMethod]
        public void TestEnumerator3Generic()
        {
            _03_IEnumerableGeneric mc = new _03_IEnumerableGeneric();
            // Отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            // Вновь отобразить содержимое объекта mc. 
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");

            /*
            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset
            Dispose

            GetEnumerator
            MoveNext
            А MoveNext
            В MoveNext
            C MoveNext
            D MoveNext
            Reset
            Dispose
            */
        }

        [TestMethod]
        public void TestYield4()
        {
            _04_Yield mc = new _04_Yield();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            foreach (char ch2 in mc.IterateEnumerable())
            {
                Debug.Write(ch2 + " ");
            }
            Debug.WriteLine("");
            /*
            yield return
            A yield return
            B yield return
            C yield return
            D 
            yield return
            A yield return
            B yield return
            C yield return
            D 
            */
        }

        [TestMethod]
        public void TestYield5()
        {
            _05_YieldBreak mc = new _05_YieldBreak();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            /*
            yield return
            A yield return
            B yield return
            C yield return
            D yield return
            E yield return
            F yield return
            G yield return
            H yield return
            I yield return
            J yield break
            */
        }

        [TestMethod]
        public void TestYield6()
        {
            _06_YieldMulti mc = new _06_YieldMulti();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            /*
            GetEnumerator
            A yield return
            В yield return
            C yield return
            D yield return
            Е yield return last 
            */
        }

        [TestMethod]
        public void TestYield7()
        {
            _04_Yield mc = new _04_Yield();
            Debug.WriteLine("Возвратить по очереди первые 7 букв:");
            foreach (char ch in mc.MyItr(7))
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("\n");
            Debug.WriteLine("Возвратить по очереди буквы от F до L:");
            foreach (char ch in mc.MyItr(5, 12))
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            /*
            Возвратить по очереди первые 7 букв:
            A B C D E F G 

            Возвратить по очереди буквы от F до L:
            F G H I J K L  
            */
        }

        [TestMethod]
        public void TestYield8()
        {
            int[] nums = { 4, 3, 6, 4, 7, 9 };
            _07_YieldGeneric<int> mc = new _07_YieldGeneric<int>(nums);
            foreach (int x in mc)
            {
                Debug.Write(x + " ");
            }
            Debug.WriteLine("");

            foreach (int x in mc.IterateEnumerable())
            {
                Debug.Write(x + " ");
            }
            Debug.WriteLine("");

            bool[] bVals = { true, true, false, true };
            _07_YieldGeneric<bool> mc2 = new _07_YieldGeneric<bool>(bVals);
            foreach (bool b in mc2)
            {
                Debug.Write(b + " ");
            }
            Debug.WriteLine("");
            /*
            4 3 6 4 7 9 
            4 3 6 4 7 9 
            True True False True 
            */
        }

        [TestMethod]
        public void TestYield9()
        {
            List<int> lst = new List<int> { 1, 2 };
            System.Collections.IEnumerator en = lst.GetEnumerator();
            while (en.MoveNext())
            {
                Debug.WriteLine(en.Current);
            }
        }
    }
}
