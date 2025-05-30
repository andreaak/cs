﻿using CSTest._09_Array._0_Setup;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._09_Array
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestArray1()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            array = new int[5];

            array = new int[5] { 1, 2, 3, 4, 5 };

            array = new int[] { 1, 2, 3, 4, 5 };

            array = new[] { 1, 2, 3, 4, 5 };

            // Вывод на экран значений элементов массива.
            for (int i = 0; i < array.Length; i++)
            {
                Debug.WriteLine(array[i]);
            }
        }

        // Массивы (двумерный массив).
        [Test]
        public void TestArray2TwoDimensional()
        {
            Random random = new Random();

            int[,] array = new int[3, 3];


            // Заполнение массива случайными значениями.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    array[i, j] = random.Next(0, 10);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Debug.Write(array[i, j] + " ");
                }
                Debug.Write("\n");
            }
        }

        [Test]
        public void TestArray3ArrayMultiDim()
        {
            int[,,] threeDim = {
                                  {{1, 2},{ 3, 4}},
                                  {{5, 6}, {7, 8}}
                                };

            // Получаем количество подмассивов в Массиве - GetLength(0). - 2 подмассива.
            for (int i = 0; i < threeDim.GetLength(0); ++i)
            {
                // Получаем количество элементов в Подмассиве - GetLength(1). - 4 элемента в каждом подмассиве.
                for (int j = 0; j < threeDim.GetLength(1); ++j)
                {
                    for (int k = 0; k < threeDim.GetLength(2); k++)
                    {
                        Debug.Write(threeDim[i, j, k] + ", ");
                    }
                    Debug.WriteLine("");
                }
            }
            Debug.WriteLine(threeDim.Length);
        }

        [Test]
        public void TestArray3Jagged()
        {
            int[][] jagged = new int[3][];

            jagged[0] = new int[] { 1, 2 };
            jagged[1] = new int[] { 1, 2, 3, 4, 5 };
            jagged[2] = new int[] { 1, 2, 3 };


            // Во внешнем цикле выполняется проход по всем вложенным массивам.
            for (int i = 0; i < jagged.Length; ++i)
            {
                // Во внутреннем цикле выполняется обращение к каждому элементу вложенного массива.
                for (int j = 0; j < jagged[i].Length; ++j)
                {
                    Debug.Write(jagged[i][j] + " ");
                }
                Debug.Write("\n");
            }
        }

        [Test]
        public void TestArray4UseArrayClass()
        {
            // Все массивы являются производными от класса Array.
            int[] vector = { 1, 2, 3 };

            Array array = vector;

            for (int i = 0; i < array.Length; i++)
            {
                Debug.WriteLine(vector[i]);
            }
        }

        [Test]
        public void TestArray5()
        {
            // Массив Int32.
            var array1 = new[] { 1, 2, 3, 4, 5 };
            Debug.WriteLine(array1.GetType());


            // Массив Doubles.
            var array2 = new[] { 3.1415, 1, 6 };
            Debug.WriteLine(array2.GetType());


            // Не компилируется. (Несовместимые типы)
            // var array3 = new [] { 1, "string" };
        }

        [Test]
        public void TestArray6Covariance()
        {
            Dog[] dogs = { new Dog(), new Dog(), new Dog() };

            for (int i = 0; i < dogs.Length; i++)
            {
                dogs[i].Voice();
                dogs[i].Jump();
            }

            Debug.WriteLine(new string('-', 10));

            IAnimal[] animal = dogs; // Ковариантность.

            for (int i = 0; i < dogs.Length; i++)
            {
                animal[i].Voice();
                //animal[i].Jump();
            }

            Debug.WriteLine(new string('-', 10));

            dogs = (Dog[])animal;   // Не является Контрвариантностью.

            for (int i = 0; i < dogs.Length; i++)
            {
                dogs[i].Voice();
                dogs[i].Jump();
            }
        }

        [Test]
        public void TestArray6Resize()
        {
            Dog[] array = new Dog[] {new Dog()};
            Debug.WriteLine("Size: " + array.Length);
            Array.Resize(ref array, 5);
            Debug.WriteLine("Size: " + array.Length);
            /*
            Size: 1
            Size: 5 
            */
        }
    }
}
