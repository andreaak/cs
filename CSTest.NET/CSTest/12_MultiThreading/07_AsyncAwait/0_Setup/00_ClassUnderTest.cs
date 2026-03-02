using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
#if CS5
#endif

namespace CSTest._12_MultiThreading._07_AsyncAwait._0_Setup
{
    /*
    Ключевое слово async указывает компилятору, что метод, является асинхронным.
    await указывает компилятору, что в этой точке необходимо дождаться окончания асинхронной операции
    (при этом управление возвращается вызвавшему методу).
    Ассинхронный метод может иметь 3 типа возращаемого значения: void, Task, Task<T>
    */
    class _00_ClassUnderTest
    {
        public void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }
#if CS5
        public async Task Operation2()
        {
            Debug.WriteLine("Operation2 ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Debug.WriteLine("Begin");
            await Task.Delay(2000);
            Debug.WriteLine("End");
        }
#endif
        public int OperationWithResult()
        {
            Debug.WriteLine("OperationWithResult ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
            return 2 + 2;
        }

        public double OperationWithArgumentAndResult(object argument)
        {
            Debug.WriteLine("OperationWithArgumentAndResult ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Thread.Sleep(2000);
            return (double)argument * (double)argument;
        }

#if CS5

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
            Debug.WriteLine("OperationAsync7 ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //await Task.Factory.StartNew(Operation);
            await Task.Run(() => Operation());
            Debug.WriteLine("AfterFirst " + sw.ElapsedMilliseconds);
            await Operation2();
            Debug.WriteLine("AfterSecond " + sw.ElapsedMilliseconds);
            sw.Start();


        }

        public async Task<int> OperationAsync8_ReturnTaskWithResult()
        {
            //int result = await Task<int>.Factory.StartNew(OperationWithResult);
            //return result;
            return await Task<int>.Factory.StartNew(OperationWithResult);
        }

        public async Task<double> OperationAsync9_ReturnTaskWithResult_Argument(double argument)
        {
            //double result = await Task<double>.Factory.StartNew(OperationWithArgumentAndResult);
            //return result;
            return await Task<double>.Factory.StartNew(OperationWithArgumentAndResult, argument);
        }



#endif
    }
}
