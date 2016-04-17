using System.Diagnostics;
using Patterns._03_Behavioral.Observer._006_Base.Subject;

namespace Patterns._03_Behavioral.Observer._006_Base.Observer
{
    class ConcreteObserver : Observer
    {
        string state;
        ConcreteSubject subject;

        public ConcreteObserver(ConcreteSubject subject)
        {
            this.subject = subject;
        }

        public override void Update()
        {
            state = subject.GetState();
            Debug.WriteLine("Observer {0} has state - {1}", 
                GetHashCode(), state);
        }

        public void ChangeState(string state)
        {
            this.state = state;
            this.subject.SetState(state);
        }
    }
}
