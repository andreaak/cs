﻿namespace Patterns._02_Structural.Decorator._001_Beverage.Beverage
{
    public abstract class BeverageBase
    {
        protected string Desctiption = "";

        public string GetDescription()
        {
            return Desctiption;
        }

        public abstract double GetCost();
    }
}
