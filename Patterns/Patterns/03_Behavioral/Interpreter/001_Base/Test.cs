using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Interpreter._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Context context = new Context
            {
                Vocabulary = 'a',
                Source = "aaa"
            };

            var expression = new NonterminalExpression();
            expression.Interpret(context);

            Debug.WriteLine(context.Result);
        }
    }
}
