using System.Diagnostics;

namespace Patterns._02_Structural.Facade._004_OpenAndCloseClasses
{
    // ------------------------- Фасад (Открытый класс). -------------------------

    public class Facade
    {
        public void Method()
        {
            new SubsystemClassA().MethodA();
            new SubsystemClassB().MethodB();
        }
    }

    // ----------------     Классы подсистем (Закрытые классы). ------------------

    // По умолчанию все классы - internal.

    internal class SubsystemClassA 
    {
        public void MethodA() { Debug.WriteLine("SubsystemClassA"); }
    }

    internal class SubsystemClassB
    {
        public void MethodB() { Debug.WriteLine("SubsystemClassB"); }
    } 
}
