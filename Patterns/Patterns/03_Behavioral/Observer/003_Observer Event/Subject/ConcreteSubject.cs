namespace Patterns._03_Behavioral.Observer._003_Observer_Event.Subject
{
    // Конкретный издатель.
    class ConcreteSubject : Subject
    {
        public override string State { get; set; }

        public override void Notify()
        {
            observers.Invoke(State);
        }
    }
}
