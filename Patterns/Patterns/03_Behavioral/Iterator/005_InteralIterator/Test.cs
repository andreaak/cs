using System;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._005_InteralIterator
{
    delegate double Function(double arg);

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Enumerable enumerable = new Enumerable();

            IEnumerable power2List = enumerable.Transform(new Function(Power2));

            foreach (var item in power2List)
                Debug.WriteLine(item);

            IEnumerable power3List = enumerable.Transform(n => Math.Pow(n, 3));

            foreach (var item in power3List)
                Debug.WriteLine(item);

            // Задержка.
            //Console.ReadKey();
        }
        
        static double Power2(double n)
        {
            return Math.Pow(n, 2);
        }
    }
}
