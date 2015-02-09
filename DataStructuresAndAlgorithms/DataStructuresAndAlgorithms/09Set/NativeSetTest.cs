using System.Collections;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Set
{
    [TestClass]
    public class NativeSetTest
    {
        [TestMethod]
        public void Test1()
        {
            int[] array = { 3, 4, 5, 6, 7, 8, 9 };
            int[] arr = { 1, 3, 5, 7, 9 };

            System.Collections.Generic.HashSet<int> set1 = new System.Collections.Generic.HashSet<int>();
            System.Collections.Generic.HashSet<int> set2 = new System.Collections.Generic.HashSet<int>(array);
            System.Collections.Generic.HashSet<int> set3 = new System.Collections.Generic.HashSet<int>(arr);

            #region Добавление нового элемента в множество

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);
            set1.Add(5);

            #endregion

            #region Объединение множеств

            Debug.Write("set1:");
            PrintValues(set1);

            Debug.Write(" UnionWith set2:");
            PrintValues(set2);

            Debug.Write(" = set1: ");
            set1.UnionWith(set2);
            PrintValues(set1);

            Debug.WriteLine("\n\n");

            #endregion

            #region Разность множеств

            Debug.Write("set1:");
            PrintValues(set1);

            Debug.Write(" ExceptWith set2:");
            PrintValues(set2);

            Debug.Write(" = set1: ");
            set1.ExceptWith(set2);
            PrintValues(set1);

            Debug.WriteLine("\n\n");

            #endregion

            #region Пересечение двух множеств

            Debug.Write("set1:");
            PrintValues(set1);

            Debug.Write(" IntersectWith set3:");
            PrintValues(set3);

            Debug.Write(" = set1: ");
            set1.IntersectWith(set3);
            PrintValues(set1);

            Debug.WriteLine("\n\n");

            #endregion

            #region Симметрическая разность множеств

            Debug.Write("set2:");
            PrintValues(set2);

            Debug.Write(" IntersectWith set3:");
            PrintValues(set3);

            Debug.Write(" = set2: ");
            set2.SymmetricExceptWith(set3);
            PrintValues(set2);

            Debug.WriteLine("\n\n");

            #endregion

            #region Подмножество

            Debug.Write("set1: ");
            PrintValues(set1);

            Debug.Write(" set3: ");
            PrintValues(set3);

            Debug.WriteLine("\n\n");

            Debug.Write("Множество set1 являелся подмножеством set3 = ");

            Debug.Write(set1.IsSubsetOf(set3));

            Debug.WriteLine("\n\n");

            #endregion

            #region Копирование множества в массив

            int[] mass = new int[10];

            set3.CopyTo(mass);

            Debug.WriteLine("Копирование множества в массив: ");

            for (int i = 0; i < mass.Length; i++)
            {
                Debug.WriteLine(mass[i]);
            }

            #endregion

            #region Удаление множества

            Debug.WriteLine(" \n Удаление множества set3 \n");

            Debug.Write("Множество set3: ");
            PrintValues(set3);

            set3.Clear();

            PrintValues(set3);

            Debug.WriteLine(" Множество удалено");
            Debug.Write(" \n \n");

            #endregion



        }

        static void PrintValues(System.Collections.Generic.HashSet<int> set)
        {
            foreach (var item in set)
            {
                Debug.Write(item);
            }
        }
    }
}
