using System;

namespace Patterns.Creational.AbstractFactory._006_BaseModified
{
    class ConcreteFactory2 : IAbstractFactory
    {
        dynamic product;

        public dynamic Make(Product product)
        {
            // ��������� ������� ������������������ ����� �������� ��������� - 2.
            string name = this.GetType().Namespace + "." + product.ToString() + "2";

            // ������������ �������� �������� ��������� - 2. 
            this.product = Activator.CreateInstance(Type.GetType(name));

            return this.product;
        }
    }
}
