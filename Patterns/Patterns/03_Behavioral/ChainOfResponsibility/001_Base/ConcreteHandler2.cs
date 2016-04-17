using System.Diagnostics;

namespace Patterns._03_Behavioral.ChainOfResponsibility._001_Base
{
    class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request == 2)
                Debug.WriteLine("Two");
            else if (Successor != null)
                Successor.HandleRequest(request);
        }
    }
}
