using NUnit.Framework;
using Patterns._01_Creational.Buider._002_Base.Builder;

namespace Patterns._01_Creational.Buider._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
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
