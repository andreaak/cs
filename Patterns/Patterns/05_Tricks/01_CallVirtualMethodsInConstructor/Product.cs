using System.Diagnostics;

namespace Patterns._05_Tricks._01_CallVirtualMethodsInConstructor
{
    public abstract class Product
    {
        protected Product()
        {
            Debug.WriteLine("Product: ctor");
        }

        protected internal abstract void PostConstruction();
    }

    public class ConcreteProduct : Product
    {
        // ���������� ����������� �� �������� �������� ��������
        // ��������� ������� ��������.
        public ConcreteProduct()
        {
            Debug.WriteLine("ConcreteProduct: construction");
        }

        public ConcreteProduct(int w)
        {
            Debug.WriteLine("ConcreteProduct: construction with param: " + w);
        }

        protected internal override void PostConstruction()
        {
            Debug.WriteLine("ConcreteProduct: post construction");
        }
    }
    public class ConcreteProduct2 : IPostConstruction
    {
        // ���������� ����������� �� �������� �������� ��������
        // ��������� ������� ��������.
        public ConcreteProduct2()
        {
            Debug.WriteLine("ConcreteProduct2: construction");
        }

        public ConcreteProduct2(int w)
        {
            Debug.WriteLine("ConcreteProduct2: construction with param: " + w);
        }

        public void PostConstruction()
        {
            Debug.WriteLine("ConcreteProduct2: post construction");
        }
    }
}