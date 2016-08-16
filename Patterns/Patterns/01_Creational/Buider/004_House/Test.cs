using NUnit.Framework;
using Patterns._01_Creational.Buider._004_House.Builder;

namespace Patterns._01_Creational.Buider._004_House
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Builder.Builder builder = new ConcreteBuilder();
            Foreman foreman = new Foreman(builder);
            foreman.Construct();

            House.House house = builder.GetResult();

            // Delay.
            //Debug.ReadKey();
        }
    }
}
