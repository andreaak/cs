using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._01_IEnumerable
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
        public void TestEnumerator4()
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
