using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Patterns._04_Other._02_ProducerComsumer.EarlierRealization;

namespace Patterns._04_Other._02_ProducerComsumer
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
            11 64 53 58 96 84 72 8 95 73 79 74 16 61 99 87 99 50 29 9 
            9 25 63 92 23 7 64 58 58 76 43 88 38 2 41 16 40 92 87 48 
            44 20 64 8 75 71 79 0 46 54 12 68 61 76 22 89 65 51 21 69 
            79 74 65 28 36 89 29 51 34 75 37 32 32 85 3 68 96 91 52 
            Signaling threads to terminate...
            Consumer Thread: consumed 2532300 items
            The thread 0x1e24 has exited with code 0 (0x0).
            Producer thread: produced 2532319 items
            */
        }

        [Test]
        public void TestProducerComsumerBlockingCollection()
        {
            BlockingCollection<int> queue = new BlockingCollection<int>();
            CancellationTokenSource endTokenSource = new CancellationTokenSource();

            Debug.WriteLine("Configuring tasks...");
            CS4Realization.Producer producer1 = new CS4Realization.Producer(queue, endTokenSource);
            CS4Realization.Producer producer2 = new CS4Realization.Producer(queue, endTokenSource);
            CS4Realization.Consumer consumer = new CS4Realization.Consumer(queue, endTokenSource);

            var tasks = new[]
            {
                new Task(producer1.ThreadRun),
                new Task(producer2.ThreadRun),
                new Task(consumer.ThreadRun)
            };

            Debug.WriteLine("Launching producer and consumer tasks...");
            Array.ForEach(tasks, task => task.Start());

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(2500);
                ShowQueueContents(queue);
            }

            Debug.WriteLine("Signaling tasks to terminate...");
            endTokenSource.Cancel();
            Task.WaitAll(tasks);

            /*
            Configuring tasks...
            Launching producer and consumer tasks...
            38 71 69 77 51 30 30 43 89 65 15 67 74 10 97 11 24 62 10 49 5 
            67 85 0 95 1 83 96 63 27 21 96 35 99 73 85 66 94 
            50 76 73 98 37 71 3 7 79 80 87 57 64 43 38 5 83 94 24 15 29 
            68 31 53 81 89 33 32 12 12 23 8 93 22 10 3 88 
            Signaling tasks to terminate...
            Producer thread: produced 10468890 items
            Consumer Thread: consumed 22829472 items
            Producer thread: produced 12360602 items
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

        private static void ShowQueueContents(BlockingCollection<int> q)
        {
            foreach (int item in q)
            {
                Debug.Write(string.Format("{0} ", item));
            }
            Debug.WriteLine("");
        }
    }
}
