using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example8
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Console.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            Console.WriteLine(BigObject1.Instance);

            Console.WriteLine("Второе обращение к экземпляру BigObject...");
            Console.WriteLine(BigObject1.Instance);

            // Delay.
            //Console.ReadKey();
        }

        [TestMethod]
        public void Test2()
        {
            Console.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            Console.WriteLine(BigObject2.GetInstance());

            Console.WriteLine("Второе обращение к экземпляру BigObject...");
            Console.WriteLine(BigObject2.GetInstance());

            // Delay.
            //Console.ReadKey();
        }

        [TestMethod]
        public void Test3()
        {
            System.Lazy<BigObject3> lazy = new Lazy<BigObject3>();

            Console.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            lazy.Value.Operation();

            Console.WriteLine("Второе обращение к экземпляру BigObject...");
            lazy.Value.Operation();

            // Delay.
            //Console.ReadKey();
        }

    }
}
