using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    class TestClassReflected
    {
        public void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }

        public void OperationAsync()
        {
            TestClassReflected.AsyncStateMachine stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start<TestClassReflected.AsyncStateMachine>(ref stateMachine);//builder вызывает MoveNext()
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AsyncStateMachine : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public TestClassReflected outer;
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
                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter, TestClassReflected.AsyncStateMachine>(ref awaiter, ref this);
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

        public void OperationAsync2()
        {
            TestClassReflected.AsyncStateMachine2 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AsyncStateMachine2 : IAsyncStateMachine
        {
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public TestClassReflected outer;

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

        public void OperationAsync3()
        {
            TestClassReflected.AsyncStateMachine3 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start<TestClassReflected.AsyncStateMachine3>(ref stateMachine);//builder вызывает MoveNext()
        }

        private struct AsyncStateMachine3 : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public TestClassReflected outer;

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

        public int Operation4()
        {
            Debug.WriteLine("Operation4 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
        }

        public void OperationAsync5()
        {
            TestClassReflected.AsyncStateMachine5 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncVoidMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
        }

        private struct AsyncStateMachine5 : IAsyncStateMachine
        {
            public int state;
            public AsyncVoidMethodBuilder builder;//для void OperationAsync() {...}
            public TestClassReflected outer;
            TaskAwaiter<int> awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<int> function = outer.Operation4;
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

        public Task OperationAsync6()
        {
            TestClassReflected.AsyncStateMachine6 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine6 : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder builder;//для Task OperationAsync() {...}
            public TestClassReflected outer;
            TaskAwaiter awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    awaiter = Task.Factory.StartNew(outer.Operation).GetAwaiter();

                    state = 0;

                    builder.AwaitOnCompleted(ref awaiter, ref this);
                    return;
                }
                // Срабатывает только при втором вызове
                // Задача помечается как успешно выполненная,
                // тогда срабатывает продолжение
                builder.SetResult();
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

        public Task<int> OperationAsync7()
        {
            TestClassReflected.AsyncStateMachine7 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder<int>.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine7 : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder<int> builder;//для Task OperationAsync() {...}
            public TestClassReflected outer;
            TaskAwaiter<int> awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<int> function = outer.Operation4;
                    Task<int> task = Task<int>.Factory.StartNew(function);

                    awaiter = task.GetAwaiter();
                    state = 0;

                    builder.AwaitOnCompleted(ref awaiter, ref this);
                    return;
                }
                // Срабатывает только при втором вызове
                // Задача помечается как успешно выполненная,
                // тогда срабатывает продолжение
                int result = awaiter.GetResult();
                builder.SetResult(result);
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

        public double Operation8(object argument)
        {
            Debug.WriteLine("Operation8 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return (double)argument * (double)argument;
        }

        public Task<double> OperationAsync8(double argument)
        {
            TestClassReflected.AsyncStateMachine8 stateMachine;
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder<double>.Create();
            stateMachine.state = -1;
            stateMachine.argument = argument;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine8 : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder<double> builder;//для Task OperationAsync() {...}
            public TestClassReflected outer;
            TaskAwaiter<double> awaiter;
            public double argument;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<object, double> function = outer.Operation8;
                    Task<double> task = Task<double>.Factory.StartNew(function, argument);

                    awaiter = task.GetAwaiter();
                    state = 0;

                    builder.AwaitOnCompleted(ref awaiter, ref this);
                    return;
                }
                // Срабатывает только при втором вызове
                // Задача помечается как успешно выполненная,
                // тогда срабатывает продолжение
                double result = awaiter.GetResult();
                builder.SetResult(result);
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
}
