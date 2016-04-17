using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._01_Thread._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    /*
    Многозадачность — свойство операционной системы или среды программирования обеспечивать 
    возможность параллельной (или псевдопараллельной) обработки нескольких процессов
    Многозадачность бывает 2 видов:
    На основе процессов - позволяет выполнять одновременно более одной программы в контексте операционной системы.
        При использовании многозадачности на основе процессов, программа является наименьшей единицей кода, 
        выполнение которой может контролировать планировщик задач.
    На основе потоков - у каждого процесса может быть один или более потоков.
        Это означает, что процесс может решать более одной задачи одновременно.
        Многозадачность на основе потоков означает параллельное выполнение отдельных частей программы.

    C# поддерживает параллельное выполнение кода через многопоточность. 
    Потоки и процессы — это связанные понятия в вычислительной технике. 
    Оба представляют собой последовательность инструкций, которые должны выполняться процессором в определенном порядке.   
    Поток – это независимый путь исполнения, способный выполняться одновременно с другими потоками. 
    Поток (Thread) – путь выполнения действий внутри  исполняемого приложения. 
    Поток - элементарная единица исполнения, которую можно планировать средствами операционной системы. 
    Процессы существуют в операционной системе и соответствуют тому, что пользователи видят как программы или приложения. 
    Поток существует внутри процесса. Каждый процесс состоит из одного или более потоков. 
    Программа на C# запускается как единственный поток, автоматически создаваемый CLR и операционной системой 
    (“главный” или первичный поток), и становится многопоточной при помощи создания дополнительных потоков.
    Процесс может создавать один или более потоков для выполнения частей программного кода, связанного с процессом. 
    Следует использовать делегат ThreadStart или ParameterizedThreadStart для задания программного кода, управляемого потоком. 
    С помощью делегата ParameterizedThreadStart можно передавать данные в потоковую процедуру. 
    
    CLR назначает каждому потоку свой стек размером 1МБ и методы имеют свои собственные локальные переменные.

    Наиболее опасными проблемами, из тех, что возникают во время параллельного выполнения нескольких потоков
    являются проблемы взаимоблокировки и состояния гонки. 
    Взаимоблокировка – это такое состояние потоков, когда каждый ждет окончания работы остальных и при этом ничего не делает. 
    Состояние гонки – это попытки потоков получить доступ к одному ресурсу без должной его блокировки. 
    */

    [TestClass]
    public class _01_BasicTest
    {

        [TestMethod]
        // Создать поток исполнения.
        public void TestThreadStart1Instance()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);

            // Сначала сконструировать объект типа MyThread. 
            _01_BasicThread mt = new _01_BasicThread("Потомок #1");
            // Далее сконструировать поток из этого объекта. 
            ThreadStart method = new ThreadStart(mt.Run);
            Thread newThrd = new Thread(method);
            // И наконец, начать выполнение потока. 
            newThrd.Start();
            do
            {

                Debug.WriteLine(".");
                Thread.Sleep(100);
            } while (mt.Count != 10);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
            .
                      Потомок #1 начат.
                      Thread HashCode 13
                      Thread Id 13
            .
                      В потоке Потомок #1, Count = 0
            .
                      В потоке Потомок #1, Count = 1
            .
                      В потоке Потомок #1, Count = 2
            .
                      В потоке Потомок #1, Count = 3
            .
                      В потоке Потомок #1, Count = 4
            .
                      В потоке Потомок #1, Count = 5
            .
                      В потоке Потомок #1, Count = 6
            .
                      В потоке Потомок #1, Count = 7
            .
                      В потоке Потомок #1, Count = 8
            .
                      В потоке Потомок #1, Count = 9
                      Потомок #1 завершен.
            The thread '<No Name>' (0x237c) has exited with code 0 (0x0).
            Основной поток завершен.
            */
        }

        [TestMethod]
        // CLR назначает каждому потоку свой стек и методы имеют свои собственные локальные переменные.
        // При работе с потоками можно представить, что строится копия статического метода
        // На самом деле происходит работа по сохранению текущего значения потока
        public void TestThreadStart1Static()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);

            // Сначала сконструировать объект типа MyThread. 
            _01_BasicThread mt = new _01_BasicThread("Потомок #1");
            // Далее сконструировать поток из статического метода. 
            ThreadStart method = new ThreadStart(_01_BasicThreadUtils.WriteSecond);
            Thread newThrd = new Thread(method);
            newThrd.Start();
            _01_BasicThreadUtils.WriteSecond(null);

            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
            Thread Id 10, counter = 0
                      Thread Id 13, counter = 0
                      Thread Id 13, counter = 1
                      Thread Id 13, counter = 2
                      Thread Id 13, counter = 3
                      Thread Id 13, counter = 4
                      Thread Id 13, counter = 5
            Thread Id 10, counter = 1
                      Thread Id 13, counter = 6
            Thread Id 10, counter = 2
            Thread Id 10, counter = 3
                      Thread Id 13, counter = 7
                      Thread Id 13, counter = 8
                      Thread Id 13, counter = 9
            Thread Id 10, counter = 4
            The thread '<No Name>' (0xa14) has exited with code 0 (0x0).
            Thread Id 10, counter = 5
            Thread Id 10, counter = 6
            Thread Id 10, counter = 7
            Thread Id 10, counter = 8
            Thread Id 10, counter = 9
            Основной поток завершен.
            */
        }


        [TestMethod]
        // Другой способ запуска потока.
        public void TestThreadStart2Autostart()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            // Сначала сконструировать объект типа MyThread. 
            _01_BasicThreadAutoStart mt = new _01_BasicThreadAutoStart("Потомок #1");
            do
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            } while (mt.Count != 10);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
            .
                      Потомок #1 начат.
                      Thread HashCode 13
                      Thread Id 13
            .
                      В потоке Потомок #1, Count = 0
            .
                      В потоке Потомок #1, Count = 1
            .
                      В потоке Потомок #1, Count = 2
            .
                      В потоке Потомок #1, Count = 3
            .
                      В потоке Потомок #1, Count = 4
            .
                      В потоке Потомок #1, Count = 5
            .
                      В потоке Потомок #1, Count = 6
            .
                      В потоке Потомок #1, Count = 7
            .
                      В потоке Потомок #1, Count = 8
            .
                      В потоке Потомок #1, Count = 9
                      Потомок #1 завершен.
            The thread 'Потомок #1' (0xa3c) has exited with code 0 (0x0).
            Основной поток завершен.
            */
        }


        [TestMethod]
        // Создать несколько потоков исполнения.
        public void TestThreadStart3MultiThreads()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            // Сконструировать три потока. 
            _01_BasicThreadAutoStart mtl = new _01_BasicThreadAutoStart("Потомок #1");
            _01_BasicThreadAutoStart mt2 = new _01_BasicThreadAutoStart("Потомок #2");
            _01_BasicThreadAutoStart mt3 = new _01_BasicThreadAutoStart("Потомок #3");
            do
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            } while (mtl.Count < 10 ||
            mt2.Count < 10 ||
            mt3.Count < 10);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
            .
                      Потомок #1 начат.
                      Thread HashCode 13
                      Thread Id 13
            .
                      В потоке Потомок #1, Count = 0
            .
                      В потоке Потомок #1, Count = 1
            .
                      В потоке Потомок #1, Count = 2
            .
                      В потоке Потомок #1, Count = 3
            .
                      В потоке Потомок #1, Count = 4
            .
                      В потоке Потомок #1, Count = 5
            .
                      В потоке Потомок #1, Count = 6
            .
                      В потоке Потомок #1, Count = 7
            .
                      В потоке Потомок #1, Count = 8
            .
                      В потоке Потомок #1, Count = 9
                      Потомок #1 завершен.
            The thread 'Потомок #1' (0xa3c) has exited with code 0 (0x0).
            Основной поток завершен.
            */
        }

        [TestMethod]
        // Пример передачи аргумента методу потока.
        public void TestThreadStart4WithArgument()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            // Сначала сконструировать объект типа MyThread. 
            _01_BasicThreadWithArgument mt = new _01_BasicThreadWithArgument("Потомок #1");
            // Далее сконструировать поток из этого объекта. 
            ParameterizedThreadStart method = new ParameterizedThreadStart(mt.Run);
            Thread newThrd = new Thread(method);
            // И наконец, начать выполнение потока. 

            int count = 5;
            newThrd.Start(count);
            do
            {

                Debug.WriteLine(".");
                Thread.Sleep(100);
            } while (mt.Count < count);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
            .
                      Потомок #1 начат со счета 5
                      Thread HashCode 13
                      Thread Id 13
            .
                      В потоке Потомок #1, Count = 0
            .
                      В потоке Потомок #1, Count = 1
            .
                      В потоке Потомок #1, Count = 2
            .
                      В потоке Потомок #1, Count = 3
            .
                      В потоке Потомок #1, Count = 4
                      Потомок #1 завершен.
            The thread '<No Name>' (0x1e80) has exited with code 0 (0x0).
            Основной поток завершен.
            */
        }

        [TestMethod]
        // Пример передачи аргумента методу потока.
        public void TestThreadStart5WithArgument()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            // Обратите внимание на то, что число повторений 
            // передается этим двум объектам типа MyThread. 
            _01_BasicThreadAutoStartWithArgument mt = new _01_BasicThreadAutoStartWithArgument("Потомок #1", 5);
            _01_BasicThreadAutoStartWithArgument mt2 = new _01_BasicThreadAutoStartWithArgument("Потомок #2", 3);
            do
            {
                Thread.Sleep(100);
            } while (mt.Thread.IsAlive | mt2.Thread.IsAlive);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
                      Потомок #1 начат со счета 5
                      Потомок #2 начат со счета 3
                      Thread HashCode 14
                      Thread Id 14
                      Thread HashCode 13
                      Thread Id 13
                      В потоке Потомок #2, Count = 0
                      В потоке Потомок #1, Count = 0
                      В потоке Потомок #2, Count = 1
                      В потоке Потомок #1, Count = 1
                      В потоке Потомок #2, Count = 2
                      Потомок #2 завершен.
            The thread 'Потомок #2' (0x1370) has exited with code 0 (0x0).
                      В потоке Потомок #1, Count = 2
                      В потоке Потомок #1, Count = 3
                      В потоке Потомок #1, Count = 4
                      Потомок #1 завершен.
            The thread 'Потомок #1' (0xf8c) has exited with code 0 (0x0).
            Основной поток завершен.
            */
        }

        [TestMethod]
        // Пример передачи аргумента методу потока.
        public void TestThreadStart6Closure()
        {
            Debug.WriteLine("Основной поток начат.");
            Debug.WriteLine("Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine("Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            int counter = 0;
            string temp = new string(' ', 10);

            // ThreadStart
            Thread thread = new Thread(delegate()
            {
                Debug.WriteLine(temp + "1. counter = {0}", ++counter);
            });
            thread.Start();

            Thread.Sleep(100);
            Debug.WriteLine("2. counter = {0}", counter);

            // ParameterizedThreadStart
            thread = new Thread((object argument) =>
            {
                Debug.WriteLine(temp + "3. counter = {0}", (int)argument);
            });
            thread.Start(counter);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток начат.
            Thread HashCode 10
            Thread Id 10
                      1. counter = 1
            The thread '<No Name>' (0x2770) has exited with code 0 (0x0).
            2. counter = 1
            Основной поток завершен.
                      3. counter = 1
            The thread '<No Name>' (0x2b58) has exited with code 0 (0x0).
            The thread '<No Name>' (0x1830) has exited with code 0 (0x0).
            */
        }

        [TestMethod]
        public void TestThreadStart6StaticVariable()
        {
            // Запуск вторичного потока.
            Thread thread = new Thread(_01_BasicThreadUtils.Method);
            thread.Start();
            thread.Join();

            Debug.WriteLine("Основной поток завершил работу...");
            /*
            1 - СТАРТ --- 13
            2 - СТАРТ --- 14
            3 - СТАРТ --- 15
            4 - СТАРТ --- 16
            5 - СТАРТ --- 17
            Поток 18 завершился.
            Поток 17 завершился.
            Поток 16 завершился.
            Поток 15 завершился.
            Поток 14 завершился.
            Поток 13 завершился.
            Основной поток завершил работу...
            */
        }


    }
}
