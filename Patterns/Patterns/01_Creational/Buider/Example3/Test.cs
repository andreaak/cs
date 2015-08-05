using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.Builder.Example3
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Builder builder = new ConcreteBuilder();
            Foreman foreman = new Foreman(builder);
            foreman.Construct();

            House house = builder.GetResult();

            // Delay.
            //Console.ReadKey();
        }
    }
}
