using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;

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
    }

#endif
}