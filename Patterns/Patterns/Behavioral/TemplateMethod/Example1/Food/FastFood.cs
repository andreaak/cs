using System;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod.Example1.Food
{
    abstract class FastFood
    {
        public void Prepare()
        {
            RoastBread();
            PrepareMainIngredient();
            PutVegetables();

            if (CustomerWantsTopings())
                AddTopings();
        }

        public abstract void PrepareMainIngredient();

        public abstract void AddTopings();

        public virtual bool CustomerWantsTopings()
        {
            return true;
        }

        public void RoastBread()
        {
            Debug.WriteLine("Bread");
        }

        public void PutVegetables()
        {
            Debug.WriteLine("Vegetables");
        }
    }
}
