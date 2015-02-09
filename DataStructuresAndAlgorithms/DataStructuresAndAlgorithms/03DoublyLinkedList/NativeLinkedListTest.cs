using System;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.DoublyLinkedList
{
    [TestClass]
    public class NativeLinkedListTest
    {
        [TestMethod]
        public void Test1()
        {
            #region Test1: Создание связанного списка

            string[] words = { "the", "fox", "jumped", "over", "the", "dog" };

            System.Collections.Generic.LinkedList<string> sentence = new System.Collections.Generic.LinkedList<string>(words);

            PrintValues(sentence, "Элементы связанного списка: ");

            #endregion

            #region Test2: Метод Contains определяет, принадлежит ли значение объекту LinkedList<T>.

            Debug.WriteLine("Test 2: List sentence contains \"jumped\" = {0} \n", sentence.Contains("jumped"));

            #endregion

            #region Test3: Добавление слова 'today' в начало связанного списка.

            sentence.AddFirst("today");
            PrintValues(sentence, "Test 3: Добавление слова 'today' в начало связанного списка");

            #endregion

            #region Test 4: Удаления первого элемента с добавлением его в конец списка.

            // Инициализирует новый экземпляр класса LinkedListNode<T> содержащего указанное значение.
            System.Collections.Generic.LinkedListNode<string> mark1 = sentence.First;

            sentence.RemoveFirst();
            sentence.AddLast(mark1);

            PrintValues(sentence, "Test 4: Удаления первого элемента с добавлением его в конец списка");

            #endregion

            #region Test 5: Замена последнего элемента на 'yesterday'.

            sentence.RemoveLast();
            sentence.AddLast("yesterday");
            PrintValues(sentence, "Test 5: Замена последнего элемента на 'yesterday':");

            #endregion

            #region Test 6: Перемещение последнего элемента на первую позицию

            mark1 = sentence.Last;

            sentence.RemoveLast();
            sentence.AddFirst(mark1);
            PrintValues(sentence, "Test 6: Перемещение последнего элемента на первую позицию:");

            #endregion

            #region Test 7: Нахождение последнего из списка искомого элемента


            System.Collections.Generic.LinkedListNode<string> current = sentence.FindLast("the");

            IndicateNode(current, "Test 7: Нахождение последнего из списка искомого элемента 'the':");

            #endregion

            #region Test 8: Добавить 'lazy' и 'old' после элемента 'the'

            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");

            IndicateNode(current, "Test 8: Добавить 'lazy' и 'old' после элемента 'the'");

            PrintValues(sentence, "Show:");

            #endregion

            #region Test 9: Удаление произвольного элемента из списка

            sentence.Remove("lazy");
            PrintValues(sentence, "Удаление произвольного элемента из списка:");

            #endregion
        }

        private static void PrintValues(System.Collections.Generic.LinkedList<string> words, string test)
        {
            Debug.WriteLine(test);
            foreach (string word in words)
            {
                Debug.Write(word + " ");
            }
            Debug.WriteLine("");
            Debug.WriteLine("");
        }

        private static void IndicateNode(System.Collections.Generic.LinkedListNode<string> node, string test)
        {
            Debug.WriteLine(test);

            if (node.List == null)
            {
                Console.WriteLine(" Элемента '{0}' нет в списке. \n", node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");

            // Инициализация нового экземпляр класса LinkedListNode<T> содержащего указанное значение.
            System.Collections.Generic.LinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                Debug.WriteLine(result);
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                Debug.WriteLine(result);
                node = node.Next;
            }

            Debug.WriteLine(result);
            Debug.WriteLine("");
        }
    
    }
}
