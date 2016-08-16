using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Command._003_CalculatorUndoRedo
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Calculator calculator = new Calculator();
            int result = 0;

            result = calculator.Add(5);
            Debug.WriteLine(result);

            result = calculator.Sub(3);
            Debug.WriteLine(result);

            Debug.WriteLine(new string('-', 3));

            result = calculator.Undo(2);
            Debug.WriteLine(result);

            result = calculator.Redo(1);
            Debug.WriteLine(result);

            // Delay.
            //Console.ReadKey();
        }
    }
}
