namespace Patterns._03_Behavioral.State._002_Base
{
    class Context
    {
        public State State { get; set; }

        public Context(State state)
        {
            this.State = state;
        }

        public void Request()
        {
            this.State.Handle(this);
        }
    }
}
