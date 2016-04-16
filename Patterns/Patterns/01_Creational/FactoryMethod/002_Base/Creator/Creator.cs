namespace Patterns._01_Creational.FactoryMethod._002_Base.Creator
{   
    abstract class Creator
    {
        Product.Product product;

        public abstract Product.Product FactoryMethod();

        public void AnOperation()
        {
            product = FactoryMethod();
        }
    }
}
