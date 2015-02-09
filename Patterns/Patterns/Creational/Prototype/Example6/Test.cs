using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Creational.Prototype.Example6
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Prototype1 original = new Prototype1();
            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.a);

            // Клонирование объекта. 
            Prototype1 clone = original.Clone() as Prototype1;
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.a + "\n");

            // Проверка на глубокое клонирование.
            clone.a = 0;

            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.a);
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.a);

            // Delay.
            Console.ReadKey();
        }

        [TestMethod]
        public void Test2()
        {
            Prototype2 original = new Prototype2();
            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.A.a);

            // Клонирование объекта. 
            Prototype2 clone = original.Clone() as Prototype2;
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            clone.A.a = 0;

            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.A.a);
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.A.a);

            // Delay.
            Console.ReadKey();
        }

        [TestMethod]
        public void Test3()
        {
            Prototype3 original = new Prototype3();
            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.A.a);

            // Клонирование объекта. 
            Prototype3 clone = original.Clone() as Prototype3;
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            clone.A.a = 0;

            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.A.a);
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.A.a);

            // Delay.
            Console.ReadKey();
        }

        [TestMethod]
        public void Test4()
        {
            B4 original = new B4();
            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);

            // Клонирование объекта. 
            B4 clone = original.Clone() as B4;
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            // Попытка изменения поля b в клоне B через объект A.
            clone.A.B.b = 0;

            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Delay.
            Console.ReadKey();
        }

        [TestMethod]
        public void Test5()
        {
            B5 original = new B5();
            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);

            // Клонирование объекта. 
            B5 clone = original.Clone() as B5;
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            // Попытка изменения поля b в клоне B через объект A.
            clone.A.B.b = 0;

            System.Diagnostics.Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);
            System.Diagnostics.Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Delay.
            Console.ReadKey();
        }
    }
}
