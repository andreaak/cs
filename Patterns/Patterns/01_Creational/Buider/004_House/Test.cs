using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.Builder._004_House
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
            //Debug.ReadKey();
        }
    }
}
