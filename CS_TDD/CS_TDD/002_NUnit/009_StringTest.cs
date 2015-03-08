using CS_TDD._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class StringTest
    {
        // Этот тест проходит только в том случае, если исключительная ситуация ArgumentOutOfRangeException была сгенерированна при
        // передаче конструктору неправильных параметров
        [Test]
        public void TestConverterGenareteCtorArgumentException()
        {
            try
            {
                UahConverter converter = new UahConverter(0.25, -10, 8);

                converter.OutputCurrency = Currency.EUR;
                converter.Value = 1000;
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "Ctor");
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        // Этот тест проходит только в том случае, если исключительная ситуация ArgumentOutOfRangeException была сгенерированна при
        // передаче свойству Value неправильного значения
        [Test]
        public void TestConverterGenareteInputValueArgumentException()
        {
            try
            {
                UahConverter converter = new UahConverter(0.25, 10, 8);

                converter.OutputCurrency = Currency.EUR;
                converter.Value = -1000;
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.ParamName, "Value");
                return;
            }

            Assert.Fail("No exception was thrown.");
        }
    }
}
