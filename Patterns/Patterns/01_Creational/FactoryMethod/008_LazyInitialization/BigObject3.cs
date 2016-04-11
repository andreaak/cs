using System;
using System.Diagnostics;
using System.Threading;

namespace Creational.FactoryMethod._008_LazyInitialization
{
    public sealed class BigObject3
    {
        public BigObject3()
        {
            // Большая работа.
            Thread.Sleep(3000);
            Debug.WriteLine("Экземпляр BigObject создан");
        }

        public void Operation()
        {
            Debug.WriteLine("Operation");
        }
    }
}
