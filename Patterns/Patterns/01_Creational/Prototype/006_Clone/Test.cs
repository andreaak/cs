using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._01_Creational.Prototype._006_Clone
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Prototype1 original = new Prototype1();
            Debug.WriteLine("Оригинал : " + original.a);

            // Клонирование объекта. 
            Prototype1 clone = original.Clone() as Prototype1;
            Debug.WriteLine("Клон : " + clone.a + "\n");

            // Проверка на глубокое клонирование.
            clone.a = 0;

            Debug.WriteLine("Оригинал : " + original.a);
            Debug.WriteLine("Клон : " + clone.a);

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test2()
        {
            Prototype2 original = new Prototype2();
            Debug.WriteLine("Оригинал : " + original.A.a);

            // Клонирование объекта. 
            Prototype2 clone = original.Clone() as Prototype2;
            Debug.WriteLine("Клон : " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            clone.A.a = 0;

            Debug.WriteLine("Оригинал : " + original.A.a);
            Debug.WriteLine("Клон : " + clone.A.a);

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test3()
        {
            Prototype3 original = new Prototype3();
            Debug.WriteLine("Оригинал : " + original.A.a);

            // Клонирование объекта. 
            Prototype3 clone = original.Clone() as Prototype3;
            Debug.WriteLine("Клон : " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            clone.A.a = 0;

            Debug.WriteLine("Оригинал : " + original.A.a);
            Debug.WriteLine("Клон : " + clone.A.a);

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test4()
        {
            B4 original = new B4();
            Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);

            // Клонирование объекта. 
            B4 clone = original.Clone() as B4;
            Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            // Попытка изменения поля b в клоне B через объект A.
            clone.A.B.b = 0;

            Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);
            Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test5()
        {
            B5 original = new B5();
            Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);

            // Клонирование объекта. 
            B5 clone = original.Clone() as B5;
            Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Проверка на глубокое клонирование.
            // Попытка изменения поля b в клоне B через объект A.
            clone.A.B.b = 0;

            Debug.WriteLine("Оригинал : " + original.b + " " + original.A.a);
            Debug.WriteLine("Клон : " + clone.b + " " + clone.A.a + "\n");

            // Delay.
            //Debug.ReadKey();
        }
    }
}
