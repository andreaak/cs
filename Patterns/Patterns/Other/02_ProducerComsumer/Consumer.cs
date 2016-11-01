using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Patterns.Other._02_ProducerComsumer
{
    public class Consumer
    {
        private Queue<int> _queue;
        private SyncEvents _syncEvents;

        public Consumer(Queue<int> q, SyncEvents e)
        {
            _queue = q;
            _syncEvents = e;
        }

        // Consumer.ThreadRun
        public void ThreadRun()
        {
            int count = 0;
            while (WaitHandle.WaitAny(_syncEvents.EventArray) != 1)
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    int item = _queue.Dequeue();
                }
                count++;
            }
            Debug.WriteLine("Consumer Thread: consumed {0} items", count);
        }
    }
}