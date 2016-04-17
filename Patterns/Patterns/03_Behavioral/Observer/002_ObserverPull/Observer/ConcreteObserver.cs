using Patterns._03_Behavioral.Observer._002_ObserverPull.Subject;

namespace Patterns._03_Behavioral.Observer._002_ObserverPull.Observer
{
    class ConcreteObserver : Observer
    {
        string observerState;
        ConcreteSubject subject;

        public ConcreteObserver(ConcreteSubject subject)
        {
            this.subject = subject;
        }

        public override void Update()
        {
            observerState = subject.State;
        }
    }
}
