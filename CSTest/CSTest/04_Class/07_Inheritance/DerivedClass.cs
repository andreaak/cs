
// Наследование.

using System.Diagnostics;
namespace CSTest._04_Class._07_Inheritance
{
    class DerivedClass : BaseClass
    {
        public int derivedField;
        public int field4;
        public int field5;

        // Конструктор по умолчанию.
        public DerivedClass()
        {
            // Изменяем все доступные поля унаследованные от базового класса.

            publicField = "DerivedClass.publicField";
            protectedField = "DerivedClass.protectedField";            
        }

        // Пользовательский конструктор.
        // При создании объекта производного класса, конструктор производного класса 
        // автоматически вызывает конструктор по умолчанию из базового класса.
        // Конструктор базового класса, присвоит всем данным какие-то свои безопасные значения.
        // После этого начнет работу конструктор производного класса, который повторно
        // будет определять значения для унаследованых членов. (ДВОЙНАЯ РАБОТА)!
        public DerivedClass(int number1, int number2)
        {
            // Инициализируем поле базового класса.
            baseNumber = number1;

            // Инициализируем поле производного (данного) класса.
            derivedField = number2;
        }

        // Пользовательский конструктор.
        // Вызывается пользовательский конструктор базового класса, при этом не нужно, 
        // присваивать значения, унаследованным членам в конструкторе производного класса.
        public DerivedClass(int number1, int number2, string stubForTest)
            : base(number1)
        {
            derivedField = number2;
        }

        // Замещение метода базового класса.
        public new void Method()
        {
            Debug.WriteLine("Method from DerivedClass");
        }

        // Переопределение метода базового класса.
        public override void MethodVirtual()
        {
            Debug.WriteLine("MethodVirtual from DerivedClass");
        }
        
        // Переопределение метода базового класса.
        public override void MethodVirtual2()
        {
            // Вызов метода базового класса.
            base.MethodVirtual2();
            Debug.WriteLine("MethodVirtual2 from DerivedClass");
        }
    }
}
