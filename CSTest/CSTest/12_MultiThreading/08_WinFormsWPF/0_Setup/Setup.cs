using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._08_WinFormsWPF._0_Setup
{
    class Setup
    {
        public static void ExceptionMethod()
        {
            Debug.WriteLine("Начало работы");
            Thread.Sleep(5000);
            Debug.WriteLine("Возникновение исключения");
            throw new Exception("TEST");
        }

        public static void LongMethod()
        {
            Debug.WriteLine("Начало работы");
            Thread.Sleep(5000);
            Debug.WriteLine("Окончание работы");
        }

        public static void CancelTask(CancellationTokenSource cts)
        {
            if (cts != null)
            {
                Debug.WriteLine("Отмена задачи");
                cts.Cancel();
            }
        }

        public static void DisposeCancellationTokenSource(CancellationTokenSource cts)
        {
            cts.Dispose();
        }
    }
}
