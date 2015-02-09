using System;

// Адаптер (Уровень класса).

namespace Patterns.Structural.Adapter.Example2
{
    class Program
    {
        static void Main()
        {
            ITarget target = new Adapter();
            target.Request();
        }
    }
}
