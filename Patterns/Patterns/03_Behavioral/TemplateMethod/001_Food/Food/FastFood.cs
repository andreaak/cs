using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._001_Food.Food
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
