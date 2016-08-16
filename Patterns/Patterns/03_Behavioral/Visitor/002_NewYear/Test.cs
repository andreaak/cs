using NUnit.Framework;

namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Village village = new Village();
            village.Add(new BoysHouse());
            village.Add(new GirlsHouse());

            village.Accept(new Santa());

            // Delay.
            //Console.ReadKey();
        }
    }
}
