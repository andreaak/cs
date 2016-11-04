#if CS5

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class _05_ClassUnderTest
    {

        public async Task OperationAsync(int item)
        {
            Debug.WriteLine("Task {0} started", item);
            await Task.Delay(item * 200);
            Debug.WriteLine("Task {0} completed", item);
        }

        public async Task<int> OperationWithResultAsync(int item)
        {
            Debug.WriteLine("Task {0} started", item);
            await Task.Delay(item * 200);
            Debug.WriteLine("Task {0} completed", item);
            return item;
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
}

#endif
