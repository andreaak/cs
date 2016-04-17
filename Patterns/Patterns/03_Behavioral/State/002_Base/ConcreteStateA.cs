namespace Patterns._03_Behavioral.State._002_Base
{
    class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateB();
        }
    }
}
