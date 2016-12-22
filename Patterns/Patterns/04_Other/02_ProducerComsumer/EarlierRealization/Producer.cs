using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Patterns._04_Other._02_ProducerComsumer.EarlierRealization
{
    public class Producer
    {
        private Queue<int> _queue;
        private SyncEvents _syncEvents;

        public Producer(Queue<int> q, SyncEvents e)
        {
            _queue = q;
            _syncEvents = e;
        }

        // Producer.ThreadRun
        public void ThreadRun()
        {
            int count = 0;
            Random r = new Random();
            while (!_syncEvents.ExitThreadEvent.WaitOne(0, false))
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    while (_queue.Count < 20)
                    {
                        _queue.Enqueue(r.Next(0, 100));
                        _syncEvents.NewItemEvent.Set();
                        count++;
                    }
                }
            }
            Debug.WriteLine("Producer thread: produced {0} items", count);
        }
    }
}