using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructuresAndAlgorithms.Algoritms;

namespace DataStructuresAndAlgorithms.Algoritms.InsertionSort
{
    [TestClass]
    public class Test
    {
        /*
        
        Алгоритм сортировки вставками – это простой алгоритм, 
        на каждом шаге которого выбирается один из элементов массива, 
        для которого осуществляется поиск нужной позиции в отсортированном массиве.
        При нахождении нужной позиции, массив ячеек сдвигается (длина сдвигаемой части: нужная позиция - позиция текущего элемента), 
        а значение текущего элемента заносится в нужную позицую. 
        Алгоритм выполняется до тех пор, пока набор входных данных не будет исчерпан.
        Выбор очередного элемента из исходного осуществляется произвольно.
        Этапы сортировки:
        1.Нахождение нужной позиции в отсортированном массиве. Если позиция не отличается от текущей то переходим на пункт 5.
        2.Сохранение значения текущего элемента в временной переменной
        3.Сдвиг подмассива ячеек с нужной позиции на 1 ячейку(затронутый подмассив: нужная позиция : позиция текущего элемента - 1).
        4.Значение текущего элемента из временной переменной заносится в нужную позицую
        5.Сортировка остальных элементов списка,исключив из рассмотрения уже отсортированные.


        Сложность алгоритма   |   Лучший вариант  |   Средний вариант | Худший вариант
        Степень сложности     |   O(n)            |   O(n^2)          | O(n^2)
        Рост памяти           |   O(1)            |   O(1)            | O(1)
        
        В отличие от сортировки выбором время работы алгоритма сортировки вставками 
        в лучшем случае (при уже отсортированном списке) оценивается как O(n). 
        Фактически сортировка вставками предлагает эффективный способ добавления новых элементов в предварительно отсортированный список. 
        Сложность алгоритма в среднем и худшем случаях оценивается как O(n2), 
        то есть это не лучший выбор для больших наборов данных, расположенных в случайном порядке. 
        Сортировка вставками представляет собой устойчивый локальный алгоритм, особенно подходящий для обработки небольших наборов данных. 
        Часто он становится основной для более сложных алгоритмов сортировки.
        */


        [TestMethod]
        public void Test1()
        {
            int[] items = { 3, 7, 4, 4, 6, 5, 8 };
            Debug.WriteLine("Base Array\n");
            items.PrintValues();
            Debug.WriteLine("");
            Debug.WriteLine("InsertionSort\n");
            InsertionSort(items);
            Debug.WriteLine("");

            /*
            
            Base Array
            3 7 4 4 6 5 8 

            InsertionSort

            Current index: 1

            Input array
            3 7 4 4 6 5 8 

            Output array
            3 7 4 4 6 5 8 

            Current index: 2

            Input array
            3 7 4 4 6 5 8 

            Search Index
            Start Shift
            3 4 4 4 6 5 8 
            3 4 4 4 6 5 8 
            3 4 7 4 6 5 8 
            End Shift

            Output array
            3 4 7 4 6 5 8 

            Current index: 3

            Input array
            3 4 7 4 6 5 8 

            Search Index
            Search Index
            Start Shift
            3 4 4 4 6 5 8 
            3 4 4 4 6 5 8 
            3 4 4 7 6 5 8 
            End Shift

            Output array
            3 4 4 7 6 5 8 

            Current index: 4

            Input array
            3 4 4 7 6 5 8 

            Search Index
            Search Index
            Search Index
            Start Shift
            3 4 4 6 6 5 8 
            3 4 4 6 6 5 8 
            3 4 4 6 7 5 8 
            End Shift

            Output array
            3 4 4 6 7 5 8 

            Current index: 5

            Input array
            3 4 4 6 7 5 8 

            Search Index
            Search Index
            Search Index
            Start Shift
            3 4 4 5 7 5 8 
            3 4 4 5 7 7 8 
            3 4 4 5 5 7 8 
            3 4 4 5 6 7 8 
            End Shift

            Output array
            3 4 4 5 6 7 8 

            Current index: 6

            Input array
            3 4 4 5 6 7 8 

            Output array
            3 4 4 5 6 7 8 

            
            */
        }

        public void InsertionSort(int[] items)
        {
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length)
            {
                Debug.WriteLine("Current index: " + sortedRangeEndIndex + "\n");
                Debug.WriteLine("Input array");
                items.PrintValues();
                Debug.WriteLine("");

                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }

                Debug.WriteLine("Output array");
                items.PrintValues();
                Debug.WriteLine("");
                sortedRangeEndIndex++;
            }
        }

        private int FindInsertionIndex(int[] items, int valueToInsert)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].CompareTo(valueToInsert) > 0)
                {
                    return i;
                }
                Debug.WriteLine("Search Index");
            }

            throw new InvalidOperationException("Индекс не найден");
        }

        // Вставка данных в массив с их сдвигом при необходимости
        private void Insert(int[] items, int indexInsertingTo, int indexInsertingFrom)
        {
            Debug.WriteLine("Start Shift");

            if (indexInsertingFrom > indexInsertingTo)
            {
                int tmp = items[indexInsertingFrom];
                Array.Copy(items, indexInsertingTo, items, indexInsertingTo + 1, indexInsertingFrom - indexInsertingTo);
                items[indexInsertingTo] = tmp;
            }

            items.PrintValues();
            Debug.WriteLine("End Shift\n");
        }

        //private void Insert(int[] items, int indexInsertingTo, int indexInsertingFrom)
        //{
                /*
                itemArray =       0 1 2 4 5 6 3 7     
                insertingAt =     3     
                insertingFrom =   6     

                 Действия      
                  1: Сохранить значение 4 в temp -> temp = 4     
                  2: Записать 3 по индексу 3: 0 1 2 3 5 6 3 7 -> itemArray[3] = 3     
                  3: Сдвинуть массив на одно значение в право, от индекса 4 до 6:     
                     itemArray =       0 1 2 3 5 6 3 7
                     itemArray =       0 1 2 3 5 6 6 7   temp = 4
                     itemArray =       0 1 2 3 5 5 6 7   temp = 4

                  4: Запись значения переменной temp по индексу 4.     
                     0 1 2 3 4 5 6 7   temp = 4  
                */
        
        //    //// Шаг 1.     
        //    //int temp = items[indexInsertingTo];

        //    //// Шаг 2.  
        //    //items[indexInsertingTo] = items[indexInsertingFrom];
        //    //items.PrintValues();

        //    //// Шаг 3.     
        //    //for (int current = indexInsertingFrom; current > indexInsertingTo; current--)
        //    //{
        //    //    items[current] = items[current - 1];
        //    //    items.PrintValues();
        //    //}

        //    //// Шаг 4.     
        //    //items[indexInsertingTo + 1] = temp;
        //}
    }
}