using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _01_BasicTest
    {
        [TestMethod]
        // Создать поток исполнения.
        public void Test1()
        {
            Debug.WriteLine("Основной поток начат.");
            // Сначала сконструировать объект типа MyThread. 
            MyThread mt = new MyThread("Потомок #1");
            // Далее сконструировать поток из этого объекта. 
            Thread newThrd = new Thread(mt.Run);
            // И наконец, начать выполнение потока. 
            newThrd.Start();
            do
            {

                Debug.Write(".");
                Thread.Sleep(100);
            } while (mt.Count != 10);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        // Другой способ запуска потока.
        public void Test2()
        {
            Debug.WriteLine("Основной поток начат.");
            // Сначала сконструировать объект типа MyThread. 
            MyThreadAutoStart mt = new MyThreadAutoStart("Потомок #1");
            do
            {
                Debug.Write(".");
                Thread.Sleep(100);
            } while (mt.Count != 10);
            Debug.WriteLine("Основной поток завершен.");
        }


        [TestMethod]
        // Создать несколько потоков исполнения.
        public void Test3()
        {
            Debug.WriteLine("Основной поток начат.");
            // Сконструировать три потока. 
            MyThreadAutoStart mtl = new MyThreadAutoStart("Потомок #1");
            MyThreadAutoStart mt2 = new MyThreadAutoStart("Потомок #2");
            MyThreadAutoStart mt3 = new MyThreadAutoStart("Потомок #3");
            do
            {
                Debug.Write(".");
                Thread.Sleep(100);
            } while (mtl.Count < 10 ||
            mt2.Count < 10 ||
            mt3.Count < 10);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        // Пример передачи аргумента методу потока.
        public void Test4()
        {
            // Обратите внимание на то, что число повторений 
            // передается этим двум объектам типа MyThread. 
            MyThreadWithArgument mt = new MyThreadWithArgument("Потомок #1", 5);
            MyThreadWithArgument mt2 = new MyThreadWithArgument("Потомок #2", 3);
            do
            {
                Thread.Sleep(100);
            } while (mt.Thrd.IsAlive | mt2.Thrd.IsAlive);
            Debug.WriteLine("Основной поток завершен.");
        }

    }
}
