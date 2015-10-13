using CSTest._04_Class._14_PartialClasses.PartialClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._14_PartialClasses
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
            CSTest._04_Class._14_PartialClasses.PartialMethods.PartialClass instance = 
                new CSTest._04_Class._14_PartialClasses.PartialMethods.PartialClass();

            instance.CallPartialMethod();
        }
    }
}
