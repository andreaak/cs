using System;

namespace Patterns._01_Creational.AbstractFactory._006_BaseModified.AbstractFactory
{
    class ConcreteFactory1 : IAbstractFactory
    {
        dynamic product;

        public dynamic Make(Product product)
        {
            // Получение полного квалифицированного имени продукта семейства - 1.
            string name = this.GetType().Namespace + "." + product.ToString() + "1";

            // Динамическое создание продукта семейства - 1. 
            this.product = Activator.CreateInstance(Type.GetType(name));

            return this.product;
        }
    }
}
