using System.Diagnostics;
using NUnit.Framework;
using CSTest._04_Class._00_Base._0_Setup;

namespace CSTest._04_Class._01_Fields
{

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestConst1()
        {
            Debug.WriteLine(_02_Constants.field);

            // Ошибка Компиляции.
            //_02_Constants.field = "Попытка записи в поле только для чтения.";
        }

        [Test]
        public void TestReadOnly1()
        {
            _03_ReadOnly program = new _03_ReadOnly();

            Debug.WriteLine(program.field);

            // Ошибка Компиляции.
            //program.field = "Попытка записи в поле только для чтения.";
        }

        [Test]
        public void TestStaticFieldsAndMethods()
        {
            TestClass.staticField = "Hello world! Static";

            Debug.WriteLine(TestClass.staticField);

            // Вызов метода с именем StaticMethod.
            TestClass.StaticMethod();
            /*
            Hello world! Static
            Hello world! Static
            */
        }
    }
}
