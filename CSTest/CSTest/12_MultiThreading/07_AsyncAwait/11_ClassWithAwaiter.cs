#if CS5

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class _11_ClassWithAwaiter
    {
        public int OperationWithResult()
        {
            Debug.WriteLine("OperationWithResult ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Debug.WriteLine("OperationWithResult End ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            return 2 + 2;
        }

        public void OperationAsync4_UseAwaiter_WithResult()
        {
            Task<int> task = Task<int>.Factory.StartNew(OperationWithResult);
            TaskAwaiter<int> awaiter = task.GetAwaiter();
            Action continuation = () => Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
            awaiter.OnCompleted(continuation);
        }

        public async void OperationAsync5_CustomAwaiter()
        {
            var customTask = new CustomAwaitable<int>(OperationWithResult);
            var awaiter = customTask.GetAwaiter();
            Action continuation = () =>
            {
                Thread.Sleep(1000);
                Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
            };
            awaiter.OnCompleted(continuation);
            var result = await customTask;
            Debug.WriteLine(result);
        }
    }

    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }

    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }

    class CustomAwaitable<T> : IAwaitable<T>
    {
        public Task<T> Task { get; }

        public CustomAwaitable(Func<T> delFunc)
        {
            Task = Task<T>.Factory.StartNew(delFunc);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new CustomAwaiter<T>(this);
        }
    }

    class CustomAwaiter<T> : IAwaiter<T>
    {
        private Action continuation;
        private readonly Task<T> task;

        public CustomAwaiter(CustomAwaitable<T> awaitable)
        {
            task = awaitable.Task;
        }

        public bool IsCompleted
        {
            get
            {
                Debug.WriteLine("IsCompleted " + task.IsCompleted);
                return task.IsCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            Debug.WriteLine("OnCompleted");
            if (this.continuation == null)
                this.continuation = continuation;
            else if(task.IsCompleted)
                Task.Run(continuation);
        }

        public T GetResult()
        {
            Debug.WriteLine("GetResult");
            return task.Result;
        }
    }
}
#endif