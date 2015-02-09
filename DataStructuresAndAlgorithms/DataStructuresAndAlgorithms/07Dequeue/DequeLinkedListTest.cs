using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Deque
{
    [TestClass]
    public class DequeLinkedListTest
    {
        [TestMethod]
        public void Test1()
        {
            DequeLinkedList<int> instance = new DequeLinkedList<int>();

            // First -> указатель на начало очереди
            // Last  -> указатель на конец очереди

            instance.EnqueueLast(1);    // -> -> -> -> -> -> ->  
            instance.EnqueueLast(2);    // Двусвязная очередь: 5 -> 4 -> 3 -> 2 -> 1
            instance.EnqueueLast(3);    // -> -> -> -> -> -> ->
            instance.EnqueueLast(4);
            instance.EnqueueLast(5);

            Debug.WriteLine(instance.PeekFirst()); // возвращает первый элемент очереди
            Debug.WriteLine(instance.PeekLast());  // возвращает последний элемент очереди 

            instance.EnqueueFirst(6);     // -> -> -> -> -> -> -> 
            instance.EnqueueFirst(7);     // Двусвязная очередь: 5 -> 4 -> 3 -> 2 -> 1 -> 6 -> 7 -> 8
            instance.EnqueueFirst(8);     // -> -> -> -> -> -> ->


            Debug.WriteLine(instance.PeekFirst()); // возвращает первый элемент очереди
            Debug.WriteLine(instance.PeekLast());  // возвращает последний элемент очереди 
        }
    }
}
