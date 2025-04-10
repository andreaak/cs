using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using System;
using System.Runtime.ExceptionServices;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _05_TestWhen
    {
        [Test]
        public async void TestAsyncAwait01_Sequential()
        {
            var mc = new _05_ClassUnderTest();
            Debug.WriteLine("Before tasks await");
            await mc.OperationAsync(0);
            await mc.OperationAsync(1);
            await mc.OperationAsync(2);
            await mc.OperationAsync(3);
            await mc.OperationAsync(4);
            Debug.WriteLine("After tasks await");

            /*
                Before tasks await
                Task 0 started
                Task 0 completed
                Task 1 started
                Task 1 completed
                Task 2 started
                Task 2 completed
                Task 3 started
                Task 3 completed
                Task 4 started
                Task 4 completed
                After tasks await
             */
        }

        [Test]
        public async void TestAsyncAwait02_WhenAll()
        {
            var mc = new _05_ClassUnderTest();
            var tasks = new[] { 0, 1, 2, 3, 4, }.Select(item => mc.OperationAsync(item));
            Debug.WriteLine("Before tasks await");
            await Task.WhenAll(tasks);
            Debug.WriteLine("After tasks await");

            /*
                Before tasks await
                Task 0 started
                Task 0 completed
                Task 1 started
                Task 2 started
                Task 3 started
                Task 4 started
                Task 1 completed
                Task 2 completed
                Task 3 completed
                Task 4 completed
                After tasks await
             */
        }

        [Test]
        public async void TestAsyncAwait03_WhenAny()
        {
            var mc = new _05_ClassUnderTest();
            var tasks = new[] { 1, 2, 3, 4, 5 }.Select(item => mc.OperationAsync(item));
            Debug.WriteLine("Before tasks await");
            await Task.WhenAny(tasks);
            Debug.WriteLine("After tasks await");

            /*
                Before tasks await
                Task 1 started
                Task 2 started
                Task 3 started
                Task 4 started
                Task 5 started
                Task 1 completed
                After tasks await
            */
        }

        [Test]
        public async void TestAsyncAwait03_WhenAny_Interleaving()
        {
            var mc = new _05_ClassUnderTest();
            List<Task<int>> tasks = new[] { 1, 2, 3, 4, 5 }.Select(item => mc.OperationWithResultAsync(item)).ToList();

            Debug.WriteLine("Before tasks await");
            while (tasks.Count > 0)
            {
                Task<int> task = await Task.WhenAny(tasks);
                Debug.WriteLine("Task {0} awaited", task.Result);
                tasks.Remove(task);
                Debug.WriteLine("Task {0} some process with result", task.Result);
            }
            Debug.WriteLine("After tasks await");

            /*
                Task 1 started
                Task 2 started
                Task 3 started
                Task 4 started
                Task 5 started
                Before tasks await
                Task 1 completed
                Task 1 awaited
                Task 1 some process with result
                Task 2 completed
                Task 2 awaited
                Task 2 some process with result
                Task 3 completed
                Task 3 awaited
                Task 3 some process with result
                Task 4 completed
                Task 4 awaited
                Task 4 some process with result
                Task 5 completed
                Task 5 awaited
                Task 5 some process with result
                After tasks await
            */
        }

        [Test]
        public async void TestAsyncAwait03_WhenAny_Throttling()
        {
            const int CONCURRENCY_LEVEL = 3;

            var mc = new _05_ClassUnderTest();
            var indexes = new[] {1, 2, 3, 4, 5};

            int index = 0;
            List<Task<int>> tasks = new List<Task<int>>();
            while (index < CONCURRENCY_LEVEL && index < indexes.Length)
            {
                tasks.Add(mc.OperationWithResultAsync(index));
                Debug.WriteLine("Task {0} added", index);
                index++;
            }

            Debug.WriteLine("Before tasks await");
            while (tasks.Count > 0)
            {
                Task<int> task = await Task.WhenAny(tasks);
                Debug.WriteLine("Task {0} awaited", task.Result);
                tasks.Remove(task);
                Debug.WriteLine("Task {0} some process with result", task.Result);
                if (index < indexes.Length)
                {
                    tasks.Add(mc.OperationWithResultAsync(index));
                    Debug.WriteLine("Task {0} added", index);
                    index++;
                }
            }
            Debug.WriteLine("After tasks await");

            /*
            Task 0 started
            Task 0 completed
            Task 0 added
            Task 1 started
            Task 1 added
            Task 2 started
            Task 2 added
            Before tasks await
            Task 0 awaited
            Task 0 some process with result
            Task 3 started
            Task 3 added
            Task 1 completed
            Task 1 awaited
            Task 1 some process with result
            Task 4 started
            Task 4 added
            Task 2 completed
            Task 2 awaited
            Task 2 some process with result
            Task 3 completed
            Task 3 awaited
            Task 3 some process with result
            Task 4 completed
            Task 4 awaited
            Task 4 some process with result
            After tasks await
            */
        }

        [Test]
        public void TestAsyncAwait14_WaitAll()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync_WaitAll(cts);
            Debug.WriteLine("Async download started\n");

            //Thread.Sleep(2000);
            //Debug.WriteLine("Cancel Task\n");
            //cts.Cancel();
            Thread.Sleep(20000);

            /*
            Staring async download

            Async download started


            Length of the download:  262103
            URL:http://msdn.microsoft.com/en-us/library/hh290136.aspx, Thread:15

            Length of the download:  474478
            URL:http://msdn.microsoft.com/library/windows/apps/br211380.aspx, Thread:8

            Length of the download:  150754
            URL:http://msdn.microsoft.com/en-us/library/ee256749.aspx, Thread:15
            URL:http://msdn.microsoft.com/en-us/library/hh290140.aspx, Thread:15

            Length of the download:  150226

            Length of the download:  264298
            URL:http://msdn.microsoft.com/en-us/library/hh290138.aspx, Thread:15

            Length of the download:  48318
            URL:http://msdn.microsoft.com/en-us/library/dd470362.aspx, Thread:8
            URL:http://msdn.microsoft.com/en-us/library/aa578028.aspx, Thread:8

            Length of the download:  179341

            Length of the download:  223716
            URL:http://msdn.microsoft.com/en-us/library/ms404677.aspx, Thread:15

            Length of the download:  156120
            URL:http://msdn.microsoft.com/en-us/library/ff730837.aspx, Thread:9
            URL:http://msdn.com, Thread:9

            Length of the download:  48537

            Downloads complete.
            */
        }

        [Test]
        public async void TestAsyncAwait15_WhenAll()
        {
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            var tasks = new[] { 100, 200, 300, 400, 500 }
                .Select(item => Task.Run(async () => await mc.OperationWithResultAsync(item).WaitOrCancel(cts.Token).ConfigureAwait(false), cts.Token)).ToList();

            await Task.WhenAll(tasks);

            Debug.WriteLine("After tasks await");

            /*
            Task 200 started
            Task 100 started
            Task 300 started
            Task 400 started
            Task 500 started
            Task 100 completed
            Task 200 completed
            Task 300 completed
            The thread 0x292c has exited with code 0 (0x0).
            Task 400 completed
            Task 500 completed
            After tasks await
            */
        }

        [Test]
        public async void TestAsyncAwait16_WhenAll_Canceled()
        {
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            var tasks = new[] { 100, 200, 300, 400, 500 }
                .Select(item => Task.Run(async () => await mc.OperationWithResultAsync(item).WaitOrCancel(cts.Token).ConfigureAwait(false))).ToList();

            cts.CancelAfter(1000);

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {

                Debug.WriteLine("Operatons canceled");
            }

            Debug.WriteLine("After tasks await");

            /*
            Task 100 started
            Task 300 started
            Task 200 started
            Task 400 started
            Task 500 started
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Operatons canceled
            After tasks await
            */
        }

        [Test]
        public void TestAsyncAwait19_WhenAll_Canceled()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                var task = Task.Run(async () => await Test19CancelAsync(cts).ConfigureAwait(false));
                task.Wait();
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Operatons canceled");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                var e = ParseAggregateException(ex);
                Debug.WriteLine(e.ToString());
            }
            Debug.WriteLine("After tasks await");

            /*
            Task 100 started
            Task 150 started
            Task 200 started
            Task 250 started
            Task 300 started
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.AggregateException' in mscorlib.dll
            System.AggregateException: One or more errors occurred. ---> System.Threading.Tasks.TaskCanceledException: A task was canceled.
               --- End of inner exception stack trace ---
               at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)
               at System.Threading.Tasks.Task.Wait(Int32 millisecondsTimeout, CancellationToken cancellationToken)
               at System.Threading.Tasks.Task.Wait()
               at CSTest._12_MultiThreading._07_AsyncAwait._05_TestWhen.TestAsyncAwait19_WhenAll_Canceled() in D:\Projects\CS\CSTest\CSTest\12_MultiThreading\07_AsyncAwait\05_TestWhen.cs:line 403
            ---> (Inner Exception #0) System.Threading.Tasks.TaskCanceledException: A task was canceled.<---
            */
        }

        [Test]
        public void TestAsyncAwait20_HandleTask()
        {
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            int item = 100;
            HandleTask(async () => await mc.OperationWithResultAsync(item).ConfigureAwait(false));
            Debug.WriteLine("After tasks await");

            /*
            Task 100 started
            Task 100 completed
            After tasks await
            */
        }

        [Test]
        public void TestAsyncAwait21_HandleTask()
        {
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            int item = 100;
            HandleTask(async () => await mc.OperationAsync(item).ConfigureAwait(false));
            Debug.WriteLine("After tasks await");

            /*
            Task 100 started
            Task 100 completed
            After tasks await
            */
        }

        [Test]
        public void TestAsyncAwait22_HandleTask()
        {
            var mc = new _05_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            int item = 100;
            HandleTask(() => mc.OperationWithResultAsync(item));
            Debug.WriteLine("After tasks await");

            /*

            */
        }

        private async Task Test19CancelAsync(CancellationTokenSource cts)
        {
            var mc = new _05_ClassUnderTest();

            var tasks = new[] { 100, 150, 200, 250, 300 }
                .Select(async (item) => await mc.OperationWithResultAsync(item).WaitOrCancel(cts.Token).ConfigureAwait(false)).ToList();

            cts.CancelAfter(1000);

            await Task.WhenAll(tasks);
        }

        public static Task<T> HandleTask<T>(Func<Task<T>> func)
        {
            var task = Task.Run(async () => await func());
            try
            {
                task.Wait();
            }
            catch (Exception)
            {
                throw;
            }
            return task;
        }

        public static void HandleTask(Func<Task> func)
        {
            var task = Task.Run(async () => await func());
            try
            {
                task.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Exception ParseAggregateException(Exception ex)
        {
            var result = ex;
            var exception = ex as AggregateException;
            if (exception != null)
            {
                try
                {
                    exception.Handle(innerEx =>
                    {
                        if (innerEx is OperationCanceledException)
                        {
                            return true;
                        }
                        ExceptionDispatchInfo.Capture(innerEx).Throw();
                        //Note: this throw won't get called, if we don't put something here the compiler complains because
                        //it doesn't know that the above line will throw
                        throw innerEx;
                    });
                }
                catch (Exception innerEx)
                {
                    result = innerEx;
                }
            }
            return result;
        }
    }

    public static class TaskExtensions
    {
        public static async Task<T> WaitOrCancel<T>(this Task<T> task, CancellationToken token)
        {
            var finishedTask = await Task.WhenAny(task, token.WhenCanceled());
            if(finishedTask != task)
            {
                token.ThrowIfCancellationRequested();
            }

            return await task;
        }

        public static Task WhenCanceled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }
    }

#endif
}

