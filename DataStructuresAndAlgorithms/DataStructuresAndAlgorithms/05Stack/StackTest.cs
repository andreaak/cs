using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Stack
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void Test1()
        {
            StackLinkedList<int> instance = new StackLinkedList<int>();

            // Добавление элементов в стек                 // |           |
            instance.Push(10);                             // |     35    |   <----  Указатель на вершину стека
            instance.Push(15);                             // |     25    |
            instance.Push(25);                             // |     15    | 
            instance.Push(35);                             // |     10    |
            // +___________+ 

            Debug.WriteLine("Вершина стека: {0}", instance.Peek());

            // Извлечение двух элементов из массива

            int a = instance.Pop();
            instance.Pop();

            Debug.WriteLine("a={0}", a);

            Debug.WriteLine("");
            Debug.WriteLine("Вершина стека: {0}", instance.Peek());
            Debug.WriteLine("Кличество элементов в стеке: {0}", instance.Count);   
        }

        [TestMethod]
        public void Test2()
        {
            StackArray<int> instance = new StackArray<int>();

            // Добавление элементов в стек                 // |           |
            instance.Push(10);                             // |     35    |   <----  Указатель на вершину стека
            instance.Push(15);                             // |     25    |
            instance.Push(25);                             // |     15    | 
            instance.Push(35);                             // |     10    |
            // +___________+ 

            Debug.WriteLine("Вершина стека: {0}", instance.Peek());

            // Извлечение двух элементов из массива

            int a = instance.Pop();
            instance.Pop();

            Debug.WriteLine("a={0}", a);

            Debug.WriteLine("");
            Debug.WriteLine("Вершина стека: {0}", instance.Peek());
            Debug.WriteLine("Кличество элементов в стеке: {0}", instance.Count);
        }

        [TestMethod]
        public void Test3()
        {
            StackLinkedList2<int> instance = new StackLinkedList2<int>();

            // Добавление элементов в стек                 // |           |
            instance.Push(10);                             // |     35    |   <----  Указатель на вершину стека
            instance.Push(15);                             // |     25    |
            instance.Push(25);                             // |     15    | 
            instance.Push(35);                             // |     10    |
            // +___________+ 

            Debug.WriteLine("Вершина стека: {0}", instance.Peek());

            // Извлечение двух элементов из массива

            int a = instance.Pop();
            instance.Pop();

            Debug.WriteLine("a={0}", a);

            Debug.WriteLine("");
            Debug.WriteLine("Вершина стека: {0}", instance.Peek());
            Debug.WriteLine("Кличество элементов в стеке: {0}", instance.Count);
        }
    }
}
