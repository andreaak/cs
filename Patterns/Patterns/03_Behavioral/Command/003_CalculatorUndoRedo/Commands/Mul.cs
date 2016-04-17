namespace Patterns._03_Behavioral.Command._003_CalculatorUndoRedo.Commands
{
    class Mul : Command
    {
        public Mul(ArithmeticUnit unit, int operand)
        {
            this.unit = unit;
            this.operand = operand;
        }

        public override void Execute()
        {
            unit.Run('*', operand);
        }

        public override void UnExecute()
        {
            unit.Run('/', operand);
        }
    }
}
