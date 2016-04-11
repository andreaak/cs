using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Structural.Decorator._001_Beverage.Beverage;
using Patterns.Structural.Decorator._001_Beverage.Decorators;
using Patterns.Structural.Decorator.Decorators;
using System.Diagnostics;

namespace Patterns.Structural.Decorator._001_Beverage
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            BeverageBase espresso = new Espresso();
            BeverageBase blackTea = new BlackTea();
            BeverageBase greenTea = new GreenTea();

            PrintBeverage(espresso);
            PrintBeverage(blackTea);
            PrintBeverage(greenTea);

            Debug.WriteLine("-----------");

            BeverageBase capuccino = new SugarCondiment(new MilkCondiment(new Espresso()));
            PrintBeverage(capuccino);

            BeverageBase greenTeaWithSugar = new SugarCondiment(new GreenTea());
            PrintBeverage(greenTeaWithSugar);

        }

        static void PrintBeverage(BeverageBase beverage)
        {
            Debug.WriteLine(string.Format("Beverage: {0}; Price: {1}",
                beverage.GetDescription(), beverage.GetCost()));
        }
    }
}
