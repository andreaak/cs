using System.Diagnostics;


namespace CSTest._12_MultiThreading._07_AsyncAwait._0_Setup
{
#if CS5

    class TestOperations
    {
        public static void Operation()
        {
            Debug.WriteLine("Operation ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Debug.WriteLine("Begin");
            Thread.Sleep(2000);
            Debug.WriteLine("End");
        }

        public static async Task Operation2()
        {
            Debug.WriteLine("Operation2 ThreadID {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            Debug.WriteLine("Begin");
            await Task.Delay(2000);
            Debug.WriteLine("End");
        }

        public static int OperationWithResult()
        {
            Debug.WriteLine("OperationWithResult ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return 2 + 2;
        }

        public static double OperationWithArgumentAndResult(object argument)
        {
            Debug.WriteLine("OperationWithArgumentAndResult ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return (double)argument * (double)argument;
        }

    }
#endif
}
