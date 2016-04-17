namespace Patterns._03_Behavioral.Observer._003_Observer_Event.Subject
{
    // Издатель.
    abstract class Subject
    {
        protected Observer.Observer observers = null;

        public event Observer.Observer Event
        {
            add { observers += value; }
            remove { observers -= value; }
        }

        public abstract string State { get; set; }
        public abstract void Notify();
    }
}
