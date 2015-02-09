using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.SelectionSort
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            int[] array = { 3, 7, 4, 4, 6, 5, 8 };
            Debug.WriteLine("Base Array\n");
            PrintValues(array);
            Debug.WriteLine("");
            Debug.WriteLine("SelectionSort\n");
            SelectionSort(array);
            Debug.WriteLine("");
        }

        //Сортировка выбором – алгоритм сортировки, который на каждом своем шаге 
        //отыскивает наименьший элемент вне отсортированной части массива и устанавливает его в соответствующую позицию массива.

        //Этапы сортировки:
        //1.Нахождение минимального значения в массиве.
        //2.Обмен этого значения со значением первой не отсортированной позиции(обмен не нужен, если минимальный элемент уже находится на данной позиции).
        //3.Сортировка остальных элементов списка,исключив из рассмотрения уже отсортированные. 

        //Base Array
        //3 7 4 4 6 5 8 

        //SelectionSort

        //Current index: 0

        //Input array
        //3 7 4 4 6 5 8 

        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Output array
        //3 7 4 4 6 5 8 

        //Current index: 1

        //Input array
        //3 7 4 4 6 5 8 

        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Output array
        //3 4 7 4 6 5 8 

        //Current index: 2

        //Input array
        //3 4 7 4 6 5 8 

        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Output array
        //3 4 4 7 6 5 8 

        //Current index: 3

        //Input array
        //3 4 4 7 6 5 8 

        //Search Smallest Element
        //Search Smallest Element
        //Search Smallest Element
        //Output array
        //3 4 4 5 6 7 8 

        //Current index: 4

        //Input array
        //3 4 4 5 6 7 8 

        //Search Smallest Element
        //Search Smallest Element
        //Output array
        //3 4 4 5 6 7 8 

        //Current index: 5

        //Input array
        //3 4 4 5 6 7 8 

        //Search Smallest Element
        //Output array
        //3 4 4 5 6 7 8 

        //Current index: 6

        //Input array
        //3 4 4 5 6 7 8 

        //Output array
        //3 4 4 5 6 7 8


        //Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        //Степень сложности     |   O(n)            |   O(n^2)          | O(n^2)
        //Рост памяти           |   O(1)            |   O(1)            | O(1)

        static public void SelectionSort(int[] items)
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                
                Debug.WriteLine("Current index: " + sortedRangeEnd + "\n");
                Debug.WriteLine("Input array");
                PrintValues(items);
                Debug.WriteLine("");

                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
                
                Debug.WriteLine("Output array");
                PrintValues(items);
                Debug.WriteLine("");
            }
        }

        // Метод находит минимальный элемент в массиве.
        static private int FindIndexOfSmallestFromIndex(int[] items, int sortedRangeEnd)
        {
            int currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
                Debug.WriteLine("Search Smallest Element");
            }

            return currentSmallestIndex;
        }

        static void Swap(int[] items, int left, int right)
        {
            if (left != right)
            {
                int temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }

        private static void PrintValues(int[] arr)
        {
            foreach (int i in arr)// O(n)
            {
                Debug.Write(i.ToString() + ' ');
            }
            Debug.WriteLine("");
        }
    }
}