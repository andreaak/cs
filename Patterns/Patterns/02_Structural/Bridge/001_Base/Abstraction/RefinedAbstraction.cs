namespace Patterns._02_Structural.Bridge._001_Base.Abstraction
{
    class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(Implementor.Implementor implementor)
            : base(implementor)
        {
        }

        public override void Operation()
        {
            // ...
            base.Operation();
            // ...
        }
    }
}
