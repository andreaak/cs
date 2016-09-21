using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _05_TestWhen
    {
        [Test]
        public void TestAsyncAwait14_WaitAll()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _05_TestWhen();
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

        public async void DoDownloadAsync_WaitAll(CancellationTokenSource cts)
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
}

#endif
}