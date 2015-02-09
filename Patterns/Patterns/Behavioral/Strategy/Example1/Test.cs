using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Behavioral.Strategy.Example1.Ducks;
using Patterns.Behavioral.Strategy.Example1.Fly;
using Patterns.Behavioral.Strategy.Example1.Quack;
using System.Collections.Generic;

namespace Patterns.Behavioral.Strategy.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            List<DuckBase> ducks = new List<DuckBase>();
            ducks.Add(new ExoticDuck());
            ducks.Add(new SimpleDuck());
            ducks.Add(new WoodenDuck());
            ducks.Add(new RubberDuck());

            foreach (var duck in ducks)
            {
                duck.Display();
                duck.Swim();
                duck.Quack();
                duck.Fly();

            }

            DuckBase upgradableDuck = new UpgradableDuck();
            upgradableDuck.Display();
            upgradableDuck.Swim();
            upgradableDuck.Quack();
            upgradableDuck.Fly();

            upgradableDuck.SetFlyBehaviour(new FlyWithWings());
            upgradableDuck.SetQuackBehaviour(new ExoticQuack());
            upgradableDuck.Quack();
            upgradableDuck.Fly();
        }
    }
}
