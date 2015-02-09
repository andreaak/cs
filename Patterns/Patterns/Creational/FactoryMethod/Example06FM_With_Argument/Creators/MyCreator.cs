
namespace Creational.FactoryMethod.Example6
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
