using System.Diagnostics;

namespace Patterns._03_Behavioral.Mediator._001_Base
{
    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator)
            : base(mediator)
        {
        }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Debug.WriteLine(this + " got " + message);
        }
    }
}
