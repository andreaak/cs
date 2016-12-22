using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Patterns._04_Other._02_ProducerComsumer.CS4Realization
{
    public class Consumer
    {
        private BlockingCollection<int> _queue;
        private CancellationTokenSource _endTokenSource;
        private int _count;

        public Consumer(BlockingCollection<int> q, CancellationTokenSource e)
        {
            _queue = q;
            _endTokenSource = e;
            _count = 0;
        }

        // Consumer.ThreadRun
        public void ThreadRun()
        {
            while (!_endTokenSource.IsCancellationRequested)
            {
                _count++;
                int item = _queue.Take();
            }
            Debug.WriteLine("Consumer Thread: consumed {0} items", _count);
        }
    }
}