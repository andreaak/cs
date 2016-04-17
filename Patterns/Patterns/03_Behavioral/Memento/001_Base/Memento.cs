namespace Patterns._03_Behavioral.Memento._001_Base
{
    class Memento
    {
        public string State { get; private set; }

        public Memento(string state)
        {
            this.State = state;
        }
    }
}
