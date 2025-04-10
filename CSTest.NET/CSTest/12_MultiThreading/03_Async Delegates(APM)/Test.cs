using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading
{
    /*
    Вызов асинхронных делегатов позволяет неявно помещать потоки в ThreadPool, 
    тем самым избавляя программиста от необходимости работать с ним напрямую. 
    Сигнатура метода BeginInvoke не соответствует методу Invoke. 
    Это объясняется тем, что нужен некоторый способ идентификации определенного элемента работы, 
    который только что был отложен вызовом BeginInvoke. Таким образом, 
    BeginInvoke возвращает ссылку на объект, реализующий интерфейс IAsyncResult. 
    Этот объект подобен cookie-набору, который сохраняется для идентификации выполняющегося элемента работы. 
    Через методы интерфейса IAsyncResult можно проверять состояние операции, например, ее готовность. 
    Когда поток, запрошенный для выполнения операции, завершит свою работу, он вызывает EndInvoke на делегате. 
    Однако, поскольку метод должен иметь способ идентификации асинхронной операции, 
    результат которой нужно получить, ему должен быть передан объект, полученный из метода BeginInvoke. 
    Если в процессе асинхронного выполнения в пуле потоков целевого кода делегата будет сгенерировано исключение, 
    оно сгенерируется повторно, когда инициирующий поток вызовет EndInvoke. 
    
    Интерфейс IAsyncResult реализован с помощью классов, содержащих методы, которые могут работать асинхронно. 
    Объект, который обеспечивает работу интерфейса IAsyncResult, 
    хранит в себе сведения о состоянии асинхронной операции и предоставляет объект синхронизации, 
    сигнализирующий потоку о завершении операции. 
    Для обработки результатов асинхронной операции в отдельном потоке используется делегат AsyncCallback. 
    Делегат AsyncCallback представляет метод обратного вызова, который вызывается при завершении асинхронной операции. 
    Метод обратного вызова принимает параметр IAsyncResult, который впоследствии используется 
    для получения результатов асинхронной операции. 
    Класс AsyncResult используется в сочетании с асинхронными вызовами методов с помощью делегатов. 
    IAsyncResult, возвращенный из делегатского метода BeginInvoke, можно привести к AsyncResult. 
    AsyncResult имеет свойство AsyncDelegate, содержащее объект делегата, к которому был направлен асинхронный вызов. 
    */

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestAsyncDelegates1()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            var myDelegate = new Action(_01_Base.Method);

            // Выполнение метода Method в отдельном потоке, взятом из пула потоков.
            myDelegate.BeginInvoke(null, null);
            Debug.WriteLine("Main");
            // Delay.
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(50);
                Debug.Write(".");
            }
            /*
            Первичный поток: Id 10
            Main

            Асинхронный метод запущен.
            Вторичный поток: Id 8
            ...............................................................................
            .................................................................................
            Асинхронная операция завершена.
            */
        }

        [Test]
        public void TestAsyncDelegates2EndInvoke()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            var myDelegate = new Action(_01_Base.Method);

            // BeginInvoke - выполняем метод Method в отдельном потоке, взятом из пула потоков.
            // IAsyncResult - представляет состояние асинхронной операции.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(null, null);

            Debug.WriteLine("Первичный поток продолжает работать.");

            // Ожидание завершения асинхронной операции.
            myDelegate.EndInvoke(asyncResult);

            Debug.WriteLine("Первичный поток завершил работу.");

            /*
            Первичный поток: Id 10
            Первичный поток продолжает работать.

            Асинхронный метод запущен.
            Вторичный поток: Id 9
            ................................................................................
            Асинхронная операция завершена.
            Первичный поток завершил работу.
            */
        }

        [Test]
        public void TestAsyncDelegates3EndInvokeWithResult()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            var myDelegate = new Func<int, int, int>(_01_Base.Add);

            // Так как класс делегата сообщается с методами, которые принимают два целочисленных параметра, метод BeginInvoke также
            // начинает принимать два дополнительных параметра, кроме двух последних постоянных аргументов.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            // Ожидание завершения асинхронной операции и получение результата работы метода.
            int result = myDelegate.EndInvoke(asyncResult);

            Debug.WriteLine("Результат = " + result);
            /*
            Первичный поток: Id 10
            Поток: Id 9
            Результат = 3
            */
        }

        [Test]
        public void TestAsyncDelegates4Polling()
        {
            var myDelegate = new Func<int, int, int>(_01_Base.Add);

            // Запуск асинхронной задачи.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            Debug.WriteLine("Асинхронный метод запущен. Метод Main продолжает работать.");

            // Выполнение цикла до тех пор, пока работает асинхронная операция.
            while (!asyncResult.IsCompleted)
            {
                Thread.Sleep(100);
                Debug.Write(".");
            }

            // Получение результата асинхронной операции.
            int result = myDelegate.EndInvoke(asyncResult);

            Debug.WriteLine("\nРезультат = " + result);
            /*
            Поток: Id 9
            ...................
            Результат = 3
            */
        }

        [Test]
        public void TestAsyncDelegates5WaitHandle()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            var myDelegate = new Func<int, int, int>(_01_Base.Add);

            // Запуск асинхронной задачи.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            Debug.WriteLine("Асинхронный метод запущен. Метод Main продолжает работать.");
            Debug.WriteLine("Метод Main ожидает завершения асинхронной задачи.");

            Debug.WriteLine(asyncResult.AsyncWaitHandle.GetType());

            // AsyncWaitHandle типа WaitHandle, переходит в сигнальное состояние при завершении асинхронной операции.
            asyncResult.AsyncWaitHandle.WaitOne();
            Debug.WriteLine("Асинхронный метод завершен.");

            // Получение результата асинхронной операции.
            int result = myDelegate.EndInvoke(asyncResult);

            // Закрываем WaitHandle.
            asyncResult.AsyncWaitHandle.Close();

            Debug.WriteLine("Результат = " + result);

            /*
            Первичный поток: Id 10
            Асинхронный метод запущен. Метод Main продолжает работать.
            Поток: Id 9
            Метод Main ожидает завершения асинхронной задачи.
            System.Threading.ManualResetEvent
            Асинхронный метод завершен.
            Результат = 3
            */
        }

        [Test]
        public void TestAsyncDelegates6WaitHandle()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            var myDelegate = new Func<int, int, int>(_01_Base.Add);

            // Запуск асинхронной задачи.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            Debug.WriteLine("Асинхронный метод запущен. Метод Main продолжает работать.");
            Debug.WriteLine("Метод Main ожидает завершения асинхронной задачи.");

            Debug.WriteLine(asyncResult.GetType());
            Debug.WriteLine(asyncResult.AsyncWaitHandle.GetType());

            // AsyncWaitHandle типа WaitHandle, переходит в сигнальное состояние при завершении асинхронной операции.
            while (true)
            {
                Debug.Write(".");
                if (asyncResult.AsyncWaitHandle.WaitOne(50, false))
                {
                    Debug.WriteLine("\nМожно извлечь результат сейчас");
                    break;
                }
            }
            Debug.WriteLine("Асинхронный метод завершен.");

            // Получение результата асинхронной операции.
            int result = myDelegate.EndInvoke(asyncResult);

            // Закрываем WaitHandle.
            asyncResult.AsyncWaitHandle.Close();

            Debug.WriteLine("Результат = " + result);

            /*
            Первичный поток: Id 10
            Асинхронный метод запущен. Метод Main продолжает работать.
            Метод Main ожидает завершения асинхронной задачи.
            Поток: Id 8
            System.Runtime.Remoting.Messaging.AsyncResult
            System.Threading.ManualResetEvent
            .................................
            Можно извлечь результат сейчас
            Асинхронный метод завершен.
            Результат = 3
            */
        }

        [Test]
        public void TestAsyncDelegates7CallBack()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            Action myDelegate = new Action(_01_Base.Method);

            // Делегат, метод которого будет запущен по завершению асинхронной операции.
            AsyncCallback callback = new AsyncCallback(_01_Base.CallBack);

            // Первый параметр: 
            // Принимает метод обратного вызова, который должен сработать по завершению асинхронной операции.
            // Второй параметр: 
            // Дополнительный объект хранящий состояние, который будет доступен в методе обратного вызова.
            myDelegate.BeginInvoke(callback, "Hello world");

            Debug.WriteLine("Первичный поток продолжает работать.");
            Thread.Sleep(5000);

            /*
            Первичный поток: Id 10
            Первичный поток продолжает работать.

            Асинхронный метод запущен.

            Вторичный поток: Id 8
            ................................................................................
            Асинхронная операция завершена.

            Callback метод. Thread Id 8
            Информация связанная с асинхронной операцией - Hello world
            */
        }

        [Test]
        public void TestAsyncDelegates8CallBackWithResult()
        {
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            Func<int, int, int> myDelegate = new Func<int, int, int>(_01_Base.Add);

            myDelegate.BeginInvoke(1, 2, _01_Base.CallBackWithResult, "a + b = {0}");

            Debug.WriteLine("Первичный поток завершил работу.");

            Thread.Sleep(5000);

            /*
            Первичный поток: Id 10
            Первичный поток завершил работу.
            Поток: Id 9
            Callback метод начал работу.
            Callback метод. Thread Id 9
            Результат асинхронной операции: a + b = 3
            Callback метод завершил работу.
            */
        }

        // По умолчанию в Асинхронном шаблоне, IsBackground = true (С завершением первичного потока завершается вторичный).
        // IsBackground = false (Первичный поток ожидает окончания работы вторичного потока).
        [Test]
        public void TestAsyncDelegates9CallBack()
        {
            Debug.WriteLine("Первичный поток начал работу.");
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            Action work = new Action(_01_Base.MethodWithChangeBackground);
            work.BeginInvoke(new AsyncCallback(_01_Base.CallBack2), (object)work);

            Debug.WriteLine("\nПервичный поток завершил работу.\n");
            Thread.Sleep(2000);
            /*
            Первичный поток начал работу.
            Первичный поток: Id 10
            Вторичный поток начал работу.
            Вторичный поток. Thread Id 9
            ...................
            Первичный поток завершил работу.

            .A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
            Callback метод начал работу.
            Callback метод. Thread Id 9
            */
        }
    }
}
