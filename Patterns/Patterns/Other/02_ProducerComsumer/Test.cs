using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Patterns.Other._02_ProducerComsumer
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestProducerComsumer()
        {
            Queue<int> queue = new Queue<int>();
            SyncEvents syncEvents = new SyncEvents();

            Debug.WriteLine("Configuring worker threads...");
            Producer producer = new Producer(queue, syncEvents);
            Consumer consumer = new Consumer(queue, syncEvents);
            Thread producerThread = new Thread(producer.ThreadRun);
            Thread consumerThread = new Thread(consumer.ThreadRun);

            Debug.WriteLine("Launching producer and consumer threads...");
            producerThread.Start();
            consumerThread.Start();

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(2500);
                ShowQueueContents(queue);
            }

            Debug.WriteLine("Signaling threads to terminate...");
            syncEvents.ExitThreadEvent.Set();

            producerThread.Join();
            consumerThread.Join();
            /*
            Configuring worker threads...
            Launching producer and consumer threads...
            8 22 99 79 51 98 10 35 89 18 81 79 43 15 18 61 62 14 36 43 
            48 13 39 99 62 82 90 18 83 6 32 22 13 81 95 70 82 89 88 
            77 41 32 89 95 39 5 48 81 7 97 92 66 74 28 8 56 23 3 74 
            49 43 54 86 13 75 53 28 99 50 46 28 32 26 68 87 98 25 58 26 
            Signaling threads to terminate... 
            */
        }

        private static void ShowQueueContents(Queue<int> q)
        {
            lock (((ICollection)q).SyncRoot)
            {
                foreach (int item in q)
                {
                    Debug.Write(string.Format("{0} ", item));
                }
            }
            Debug.WriteLine("");
        }
    }
}
