using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._007_FactoryMethodInConstructor
{
    // Продукты ---------------------------------------------------------

    abstract class Product2
    {
    }

    class DefaultProduct2 : Product2
    {
    }

    class SpecialProduct2 : Product2
    {
    }

    // Создатели --------------------------------------------------------

    abstract class Creator2
    {
        protected Product product = null;

        public Creator2()
        {
            product = FactoryMethod();
            Debug.WriteLine(product.GetType().Name);
        }

        // Виртуальный фабричный метод базового класса вызовется 
        // при его замещении в производном классе.
        public virtual Product FactoryMethod()
        {
            return new DefaultProduct();
        }
    }

    class ConcreteCreator2 : Creator2
    {
        public ConcreteCreator2()
        {
            product = FactoryMethod();
            Debug.WriteLine(product.GetType().Name);
        }

        // Замещение/перекрытие виртуального метода.
        public new Product FactoryMethod()
        {
            return new SpecialProduct();
        }
    }
}
