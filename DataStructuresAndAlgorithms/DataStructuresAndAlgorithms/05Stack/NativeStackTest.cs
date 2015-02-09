using System.Collections;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Stack
{
    [TestClass]
    public class NativeStackTest
    {
        [TestMethod]
        public void Test1()
        {
            //////////////////////////////////// Конструкторы //////////////////////////////////////////////////////////////

            int[] array = { 1, 2, 3, 4, 5 };

            System.Collections.Generic.Stack<int> instance1 = new System.Collections.Generic.Stack<int>();
            System.Collections.Generic.Stack<int> instance2 = new System.Collections.Generic.Stack<int>(10);
            System.Collections.Generic.Stack<int> instance3 = new System.Collections.Generic.Stack<int>(array);

            Debug.WriteLine("Количество элементов в первом стеке: {0} ", instance1.Count);
            Debug.WriteLine("Количество элементов во втором стеке: {0} ", instance2.Count);
            Debug.WriteLine("Количество элементов в третьем стеке: {0}", instance3.Count);

            ///////////////////////////////////////// Методы ///////////////////////////////////////////////////////////////////

            instance1.Push(1);
            instance1.Push(2);
            instance1.Push(3);
            instance1.Push(4);
            instance1.Push(5);

            instance1.Pop();

            Debug.WriteLine("Вершина стека: {0} ", instance1.Peek());

            Debug.WriteLine("Стек содержит значение 2: {0}", instance1.Contains(2));

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Debug.WriteLine("\n Демонстрация метода ToArray \n");

            int[] myStandardArray = instance1.ToArray();

            for (int i = 0; i < myStandardArray.Length; i++)
            {
                Debug.WriteLine(myStandardArray[i]);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Debug.WriteLine("\n Демонстрация метода CopyTo \n");

            int[] Array = new int[10];

            instance1.CopyTo(Array, 3);

            for (int i = 0; i < Array.Length; i++)
            {
                Debug.WriteLine(Array[i]);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Debug.WriteLine("\n Демонстрация метода Clear \n");

            instance1.Clear();

            //  Debug.WriteLine("Вершина стека: {0} ", instance1.Peek()); // Раскомментировать 

        }

    }
}
