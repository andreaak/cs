
namespace Creational.FactoryMethod._006_FactoryMethodWithArgument
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
