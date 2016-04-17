using Patterns._03_Behavioral.Mediator._003_Shop.Colleagues;

namespace Patterns._03_Behavioral.Mediator._003_Shop
{
    abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}
