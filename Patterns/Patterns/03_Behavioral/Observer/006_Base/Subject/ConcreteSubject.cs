namespace Patterns._03_Behavioral.Observer._006_Base.Subject
{
    class ConcreteSubject : Subject
    {
        string state = string.Empty;

        public override void SetState(string state)
        {
            this.state = state;
            this.Notify();
        }

        public override string GetState()
        {
            return this.state;
        }
    }
}
