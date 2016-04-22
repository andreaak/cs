using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.BubbleSort
{
    [TestClass]
    public class Test
    {
        /*
        Алгоритм сортировки пузырьком – это простой алгоритм сортировки, 
        который выполняя проходы по массиву элементов,каждый раз перемещает наибольший элемент в его конец.

        Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        Степень сложности     |   O(n)            |   O(n^2)          | O(n^2)
        Рост памяти           |   O(1)            |   O(1)            | O(1)
        */

        [TestMethod]
        public void TestBubbleSort1()
        {
            int[] items = new int[] { 5, 4, 3, 2, 1 }; // O(n)
            Debug.WriteLine("Base Array\n");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("BubbleSort_1\n");
            BubbleSort_1(items); // O(1)
            /*
            Base Array

            5 4 3 2 1 

            BubbleSort_1

            4 5 3 2 1 
            4 3 5 2 1 
            4 3 2 5 1 
            4 3 2 1 5 

            3 4 2 1 5 
            3 2 4 1 5 
            3 2 1 4 5 

            2 3 1 4 5 
            2 1 3 4 5 

            1 2 3 4 5 
            */
        }

        private void BubbleSort_1(int[] items)
        {
            for (int i = items.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (items[j] > items[j + 1])
                    {
                        items.Swap(j, j + 1);
                    }
                    items.PrintValues();
                }
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TestBubbleSort2()
        {
            int[] items = new int[] { 5, 4, 3, 2, 1 }; // O(n)
            Debug.WriteLine("Base Array\n");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("BubbleSort_2");
            BubbleSort_2(items); // O(1)
            /*
            Base Array

            5 4 3 2 1 

            BubbleSort_2
            4 5 3 2 1 
            4 3 5 2 1 
            4 3 2 5 1 
            4 3 2 1 5 

            3 4 2 1 5 
            3 2 4 1 5 
            3 2 1 4 5 

            2 3 1 4 5 
            2 1 3 4 5 

            1 2 3 4 5 
            */

        }

        private void BubbleSort_2(int[] items)
        {
            bool swapped;
            int maxIndex = items.Length;
            do
            {
                swapped = false;

                for (int i = 1; i < maxIndex; i++)
                {
                    if (items[i - 1].CompareTo(items[i]) > 0)
                    {
                        items.Swap(i - 1, i);

                        swapped = true;
                    }
                    items.PrintValues();
                }
                maxIndex--;
                Debug.WriteLine("");
            }

            while (swapped != false);
        }
    }
}


