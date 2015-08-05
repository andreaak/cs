using System;
using System.Diagnostics;
using System.Threading;

namespace Creational.FactoryMethod.Example8
{
    public sealed class BigObject3
    {
        public BigObject3()
        {
            // Большая работа.
            Thread.Sleep(3000);
            Console.WriteLine("Экземпляр BigObject создан");
        }

        public void Operation()
        {
            Console.WriteLine("Operation");
        }
    }
}
