using System.Collections;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Queue
{
    [TestClass]
    public class NativeQueueTest
    {
        [TestMethod]
        public void Test1()
        {
            ////////////////////////////////////////// Конструкторы /////////////////////////////////////////////////////////

            Debug.WriteLine("\n Конструкторы \n");

            int[] array = { 1, 2, 3, 4, 5 };

            System.Collections.Generic.Queue<int> instance1 = new System.Collections.Generic.Queue<int>();
            System.Collections.Generic.Queue<int> instance2 = new System.Collections.Generic.Queue<int>(5);
            System.Collections.Generic.Queue<int> instance3 = new System.Collections.Generic.Queue<int>(array);

            ///////////////////////////////////////////// Методы ////////////////////////////////////////////////////////////

            Debug.WriteLine("\n Добавление и удаление элементов с очереди \n");

            Debug.WriteLine("Количество элементов в первой очереди: {0}", instance1.Count);
            Debug.WriteLine("Количество элементов во второй очереди: {0}", instance2.Count);
            Debug.WriteLine("Количество элементов в третьей очереди: {0}", instance3.Count);

            instance1.Enqueue(1);
            instance1.Enqueue(2);
            instance1.Enqueue(3);
            instance1.Enqueue(4);
            instance1.Enqueue(5);

            instance1.Dequeue();

            Debug.WriteLine("Начало очереди: {0}", instance1.Peek());
            
            ///////////////////////////////////////////////////////Contains/////////////////////////////////////////////////////

            Debug.WriteLine("\n Демонстрация метода Contains \n");

            Debug.WriteLine("Очередь содержит значение 3 : {0}", instance1.Contains(3));

            //////////////////////////////////////////////////////ToArray///////////////////////////////////////////////////////

            Debug.WriteLine("\n Демонстрация метода ToArray \n");

            int[] myStandardArray = instance1.ToArray();

            for (int i = 0; i < myStandardArray.Length; i++)
            {
                Debug.WriteLine(myStandardArray[i]);
            }

            //////////////////////////////////////////////////////CopyTo////////////////////////////////////////////////////////////

            Debug.WriteLine("\n Демонстрация метода CopyTo \n");

            int[] Array = new int[10];

            instance1.CopyTo(Array, 3);

            for (int i = 0; i < Array.Length; i++)
            {
                Debug.WriteLine(Array[i]);
            }

            ////////////////////////////////////////////////////////Clear///////////////////////////////////////////////////////////

            Debug.WriteLine("\n Демонстрация метода Clear \n");

            instance1.Clear();

            Debug.WriteLine("Вершина стека: {0} ", instance1.Peek()); // Раскомментировать 

        }

    }
}
