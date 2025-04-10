using System;
using System.Diagnostics;
//using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace CSTest._12_MultiThreading
{
    class _01_Base
    {
        // Метод для выполнения в отдельном потоке.
        public static void Method()
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            Debug.WriteLine("Асинхронный метод запущен.");
            Debug.WriteLine("Вторичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(50);
                Debug.Write(".");
            }

            Debug.WriteLine("\nАсинхронная операция завершена.");
            //Console.ForegroundColor = ConsoleColor.Gray;
        }


        // Метод для выполнения в отдельном потоке.
        public static int Add(int a, int b)
        {
            Debug.WriteLine("Поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return a + b;
        }
        
        // Callback метод для обработки завершения асинхронной операции.
        public static void CallBack(IAsyncResult asyncResult)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Debug.WriteLine("Callback метод начал работу.");
            Debug.WriteLine("Callback метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Информация связанная с асинхронной операцией - " + asyncResult.AsyncState);
            //Console.ForegroundColor = ConsoleColor.Gray;
            Debug.WriteLine("\nCallback метод завершил работу.");
        }

        // Метод обработки завершения асинхронной операции.
        public static void CallBackWithResult(IAsyncResult iAsyncResult)
        {
            //Debug.WriteLine("Callback метод начал работу.");
            //Debug.WriteLine("Callback метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            //// Получение экземпляра делегата, на котором была вызвана асинхронная операция.
            //AsyncResult asyncResult = (AsyncResult)iAsyncResult;
            //var caller = (Func<int, int, int>)asyncResult.AsyncDelegate;

            //// Получение результатов асинхронной операции.
            //int sum = caller.EndInvoke(iAsyncResult);

            ////получение параметра
            //string result = string.Format(iAsyncResult.AsyncState.ToString(), sum);
            //Debug.WriteLine("Результат асинхронной операции: " + result);
            //Debug.WriteLine("Callback метод завершил работу.");
        }

        public static void MethodWithChangeBackground()
        {
            Debug.WriteLine("Вторичный поток начал работу.");
            Debug.WriteLine("Вторичный поток. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            //Плохой стиль. В Debug не работает
            Thread.CurrentThread.IsBackground = false; // Закомментировать.


            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(50);
                Debug.Write(".");
            }
                
            Debug.WriteLine("\nВторичный поток завершил работу.");
        }

        public static void CallBack2(IAsyncResult asyncResult)
        {
            Debug.WriteLine("Callback метод начал работу.");
            Debug.WriteLine("Callback метод. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            //получение параметра
            var work = asyncResult.AsyncState as Action;
            if (work != null)
            {
                work.EndInvoke(asyncResult);
            }
            Debug.WriteLine("Callback метод завершил работу.");
        }
    }

    internal enum Color
    {
        White, // Присваивается значение 0
        Red, // Присваивается значение 1
        Green, // Присваивается значение 2
        Blue, // Присваивается значение 3
        Orange // Присваивается значение 4
    }

}
