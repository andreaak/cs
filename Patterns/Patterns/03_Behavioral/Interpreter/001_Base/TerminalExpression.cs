namespace Patterns._03_Behavioral.Interpreter._001_Base
{
    class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            context.Result = context.Source[context.Position] == context.Vocabulary;
        }
    }
}
