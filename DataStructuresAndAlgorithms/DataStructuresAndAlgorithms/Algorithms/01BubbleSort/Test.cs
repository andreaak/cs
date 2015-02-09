using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.BubbleSort
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            int[] array = new int[] { 5, 4, 3, 2, 1 }; // O(n)
            Debug.WriteLine("Base Array\n");
            PrintValues(array);
            Debug.WriteLine("");
            Debug.WriteLine("BubbleSort_1\n");
            BubbleSort_1(array); // O(1)
            Debug.WriteLine("BubbleSort_2\n");
            array = new int[] { 5, 4, 3, 2, 1 }; // O(n)
            BubbleSort_2(array); // O(1)
        }

        //Алгоритм сортировки пузырьком – это простой алгоритм сортировки, 
        //который выполняя проходы по массиву элементов,каждый раз перемещает наибольший элемент в его конец.

        //Base Array
        //5 4 3 2 1 

        //4 5 3 2 1
        //4 3 5 2 1
        //4 3 2 5 1
        //4 3 2 1 5 

        //3 4 2 1 5
        //3 2 4 1 5
        //3 2 1 4 5

        //2 3 1 4 5
        //2 1 3 4 5
        //1 2 3 4 5

        //2 3 1 4 5 
        //2 1 3 4 5 

        //1 2 3 4 5 

        //Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        //Степень сложности     |   O(n)            |   O(n^2)          | O(n^2)
        //Рост памяти           |   O(1)            |   O(1)            | O(1)
        
        private static void BubbleSort_1(int[] items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            { 
                for (int j = 0; j < items.Length - i - 1; j++)
                {
                    if (items[j] > items[j + 1])
                    {
                        Swap(items, j, j + 1);
                    }
                    PrintValues(items);
                }
                Debug.WriteLine("");
            }
        }

        private static void BubbleSort_2(int[] items)
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
                        Swap(items, i - 1, i);

                        swapped = true;
                    }
                    PrintValues(items);
                }
                maxIndex--;
                Debug.WriteLine("");
            }

            while (swapped != false);
        }

        private static void PrintValues(int[] arr)
        {
            foreach (int i in arr)// O(n)
            {
                Debug.Write(i.ToString() + ' ');
            }
            Debug.WriteLine("");
        }

        private static void Swap(int[] items, int left, int right)
        {
            if (left != right)
            {
                int temp = items[left];// O(1)
                items[left] = items[right];// O(1)
                items[right] = temp;// O(1)
            }
        }

    }
}


