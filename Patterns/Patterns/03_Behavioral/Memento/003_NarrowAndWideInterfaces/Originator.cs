namespace Patterns._03_Behavioral.Memento._003_NarrowAndWideInterfaces
{
    class Originator
    {
        public string State { get; set; }

        public void SetMemento(IWideInterface memento)
        {
            State = memento.State;
        }

        public INarrowInterface CreateMemento()
        {
            return new Memento(State);
        }
    }
}
