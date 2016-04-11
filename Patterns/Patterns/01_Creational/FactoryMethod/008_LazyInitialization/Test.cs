using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Creational.FactoryMethod._008_LazyInitialization
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Debug.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            Debug.WriteLine(BigObject1.Instance);

            Debug.WriteLine("Второе обращение к экземпляру BigObject...");
            Debug.WriteLine(BigObject1.Instance);

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test2()
        {
            Debug.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            Debug.WriteLine(BigObject2.GetInstance());

            Debug.WriteLine("Второе обращение к экземпляру BigObject...");
            Debug.WriteLine(BigObject2.GetInstance());

            // Delay.
            //Debug.ReadKey();
        }

        [TestMethod]
        public void Test3()
        {
            System.Lazy<BigObject3> lazy = new Lazy<BigObject3>();

            Debug.WriteLine("Первое обращение к экземпляру BigObject...");
            // Создание объекта происходит только при этом обращении к объекту.
            lazy.Value.Operation();

            Debug.WriteLine("Второе обращение к экземпляру BigObject...");
            lazy.Value.Operation();

            // Delay.
            //Debug.ReadKey();
        }

    }
}
