using System.Diagnostics;

namespace Patterns._02_Structural.Proxy._004_Surrogates
{
    class Operator : IHuman
    {
        public void Request()
        {
            Debug.WriteLine("Operator");
        }
    }
}
