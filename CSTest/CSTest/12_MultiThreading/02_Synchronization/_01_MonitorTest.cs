using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._02_Synchronization._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    // Критическая секция (critical section).
    [TestClass]
    /*
    В случае использования нескольких потоков  приходится координировать  их действия такой процесс, называется синхронизацией. 
    Основная причина применения синхронизации  - необходимость разделять среди двух или более потоков общий ресурс 
    (разделяемый ресурс), который может быть одновременно доступен только одному потоку. 
    В основу синхронизации положено понятие блокировки, посредством которой  организуется управление доступом к кодовому блоку (критической секции).  
    Когда  доступный для каждого из потоков объект (объект синхронизации доступа) заблокирован одним потоком, 
    остальные потоки не могут получить доступ к заблокированному кодовому блоку (критической секции). 
    Когда же блокировка снимается одним потоком, объект(объект синхронизации доступа) становится доступным для использования в другом потоке.  
    Итоги использования блокировки: 
        - Если блокировка любого заданного объекта получена в одном потоке, то после блокировки объекта она не может быть получена в другом потоке. 
        - Остальным потокам, пытающимся получить блокировку того же самого объекта, придется ждать до тех пор, пока объект не окажется в разблокированном состоянии.  
        - Когда поток выходит из заблокированного фрагмента кода, соответствующий объект разблокируется.
    Объектом синхронизации доступа к разделяемому ресурсу считается такой объект, который представляет синхронизируемый ресурс. 
    В некоторых случаях им оказывается экземпляр самого ресурса или же произвольный экземпляр класса, используемого для синхронизации.
    */

    /*
    Monitor.Enter(this) - блокирует блок кода так, что его может использовать только текущий поток. 
    Все остальные потоки ждут пока текущий поток, закончит работу и вызовет Monitor.Exit(this).
    Методы Wait(), Pulse() и PulseAll() определены в классе Monitor и могут вызываться только из заблокированного фрагмента блока. 
    Они применяются следующим образом. Когда выполнение потока временно заблокировано, он вызывает метод Wait(). 
    В итоге поток переходит в состояние ожидания, а блокировка с  соответствующего объекта снимается, 
    что дает возможность использовать этот объект в другом потоке. 
    В дальнейшем ожидающий поток активизируется, когда другой поток войдет в аналогичное состояние блокировки, 
    и вызывает метод Pulse() или PulseAll(). 
    При вызове метода Pulse() возобновляется выполнение первого потока, ожидающего  своей очереди на получение блокировки. 
    А вызов метода PulseAll() сигнализирует о снятии блокировки всем ожидающим потокам. 
    */
    public class _01_MonitorTest
    {
        [TestMethod]
        public void TestMonitor1()
        {
            Thread[] threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_01_MonitorThread.Function);
                threads[i].Name = "Thread " + i;
                threads[i].Start();
            }

            Thread.Sleep(6000);
            /*
            Thread 0 start work
            Thread 0 end work
            The thread 'Thread 0' (0x16a8) has exited with code 0 (0x0).
            Thread 4 start work
            Thread 4 end work
            The thread 'Thread 4' (0x1204) has exited with code 0 (0x0).
            Thread 3 start work
            Thread 3 end work
            The thread 'Thread 3' (0x23e0) has exited with code 0 (0x0).
            Thread 2 start work
            Thread 2 end work
            The thread 'Thread 2' (0x1a80) has exited with code 0 (0x0).
            Thread 1 start work
            Thread 1 end work
            The thread 'Thread 1' (0x2bc8) has exited with code 0 (0x0).
            */
        }

        [TestMethod]
        public void TestMonitor2()
        {
            var reporter = new Thread(_01_MonitorThread.Report1)
            {
                IsBackground = true
            };
            reporter.Start();

            var threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_01_MonitorThread.Function1);
                threads[i].Name = "Thread " + i;
                threads[i].Start();
            }

            Thread.Sleep(3000);
            /*
            0 поток(ов) активно
            5 поток(ов) активно
            5 поток(ов) активно
            The thread 'Thread 4' (0x20ac) has exited with code 0 (0x0).
            4 поток(ов) активно
            The thread 'Thread 3' (0x19dc) has exited with code 0 (0x0).
            3 поток(ов) активно
            3 поток(ов) активно
            The thread 'Thread 2' (0x1fc8) has exited with code 0 (0x0).
            2 поток(ов) активно
            The thread 'Thread 1' (0x1fd8) has exited with code 0 (0x0).
            1 поток(ов) активно
            1 поток(ов) активно
            1 поток(ов) активно
            The thread 'Thread 0' (0x1d30) has exited with code 0 (0x0).
            0 поток(ов) активно
            0 поток(ов) активно
            */
        }

        [TestMethod]
        //Блокировка в разных методах
        public void TestMonitor3()
        {
            new Thread(_01_MonitorThread.Function2).Start();

            new Thread(_01_MonitorThread.Function3).Start();

            Thread.Sleep(15000);
            /*
            Начало блокировки Function2
            Конец блокировки Function2
            Начало блокировки Function3
            Конец блокировки Function3
            Начало блокировки Function2
            Конец блокировки Function2
            Начало блокировки Function3
            Конец блокировки Function3
            Начало блокировки Function2
            Конец блокировки Function2
            The thread '<No Name>' (0x244) has exited with code 0 (0x0).
            Начало блокировки Function3
            Конец блокировки Function3
            */
        }

        [TestMethod]
        // Lock - не принимает типов значений, а только ссылочные.
        public void TestMonitor4()
        {
            Thread[] threads = { new Thread(_01_MonitorThread.FunctionWithError), 
                                   new Thread(_01_MonitorThread.FunctionWithError), 
                                   new Thread(_01_MonitorThread.FunctionWithError) };

            foreach (Thread t in threads)
            {
                t.Start();
            }
            Thread.Sleep(5000);
        }


    }
}
