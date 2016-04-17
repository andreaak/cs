namespace Patterns._03_Behavioral.ChainOfResponsibility._001_Base
{
    abstract class Handler
    {
        public Handler Successor { get; set; }
        public abstract void HandleRequest(int request);
    }
}
