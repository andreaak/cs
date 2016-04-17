using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._01_Elements_CS._02_Variables
{
    class _02_ReadOnly
    {
        // Поле только для чтения (устанавливается только конструктором)!
        public readonly string field = "hello";

        // Конструктор.
        public _02_ReadOnly()
        {
            Debug.WriteLine(field);
            field = "Поле только для чтения ";

            field += "!";
        }

        public _02_ReadOnly(int i)
            :this()
        {
            field = "Поле только для чтения ";

            field += "!";
        }
    }

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestReadOnly1()
        {
            _02_ReadOnly program = new _02_ReadOnly();

            Debug.WriteLine(program.field);

            // Ошибка Компиляции.
            //program.field = "Попытка записи в поле только для чтения.";
        }
    }
}
