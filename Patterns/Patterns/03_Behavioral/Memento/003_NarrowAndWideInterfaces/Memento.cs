namespace Patterns._03_Behavioral.Memento._003_NarrowAndWideInterfaces
{
    class Memento : IWideInterface
    {
        public string State { get;  set; }

        public Memento(string state)
        {
            this.State = state;
        }
    }
}
