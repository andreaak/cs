using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Behavioral.State._001_Car
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
