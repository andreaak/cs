using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Flyweight._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Flyweight [] flyweight = new Flyweight[100];
            FlyweightFactory factory = new FlyweightFactory();

            for (int i = 0; i < flyweight.Length; i++)
            {
                flyweight[i] = factory.GetConcreteFlyweight("1");
                flyweight[i].Operation(ConsoleColor.Yellow);
            }

            for (int i = 0; i < flyweight.Length; i++)
            {
                flyweight[i] = factory.GetUnsharedConcreteFlyweight();
                flyweight[i].Operation(ConsoleColor.Green);
            }

            //Console.ReadKey();
        }
    }
}