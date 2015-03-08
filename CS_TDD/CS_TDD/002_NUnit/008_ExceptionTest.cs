using CS_TDD._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class ExceptionTest
    {
        // Метод теста, помеченный атрибутом ExpectedException будет пройден только тогда, когда в его теле будет 
        // обнаружена исключительная ситуация того типа, который указан в параметре этого атрибута
        [Test]
        [ExpectedException(typeof(DivideByZeroException), ExpectedMessage = "Попытка деления на нуль.")]
        public static void TestCalculatorDivideByZeroException()
        {
            //throw new Exception();
            new Calculator().Div(1, 0);
        }

        // Класс UahConverter может сгенерировать исключительную ситуацию типа ArgumentOutOfRangeException в двух случаях,
        // поэтому такой тест не всегда будет отрабатывать правильно 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConverterGenereteArgumentException()
        {
            UahConverter converter = new UahConverter(0.25, 10, 8);

            converter.OutputCurrency = Currency.EUR;
            converter.Value = -1000;
        }
    }
}
