using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _06_ContinueWith
    {
        CancellationTokenSource cts;

        
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
        static void ContTask(Task t)
        {
            Debug.WriteLine("Продолжение запущено");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В продолжении подсчет равен " + count);
            }
            Debug.WriteLine("Продолжение завершено");
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи.
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект первой задачи. 
            Task tsk = new Task(MyTask);
            //А теперь создать продолжение задачи. 
            Task taskCont = tsk.ContinueWith(ContTask);
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
        public void TestContinueInTask()
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
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи при отмене.
        public void TestCancelTask()
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
        public void TestExceptionInTask()
        {
            Debug.WriteLine("Основной поток запущен.");
            UIContext.Initialize();
            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                ExceptionMethod();
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
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
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
