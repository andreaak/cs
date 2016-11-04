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
    class _07_ClassUnderTest
    {
        public async Task SumPageSizesAsync(CancellationToken ct)
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

            Debug.WriteLine("Total bytes returned:  {0}", total);
        }


        public async void DoAsync_Generic(CancellationTokenSource cts, Func<CancellationToken, Task> func)
        {
            try
            {
                Task sumTask = func(cts.Token);
                //Do some other stuff...
                //....

                //Wait until sumTask is Finished...
                await sumTask;
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Operation canceled.");
            }
            catch (Exception)
            {
                Debug.WriteLine("Operation failed.");
            }

            Debug.WriteLine("DoAsync_Generic ended");
        }

        public async Task DoAsync_CancelToken(CancellationToken ct)
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                Debug.WriteLine("Task {0} started", item);
                await Task.Delay(item * 100, ct);
                Debug.WriteLine("Task {0} ended", item);
            }
        }

        public async Task DoAsync_ThrowIfCancellationRequested(CancellationToken ct)
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                Debug.WriteLine("Task {0} started", item);
                await Task.Delay(item * 100);
                Debug.WriteLine("Task {0} ended", item);
                ct.ThrowIfCancellationRequested();
            }
        }

        public async Task DoAsync_IsCancellationRequested(CancellationToken ct)
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                Debug.WriteLine("Task {0} started", item);
                await Task.Delay(item * 100);
                Debug.WriteLine("Task {0} ended", item);
                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}

#endif