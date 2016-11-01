using System.Diagnostics;

namespace CSTest._04_Class._00_Base._0_Setup
{
    class TestClass
    {
        public static string staticField;

        public static void StaticMethod()
        {
            Debug.WriteLine(staticField ?? "Null");
        }

        public string instanceField;

        public void InstanceMethod()
        {
            Debug.WriteLine(instanceField ?? "Null");
        }

        // Объект не может скрывать (инкапсулировать) ничего, от другого объекта того же класса.
        public void CallMethod(TestClass my)
        {
            // private метод виден на экземпляре!
            my.PrivateMethod();
        }

        private void PrivateMethod()
        {
            Debug.WriteLine(instanceField ?? "Null");
        }
    }
}