using System.Diagnostics;

namespace Patterns.Structural.Adapter._002_Class
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Debug.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
