using System.Diagnostics;

namespace Patterns._02_Structural.Adapter._003_Object
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Debug.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
