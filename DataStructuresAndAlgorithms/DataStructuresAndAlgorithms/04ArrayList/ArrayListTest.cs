using System.Collections;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.ArrayList
{
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void Test1()
        {
            ArrayList<int> dArr1 = new ArrayList<int>();

            ArrayList<int> dArr2 = new ArrayList<int>(5);

            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr3 = new ArrayList<int>(array);

            PrintValues(dArr1);
            Debug.WriteLine(dArr1.Count);
            PrintValues(dArr2);
            Debug.WriteLine(dArr2.Count);
            PrintValues(dArr3);
            Debug.WriteLine(dArr3.Count);
        }

        [TestMethod]
        public void TestAdd()
        {
            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr = new ArrayList<int>(array);

            dArr.Add(50);

            PrintValues(dArr); 
        }

        [TestMethod]
        public void TestInsert()
        {
            int[] array = { 5, 10, 15, 20 };

            array.CopyTo(array, 0);

            ArrayList<int> dArr = new ArrayList<int>(array);

            dArr.Insert(3, 50);

            PrintValues(dArr);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr = new ArrayList<int>(array);

            PrintValues(dArr);

            dArr.RemoveAt(2);

            PrintValues(dArr);
        }

        [TestMethod]
        public void TestRemove()
        {
            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr = new ArrayList<int>(array);

            PrintValues(dArr);

            dArr.Remove(15);

            PrintValues(dArr);
        }

        [TestMethod]
        public void TestIndexator()
        {
            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr = new ArrayList<int>(array);

            dArr[3] = 55;

            Debug.WriteLine("Элемент под индексом 3: {0} ", dArr[3]);

            PrintValues(dArr);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            int[] array = { 5, 10, 15, 20, 25 };

            ArrayList<int> dArr = new ArrayList<int>(array);

            Debug.WriteLine("Значение 5 пренадлежит массиву: {0} ", dArr.Contains(5));

            Debug.WriteLine(dArr.IndexOf(15));

            PrintValues(dArr);
        }


        public static void PrintValues(IEnumerable obj)
        {

            Debug.WriteLine("Dynamic array");

            foreach (var item in obj)

                Debug.Write(string.Format("   {0}", item));
            Debug.WriteLine("");
        }
    }
}
