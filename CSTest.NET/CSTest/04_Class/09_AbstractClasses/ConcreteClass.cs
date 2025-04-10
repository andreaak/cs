using System.Diagnostics;

namespace CSTest._04_Class._09_AbstractClasses
{
    // Конкретный класс.
    class ConcreteClass : AbstractClass
    {
        string s = "FIRST";

        // Конструктор (отрабатывает вторым).
        public ConcreteClass()
        {
            Debug.WriteLine("3 ConcreteClass()");
            s = "SECOND";
        }

        public override void Method()
        {
            Debug.WriteLine("Implementation");
        }

        // Переопределяем виртуальный метод VirtualMethod() базового абстрактного класса.
        // Если мы не переопределим виртуальный метод, то будет использован метод из базового класса.
        public override void VirtualMethod()
        {
            Debug.WriteLine("DerivedClass.VirtualMethod();");
        }

        // Реализуем абстрактный метод AbstractMethod() базового абстрактного класса.
        public override void AbstractMethod()
        {
            Debug.WriteLine("Реализация метода AbstractMethod() в ConcreteClass  {0}", s);
        }

        public override event System.EventHandler Changed;
    }
}
