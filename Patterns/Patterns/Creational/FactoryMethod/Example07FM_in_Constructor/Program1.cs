using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example7
{
    // Продукты ---------------------------------------------------------

    abstract class Product
    {
    }

    class DefaultProduct : Product
    {       
    }

    class SpecialProduct : Product
    {
    }

    // Создатели --------------------------------------------------------

    abstract class Creator
    {
        protected Product product = null;

        public Creator()
        {
            product = FactoryMethod();
            Debug.WriteLine(product.GetType().Name);
        }

        // Виртуальный фабричный метод базового класса не вызовется 
        // при его переопределении в производном классе.
        public virtual Product FactoryMethod()
        {
            return new DefaultProduct();
        }
    }

    class ConcreteCreator : Creator
    {
        public ConcreteCreator()
        {
            product = FactoryMethod();
            Debug.WriteLine(product.GetType().Name);
        }

        // Переопределение виртуального метода.
        public override Product FactoryMethod()
        {
            return new SpecialProduct();
        }
    }
}
