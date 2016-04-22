using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algoritms.MergeSort
{
    [TestClass]
    public class Test
    {
        /*
        Сортировка слиянием – алгоритм сортировки который упорядочивает массив в определённом порядке.
        Реализует принцип «разделяй и властвуй», сначала массив разбивается на части,
        после чего каждая часть сортируется отдельно и объединяется в общее решение.
        Этапы сортировки:
            1.Массив разбивается на два подмассива примерно одинакового размера.
            Рекурсивное разбиение массива на подмассивы происходит до тех пор,
            пока размер подмассива не достигнет единицы(любой массив длины 1 можно считать упорядоченным).
            2.Каждая из частей сортируется отдельно.
            3.Два упорядоченные подмассивы соединяются в один.
        Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        Степень сложности     |   O(n log n)      |   O(n log n)      | O(n log n)
        Рост памяти           |   O(n)            |   O(n)            | O(n)
        */

        [TestMethod]
        public void TestMergeSort()
        {
            int[] items = { 3, 8, 2, 1, 5, 4, 6, 7 };
            Debug.WriteLine("Base Array");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("MergeSort\n");
            MergeSort(items);
            Debug.WriteLine("");
            /*
            Base Array
            3 8 2 1 5 4 6 7 
            MergeSort

            Input array
            3 8 2 1 5 4 6 7 
            Input array
            3 8 2 1 
            Input array
            3 8 
            Input array
            3 
            Input array
            8 
            Process array
            3 8 
            Left array
            3 
            Right array
            8 
            Start Merge
            Merge
            3 8 
            Merge
            3 8 
            Input array
            2 1 
            Input array
            2 
            Input array
            1 
            Process array
            2 1 
            Left array
            2 
            Right array
            1 
            Start Merge
            Merge
            1 1 
            Merge
            1 2 
            Process array
            3 8 2 1 
            Left array
            3 8 
            Right array
            1 2 
            Start Merge
            Merge
            1 8 2 1 
            Merge
            1 2 2 1 
            Merge
            1 2 3 1 
            Merge
            1 2 3 8 
            Input array
            5 4 6 7 
            Input array
            5 4 
            Input array
            5 
            Input array
            4 
            Process array
            5 4 
            Left array
            5 
            Right array
            4 
            Start Merge
            Merge
            4 4 
            Merge
            4 5 
            Input array
            6 7 
            Input array
            6 
            Input array
            7 
            Process array
            6 7 
            Left array
            6 
            Right array
            7 
            Start Merge
            Merge
            6 7 
            Merge
            6 7 
            Process array
            5 4 6 7 
            Left array
            4 5 
            Right array
            6 7 
            Start Merge
            Merge
            4 4 6 7 
            Merge
            4 5 6 7 
            Merge
            4 5 6 7 
            Merge
            4 5 6 7 
            Process array
            3 8 2 1 5 4 6 7 
            Left array
            1 2 3 8 
            Right array
            4 5 6 7 
            Start Merge
            Merge
            1 8 2 1 5 4 6 7 
            Merge
            1 2 2 1 5 4 6 7 
            Merge
            1 2 3 1 5 4 6 7 
            Merge
            1 2 3 4 5 4 6 7 
            Merge
            1 2 3 4 5 4 6 7 
            Merge
            1 2 3 4 5 6 6 7 
            Merge
            1 2 3 4 5 6 7 7 
            Merge
            1 2 3 4 5 6 7 8 
            */
        }

        static public void MergeSort(int[] items)
        {
            // Если массив содержит один элемент - прервать выполнение метода  
            Debug.WriteLine("Input array");
            items.PrintValues();

            if (items.Length <= 1)
            {
                return;
            }

            // 

            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;
            int[] left = new int[leftSize];
            int[] right = new int[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);

            // Рекурсивное деление массива

            MergeSort(left);
            MergeSort(right);
            
            Debug.WriteLine("Process array");
            items.PrintValues();
            Debug.WriteLine("Left array");
            items.PrintValues();
            Debug.WriteLine("Right array");
            items.PrintValues();
            Debug.WriteLine("Start Merge");
            Merge(items, left, right);

        }

        static private void Merge(int[] items, int[] left, int[] right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;
            int remaining = left.Length + right.Length; // общая длинна правой и левой части сортируемого массива

            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    items[targetIndex] = right[rightIndex++];
                }

                else if (rightIndex >= right.Length)
                {
                    items[targetIndex] = left[leftIndex++];
                }

                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    items[targetIndex] = left[leftIndex++];
                }

                else
                {
                    items[targetIndex] = right[rightIndex++];
                }
                Debug.WriteLine("Merge");
                items.PrintValues();
                targetIndex++;
                remaining--;
            }
        }
    }
}