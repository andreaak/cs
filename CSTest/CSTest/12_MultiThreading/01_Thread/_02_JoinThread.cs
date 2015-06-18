using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _02_JoinThread
    {
        [TestMethod]
        // Использовать свойство IsAlive для отслеживания момента окончания потоков
        public void Test1()
        {
            Debug.WriteLine("Основной поток начат.");
            // Сконструировать три потока. 
            MyThreadAutoStart mtl = new MyThreadAutoStart("Поток #1");
            MyThreadAutoStart mt2 = new MyThreadAutoStart("Поток #2");
            MyThreadAutoStart mt3 = new MyThreadAutoStart("Поток #3");
            do
            {
                Debug.Write(".");
                Thread.Sleep(100);
            } while (mtl.Thrd.IsAlive &&
            mt2.Thrd.IsAlive &&
            mt3.Thrd.IsAlive);
            Debug.WriteLine("Основной поток завершен.");

        }

        [TestMethod]
        // Использовать метод Join(). 
        public void Test2()
        {
            // Использовать метод Join() для ожидания до тех пор, 
            // пока потоки не завершатся, 
            Debug.WriteLine("Основной поток начат.");
            // Сконструировать три потока. 
            MyThreadAutoStart mtl = new MyThreadAutoStart("Потомок #1");
            MyThreadAutoStart mt2 = new MyThreadAutoStart("Потомок #2");
            MyThreadAutoStart mt3 = new MyThreadAutoStart("Потомок #3");
            mtl.Thrd.Join();
            Debug.WriteLine("Потомок #1 присоединен.");
            mt2.Thrd.Join();
            Debug.WriteLine("Потомок #2 присоединен.");
            mt3.Thrd.Join();
            Debug.WriteLine("Потомок #3 присоединен.");
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
