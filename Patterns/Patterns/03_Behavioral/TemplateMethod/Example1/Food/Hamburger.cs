using System;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod.Example1.Food
{
    class Hamburger : FastFood
    {
        public override void PrepareMainIngredient()
        {
            Console.WriteLine("Meat");
        }

        public override void AddTopings()
        {
            Console.WriteLine("Ketchup");
        }
    }
}
