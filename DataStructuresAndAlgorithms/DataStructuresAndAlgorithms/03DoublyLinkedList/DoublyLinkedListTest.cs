using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.DoublyLinkedList
{
    [TestClass]
    public class DoublyLinkedListTest
    {
        [TestMethod]
        public void Test1()
        {
            DoublyLinkedList<int> instance = new DoublyLinkedList<int>
            {
            };

            instance.AddFirst(5);
            instance.AddFirst(3);
            instance.AddLast(7);

            PrintValues(instance, "Doubly linked list");

            #region Удаление элемента из списка

            instance.Remove(5);

            PrintValues(instance, "Doubly linked list");

            #endregion

            #region Определение принадлежит ли элемент списку

            Debug.WriteLine(instance.Contains(3));

            #endregion

            #region Копирование списка в массив

            Debug.WriteLine("Копирование списка в массив");

            int[] arr = new int[5];

            instance.CopyTo(arr, 1);

            for (int i = 0; i < arr.Length; i++)
            {
                Debug.WriteLine(arr[i]);
            }
            #endregion

            #region Удаление последнего элемента из списка

            instance.RemoveLast();

            PrintValues(instance, "Doubly linked List");

            #endregion

            #region Удаление списка

            instance.Clear();

            PrintValues(instance, "List is clear");

            #endregion
        }

        public static void PrintValues(DoublyLinkedList<int> words, string test)
        {
            Debug.WriteLine(test);
            foreach (int word in words)
            {
                Console.Write(word + " ");
            }
            Debug.WriteLine("");
            Debug.WriteLine("");
        }
    }
}
