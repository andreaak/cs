using System.Collections;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.ArrayList
{
    [TestClass]
    public class NativeArrayListTest
    {
        [TestMethod]
        public void Test1()
        {
            int[] mass = { 6, 7, 8, 9, 10, 1, 2, 3, 4, 5 };

            System.Collections.ArrayList arr = new System.Collections.ArrayList(mass);

            // arr.Capacity = 11;            
            PrintValues(arr);

            #region Демострация метода IndexOf
            Debug.WriteLine("Демострация метода IndexOf {0} \n", arr.IndexOf(4));
            #endregion

            #region Демострация метода BinarySearch
            Debug.WriteLine("Демострация метода BinarySearch {0} \n", arr.BinarySearch(4));
            #endregion


            #region Демонстрация метода Sort

            Debug.WriteLine("Демонстрация метода Sort");
            arr.Sort();
            PrintValues(arr);

            #endregion

            #region Демонстрация метода Reverse

            Debug.WriteLine("Демонстрация метода Reverse");
            arr.Reverse();
            PrintValues(arr);

            #endregion

            Debug.WriteLine("Емкость внутреннего массива {0}", arr.Capacity);

            #region Добавление коллекции в конец существующей колекции

            int[] a = { 1, 11, 111 };

            Debug.WriteLine("Добавление коллекции в в конец существующей колекции");
            arr.AddRange(a);
            PrintValues(arr);

            #endregion

            #region Увиличение емкости внутреннего массива

            Debug.WriteLine("Емкость внутреннего массива {0}", arr.Capacity);

            #endregion

        }

        [TestMethod]
        public void TestGrowArray()
        {
            System.Collections.ArrayList arr = new System.Collections.ArrayList();

            // arr.Capacity = 15;

            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(4);

            arr.Insert(2, 50);
            //arr.RemoveAt(4);
            //arr.RemoveAt(3);

            //Debug.WriteLine(arr[4]);

            Debug.WriteLine("Размер внутреннего массива: {0} ", arr.Capacity);
            Debug.WriteLine("Количество элементов массива: {0} ", arr.Count);

            PrintValues(arr);

        }

        public static void PrintValues(IEnumerable obj)
        {

            Debug.WriteLine("Dynamic array");

            foreach (var item in obj)
            {
                Debug.Write(string.Format("   {0}", item));
            }
            Debug.WriteLine("");
            Debug.WriteLine("");
        }
    }
}
