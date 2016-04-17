using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._01_Thread._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    /*
    Для определения момента окончания потока можно воспользоваться значением свойства IsAlive. 
    Оно вернет false, когда поток завершится. 
    Кроме того, можно воспользоваться специальным методов Join(), 
    который позволяет приостановить выполнение основного потока до момента завершения тех вторичных потоков, 
    для которых метод Join() был использован.
    */
    public class _02_JoinThreadTest
    {
        [TestMethod]
        // Использовать свойство IsAlive для отслеживания момента окончания потоков
        public void TestThreadJoin1()
        {
            Debug.WriteLine("Основной поток начат.");

            // Сконструировать три потока. 
            _01_BasicThreadAutoStart mtl = new _01_BasicThreadAutoStart("Поток #1");
            _01_BasicThreadAutoStart mt2 = new _01_BasicThreadAutoStart("Поток #2");
            _01_BasicThreadAutoStart mt3 = new _01_BasicThreadAutoStart("Поток #3");
            do
            {
                Debug.WriteLine("Ожидание завершения потоков");
                Thread.Sleep(100);
            } while (mtl.Thread.IsAlive &&
            mt2.Thread.IsAlive &&
            mt3.Thread.IsAlive);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток начат.
                      Поток #1 начат.
            Ожидание завершения потоков
                      Thread HashCode 13
                      Поток #2 начат.
                      Thread Id 13
                      Thread HashCode 14
                      Thread Id 14
                      Поток #3 начат.
                      Thread HashCode 15
                      Thread Id 15
            Ожидание завершения потоков
                      В потоке Поток #1, Count = 0
                      В потоке Поток #2, Count = 0
                      В потоке Поток #3, Count = 0
            Ожидание завершения потоков
                      В потоке Поток #1, Count = 1
                      В потоке Поток #2, Count = 1
                      В потоке Поток #3, Count = 1
            Ожидание завершения потоков
                      В потоке Поток #1, Count = 2
                      В потоке Поток #2, Count = 2
                      В потоке Поток #3, Count = 2
            Ожидание завершения потоков
                      В потоке Поток #1, Count = 3
                      В потоке Поток #2, Count = 3
                      В потоке Поток #3, Count = 3
            Ожидание завершения потоков
                      В потоке Поток #1, Count = 4
                      В потоке Поток #2, Count = 4
                      В потоке Поток #3, Count = 4
            Ожидание завершения потоков
                      В потоке Поток #2, Count = 5
                      В потоке Поток #1, Count = 5
                      В потоке Поток #3, Count = 5
            Ожидание завершения потоков
                      В потоке Поток #2, Count = 6
                      В потоке Поток #1, Count = 6
                      В потоке Поток #3, Count = 6
            Ожидание завершения потоков
                      В потоке Поток #2, Count = 7
                      В потоке Поток #1, Count = 7
                      В потоке Поток #3, Count = 7
            Ожидание завершения потоков
                      В потоке Поток #2, Count = 8
                      В потоке Поток #1, Count = 8
                      В потоке Поток #3, Count = 8
            Ожидание завершения потоков
                      В потоке Поток #2, Count = 9
                      Поток #2 завершен.
            The thread 'Поток #2' (0x26d4) has exited with code 0 (0x0).
                      В потоке Поток #1, Count = 9
                      Поток #1 завершен.
            The thread 'Поток #1' (0x1338) has exited with code 0 (0x0).
            Основной поток завершен.
                      В потоке Поток #3, Count = 9
                      Поток #3 завершен.
            The thread 'Поток #3' (0x2bd4) has exited with code 0 (0x0).
            */

        }

        [TestMethod]
        // Использовать метод Join(). 
        public void TestThreadJoin2()
        {
            // Использовать метод Join() для ожидания до тех пор, 
            // пока потоки не завершатся, 
            Debug.WriteLine("Основной поток начат.");

            // Сконструировать три потока. 
            _01_BasicThreadAutoStart mtl = new _01_BasicThreadAutoStart("Потомок #1");
            _01_BasicThreadAutoStart mt2 = new _01_BasicThreadAutoStart("Потомок #2");
            _01_BasicThreadAutoStart mt3 = new _01_BasicThreadAutoStart("Потомок #3");

            Debug.WriteLine("Ожидание завершения работы потока Потомок #1.");
            mtl.Thread.Join();
            Debug.WriteLine("Потомок #1 присоединен.");
            Debug.WriteLine("Ожидание завершения работы потока Потомок #2.");
            mt2.Thread.Join();
            Debug.WriteLine("Потомок #2 присоединен.");
            Debug.WriteLine("Ожидание завершения работы потока Потомок #3.");
            mt3.Thread.Join();
            Debug.WriteLine("Потомок #3 присоединен.");
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток начат.
                      Потомок #1 начат.
                      Потомок #2 начат.
            Ожидание завершения работы потока Потомок #1.
                      Потомок #3 начат.
                      Thread HashCode 14
                      Thread HashCode 13
                      Thread Id 14
                      Thread Id 13
                      Thread HashCode 15
                      Thread Id 15
                      В потоке Потомок #2, Count = 0
                      В потоке Потомок #1, Count = 0
                      В потоке Потомок #3, Count = 0
                      В потоке Потомок #2, Count = 1
                      В потоке Потомок #3, Count = 1
                      В потоке Потомок #1, Count = 1
                      В потоке Потомок #2, Count = 2
                      В потоке Потомок #3, Count = 2
                      В потоке Потомок #1, Count = 2
                      В потоке Потомок #2, Count = 3
                      В потоке Потомок #3, Count = 3
                      В потоке Потомок #1, Count = 3
                      В потоке Потомок #2, Count = 4
                      В потоке Потомок #3, Count = 4
                      В потоке Потомок #1, Count = 4
                      В потоке Потомок #2, Count = 5
                      В потоке Потомок #1, Count = 5
                      В потоке Потомок #3, Count = 5
                      В потоке Потомок #2, Count = 6
                      В потоке Потомок #1, Count = 6
                      В потоке Потомок #3, Count = 6
                      В потоке Потомок #2, Count = 7
                      В потоке Потомок #1, Count = 7
                      В потоке Потомок #3, Count = 7
                      В потоке Потомок #2, Count = 8
                      В потоке Потомок #1, Count = 8
                      В потоке Потомок #3, Count = 8
                      В потоке Потомок #2, Count = 9
                      Потомок #2 завершен.
            The thread 'Потомок #2' (0x1df8) has exited with code 0 (0x0).
                      В потоке Потомок #1, Count = 9
                      В потоке Потомок #3, Count = 9
                      Потомок #3 завершен.
            The thread 'Потомок #3' (0x2590) has exited with code 0 (0x0).
                      Потомок #1 завершен.
            Потомок #1 присоединен.
            The thread 'Потомок #1' (0x1358) has exited with code 0 (0x0).
            Ожидание завершения работы потока Потомок #2.
            Потомок #2 присоединен.
            Ожидание завершения работы потока Потомок #3.
            Потомок #3 присоединен.
            Основной поток завершен.
            */
        }
    }
}
