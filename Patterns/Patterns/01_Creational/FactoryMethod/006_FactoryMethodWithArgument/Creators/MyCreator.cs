
using Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Products;

namespace Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Creators
{
    class MyCreator : Creator
    {
        public override Product Create(ProductType productType)
        {
            if (productType == ProductType.THEIRS)
                return new TheirProduct();

            return base.Create(productType);
        }
    }
}
