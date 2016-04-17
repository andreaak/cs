namespace Patterns._03_Behavioral.Mediator._001_Base
{
    abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}
