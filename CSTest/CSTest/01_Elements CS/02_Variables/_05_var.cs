using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._02_Variables
{
    [TestClass]
    public class _05_var
    {
        [TestMethod]
        public void TestVar1()
        {
            // Неявно типизированная локальная переменная.
            var myVar = 7;

            Debug.WriteLine(myVar);

            // Неявно типизированные локальные переменные не допускают множественного объявления.
            // var a = 1, b = 2, c = 3;

            // Неявно типизированные локальные переменные должны быть инициализированы.
            // var a; 

            // Константа не может быть неявно типизированная.
            // const var myVar = 3.14; 
        }
    }
}
