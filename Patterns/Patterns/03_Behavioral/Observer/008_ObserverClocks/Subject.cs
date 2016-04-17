using System.Collections.Generic;

namespace Patterns._03_Behavioral.Observer._008_ObserverClocks
{
    abstract class Subject
    {
        protected List<Observer> observers = new List<Observer>();

        public virtual void Attach(Observer observer)
        {
            observers.Add(observer);
            observer.Update(this);
        }

        public virtual void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public virtual void Notify()
        {
            foreach (var o in observers)
                o.Update(this);
        }
    }
}
