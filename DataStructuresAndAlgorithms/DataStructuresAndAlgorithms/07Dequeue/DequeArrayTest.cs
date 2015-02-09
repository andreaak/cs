using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Deque
{
    [TestClass]
    public class DequeArrayTest
    {
        [TestMethod]
        public void Test1()
        {
            DequeArray<int> instanse = new DequeArray<int>();


            // First -> указатель на начало очереди
            // Last  -> указатель на конец очереди

            instanse.EnqueueLast(1);     // -> -> -> -> -> -> ->  
            instanse.EnqueueLast(2);     // Двусвязная очередь:  3 -> 2 -> 1
            instanse.EnqueueLast(3);     // -> -> -> -> -> -> ->

            instanse.EnqueueFirst(4);    // -> -> -> -> -> -> ->
            instanse.EnqueueFirst(5);    // Двусвязная очередь:  3 -> 2 -> 1 -> 4 -> 5 -> 6
            instanse.EnqueueFirst(6);    // -> -> -> -> -> -> ->


            Debug.WriteLine("Первый элемент в очереди: {0}", instanse.PeekFirst());
            Debug.WriteLine("Последний элемент в очереди: {0}", instanse.PeekLast());
        }
    }
}
