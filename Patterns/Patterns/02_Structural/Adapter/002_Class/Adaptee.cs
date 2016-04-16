using System.Diagnostics;

namespace Patterns._02_Structural.Adapter._002_Class
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Debug.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
