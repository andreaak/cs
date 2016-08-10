using System.Diagnostics;
using CSTest._06_Interface._0_Setup;

namespace CSTest._06_Interface
{

    // В C# допустимо множественное наследование только от интерфейсов.
    // Множественное наследование реализации (т.е. от двух и более классов или структур) недопустимо.
    // Допустимо множественное наследование от одного класса и многих интерфейсов.
    class _02_DerivedClass : BaseClass, Interface1, Interface2, IDisposablee
    {
        public void Method1()
        {
            Debug.WriteLine("Реализация метода Method1() из _02_DerivedClass");
        }

        public void Method2()
        {
            Debug.WriteLine("Реализация метода Method2() из _02_DerivedClass");
        }

        // Реализуем метод с именем Method из базового интерфейса Interface1 
        // При реализации метода используем технику явного указания имени интерфейса в имени
        // метода, которому принадлежит данный метод.

        // По умолчанию одноименные методы являются private, 
        // но явно указывать модификаторы доступа недопустимо.
        void Interface1.Method()
        {
            Debug.WriteLine("Реализация метода Method() из Interface1");
        }

        void Interface2.Method()
        {
            Debug.WriteLine("Реализация метода Method() из Interface2");
        }

        #region IDisposablee Members

        // Этот метод не может переопределить Dispose из Base.
        // Ключевое слово 'new' указывает на то, что этот метод
        // повторно реализует метод DisposeNonVirtual интерфейса IDisposablee
        public new void DisposeNonVirtual()
        {
            Debug.WriteLine("Реализация метода DisposeNonVirtual() в _02_DerivedClass");
        }

        public override void DisposeVirtual()
        {
            Debug.WriteLine("Реализация метода DisposeVirtual() в _02_DerivedClass");
        }

        #endregion
    }
}
