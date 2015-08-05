using System;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod.Example1.Food
{
    class HotDog : FastFood
    {
        public override bool CustomerWantsTopings()
        {
            Debug.Write("Do you want mustard?: ");
            var userInput = "y";// Console.ReadLine();

            return userInput.ToLower() == "yes" || userInput.ToLower() == "y";
        }

        public override void PrepareMainIngredient()
        {
            Console.WriteLine("Sausage");
        }

        public override void AddTopings()
        {
            Console.WriteLine("Mustard");
        }
    }
}
