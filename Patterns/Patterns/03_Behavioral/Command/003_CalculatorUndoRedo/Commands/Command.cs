namespace Patterns._03_Behavioral.Command._003_CalculatorUndoRedo.Commands
{
    // "Command"
    abstract class Command
    {
        protected ArithmeticUnit unit;
        protected int operand;

        public abstract void Execute();
        public abstract void UnExecute();
    }
}
