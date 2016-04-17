using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading
{
    class _02_BackgroundWorker
    {
        private BackgroundWorker backgroundWorker;
        public _02_BackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += OnDoWork;
            backgroundWorker.RunWorkerCompleted += OnWorkCompleted;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("OnDoWork метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var t = e.Argument as Tuple<int, int>;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            e.Result = t.Item1 + t.Item2;
        }

        public void OnCalculate()
        {
            Debug.WriteLine("OnCalculate метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            backgroundWorker.RunWorkerAsync(Tuple.Create(int.Parse("1"),
            int.Parse("2")));
        }

        private void OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("OnWorkCompleted метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            if (e.Cancelled)
            {
                Debug.WriteLine("Cancelled");
                // Отменено 
            }
            else
            {
                Debug.WriteLine("Result = " + e.Result.ToString());
            }
        }

        public void OnCancel()
        {
            Debug.WriteLine("OnCancel метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            backgroundWorker.CancelAsync();
        } 

    }
}
