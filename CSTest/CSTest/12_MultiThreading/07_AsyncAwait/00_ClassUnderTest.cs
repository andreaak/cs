using System.Diagnostics;
using System.Net;
#if CS5
#endif
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class _00_ClassUnderTest
    {
        public void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }

        public async Task Operation2()
        {
            Debug.WriteLine("Operation ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Begin");
            await Task.Delay(2000);
            Debug.WriteLine("End");
        }

        public int OperationWithResult()
        {
            Debug.WriteLine("Operation4 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
        }

        public double OperationWithArgumentAndResult(object argument)
        {
            Debug.WriteLine("Operation8 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return (double)argument * (double)argument;
        }

#if CS5

        /*
        Ключевое слово async указывает компилятору, что метод, является асинхронным.
        await указывает компилятору, что в этой точке необходимо дождаться окончания асинхронной операции
        (при этом управление возвращается вызвавшему методу).
        Ассинхронный метод может иметь 3 типа возращаемого значения: void, Task, Task<T>
        */
        public async void OperationAsync2_ReturnVoid_WithoutActionAfterAwait()
        {
            Task task = new Task(Operation);
            task.Start();
            await task;
        }

        public async void OperationAsync3_ReturnVoid_ActionAfterAwait()
        {
            /*
            Id потока совпадает с Id первичного потока. Это значит, что
            данный метод начинает выполняться в контексте первичного потока.
            */
            Debug.WriteLine("OperationAsync (Part I) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Task task = new Task(Operation);
            task.Start();
            await task;
            /*
            Id потока совпадает с Id вторичного потока. Это значит, что
            данный метод начинает выполняться в контексте вторичного потока.
            */
            Debug.WriteLine("OperationAsync (Part II) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
        }


        public async void OperationAsync5_ReturnVoid_ActionWithResultAfterAwait()
        {
            Task<int> task = Task<int>.Factory.StartNew(OperationWithResult);
            // TaskAwaiter<int> awaiter = task.GetAwaiter();
            // Action continuation = () => Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
            // awaiter.OnCompleted(continuation);
            // меняем на:
            Debug.WriteLine("\nРезультат: {0}\n", await task);
        }

        public async Task OperationAsync6_ReturnTask()
        {
            await Task.Factory.StartNew(Operation);
        }

        public async Task OperationAsync7_ReturnTask_ActionWithResultAfterAwait()// or DoDownloadTaskAsync1
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            req.Method = "GET";

            var task = req.GetResponseAsync();
            WebResponse resp = await task;

            Debug.WriteLine(resp.Headers.ToString());
            Debug.WriteLine("Async download completed");
        }

        public async Task OperationAsync7_ReturnTask_WithActionAfterAwait()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //await Task.Factory.StartNew(Operation);
            await Task.Run(() => Operation());
            Debug.WriteLine("AfterFirst " + sw.ElapsedMilliseconds);
            await Task.Run(() => Operation2());
            Debug.WriteLine("AfterSecond " + sw.ElapsedMilliseconds);
            sw.Start();
        }

        public async Task<int> OperationAsync8_ReturnTaskWithResult()
        {
            //int result = await Task<int>.Factory.StartNew(Operation4);
            //return result;
            return await Task<int>.Factory.StartNew(OperationWithResult);
        }

        public async Task<double> OperationAsync9_ReturnTaskWithResult_Argument(double argument)
        {
            //int result = await Task<int>.Factory.StartNew(Operation4);
            //return result;
            return await Task<double>.Factory.StartNew(OperationWithArgumentAndResult, argument);
        }

        public async Task DoDownloadAsync10_UseAPMItems()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            req.Method = "GET";

            Task<WebResponse> getResponseTask = Task.Factory.FromAsync<WebResponse>(
            req.BeginGetResponse, req.EndGetResponse, null);
            var resp = (HttpWebResponse)await getResponseTask;

            Debug.WriteLine(resp.Headers.ToString());
            Debug.WriteLine("Async download completed");
        }

#endif
    }
}
