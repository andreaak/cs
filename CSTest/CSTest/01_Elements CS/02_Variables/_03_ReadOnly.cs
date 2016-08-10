using System.Diagnostics;
using NUnit.Framework;

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

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestReadOnly1()
        {
            _02_ReadOnly program = new _02_ReadOnly();

            Debug.WriteLine(program.field);

            // Ошибка Компиляции.
            //program.field = "Попытка записи в поле только для чтения.";
        }
    }
}
