using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _06_ContinueWithTest
    {
        CancellationTokenSource cts;
        


        [TestMethod]
        // Продемонстрировать продолжение задачи.
        public void TestTaskContinue1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект первой задачи. 
            Task tsk = new Task(MyTask);
            //А теперь создать продолжение задачи. 
            Task taskCont = tsk.ContinueWith(ContinuationTask);
            // Начать последовательность задач, 
            tsk.Start();
            // Ожидать завершения продолжения. 
            taskCont.Wait();
            tsk.Dispose();
            taskCont.Dispose();
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
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
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи при отмене.
        public void TestTaskCancel()
        {
            Debug.WriteLine("Основной поток запущен.");
            UIContext.Initialize();
            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                LongMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при отмене задачи");
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при исключении в задаче");
                DisposeCancellationTokenSource();
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                DisposeCancellationTokenSource();
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(2000);
            CancelTask();
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
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
            Первичный поток: Id 8 
            Основной поток завершен.
            Рабочий поток: Id 9 
            Начало работы
            Окончание работы
            Поток продолжения: Id 8

            Обработка при нормальном завершении задачи
            */
        }

        // Метод, исполняемый как задача, 
        static void MyTask()
        {
            Debug.WriteLine("MyTask() запущен");
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
