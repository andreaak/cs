using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace _01_ASPMVCTest.Areas._10_Async.Models
{
    public class ServicesModel
    {
        private Stopwatch timer;
        private ConcurrentQueue<string> messages;

        public NewsModel News { get; set; }
        public WeatherModel Weather { get; set; }

        public ServicesModel()
        {
            timer = Stopwatch.StartNew();
            messages = new ConcurrentQueue<string>();
        }

        public long ElapsedTime
        {
            get
            {
                return timer.ElapsedMilliseconds;
            }
        }

        public IEnumerable<string> Messages
        {
            get
            {
                return messages;
            }
        }

        public void AddMessage(string message)
        {
            messages.Enqueue(string.Format("{0} в потоке {1}", message, Thread.CurrentThread.ManagedThreadId));
        }
    }
}