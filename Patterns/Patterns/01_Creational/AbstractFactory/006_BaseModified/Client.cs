using System;

namespace Patterns._01_Creational.AbstractFactory._006_BaseModified
{
    class Client
    {
        dynamic factory;
        dynamic productA;
        dynamic productB;

        public Client(Factory factory)
        {
           // ��������� ������� ������������������ ����� ������� � ��������� �������������.
            string name = this.GetType().Namespace + "." + factory.ToString();

            // ������������ �������� ��������������� �������. 
            this.factory = Activator.CreateInstance(Type.GetType(name));

            // ���������� ���������.
            this.productA = this.factory.Make(Product.ProductA);
            this.productB = this.factory.Make(Product.ProductB);
        }

        public void Run()
        {
            productB.Interact(productA);
        }
    }
}
