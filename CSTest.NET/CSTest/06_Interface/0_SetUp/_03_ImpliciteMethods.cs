using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    interface _031_Interface
    {
        void Method1();

        void Method();
    }

    interface _032_Interface
    {
        void Method2();

        void Method();
    }

    class _03_ImpliciteMethods : _031_Interface, _032_Interface
    {
        public void Method1()
        {
            Debug.WriteLine("_03_ImpliciteMethods.Method1");
        }

        public void Method2()
        {
            Debug.WriteLine("_03_ImpliciteMethods.Method2");
        }

        /*
        Реализуем метод с именем Method из базового интерфейса Interface1 
        При реализации метода используем технику явного указания имени интерфейса в имени
        метода, которому принадлежит данный метод.

        По умолчанию одноименные методы являются private, 
        но явно указывать модификаторы доступа недопустимо.
        */
        void _031_Interface.Method()
        {
            Debug.WriteLine("_031_Interface.Method");
        }

        void _032_Interface.Method()
        {
            Debug.WriteLine("_032_Interface.Method");
        }
    }
}
