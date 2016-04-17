using System.Collections.Generic;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Manager
{
    abstract class ChangeManager
    {
        protected Dictionary<Subject.Subject, List<Observer.Observer>> mapping = null;

        public ChangeManager()
        {
            mapping = new Dictionary<Subject.Subject, List<Observer.Observer>>();
        }

        public abstract void Register(Subject.Subject subject, Observer.Observer observer);
        public abstract void Unregister(Subject.Subject subject, Observer.Observer observer);
        public abstract void Notify();
    }
}
