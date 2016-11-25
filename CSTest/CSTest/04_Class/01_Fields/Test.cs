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

            Debug.WriteLine(program.readonlyField);

            // Ошибка Компиляции.
            //program.field = "Попытка записи в поле только для чтения.";
        }

        [Test]
        public void TestReadOnly2()
        {
            _03_ReadOnly cut = new _03_ReadOnly();
            Debug.WriteLine(cut.fieldInt);
            FunctMethod(ref cut.fieldInt);
            FunctMethod2(out cut.fieldInt);
            Debug.WriteLine(cut.fieldInt);
            Debug.WriteLine(cut.readOnlyInt);
            //FunctMethod(ref cut.readOnlyInt);//A readonly field cannot be passed ref or out (except in a constructor)
            //FunctMethod2(out cut.readOnlyInt);
            Debug.WriteLine(cut.readOnlyInt);
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

        private void FunctMethod(ref int temp)
        {
            temp = 99;
        }

        private void FunctMethod2(out int temp)
        {
            temp = 99;
        }
    }
}
