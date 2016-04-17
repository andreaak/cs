namespace Patterns._03_Behavioral.Command._003_CalculatorUndoRedo.Commands
{
    class Div : Command
    {
        public Div(ArithmeticUnit unit, int operand)
        {
            this.unit = unit;
            this.operand = operand;
        }

        public override void Execute()
        {
            unit.Run('/', operand);
        }

        public override void UnExecute()
        {
            unit.Run('*', operand);
        }
    }
}
