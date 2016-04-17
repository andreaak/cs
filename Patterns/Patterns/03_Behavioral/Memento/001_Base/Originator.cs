namespace Patterns._03_Behavioral.Memento._001_Base
{
    class Originator
    {
        public string State { get; set; }

        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }

        public Memento CreateMemento()
        {
            return new Memento(State);
        }
    }
}
