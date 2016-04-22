using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.SelectionSort
{
    [TestClass]
    public class Test
    {
        /*
        Сортировка выбором – алгоритм сортировки, который на каждом своем шаге 
        отыскивает наименьший элемент вне отсортированной части массива и устанавливает его в соответствующую позицию массива.

        Этапы сортировки:
        1.Нахождение минимального значения в массиве.
        2.Обмен этого значения со значением первой не отсортированной позиции(обмен не нужен, если минимальный элемент уже находится на данной позиции).
        3.Сортировка остальных элементов списка,исключив из рассмотрения уже отсортированные. 
        
        Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        Степень сложности     |   O(n^2)          |   O(n^2)          | O(n^2)
        Рост памяти           |   O(1)            |   O(1)            | O(1)

        Время работы алгоритма в лучшем, среднем и худшем случаях оценивается как O(n^2) -
        исходный порядок данных никак не влияет на количество сравнений.
        Сортировка выбором относится к локальным алгоритмам. 
        Ее типичная реализация, неустойчива.

        */

        [TestMethod]
        public void TestSelectionSortIterative()
        {
            int[] items = { 3, 7, 4, 4, 6, 5, 8 };
            Debug.WriteLine("Base Array");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("SelectionSort");
            SelectionSort(items);
            /*
            Base Array
            3 7 4 4 6 5 8 

            SelectionSort

            Current index: 0

            Input array
            3 7 4 4 6 5 8 

            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Output array
            3 7 4 4 6 5 8 

            Current index: 1

            Input array
            3 7 4 4 6 5 8 

            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Output array
            3 4 7 4 6 5 8 

            Current index: 2

            Input array
            3 4 7 4 6 5 8 

            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Output array
            3 4 4 7 6 5 8 

            Current index: 3

            Input array
            3 4 4 7 6 5 8 

            Search Smallest Element
            Search Smallest Element
            Search Smallest Element
            Output array
            3 4 4 5 6 7 8 

            Current index: 4

            Input array
            3 4 4 5 6 7 8 

            Search Smallest Element
            Search Smallest Element
            Output array
            3 4 4 5 6 7 8 

            Current index: 5

            Input array
            3 4 4 5 6 7 8 

            Search Smallest Element
            Output array
            3 4 4 5 6 7 8 

            Current index: 6

            Input array
            3 4 4 5 6 7 8 

            Output array
            3 4 4 5 6 7 8
            */
        }

        //iterative
        public void SelectionSort(int[] items)
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                
                Debug.WriteLine("Current index: " + sortedRangeEnd + "\n");
                Debug.WriteLine("Input array");
                items.PrintValues();
                Debug.WriteLine("");

                int nextIndex = FindMinimumIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
                
                Debug.WriteLine("Output array");
                items.PrintValues();
                Debug.WriteLine("");
            }
        }

        // Метод находит минимальный элемент в массиве.
        // Находим элемент с минимальным значением, начиная с данного индекса
        private int FindMinimumIndex(int[] items, int start)
        {
            int minPos = start;
            for (int i = start + 1; i < items.Length; ++i)
            {
                if (items[i] < items[minPos])
                {
                    minPos = i;
                }
            }
            Debug.WriteLine("Smallest Element Index " + minPos);
            return minPos;
        }

        [TestMethod]
        public void TestSelectionSortRecursive()
        {
            int[] items = { 3, 7, 4, 4, 6, 5, 8 };
            Debug.WriteLine("Base Array");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("SelectionSort");
            SelectionSortRecursive(items);
            items.PrintValues();
            /*
            Base Array
            3 7 4 4 6 5 8 

            SelectionSort
            3 4 4 5 6 7 8 
            */
        }

        // Обработка массива процедурой рекурсивной сортировки выбором
        public void SelectionSortRecursive(int[] items)
        {
            SelectionSortRecursive(items, 0);
        }

        // Сортировка подмножества массива, начиная с данного индекса
        private void SelectionSortRecursive(int[] items, int start)
        {
            if (start < items.Length - 1)
            {
                Swap(items, start, FindMinimumIndex(items, start));
                SelectionSortRecursive(items, start + 1);
            }
        }

        [TestMethod]
        public void TestSelectionSortUnStable()
        {
            SelectionUnit[] items = { new SelectionUnit{ X = 5, Y = 1}
            , new SelectionUnit{ X = 3, Y = 1}
            , new SelectionUnit{ X = 5, Y = 2}
            , new SelectionUnit{ X = 2, Y = 1}
            };
            Debug.WriteLine("Base Array");
            items.PrintValues();
            Debug.WriteLine("");
            SelectionSortUnStable(items);
            Debug.WriteLine("");
            Debug.WriteLine("Result Array");
            items.PrintValues();
            /*
            Base Array
            5.1; 3.1; 5.2; 2.1; 

            Smallest Element Index 3
            Smallest Element Index 1
            Smallest Element Index 2

            Result Array
            2.1; 3.1; 5.2; 5.1; 
            */
        }

        // Устойчивая сортировка выбором для массива
        public void SelectionSortUnStable(SelectionUnit[] items)
        {
            for (int start = 0; start < items.Length - 1; ++start)
            {
                Swap(items, start, FindMinimumIndex(items, start));
            }
        }

        [TestMethod]
        public void TestSelectionSortStable()
        {
            SelectionUnit[] items = { new SelectionUnit{ X = 5, Y = 1}
            , new SelectionUnit{ X = 3, Y = 1}
            , new SelectionUnit{ X = 5, Y = 2}
            , new SelectionUnit{ X = 2, Y = 1}
            };
            Debug.WriteLine("Base Array");
            items.PrintValues();
            Debug.WriteLine("");
            SelectionSortStable(items);
            Debug.WriteLine("");
            Debug.WriteLine("Result Array");
            items.PrintValues();
            /*
            Base Array
            5.1; 3.1; 5.2; 2.1; 

            Smallest Element Index 3
            Smallest Element Index 2
            Smallest Element Index 2

            Result Array
            2.1; 3.1; 5.1; 5.2; 
            */
        }

        // Устойчивая сортировка выбором для массива
        public void SelectionSortStable(SelectionUnit[] items)
        {
            for (int start = 0; start < items.Length - 1; ++start)
            {
                Insert(items, start, FindMinimumIndex(items, start));
            }
        }

        // Метод находит минимальный элемент в массиве.
        // Находим элемент с минимальным значением, начиная с данного индекса
        private int FindMinimumIndex(SelectionUnit[] items, int start)
        {
            int minPos = start;
            for (int i = start + 1; i < items.Length; ++i)
            {
                if (items[i].X < items[minPos].X)
                {
                    minPos = i;
                }
            }
            Debug.WriteLine("Smallest Element Index " + minPos);
            return minPos;
        }

        // Вставка данных в массив с их сдвигом при необходимости
        private void Insert(SelectionUnit[] items, int start, int minIndex)
        {
            if (minIndex > start)
            {
                SelectionUnit tmp = items[minIndex];
                Array.Copy(items, start, items, start + 1, minIndex - start);
                items[start] = tmp;
            }
        }

        private void Swap(SelectionUnit[] items, int left, int right)
        {
            if (left != right)
            {
                SelectionUnit temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
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

        /* Возвращает самое большое значение из массива неотрицательных целых чисел */
        int CompareToAll(int[] array, int n)
        {
            int i, j;
            bool isMax;

            /* Проверяем, что массив содержит по крайней мере один элемент. */
            if (n <= 0)
                return -1;

            for (i = n - 1; i > 0; i--)
            {
                isMax = true;
                for (j = 0; j < n; j++)
                {
                    /* Смотрим, существуют ли большие значения. */
                    if (array[j] > array[i])
                    {
                        isMax = false; /* array[i] is not the largest value. */
                        break;
                    }
                }
                /* Если isMax имеет значение true, больших значений не существует;
                максимальный элемент array[i]. */
                if (isMax)
                {
                    break;
                }
            }
            return array[i];
        }
    }
}