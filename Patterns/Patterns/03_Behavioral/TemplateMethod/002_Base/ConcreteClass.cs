using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._002_Base
{
    class ConcreteClass : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Debug.WriteLine("PrimitiveOperation1");
        }

        public override void PrimitiveOperation2()
        {
            Debug.WriteLine("PrimitiveOperation2");
        }
    }
}
