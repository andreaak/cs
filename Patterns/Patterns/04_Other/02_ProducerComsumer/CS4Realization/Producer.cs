using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Patterns._04_Other._02_ProducerComsumer.CS4Realization
{
    public class Producer
    {
        private BlockingCollection<int> _queue;
        private CancellationTokenSource _endTokenSource;
        private int _count;

        public Producer(BlockingCollection<int> q, CancellationTokenSource e)
        {
            _queue = q;
            _endTokenSource = e;
            _count = 0;
        }

        // Producer.ThreadRun
        public void ThreadRun()
        {
            Random r = new Random();
            while (!_endTokenSource.IsCancellationRequested)
            {
                while (_queue.Count < 20)
                {
                    _count++;
                    _queue.Add(r.Next(0, 100));
                }
            }
            Debug.WriteLine("Producer thread: produced {0} items", _count);
        }
    }
}