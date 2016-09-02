using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Linq;
#if CS5
using System.Net.Http;
#endif
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class ClassUnderTest
    {
        public void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }

#if CS5

        /*
        Ключевое слово async указывает компилятору, что метод, является асинхронным.
        await указывает компилятору, что в этой точке необходимо дождаться окончания асинхронной операции
        (при этом управление возвращается вызвавшему методу).
        Ассинхронный метод может иметь 3 типа возращаемого значения: void, Task, Task<T>
        */
        public async void OperationAsync_ReturnVoid_WithoutActionAfterAwait()
        {
            Task task = new Task(Operation);
            task.Start();
            await task;
        }

        public async void OperationAsync_ReturnVoid_ActionAfterAwait()
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

        public int OperationWithResult()
        {
            Debug.WriteLine("Operation4 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
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

        public async Task<int> OperationAsync7_ReturnTaskWithResult()
        {
            //int result = await Task<int>.Factory.StartNew(Operation4);
            //return result;
            return await Task<int>.Factory.StartNew(OperationWithResult);
        }

        public double Operation8(object argument)
        {
            Debug.WriteLine("Operation8 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return (double)argument * (double)argument;
        }

        public async Task<double> OperationAsync8_ReturnTaskWithResult(double argument)
        {
            //int result = await Task<int>.Factory.StartNew(Operation4);
            //return result;
            return await Task<double>.Factory.StartNew(Operation8, argument);
        }

        public async Task DoDownloadAsync9_ReturnTask_ActionAfterAwait()// or DoDownloadTaskAsync1
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            req.Method = "GET";

            var task = req.GetResponseAsync();
            HttpWebResponse resp = (HttpWebResponse)await task;

            Debug.WriteLine(resp.Headers.ToString());
            Debug.WriteLine("Async download completed");
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

        //Исключения «выбрасываются» в месте вызова асинхронной операции, а не Callback-метода!
        public async void DoDownloadAsync11_InnerException()
        {
            using (var w = new WebClient())
            {
                try
                {
                    string txt = await w.DownloadStringTaskAsync("http://www.micr1osoft.com/");
                    Debug.WriteLine(txt);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public async void DoDownloadAsync12_OuterException()
        {
            using (var w = new WebClient())
            {
                string txt = await w.DownloadStringTaskAsync("http://www.micr1osoft.com/");
                Debug.WriteLine(txt);
            }
        }

        public async void DoDownloadAsync13_Cancel(CancellationTokenSource cts)
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                Task sumTask = SumPageSizesAsync(cts.Token);

                //Do some other stuff...
                //....

                //Wait until sumTask is Finished...
                await sumTask;

            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("\r\nDownload canceled.\r\n");
            }
            catch (Exception)
            {
                Debug.WriteLine("\r\nDownload failed.\r\n");
            }

            Debug.WriteLine("\r\n DoDownloadAsync4Cancel ended\r\n");
        }

        private async Task SumPageSizesAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            IEnumerable<string> urlList = Setup.SetUpURLList();

            var total = 0;

            foreach (var url in urlList)
            {
                HttpResponseMessage response = await client.GetAsync(url, ct);
                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

                Setup.DisplayResults(url, urlContents);

                total += urlContents.Length;
            }

            Debug.WriteLine(string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total));
        }

        public async void DoDownloadAsync14_WaitAll(CancellationTokenSource cts)
        {
            try
            {
                await AccessTheWebAsync(cts.Token);
                Debug.WriteLine("\r\nDownloads complete.");
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("\r\nDownloads canceled.\r\n");
            }
            catch (Exception)
            {
                Debug.WriteLine("\r\nDownloads failed.\r\n");
            }
        }

        private async Task AccessTheWebAsync(CancellationToken ct)
        {
            var client = new HttpClient();

            IEnumerable<string> urlList = Setup.SetUpURLList();

            IEnumerable<Task<int>> downloadTasksQuery = urlList
                                                    .Select(url => ProcessURL(url, client, ct));

            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            while (downloadTasks.Count != 0)
            {
                Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

                downloadTasks.Remove(firstFinishedTask);

                int length = firstFinishedTask.Result;

                Debug.WriteLine(string.Format("\r\nLength of the download:  {0}", length));
            }
        }

        private async Task<int> ProcessURL(string url, HttpClient client, CancellationToken ct)
        {
            HttpResponseMessage response = await client.GetAsync(url, ct);


            var task = response.Content.ReadAsByteArrayAsync();
            var taskDebug = task.ContinueWith((task1) => Debug.WriteLine("URL:{0}, Thread:{1}", url, Thread.CurrentThread.ManagedThreadId));

            byte[] urlContents = await task;

            return urlContents.Length;
        }

        private string Bonus { get; set; }

        public async Task<string> TestBonus()
        {
            if (Bonus == null)
                return null;
            await Task.Run(() => { });
            return "AsyncTest";
        }

        public Task TestBonus2()
        {
            return null;
        }

        public Task TestBonus3()
        {
            return Task.FromResult(new object());
        }
#endif
    }
}
