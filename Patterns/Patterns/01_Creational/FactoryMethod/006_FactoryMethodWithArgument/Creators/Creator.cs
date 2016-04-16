
using Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Products;

namespace Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Creators
{
    class Creator
    {
        public virtual Product Create(ProductType productType)
        {
            if (productType == ProductType.MINE)
                return new MyProduct();
            else if (productType == ProductType.YOURS)
                return new YourProduct();
            else
                return null;
        }
    }
}
