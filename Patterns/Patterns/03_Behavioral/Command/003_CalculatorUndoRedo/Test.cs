using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Command._003_CalculatorUndoRedo
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
