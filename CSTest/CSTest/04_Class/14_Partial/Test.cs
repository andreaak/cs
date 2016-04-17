using CSTest._04_Class._14_Partial.PartialClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._04_Class._14_Partial
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestPartialClass1()
        {
            PartialClass instance = new PartialClass();

            instance.MethodFromPart1(); // Метод из первой части класса PartialClass
            instance.MethodFromPart2(); // Метод со второй части класса PartialClass.
        }
        [TestMethod]
        public void TestPartialMethods1()
        {
            PartialMethods.PartialClass instance = 
                new PartialMethods.PartialClass();

            instance.CallPartialMethod();
        }
    }
}
