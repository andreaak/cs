#if CS5

using System.Diagnostics;

namespace CSTest._12_MultiThreading._07_AsyncAwait._0_Setup
{
    class _07_ClassUnderTest
    {
        public async Task CancelHttpClient(CancellationToken ct)
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


        public async Task CancelDelayAsync(CancellationToken ct)
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                Debug.WriteLine("Task {0} started", item);
                await Task.Delay(item * 100, ct);
                Debug.WriteLine("Task {0} ended", item);
            }
        }

        public async Task ThrowIfCancellationRequested(CancellationToken ct)
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                Debug.WriteLine("Task {0} started", item);
                await Task.Delay(item * 100);
                Debug.WriteLine("Task {0} ended", item);
                ct.ThrowIfCancellationRequested();
            }
        }

        public async Task IsCancellationRequested(CancellationToken ct)
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
                Debug.WriteLine("OperationCanceledException: Operation canceled.");
            }
            catch (Exception)
            {
                Debug.WriteLine("Operation failed.");
            }

            Debug.WriteLine("DoAsync_Generic ended");
        }
    }
}

#endif