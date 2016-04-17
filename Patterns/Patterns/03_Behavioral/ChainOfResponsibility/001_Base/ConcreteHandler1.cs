using System.Diagnostics;

namespace Patterns._03_Behavioral.ChainOfResponsibility._001_Base
{
    class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request == 1)
                Debug.WriteLine("One");
            else if (Successor != null)
                Successor.HandleRequest(request);
        }
    }
}
