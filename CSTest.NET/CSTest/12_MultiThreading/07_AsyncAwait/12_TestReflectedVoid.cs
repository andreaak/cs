
using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    internal class _12_TestReflectedVoid
    {
        [Test]
        public void TestAsyncAwait1_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _12_TestReflectedVoid mc = new _12_TestReflectedVoid();
            mc.OperationAsync_ReturnVoid_WithoutActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [Test]
        public void TestAsyncAwait2_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _12_TestReflectedVoid mc = new _12_TestReflectedVoid();
            mc.OperationAsync2_ReturnVoid_WithoutActionAfterAwaitClean();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [Test]
        public void TestAsyncAwait3_ReturnVoid_ActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _12_TestReflectedVoid mc = new _12_TestReflectedVoid();
            mc.OperationAsync3_ReturnVoid_ActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            MoveNext() call 1 times in ThreadID 10
            OperationAsync (Part I) ThreadID 10
            Operation ThreadID 9
            Begin

            SetStateMachine() ThreadID 10
            stateMachine.GetHashCode() 346948956
            this.GetHashCode() 346948956

            End
            MoveNext() call 2 times in ThreadID 9
            OperationAsync (Part II) ThreadID 9
            */
        }

        [Test]
        public void TestAsyncAwait5_ActionWithResultAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _12_TestReflectedVoid mc = new _12_TestReflectedVoid();
            mc.OperationAsync5_ReturnVoid_ActionWithResultAfterAwait();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 8
            Main thread ended. ThreadID 10

            Результат: 4
            */
        }

        public void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }

        public int OperationWithResult()
        {
            Debug.WriteLine("OperationWithResult ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
        }

        //public async void OperationAsync2_ReturnVoid_WithoutActionAfterAwait()
        //{
        //    Task task = new Task(Operation);
        //    task.Start();
        //    await task;
        //}

        public void OperationAsync_ReturnVoid_WithoutActionAfterAwait()
        {
            _12_TestReflectedVoid.AsyncStateMachine stateMachine = new AsyncStateMachine();
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start<_12_TestReflectedVoid.AsyncStateMachine>(ref stateMachine);//builder вызывает MoveNext()
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AsyncStateMachine : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public _12_TestReflectedVoid outer;
            private TaskAwaiter awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                int num1 = this.state;
                try
                {
                    TaskAwaiter awaiter;
                    int num2;
                    if (num1 == 0 || num1 != 1)
                    {
                        Task task = new Task(new Action(outer.Operation));
                        task.Start();
                        awaiter = task.GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.state = num2 = 1;
                            this.awaiter = awaiter;
                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter, _12_TestReflectedVoid.AsyncStateMachine>(ref awaiter, ref this);
                            return;
                        }
                        else
                        {
                            awaiter = this.awaiter;
                            this.awaiter = new TaskAwaiter();
                            this.state = num2 = -1;
                        }
                        awaiter.GetResult();
                        awaiter = new TaskAwaiter();
                    }
                }
                catch (Exception ex)
                {
                    this.state = -2;
                    this.builder.SetException(ex);
                    return;
                }
                this.state = -2;
                this.builder.SetResult();
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.builder.SetStateMachine(stateMachine);
            }

            #endregion
        }

        //public async void OperationAsync2_ReturnVoid_WithoutActionAfterAwait()
        //{
        //    Task task = new Task(Operation);
        //    task.Start();
        //    await task;
        //}

        public void OperationAsync2_ReturnVoid_WithoutActionAfterAwaitClean()
        {
            _12_TestReflectedVoid.AsyncStateMachine2 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AsyncStateMachine2 : IAsyncStateMachine
        {
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public _12_TestReflectedVoid outer;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                Task task = new Task(new Action(outer.Operation));
                task.Start();
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                //this.builder.SetStateMachine(stateMachine);
            }

            #endregion
        }

        //public async void OperationAsync3_ReturnVoid_ActionAfterAwait()
        //{
        //    /*
        //    Id потока совпадает с Id первичного потока. Это значит, что
        //    данный метод начинает выполняться в контексте первичного потока.
        //    */
        //    Debug.WriteLine("OperationAsync (Part I) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
        //    Task task = new Task(Operation);
        //    task.Start();
        //    await task;
        //    /*
        //    Id потока совпадает с Id вторичного потока. Это значит, что
        //    данный метод начинает выполняться в контексте вторичного потока.
        //    */
        //    Debug.WriteLine("OperationAsync (Part II) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
        //}

        public void OperationAsync3_ReturnVoid_ActionAfterAwait()
        {
            _12_TestReflectedVoid.AsyncStateMachine3 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start<_12_TestReflectedVoid.AsyncStateMachine3>(ref stateMachine);//builder вызывает MoveNext()
        }

        private struct AsyncStateMachine3 : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public _12_TestReflectedVoid outer;

            static int counterCallMoveNext;

            #region IAsyncStateMachine Members

            /*
            builder.Start() первый раз вызывает метод MoveNext - Синхронно,
            а второй раз builder.AwaitOnCompleted() вызывает его - Асинхронно, 
            только после того как отработает
            */
            void IAsyncStateMachine.MoveNext()
            {
                Debug.WriteLine("MoveNext() call {0} times in ThreadID {1}", ++counterCallMoveNext, Thread.CurrentThread.ManagedThreadId);
                if (state == -1)
                {
                    Debug.WriteLine("OperationAsync (Part I) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
                    Task task = new Task(new Action(outer.Operation));
                    task.Start();
                    state = 0;
                    TaskAwaiter awaiter = task.GetAwaiter();
                    builder.AwaitOnCompleted(ref awaiter, ref this);
                    return;
                }
                //Срабатывает только при втором вызове
                Debug.WriteLine("OperationAsync (Part II) ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            }

            /*
            builder.AwaitOnCompleted() вызывает данный метод синхронно во время выполнения задачи.
            */
            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Debug.WriteLine("\nSetStateMachine() ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("stateMachine.GetHashCode() {0}", stateMachine.GetHashCode());
                Debug.WriteLine("this.GetHashCode() {0}\n", this.GetHashCode());
                this.builder.SetStateMachine(stateMachine);
            }

            #endregion
        }

        //public async void OperationAsync5_ReturnVoid_ActionWithResultAfterAwait()
        //{
        //    Task<int> task = Task<int>.Factory.StartNew(OperationWithResult);
        //    // TaskAwaiter<int> awaiter = task.GetAwaiter();
        //    // Action continuation = () => Debug.WriteLine("\nРезультат: {0}\n", awaiter.GetResult());
        //    // awaiter.OnCompleted(continuation);
        //    // меняем на:
        //    Debug.WriteLine("\nРезультат: {0}\n", await task);
        //}


        public void OperationAsync5_ReturnVoid_ActionWithResultAfterAwait()
        {
            _12_TestReflectedVoid.AsyncStateMachine5ReturnValue stateMachine = new AsyncStateMachine5ReturnValue();
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
        }

        private struct AsyncStateMachine5ReturnValue : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public _12_TestReflectedVoid outer;
            TaskAwaiter<int> awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<int> function = outer.OperationWithResult;
                    Task<int> task = Task<int>.Factory.StartNew(function);

                    state = 0;
                    awaiter = task.GetAwaiter();

                    builder.AwaitOnCompleted(ref awaiter, ref this);
                    return;
                }
                //Срабатывает только при втором вызове
                int result = awaiter.GetResult();
                Debug.WriteLine("\nРезультат: {0}\n", result);
            }

            /*
            builder.AwaitOnCompleted() вызывает данный метод синхронно во время выполнения задачи.
            */
            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.builder.SetStateMachine(stateMachine);
            }

            #endregion
        }
    }

#endif
}














