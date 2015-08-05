using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Patterns.Behavioral.State.Example1
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
