﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Strategy._001_Ducks.Ducks;
using Patterns._03_Behavioral.Strategy._001_Ducks.Fly;
using Patterns._03_Behavioral.Strategy._001_Ducks.Quack;

namespace Patterns._03_Behavioral.Strategy._001_Ducks
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