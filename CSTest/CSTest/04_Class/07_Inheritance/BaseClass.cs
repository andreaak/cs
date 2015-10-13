using System.Diagnostics;

// Наследование.
namespace CSTest._04_Class._07_Inheritance
{
    class BaseClass
    {
        // Поля
        public int field1;
        public int field2;
        public int field3;  


        // Открытое поле.
        public string publicField = "BaseClass.publicField";

        // Закрытое поле.
        private string privateField = "BaseClass.privateField";

        // Защищенное поле.
        protected string protectedField = "BaseClass.protectedField";

        public int baseNumber;

        // Конструктор по умолчанию.
        public BaseClass()
        {
        }

        // Пользовательский конструктор.
        public BaseClass(int baseNumber)
        {
            this.baseNumber = baseNumber;
        }


        public void Show()
        {
            Debug.WriteLine(privateField);
        }

        public void Method()
        {
            Debug.WriteLine("Method from BaseClass");
        }

        public virtual void MethodVirtual()
        {
            Debug.WriteLine("MethodVirtual from BaseClass");
        }
        
        public virtual void MethodVirtual2()
        {
            Debug.WriteLine("MethodVirtual2 from BaseClass");
        }
    }
}
