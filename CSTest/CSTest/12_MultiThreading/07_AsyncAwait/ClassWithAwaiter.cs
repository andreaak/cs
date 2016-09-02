using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class ClassWithAwaiter
    {
        public int OperationWithResult()
        {
            Debug.WriteLine("OperationWithResult ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
        }

        public void OperationAsync4_ReturnVoid_UseAwaiter()
        {
            Task<int> task = Task<int>.Factory.StartNew(OperationWithResult);
            TaskAwaiter<int> awaiter = task.GetAwaiter();
            Action continuation = () => Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
            awaiter.OnCompleted(continuation);
        }

        public async void OperationAsync5_CustomAwaiter()
        {
            var customTask = new CustomAwaitable();
            Action continuation = () =>
            {
                Thread.Sleep(2000);
                Debug.WriteLine("\nРезультат: {0}\n", customTask.GetResult());
            };
            customTask.OnCompleted(continuation);
            var result = await customTask;
            Debug.WriteLine(result);
        }
    }

    class CustomAwaitable : INotifyCompletion
    {
        private Task task; 

        public CustomAwaitable GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted
        {
            get
            {
                Debug.WriteLine("IsCompleted");
                return task.IsCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            Debug.WriteLine("OnCompleted");
            this.task = Task.Run(continuation);
        }

        public string GetResult()
        {
            Debug.WriteLine("GetResult");
            return "Return Value";
        }
    }
}
