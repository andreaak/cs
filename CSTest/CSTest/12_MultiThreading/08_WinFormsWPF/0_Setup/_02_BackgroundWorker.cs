using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._08_WinFormsWPF._0_Setup
{
    public class _02_BackgroundWorker
    {
        // Задача объекта типа BackgroundWorker захватить свободный поток из пула потоков CLR и затем из
        // этого потока вызвать событие DoWork;
        private readonly BackgroundWorker _worker;

        public _02_BackgroundWorker()
        {
            _worker = new BackgroundWorker();
            // Метод, который будет выполнятся в отдельном потоке. Событие DoWork срабатывает при вызове RunWorkerAsync
            _worker.DoWork += new DoWorkEventHandler(OnDoWork);
            // Метод, который сработает в момент завершения BackgroundWorker
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnWorkCompleted);
            // Событие для отслеживание процесса выполнения задачи BackgroundWorker. Событие возникает при вызове метода _worker.ReportProgress(i);
            _worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
            // Для отслеживания выполнения хода работ свойство WorkerReportsProgress устанавливаем true
            _worker.WorkerReportsProgress = true;
            // Поддержка отмены выполнения фоновой операции с помощью метода CancelAsync()
            _worker.WorkerSupportsCancellation = true;
        }

        public void StartWorker()
        {
            Debug.WriteLine("StartWorker метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            // Запуск выполнения фоновой операции. Событие DoWork.
            // Вторая перегрузка RunWorkerAsync позволяет передать объект событию DoWork для его последующей обработки в потоке.
            _worker.RunWorkerAsync(Tuple.Create(int.Parse("1"), int.Parse("2")));
        }

        public void CancelWorker()
        {
            // Для работы метода, свойство WorkerSupportsCancellation должно быть равное true.
            Debug.WriteLine("CancelWorker метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            _worker.CancelAsync();
        }

        // Данный метод работает в отдельном потоке.
        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("OnDoWork метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var t = e.Argument as Tuple<int, int>;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                // Отмена выполнения фоновой задачи, сработает при вызове CancelAsync
                if (_worker.CancellationPending)
                {
                    // значение нужно установить для того что бы при событии RunWorkerCompleted определить почему оно было вызвано, 
                    // из -за того что закончилась операция или из-за отмены.
                    e.Cancel = true; 
                    return; // Отмена выполнения фоновой операции.
                }
                // Отчитываемся о проценте выполнения задачи.
                _worker.ReportProgress(i);
            }
            e.Result = t.Item1 + t.Item2;
        }

        // Метод работает из потока Dispetcher. Он может получать доступ к переменным окна.
        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("OnProgressChanged метод. Thread Id: {0} Value: {1}", Thread.CurrentThread.ManagedThreadId, e.ProgressPercentage);
            //progressBar1.Value = e.ProgressPercentage;
        }

        // Метод работает из потока Dispatcher. Он может получать доступ к переменным окна.
        private void OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("OnWorkCompleted метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            if (e.Cancelled)
            {
                // Отменено 
                Debug.WriteLine("Cancelled");
            }
            else
            {
                Debug.WriteLine("Result = " + e.Result.ToString());
            }
        }
    }
}
