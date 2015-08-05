using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.FactoryMethod.Example7
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Creator creator = new ConcreteCreator();

            // В этом примере на экран будет выведено: 
            // SpecialProduct  -  работа конструктора базового класса
            // SpecialProduct  -  работа конструктора производного класса

            // Delay.
            //Console.ReadKey();

            Creator2 creator2 = new ConcreteCreator2();

            // В этом примере на экран будет выведено: 
            // DefaultProduct  -  работа конструктора базового класса
            // SpecialProduct  -  работа конструктора производного класса

            // Delay.
            //Console.ReadKey();
        }

    }
}
