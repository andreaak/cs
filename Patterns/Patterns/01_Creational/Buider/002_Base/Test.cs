using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._01_Creational.Buider._002_Base.Builder;

namespace Patterns._01_Creational.Buider._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Builder.Builder builder = new ConcreteBuilder();
            Director director = new Director(builder);
            director.Construct();

            Product product = builder.GetResult();
            product.Show();
        }
    }
}
