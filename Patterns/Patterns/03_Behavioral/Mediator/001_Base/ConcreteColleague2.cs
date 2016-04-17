using System.Diagnostics;

namespace Patterns._03_Behavioral.Mediator._001_Base
{
    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator)
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
