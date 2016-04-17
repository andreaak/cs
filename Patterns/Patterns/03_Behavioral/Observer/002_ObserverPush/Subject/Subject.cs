using System.Collections;

namespace Patterns._03_Behavioral.Observer._002_ObserverPush.Subject
{
    abstract class Subject
    {
        ArrayList observers = new ArrayList();

        public void Attach(Observer.Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer.Observer observer)
        {
            observers.Remove(observer);
        }

        public abstract string State { get; set;}

        public void Notify()
        {
            foreach (Observer.Observer observer in observers)
                observer.Update(State);
        }
    }
}
