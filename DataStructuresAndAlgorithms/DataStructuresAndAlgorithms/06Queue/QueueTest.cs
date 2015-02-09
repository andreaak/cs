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

        [TestMethod]
        public void Test2()
        {
            // Добавление элементов в очередь

            QueueLinkedList2<int> instance = new QueueLinkedList2<int>();

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

        [TestMethod]
        public void Test3()
        {
            // Добавление элементов в очередь

            QueueArray<int> instance = new QueueArray<int>();

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

        [TestMethod]
        public void Enqueue_Updates_Count()
        {
            QueueArray<int> queue = new QueueArray<int>();

            Assert.AreEqual(0, queue.Count, "The count should start at 0");

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, queue.Count, "The count was off before calling Enqueue...");
                queue.Enqueue(i);
                Assert.AreEqual(i + 1, queue.Count, "The count was off after calling Enqueue...");
            }
        }

        [TestMethod]
        public void Dequeue_Peek_Correct_Order()
        {
            QueueArray<int> queue = new QueueArray<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expectedCount = queue.Count;

            for (int expected = 0; expected < 10; expected++)
            {
                Assert.AreEqual(expectedCount, queue.Count, "The count was off before Peek");

                Assert.AreEqual(expected, queue.Peek(), "Peek returned an unexpected result");

                Assert.AreEqual(expectedCount, queue.Count, "The count was off after Peek");

                Assert.AreEqual(expected, queue.Dequeue(), "Dequeue returned an unexpected result");

                Assert.AreEqual(expectedCount - 1, queue.Count, "The count was off after Dequeue");

                expectedCount--;
            }
        }

        [TestMethod]
        public void Enqueue_Wrapping()
        {
            QueueArray<int> queue = new QueueArray<int>();
            // fills the array (growth is 0 -> 4 -> 8)
            for (int i = 0; i < 8; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;
            foreach (int actual in queue)
            {
                Assert.AreEqual(expected++, actual, "The enumerated value was not what was expected");
            }

            // now remove three items 
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");

            // now 3..7 are left

            // put three more items back in to cause wrapping without growth
            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(3, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(4, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(5, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(6, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(7, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");
        }

        [TestMethod]
        public void Enumeration_Simple()
        {
            QueueArray<int> queue = new QueueArray<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;

            foreach (int i in queue)
            {
                Assert.AreEqual(expected, i, "The enumerated value was not expected");
                expected++;
            }
        }

        [TestMethod]
        public void Enqueue_Updates_Count_()
        {
            QueueLinkedList2<int> queue = new QueueLinkedList2<int>();

            Assert.AreEqual(0, queue.Count, "The count should start at 0");

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, queue.Count, "The count was off before calling Enqueue...");
                queue.Enqueue(i);
                Assert.AreEqual(i + 1, queue.Count, "The count was off after calling Enqueue...");
            }
        }

        [TestMethod]
        public void Dequeue_Peek_Correct_Order_()
        {
            QueueLinkedList2<int> queue = new QueueLinkedList2<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expectedCount = queue.Count;

            for (int expected = 0; expected < 10; expected++)
            {
                Assert.AreEqual(expectedCount, queue.Count, "The count was off before Peek");

                Assert.AreEqual(expected, queue.Peek(), "Peek returned an unexpected result");

                Assert.AreEqual(expectedCount, queue.Count, "The count was off after Peek");

                Assert.AreEqual(expected, queue.Dequeue(), "Dequeue returned an unexpected result");

                Assert.AreEqual(expectedCount - 1, queue.Count, "The count was off after Dequeue");

                expectedCount--;
            }
        }

        [TestMethod]
        public void Enqueue_Dequeue_Mix_()
        {
            QueueLinkedList2<int> queue = new QueueLinkedList2<int>();
            for (int i = 0; i < 8; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;
            foreach (int actual in queue)
            {
                Assert.AreEqual(expected++, actual, "The enumerated value was not what was expected");
            }

            // now remove three items 
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");

            // now 3..7 are left

            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(3, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(4, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(5, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(6, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(7, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");
        }

        [TestMethod]
        public void Enumeration_Simple_()
        {
            QueueLinkedList2<int> queue = new QueueLinkedList2<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;

            foreach (int i in queue)
            {
                Assert.AreEqual(expected, i, "The enumerated value was not expected");
                expected++;
            }
        }

        [TestMethod]
        public void Enqueue_Simple()
        {
            PriorityQueue<int> q = new PriorityQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                q.Enqueue(i);
            }

            Assert.AreEqual(q.Count, 10, "The wrong number of items are in the queue");

            int expected = 9;
            while (q.Count > 0)
            {
                Assert.AreEqual(expected, q.Dequeue(), "The expected priority value was not dequeued");
                expected--;
            }
        }

        [TestMethod]
        public void Enqueue_Specific()
        {
            PriorityQueue<int> q = new PriorityQueue<int>();

            q.Enqueue(5);
            q.Enqueue(4);
            q.Enqueue(2);
            q.Enqueue(4);
            q.Enqueue(6);
            q.Enqueue(3);

            Assert.AreEqual(6, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(5, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(4, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(4, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(3, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(2, q.Dequeue(), "Unexpected pq value");
        }

        [TestMethod]
        public void Enumeration_Simple__()
        {
            int[] input = { 2, 4, 7, 4, 2, 8, 1 };
            int[] expected = { 8, 7, 4, 4, 2, 2, 1 };

            PriorityQueue<int> queue = new PriorityQueue<int>();

            foreach (int i in input)
            {
                queue.Enqueue(i);
            }

            int index = 0;

            foreach (int i in queue)
            {
                Assert.AreEqual(expected[index], i, "The enumerated value was unexpected");
                index++;
            }
        }

        [TestMethod]
        public void TestPriorityQueue()
        {
            PriorityQueue<Complaint> queue = new PriorityQueue<Complaint>();
            int id = 0;
            queue.Enqueue(new Complaint()
            {
                Id = id++,
                Type = ComplaintType.Noise
            });
            queue.Enqueue(new Complaint()
            {
                Id = id++,
                Type = ComplaintType.Theft
            });
            queue.Enqueue(new Complaint()
            {
                Id = id++,
                Type = ComplaintType.Auto
            });
            queue.Enqueue(new Complaint()
            {
                Id = id++,
                Type = ComplaintType.Noise
            });

            foreach (var item in queue)
            {
                Debug.WriteLine("Id: {0} Type: {1}", item.Id, item.Type);
            }

        }
    }
}
