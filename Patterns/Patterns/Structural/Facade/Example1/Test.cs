using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Structural.Facade.Example1.WashingMachine;

namespace Patterns.Structural.Facade.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            var water = new WaterManagingSubsystem();
            var thermo = new Thermo();
            var engine = new Engine();
            var dryer = new Dryer();

            var washingMachine = new WashingMachine.WashingMachine(dryer, engine, thermo, water);

            // Cotton
            Debug.WriteLine("Cotton");
            washingMachine.WashCotton();

            // Wool
            Debug.WriteLine("Wool");
            washingMachine.WashWool();

            int temp = washingMachine.GetTemperature();
            // 1. принадлежат самому объекту
            // 2. методы объектов параметров
            // 3. созданы внутри метода
            // 4. поля текущего объекта
        }
    }
}
