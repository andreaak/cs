using NUnit.Framework;

namespace Patterns._03_Behavioral.State._001_Car
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var car = new Car();
            car.FillTank();
            car.TurnKey();
            car.TurnKey();
            car.TurnKey();
            car.Drive();
            car.Stop();
        }
    }
}
