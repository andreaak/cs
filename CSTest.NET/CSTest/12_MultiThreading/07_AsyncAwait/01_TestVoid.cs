using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5
    /*
    Ключевое слово async указывает компилятору, что метод, является асинхронным.
    await указывает компилятору, что в этой точке необходимо дождаться окончания асинхронной операции
    (при этом управление возвращается вызвавшему методу).
    Ассинхронный метод может иметь 3 типа возращаемого значения: void, Task, Task<T>

    Асинхронность vs Многопоточность
    После появления задач и ключевых слов async await использование асинхронности стало очень
    простым. Поэтому, программисты начали широко использовать асинхронность. Но, зачастую, люди
    считают, что асинхронность – это обязательно участие потока (Thread).
    Асинхронность не означает, что вы используете поток для выполнения операции. Асинхронное
    выполнение может происходить без участия потока. Это асинхронный ввод-вывод.
    Асинхронность в большинстве случаев как раз и означает асинхронные операции ввода-вывода,
    именно для этого она и была введена - чтобы использовать неблокирующие операции без участия
    потоков. Но, при этом, вам никто не запрещает использовать механизмы async await для CPU
    операций, пользуясь потоками.
    Зная о разнице между работой операций для потоков и для ввода-вывода, мы можем сформировать
    определение асинхронности, которое действительно ей соответствует.
    Асинхронность – это неблокирующее выполнение кода. 


    Асинхронность может применятся в следующих случаях:
     Операции CPU
    o Параллельное/неблокирующее/фоновое выполнение
    o Распараллеливание операции

     Операции ввода-вывода
    o Работа с файловой системой
    o Работа с сетью
    o Работа с удаленной базой данных
    o Работа с удаленными веб-сервисами

    CPU операции – это операции, которые выполняются ресурсами центрального процессора.
    Эти операции представлены обыкновенными синхронными методами, которые иногда необходимо
    вызывать асинхронно.
    Причины вызова таких методов асинхронно, в контексте вторичного потока:
    • Блокирование основного потока на время своего выполнения
    • Фоновое выполнение
    • Параллельное выполнение
    Для запуска синхронного метода асинхронно необходимо воспользоваться статическим методом Task.Run().
    Асинхронные CPU операции используют многопоточность, чтобы в контексте вторичных потоков
    выполнять необходимые операции, не блокируя основной/вызывающий поток. Такие операции
    зависят от ресурсов центрального процессора.



    Оператор await нельзя использовать в следующих ситуациях:
    • В теле синхронного метода, лямбда-выражения, анонимного метода
    • В блоке оператора lock
    • В выражении запроса (LINQ)
    • В небезопасном (unsafe) контексте
    • Нельзя создавать экземпляры ref struct типов в асинхронных методах.
    • В блоке catch (НАЧИНАЯ С ВЕРСИИ ЯЗЫКА C# 6.0 - РАЗРЕШЕНО ИСПОЛЬЗОВАТЬ)
    • В блоке finally (НАЧИНАЯ С ВЕРСИИ ЯЗЫКА C# 6.0 - РАЗРЕШЕНО ИСПОЛЬЗОВАТЬ)
    • В методе Main (НАЧИНАЯ С ВЕРСИИ ЯЗЫКА C# 7.1 – РАЗРЕШЕНО ИСПОЛЬЗОВАТЬ)
    */

    [TestFixture]
    public class _01_TestVoid
    {
        [Test]
        public async void TestAsyncAwait1_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderVoid mc = new _00_ClassUnderVoid();
            mc.OperationAsync2_ReturnVoid_WithoutActionAfterAwait();
            Debug.WriteLine("After Operation {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 16
            Operation ThreadID 10
            Begin
            Afret Operation 16
            End
            */
        }

        [Test]
        public async void TestAsyncAwait3_ReturnVoid_ActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderVoid mc = new _00_ClassUnderVoid();
            mc.OperationAsync3_ReturnVoid_ActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 17
            OperationAsync (Part I) ThreadID 17 Task: 
            Operation ThreadID 7 Task: 1
            Begin
            End
            OperationAsync (Part II) ThreadID 7 Task: 
            */
        }

        [Test]
        public void TestAsyncAwait5_ActionWithResultAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderVoid mc = new _00_ClassUnderVoid();
            mc.OperationAsync5_ReturnVoid_ActionWithResultAfterAwait();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 16
            Operation4 ThreadID 9 Task: 1
            Main thread ended. ThreadID 16

            Результат: 4
            */
        }

        [Test]
        public void TestAsyncAwait10_UseAPM()
        {
            Debug.WriteLine("Staring async download\n");
            _00_ClassUnderVoid mc = new _00_ClassUnderVoid();
            mc.DoDownloadAsync10_UseAPMItems();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Age: 6800
            X-Cache: HIT from proxy-zp.isd.dp.ua
            Connection: keep-alive
            Accept-Ranges: bytes
            Content-Length: 1020
            Content-Type: text/html
            Date: Thu, 22 Oct 2015 12:05:11 GMT
            ETag: "6082151bd56ea922e1357f5896a90d0a:1425454794"
            Last-Modified: Wed, 04 Mar 2015 07:39:54 GMT
            Server: Apache
            Via: 1.1 proxy-zp.isd.dp.ua (squid/3.5.10)


            Async download completed
            */
        }
    }

#endif
}