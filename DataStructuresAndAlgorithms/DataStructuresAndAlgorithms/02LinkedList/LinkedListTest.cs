using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.LinkedList
{
    [TestClass]
    public class LinkedListTest
    {
        public class Node
        {
            public int Value
            {
                get;
                set;
            }
            public Node Next
            {
                get;
                set;
            }
        }
        
        [TestMethod]
        public void Test1()
        {
            // +-----+------+
            // |  3  | null +
            // +-----+------+
            Node first = new Node
            {
                Value = 3
            };

            // +-----+------+    +-----+------+
            // |  3  | null +    |  5  | null +
            // +-----+------+    +-----+------+
            Node middle = new Node
            {
                Value = 5
            };

            // +-----+------+    +-----+------+
            // |  3  |  *---+--->|  5  | null +
            // +-----+------+    +-----+------+
            first.Next = middle;

            // +-----+------+    +-----+------+   +-----+------+
            // |  3  |  *---+--->|  5  | null +   |  7  | null +
            // +-----+------+    +-----+------+   +-----+------+
            Node last = new Node
            {
                Value = 7
            };

            // +-----+------+    +-----+------+   +-----+------+
            // |  3  |  *---+--->|  5  |  *---+-->|  7  | null +
            // +-----+------+    +-----+------+   +-----+------+
            middle.Next = last;
        }

        [TestMethod]
        public void Test2()
        {
            LinkedListNode<string> first = new LinkedListNode<string>("Hello");
            LinkedListNode<string> second = new LinkedListNode<string>("world");

            // +-------+------+    +-------+-------+ 
            // | Hello |      +    | world | null  +
            // +-------+------+    +-------+-------+ 

            first.Next = second;

            // +-------+------+    +-------+-------+ 
            // | Hello |  *---+--->| world | null  +
            // +-------+------+    +-------+-------+ 

            Debug.WriteLine(first.Value);
            Debug.WriteLine(first.Next.Value);
            Debug.WriteLine(second.Value);
        }

        [TestMethod]
        public void Test3()
        {
            LinkedList<int> instance = new LinkedList<int>
            {
            };

            #region Добавление элементов в список

            instance.Add(12);
            instance.Add(15);
            instance.Add(20);
            instance.Add(25);

            PrintValues(instance, "List");

            #endregion

            #region Удаление элемента списка

            instance.Remove(20);

            PrintValues(instance, "20 was removed");

            #endregion

            #region Копирование списка в массив
            Debug.WriteLine("Копирование списка в массив");

            int[] arr = new int[5];

            instance.CopyTo(arr, 2);

            for (int i = 0; i < arr.Length; i++)
            {
                Debug.WriteLine(arr[i]);
            }

            #endregion

            #region Удаление списка

            instance.Clear();
            PrintValues(instance, "List is cleared");

            #endregion
        }

        public static void PrintValues(LinkedList<int> words, string test)
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
