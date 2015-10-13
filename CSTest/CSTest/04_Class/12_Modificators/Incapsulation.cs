using System.Diagnostics;

namespace CSTest._04_Class._12_Modificators
{
    class Incapsulation
    {
        Incapsulation my = null;

        private void Method()
        {
            Debug.WriteLine("Hello");
        }

        public void CallMethod()
        {
            my = new Incapsulation();

            // private метод виден на экземпляре!
            my.Method();
        }
    }
}
