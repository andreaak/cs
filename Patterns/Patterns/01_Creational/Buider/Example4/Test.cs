using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.Builder.Example4
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Builder builder = new ConcreteBuilder();
            Director director = new Director(builder);
            director.Construct();

            Product product = builder.GetResult();
            product.Show();
        }
    }
}
