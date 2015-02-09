using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.QuickSort
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            int[] array = { 3, 7, 4, 4, 6, 5, 8, 12, 19, 2, 0 };
            Debug.WriteLine("Base Array");
            PrintValues(array);
            Debug.WriteLine("");
            Debug.WriteLine("QuickSort");
            QuickSort(array);
            Debug.WriteLine("");
        }

        //Быстрая сортировка – алгоритм сортировки, который является наиболее быстрым из известных методов сортировки массива со степенью роста сложности О(n log n).
        //Этапы сортировки:
        //1) Случайный выбор опорного элемента массива(pivotValue), относительно которого переупорядочиваются элементы массива.
        //2) Переместить все значения которые больше опорного вправо, а все значения что меньше опорного влево.
        //3) Повторить алгоритм для неотсортированной левой и правой части массива, пока каждый элемент не окажется на своей позиции.

        //Base Array
        //3 7 4 4 6 5 8 12 19 2 0 

        //QuickSort

        //Before Partition
        //3 7 4 4 6 5 8 12 19 2 0 
        //Left Index: 0
        //Right Index: 10
        //Pivot Index: 2
        //Before Partition
        //3 7 0 4 6 5 8 12 19 2 4 
        //Start Partition
        //3 7 0 4 6 5 8 12 19 2 4 
        //3 7 0 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 7 4 6 5 8 12 19 2 4 
        //3 0 2 4 6 5 8 12 19 7 4 
        //End Partition
        //3 0 2 4 6 5 8 12 19 7 4 
        //New Pivot index: 3


        //Before Partition
        //3 0 2 4 6 5 8 12 19 7 4 
        //Left Index: 0
        //Right Index: 2
        //Pivot Index: 0
        //Before Partition
        //2 0 3 4 6 5 8 12 19 7 4 
        //Start Partition
        //2 0 3 4 6 5 8 12 19 7 4 
        //2 0 3 4 6 5 8 12 19 7 4 
        //End Partition
        //2 0 3 4 6 5 8 12 19 7 4 
        //New Pivot index: 2


        //Before Partition
        //2 0 3 4 6 5 8 12 19 7 4 
        //Left Index: 0
        //Right Index: 1
        //Pivot Index: 0
        //Before Partition
        //0 2 3 4 6 5 8 12 19 7 4 
        //Start Partition
        //0 2 3 4 6 5 8 12 19 7 4 
        //End Partition
        //0 2 3 4 6 5 8 12 19 7 4 
        //New Pivot index: 1


        //Before Partition
        //0 2 3 4 6 5 8 12 19 7 4 
        //Left Index: 4
        //Right Index: 10
        //Pivot Index: 6
        //Before Partition
        //0 2 3 4 6 5 4 12 19 7 8 
        //Start Partition
        //0 2 3 4 6 5 4 12 19 7 8 
        //0 2 3 4 6 5 4 12 19 7 8 
        //0 2 3 4 6 5 4 12 19 7 8 
        //0 2 3 4 6 5 4 12 19 7 8 
        //0 2 3 4 6 5 4 12 19 7 8 
        //0 2 3 4 6 5 4 7 19 12 8 
        //End Partition
        //0 2 3 4 6 5 4 7 8 12 19 
        //New Pivot index: 8


        //Before Partition
        //0 2 3 4 6 5 4 7 8 12 19 
        //Left Index: 4
        //Right Index: 7
        //Pivot Index: 6
        //Before Partition
        //0 2 3 4 6 5 7 4 8 12 19 
        //Start Partition
        //0 2 3 4 6 5 7 4 8 12 19 
        //0 2 3 4 6 5 7 4 8 12 19 
        //0 2 3 4 6 5 7 4 8 12 19 
        //End Partition
        //0 2 3 4 4 5 7 6 8 12 19 
        //New Pivot index: 4


        //Before Partition
        //0 2 3 4 4 5 7 6 8 12 19 
        //Left Index: 5
        //Right Index: 7
        //Pivot Index: 5
        //Before Partition
        //0 2 3 4 4 6 7 5 8 12 19 
        //Start Partition
        //0 2 3 4 4 6 7 5 8 12 19 
        //0 2 3 4 4 6 7 5 8 12 19 
        //End Partition
        //0 2 3 4 4 5 7 6 8 12 19 
        //New Pivot index: 5


        //Before Partition
        //0 2 3 4 4 5 7 6 8 12 19 
        //Left Index: 6
        //Right Index: 7
        //Pivot Index: 6
        //Before Partition
        //0 2 3 4 4 5 6 7 8 12 19 
        //Start Partition
        //0 2 3 4 4 5 6 7 8 12 19 
        //End Partition
        //0 2 3 4 4 5 6 7 8 12 19 
        //New Pivot index: 7


        //Before Partition
        //0 2 3 4 4 5 6 7 8 12 19 
        //Left Index: 9
        //Right Index: 10
        //Pivot Index: 9
        //Before Partition
        //0 2 3 4 4 5 6 7 8 19 12 
        //Start Partition
        //0 2 3 4 4 5 6 7 8 19 12 
        //End Partition
        //0 2 3 4 4 5 6 7 8 12 19 
        //New Pivot index: 9      
        
        //Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        //Степень сложности     |   O(n log n)      |   O(n log n)      | O(n^2)
        //Рост памяти           |   O(1)            |   O(1)            | O(1)

        static public void QuickSort(int[] items)
        {
            quicksort(items, 0, items.Length - 1);
        }

        static private void quicksort(int[] items, int leftIndex, int rightIndex)
        {
            Random _pivotRng = new Random();

            if (leftIndex < rightIndex)
            {
                // Генератор псевдослучайных чисел. Определение точки раздела массива.               
                int pivotIndex = _pivotRng.Next(leftIndex, rightIndex);

                int newPivot = partition(items, leftIndex, rightIndex, pivotIndex);
                quicksort(items, leftIndex, newPivot - 1);
                quicksort(items, newPivot + 1, rightIndex);
            }
        }

        static private int partition(int[] items, int leftIndex, int rightIndex, int pivotIndex)
        {
            int pivotValue = items[pivotIndex];

            Debug.WriteLine("\nBefore Partition");
            PrintValues(items);
            Debug.WriteLine("Left Index: " + leftIndex);
            Debug.WriteLine("Right Index: " + rightIndex);
            Debug.WriteLine("Pivot Index: " + pivotIndex);

            Debug.WriteLine("Before Partition");
            Swap(items, pivotIndex, rightIndex);
            PrintValues(items);

            Debug.WriteLine("Start Partition");
            int storeIndex = leftIndex;

            for (int i = leftIndex; i < rightIndex; i++)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
                PrintValues(items);
            }

            // Перестановка значений местами.
            Swap(items, storeIndex, rightIndex);
            Debug.WriteLine("End Partition");
            PrintValues(items);
            Debug.WriteLine("New Pivot index: " + storeIndex);
            return storeIndex;
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