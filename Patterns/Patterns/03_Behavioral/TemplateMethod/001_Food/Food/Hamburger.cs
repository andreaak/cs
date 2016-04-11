using System;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod._001_Food.Food
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
