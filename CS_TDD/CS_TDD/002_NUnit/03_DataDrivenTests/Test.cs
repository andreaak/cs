using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CS_TDD._002_NUnit._03_DataDrivenTests
{
    [TestFixture]
    public class Test : AssertionHelper
    {
        [TestFixtureSetUp]
        public async void TestMethod()
        {
            //Task.Run(() => Console.WriteLine("Foo"));
            //Task task = Task.Factory.StartNew(() => { }, TaskCreationOptions.LongRunning);

            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0))
            );

            int result = await GetPrimesCountAsync(2, 1000000);
            Console.WriteLine(result);

            }

        Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            var timer = new System.Timers.Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate 
            {
                timer.Dispose();
                tcs.SetResult(null);
            };
            timer.Start();
            return tcs.Task;
        }


        async void DisplayPrimesCount()
        {
            int result = await GetPrimesCountAsync(2, 1000000);
            Console.WriteLine(result);
        }


        Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();

            // Создать таймер, который инициирует событие раз в 5000 миллисекунд:
            var timer = new System.Timers.Timer(5000) { AutoReset = false };

            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(42);
            };
            timer.Start() ;
            return tcs.Task;
        }



    Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex); 
                }
            }).Start();
            return tcs.Task;
        }

        Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                Enumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }


    }
}
