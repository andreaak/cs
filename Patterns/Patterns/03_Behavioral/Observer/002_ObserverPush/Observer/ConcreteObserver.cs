using Patterns._03_Behavioral.Observer._002_ObserverPush.Subject;
namespace Patterns._03_Behavioral.Observer._002_ObserverPush.Observer
{
    class ConcreteObserver : Observer
    {
        string observerState;
        ConcreteSubject subject;

        public ConcreteObserver(ConcreteSubject subject)
        {
            this.subject = subject;
        }

        public override void Update(string state)
        {
            observerState = state;
        }
    }
}
