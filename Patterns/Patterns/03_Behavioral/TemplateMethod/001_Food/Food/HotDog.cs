using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._001_Food.Food
{
    class HotDog : FastFood
    {
        public override bool CustomerWantsTopings()
        {
            Debug.Write("Do you want mustard?: ");
            var userInput = "y";// Debug.ReadLine();

            return userInput.ToLower() == "yes" || userInput.ToLower() == "y";
        }

        public override void PrepareMainIngredient()
        {
            Debug.WriteLine("Sausage");
        }

        public override void AddTopings()
        {
            Debug.WriteLine("Mustard");
        }
    }
}
