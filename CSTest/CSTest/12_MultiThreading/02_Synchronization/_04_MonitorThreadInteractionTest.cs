using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _04_MonitorThreadInteractionTest
    {
        [TestMethod]
        // Использовать методы Wait() и Pulse() для имитации тиканья часов
        public void Test1()
        {
            TickTock tt = new TickTock();
            MyThread mtl = new MyThread("Tick", tt);
            MyThread mt2 = new MyThread("Tock", tt);
            mtl.Thrd.Join();
            mt2.Thrd.Join();
            Debug.WriteLine("Часы остановлены");
        }

        [TestMethod]
        // Нерабочий вариант класса TickTock.
        public void Test2()
        {
            TickTock2 tt = new TickTock2();
            MyThread mtl = new MyThread("Tick", tt);
            MyThread mt2 = new MyThread("Tock", tt);
            mtl.Thrd.Join();
            mt2.Thrd.Join();
            Debug.WriteLine("Часы остановлены");
        }

        public interface ITickTock
        {
            void Tick(bool running);

            void Tock(bool running);
        }

        class TickTock : ITickTock
        {
            object lockOn = new object();
            public void Tick(bool running)
            {
                lock (lockOn)
                {
                    if (!running)
                    { // остановить часы
                        Monitor.Pulse(lockOn); // уведомить любые ожидающие потоки
                        return;
                    }
                    Debug.Write("тик ");
                    Monitor.Pulse(lockOn); // разрешить выполнение метода Tock()
                    Monitor.Wait(lockOn); // ожидать завершения метода Tock()
                }
            }
            public void Tock(bool running)
            {
                lock (lockOn)
                {
                    if (!running)
                    { // остановить часы
                        Monitor.Pulse(lockOn); // уведомить любые ожидающие потоки
                        return;
                    }
                    Debug.WriteLine("так");
                    Monitor.Pulse(lockOn); // разрешить выполнение метода Tick()
                    Monitor.Wait(lockOn); // ожидать завершения метода Tick()
                }
            }
        }

        // Нерабочий вариант класса TickTock.
        class TickTock2 : ITickTock
        {
            object lockOn = new object();
            public void Tick(bool running)
            {
                lock (lockOn)
                {
                    if (!running)
                    { // остановить часы
                        return;
                    }
                    Debug.Write("тик ");
                }
            }
            public void Tock(bool running)
            {
                lock (lockOn)
                {
                    if (!running)
                    { // остановить часы
                        return;
                    }
                    Debug.WriteLine("так");
                }
            }
        }

        class MyThread
        {
            public Thread Thrd;
            ITickTock ttOb;
            // Сконструировать новый поток,
            public MyThread(string name, ITickTock tt)
            {
                Thrd = new Thread(this.Run);
                ttOb = tt;
                Thrd.Name = name;
                Thrd.Start();
            }
            // Начать выполнение нового потока,
            void Run()
            {
                if (Thrd.Name == "Tick")
                {
                    for (int i = 0; i < 5; i++)
                        ttOb.Tick(true);
                    ttOb.Tick(false);
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                        ttOb.Tock(true);
                    ttOb.Tock(false);
                }

                

            }
        }
    }
}
