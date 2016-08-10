using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestFixture]
    public class _11_ManualResetEventSlimTest
    {
        // ManualResetEventSlim - изначально используется SpinWait блокировка на 1000 итераций, 
        // после чего происходит синхронизация с помощью объекта ядра.
        static ManualResetEventSlim slim = new ManualResetEventSlim(false, 1000);
        
        [Test]
        public void TestManualResetEventSlim1()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            // Delay.
            Thread.Sleep(15000);
            slim.Set();

            // Delay.
            Thread.Sleep(15000);
        }

        [Test]
        //ManualresetEventSlimPerformance
        public void TestManualResetEventSlim2()
        {
            var mres = new ManualResetEventSlim(false);
            var mres2 = new ManualResetEventSlim(false);

            var mre = new ManualResetEvent(false);

            long total = 0;
            int COUNT = 5;

            for (int i = 0; i < COUNT; i++)
            {
                mres2.Reset();
                //счётчик затраченного времени
                Stopwatch sw = Stopwatch.StartNew();

                //запускаем установку в потоке пула
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    //  MethodSlim(mres, true);
                    Method(mre, true);
                    mres2.Set();
                });
                //запускаем сброс в основном потоке
                //  MethodSlim(mres, false);
                Method(mre, false);

                //Ждём, пока выполнится поток пула
                mres2.Wait();
                sw.Stop();

                Debug.WriteLine("Pass {0}: {1} ms", i, sw.ElapsedMilliseconds);
                total += sw.ElapsedMilliseconds;
            }

            Debug.WriteLine("");
            Debug.WriteLine("===============================");
            Debug.WriteLine("Done in average=" + total / (double)COUNT);
            // Delay.
            Thread.Sleep(15000);
        }


        static void Function()
        {
            slim.Wait();
            Debug.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.Name);
        }

        ///////////////////////////
        // работаем с ManualResetEventSlim
        private static void MethodSlim(ManualResetEventSlim mre, bool value)
        {
            //в цикле повторяем действие достаточно большое число раз
            for (int i = 0; i < 9000000; i++)
            {
                if (value)
                {
                    mre.Set();
                }
                else
                {
                    mre.Reset();
                }
            }
        }

        // работаем с классическим ManualResetEvent
        private static void Method(ManualResetEvent mre, bool value)
        {
            //в цикле повторяем действие достаточно большое число раз
            for (int i = 0; i < 9000000; i++)
            {
                if (value)
                {
                    mre.Set();
                }
                else
                {
                    mre.Reset();
                }
            }
        }
    }
}
