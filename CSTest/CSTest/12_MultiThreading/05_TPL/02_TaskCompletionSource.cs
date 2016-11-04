using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._05_TPL
{
    [TestFixture]
    public class _02_TaskCompletionSource
    {
        [Test]
        public void TestTaskCompletionSource1()
        {
            var tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Debug.WriteLine("Start Task");
                Thread.Sleep(5000);
                Debug.WriteLine("Set Result");
                tcs.SetResult(42);
                Debug.WriteLine("End Task");
            })
            { IsBackground = true }
            .Start();

            Task<int> task = tcs.Task; // "Подчиненная” задача
            Debug.WriteLine("Print Result");
            Debug.WriteLine(task.Result); // 42
            /*
            Print Result
            Start Task
            Set Result
            End Task
            The thread 0x10490 has exited with code 0 (0x0).
            42 
             */
        }

        [Test]
        public void TestTaskCompletionSource2()
        {
            Task<int> task = Run(() =>
            {
                Debug.WriteLine("Start Task");
                Thread.Sleep(5000);
                Debug.WriteLine("End Task");
                return 42; 
            });

            Debug.WriteLine(task.Result);
            /*
            Start Task
            End Task
            Set Result
            The thread 0x48b8 has exited with code 0 (0x0).
            42
             */
        }

#if CS5
        [Test]
        public void TestTaskCompletionSource3()
        {
            var awaiter = GetAnswerToLife().GetAwaiter();

            awaiter.OnCompleted(() => Debug.WriteLine(awaiter.GetResult()));

            //Для задержки основного потока

            while (!awaiter.IsCompleted)
            {
                Thread.Sleep(1000);
            }
            /*
            Create timer
            Start timer
            Dispose timer
            Set result
            42
             */
        }
#endif
        Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();

            Debug.WriteLine("Create timer");
            // Создать таймер, который инициирует событие раз в 5000 миллисекунд:
            var timer = new System.Timers.Timer(5000) { AutoReset = false };

            timer.Elapsed += delegate
            {
                Debug.WriteLine("Dispose timer");
                timer.Dispose();
                Debug.WriteLine("Set result");
                tcs.SetResult(42);
            };
            Debug.WriteLine("Start timer");
            timer.Start();
            return tcs.Task;
        }

        Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    TResult res = function();
                    Debug.WriteLine("Set Result");
                    tcs.SetResult(res);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }
    }
}
