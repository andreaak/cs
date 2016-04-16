using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._001_Food.Food
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
