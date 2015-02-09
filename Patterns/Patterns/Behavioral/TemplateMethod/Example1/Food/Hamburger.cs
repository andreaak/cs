using System;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod.Example1.Food
{
    class Hamburger : FastFood
    {
        public override void PrepareMainIngredient()
        {
            Debug.WriteLine("Meat");
        }

        public override void AddTopings()
        {
            Debug.WriteLine("Ketchup");
        }
    }
}
