using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Queue
{
    [TestClass]
    public class QueueLinkedList
    {
        [TestMethod]
        public void Test1()
        {
            // Добавление элементов в очередь

            QueueLinkedList<int> instance = new QueueLinkedList<int>();

            instance.Enqueue(10);              // ->->->->->->->->->->->->->
            instance.Enqueue(15);              // Очередь: 30 25 20 15 10
            instance.Enqueue(20);              // ->->->->->->->->->->->->->
            instance.Enqueue(25);
            instance.Enqueue(30);

            Debug.WriteLine("Количество элементов очереди: {0}", instance.Count);
            Debug.WriteLine("Первый элемент очереди: {0}", instance.Peek()); // 10

            // Удаляем первый элемент из очереди


            int first = instance.Dequeue();    // ->->->->->->->->->->->->->
            // Очередь: 30 25 20 15 
            // ->->->->->->->->->->->->->

            Debug.WriteLine("Количество элементов очереди: {0}", instance.Count);
            Debug.WriteLine("Первый элемент очереди: {0}", instance.Peek()); //15
            Debug.WriteLine("first =  {0}", first);
        }
    }
}
