using System.Diagnostics;

namespace Patterns.Structural.Adapter._003_Object
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Debug.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
