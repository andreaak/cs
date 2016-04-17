using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Interpreter._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
