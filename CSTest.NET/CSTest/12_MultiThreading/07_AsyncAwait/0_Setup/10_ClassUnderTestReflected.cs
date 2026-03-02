using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CSTest._12_MultiThreading._07_AsyncAwait._0_Setup
{
    class _10_ClassUnderTestReflected
    {

#if CS5

        //public async Task OperationAsync6_ReturnTask()
        //{
        //    await Task.Factory.StartNew(Operation);
        //}

        public Task OperationAsync6_ReturnTask()
        {
            _10_ClassUnderTestReflected.AsyncStateMachine6Continuation stateMachine = new AsyncStateMachine6Continuation(); ;
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine6Continuation : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder builder;//для Task OperationAsync() {...}
            public _10_ClassUnderTestReflected outer;
            TaskAwaiter awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    awaiter = Task.Factory.StartNew(TestOperations.Operation).GetAwaiter();

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

        //public async Task<int> OperationAsync8_ReturnTaskWithResult()
        //{
        //    //int result = await Task<int>.Factory.StartNew(Operation4);
        //    //return result;
        //    return await Task<int>.Factory.StartNew(OperationWithResult);
        //}

        public Task<int> OperationAsync8_ReturnTaskWithResult()
        {
            _10_ClassUnderTestReflected.AsyncStateMachine8ReturnValue stateMachine = new AsyncStateMachine8ReturnValue();
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder<int>.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine8ReturnValue : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder<int> builder;//для Task OperationAsync() {...}
            public _10_ClassUnderTestReflected outer;
            TaskAwaiter<int> awaiter;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<int> function = TestOperations.OperationWithResult;
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

        //public async Task<double> OperationAsync9_ReturnTaskWithResult_Argument(double argument)
        //{
        //    //int result = await Task<int>.Factory.StartNew(Operation4);
        //    //return result;
        //    return await Task<double>.Factory.StartNew(OperationWithArgumentAndResult, argument);
        //}

        public Task<double> OperationAsync9_ReturnTaskWithResult_Argument(double argument)
        {
            _10_ClassUnderTestReflected.AsyncStateMachine9ReturnAndArgument stateMachine = new AsyncStateMachine9ReturnAndArgument();
            stateMachine.outer = this;
            stateMachine.builder = AsyncTaskMethodBuilder<double>.Create();
            stateMachine.state = -1;
            stateMachine.argument = argument;
            stateMachine.builder.Start(ref stateMachine);//builder вызывает MoveNext()
            return stateMachine.builder.Task;
        }

        private struct AsyncStateMachine9ReturnAndArgument : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder<double> builder;//для Task OperationAsync() {...}
            public _10_ClassUnderTestReflected outer;
            TaskAwaiter<double> awaiter;
            public double argument;

            #region IAsyncStateMachine Members

            void IAsyncStateMachine.MoveNext()
            {
                if (state == -1)
                {
                    Func<object, double> function = TestOperations.OperationWithArgumentAndResult;
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

#endif
    }
}
