using NUnit.Framework;
using Patterns._03_Behavioral.Mediator._003_Shop.Colleagues;

namespace Patterns._03_Behavioral.Mediator._003_Shop
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var mediator = new ConcreteMediator();
            var farmer = new Farmer(mediator);
            var cannery = new Cannery(mediator);
            var shop = new Shop(mediator);

            mediator.Farmer = farmer;
            mediator.Cannery = cannery;
            mediator.Shop = shop;

            farmer.GrowTomato();
            
            // Delay.
            //Console.ReadKey();
        }
    }
}
