using Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Manager;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Subject
{
    class Subject
    {
        ChangeManager manager;
        public string Name { get; private set; }

        public Subject(string name, ChangeManager manager)
        {
            Name = name;
            this.manager = manager;
        }

        public void Attach(Observer.Observer observer)
        {
            manager.Register(this, observer);
        }

        public void Detach(Observer.Observer observer)
        {
            manager.Unregister(this, observer);
        }

        public void Notify()
        {
            manager.Notify();
        }
    }
}
