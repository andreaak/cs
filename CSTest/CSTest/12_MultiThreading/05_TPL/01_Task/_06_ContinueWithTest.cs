using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._08_WinFormsWPF._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    // Продолжение - автоматический запуск новой задачи, после завершения первой задачи.

    [TestFixture]
    public class _06_ContinueWithTest
    {
        CancellationTokenSource cts;

        [Test]
        // Продемонстрировать продолжение задачи.
        public void TestTaskContinue1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект первой задачи. 
            Task task = new Task(TestTask);
            //А теперь создать продолжение задачи. 
            Task taskContinuation = task.ContinueWith(ContinuationTask);
            // Начать последовательность задач, 
            task.Start();
            // Ожидать завершения продолжения. 
            taskContinuation.Wait();
            task.Dispose();
            taskContinuation.Dispose();
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            MyTask() запущен
            Thread: 7
            В методе MyTask() подсчет равен 0
            В методе MyTask() подсчет равен 1
            В методе MyTask() подсчет равен 2
            В методе MyTask() подсчет равен 3
            В методе MyTask() подсчет равен 4
            MyTask завершен
            Продолжение запущено
            Thread: 8
            В продолжении подсчет равен 0
            В продолжении подсчет равен 1
            В продолжении подсчет равен 2
            В продолжении подсчет равен 3
            В продолжении подсчет равен 4
            Продолжение завершено
            Основной поток завершен.
            */
        }

        [Test]
        // Продемонстрировать продолжение задачи при исключении.
        public void TestTaskContinue2()
        {
            Debug.WriteLine("Основной поток запущен.");
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            UIContext.Initialize();
            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Debug.WriteLine("Рабочий поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
                LongMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при отмене задачи");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при исключении в задаче");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            Первичный поток: Id 6
            Рабочий поток: Id 7
            Начало работы
            Окончание работы
            Обработка при нормальном завершении задачи
            Поток продолжения: Id 8
            Основной поток завершен. 
            */
        }

        [Test]
        // Продемонстрировать продолжение задачи при отмене.
        public void TestTaskCancel()
        {
            Debug.WriteLine("Основной поток запущен.");
            UIContext.Initialize();
            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Debug.WriteLine("Рабочий поток: Id {0} ", Thread.CurrentThread.ManagedThreadId);
                LongMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при отмене задачи");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при исключении в задаче");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                DisposeCancellationTokenSource();
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(2000);
            CancelTask();
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            Рабочий поток: Id 7 
            Начало работы
            Отмена задачи
            Окончание работы
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Обработка при отмене задачи
            Поток продолжения: Id 8
            Основной поток завершен.
            */
        }

        [Test]
        // Продемонстрировать продолжение задачи при исключении.
        public void TestTaskException()
        {
            Debug.WriteLine("Основной поток запущен.");
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            UIContext.Initialize();
            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Debug.WriteLine("Рабочий поток: Id {0} ", Thread.CurrentThread.ManagedThreadId);
                ExceptionMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при отмене задачи");
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при исключении в задаче");
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                DisposeCancellationTokenSource();
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            Первичный поток: Id 6
            Рабочий поток: Id 7 
            Начало работы
            Возникновение исключения
            Exception thrown: 'System.Exception' in CSTest.dll
            Поток продолжения: Id 8
            Обработка при исключении в задаче
            Основной поток завершен.
            */
        }

        // Метод, исполняемый как задача, 
        static void TestTask()
        {
            Debug.WriteLine("MyTask() запущен");
            Debug.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyTask() подсчет равен " + count);
            }
            Debug.WriteLine("MyTask завершен");
        }

        // Метод, исполняемый как продолжение задачи, 
        static void ContinuationTask(Task t)
        {
            Debug.WriteLine("Продолжение запущено");
            Debug.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В продолжении подсчет равен " + count);
            }
            Debug.WriteLine("Продолжение завершено");
        }

        private void ExceptionMethod()
        {
            Debug.WriteLine("Начало работы");
            Thread.Sleep(5000);
            Debug.WriteLine("Возникновение исключения");
            throw new Exception("TEST");
        }

        private void CancelTask()
        {
            if (cts != null)
            {
                Debug.WriteLine("Отмена задачи");
                cts.Cancel();
            }
        }

        private void DisposeCancellationTokenSource()
        {
            cts.Dispose();
            cts = null;
        }

        private void LongMethod()
        {
            Debug.WriteLine("Начало работы");
            Thread.Sleep(5000);
            Debug.WriteLine("Окончание работы");
        }
    }
}
