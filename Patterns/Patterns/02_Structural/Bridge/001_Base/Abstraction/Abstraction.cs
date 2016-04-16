namespace Patterns._02_Structural.Bridge._001_Base.Abstraction
{
    abstract class Abstraction
    {
        protected Implementor.Implementor implementor = null;

        public Abstraction(Implementor.Implementor implementor)
        {
            this.implementor = implementor;
        }

        public virtual void Operation()
        {
            implementor.OperationImp();
        }
    }
}
