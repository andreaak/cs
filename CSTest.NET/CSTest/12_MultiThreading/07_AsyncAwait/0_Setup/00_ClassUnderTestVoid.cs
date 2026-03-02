using System.Diagnostics;
using System.Net;

namespace CSTest._12_MultiThreading._07_AsyncAwait._0_Setup
{

#if CS5

    class _00_ClassUnderVoid
    {


        public async void OperationAsync2_ReturnVoid_WithoutActionAfterAwait()
        {
            Task task = new Task(TestOperations.Operation);
            task.Start();
            await task;
        }

        public async void OperationAsync3_ReturnVoid_ActionAfterAwait()
        {
            /*
            Id потока совпадает с Id первичного потока. Это значит, что
            данный метод начинает выполняться в контексте первичного потока.
            */
            Debug.WriteLine("OperationAsync (Part I) ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Task task = new Task(TestOperations.Operation);
            task.Start();
            await task;
            /*
            Id потока совпадает с Id вторичного потока. Это значит, что
            данный метод начинает выполняться в контексте вторичного потока.
            */
            Debug.WriteLine("OperationAsync (Part II) ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
        }

        public async void OperationAsync5_ReturnVoid_ActionWithResultAfterAwait()
        {
            Task<int> task = Task<int>.Factory.StartNew(TestOperations.OperationWithResult);
            // TaskAwaiter<int> awaiter = task.GetAwaiter();
            // Action continuation = () => Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
            // awaiter.OnCompleted(continuation);
            // меняем на:
            Debug.WriteLine("\nРезультат: {0}\n", await task);
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
