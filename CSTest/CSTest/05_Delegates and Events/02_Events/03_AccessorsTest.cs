using System.Diagnostics;
using CSTest._05_Delegates_and_Events._02_Events._0_Setup.Accessors;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    [TestFixture]
    public class _04_AccessorsTest
    {
        [Test]
        public void TestAccessorsEvents()
        {
            var evt = new AccessorEvent();
            var wOb = new W();
            var xOb = new X();
            var yOb = new Y();
            var zOb = new Z();
            // Добавить обработчики в цепочку событий. 
            Debug.WriteLine("Добавление событий. ");
            evt.SomeEvent += wOb.Whandler;
            evt.SomeEvent += xOb.Xhandler;
            evt.SomeEvent += yOb.Yhandler;
            // Сохранить нельзя - список заполнен. 
            evt.SomeEvent += zOb.Zhandler;
            Debug.WriteLine("");
            // Запустить события, 
            evt.OnSomeEvent();
            Debug.WriteLine("");
            // Удалить обработчик. 
            Debug.WriteLine("Удаление обработчика xOb.Xhandler.");
            evt.SomeEvent -= xOb.Xhandler;
            evt.OnSomeEvent();
            Debug.WriteLine("");
            // Попробовать удалить обработчик еще раз. 
            Debug.WriteLine("Попытка удалить обработчик " +
            "xOb.Xhandler еще раз.");
            evt.SomeEvent -= xOb.Xhandler;
            evt.OnSomeEvent();
            Debug.WriteLine("");
            //А теперь добавить обработчик Zhandler. 
            Debug.WriteLine("Добавление обработчика zOb.Zhandler");
            evt.SomeEvent += zOb.Zhandler;
            evt.OnSomeEvent();
            /*
            Добавление событий. 
            Список событий заполнен.

            Событие получено объектом W
            Событие получено объектом X
            Событие получено объектом Y

            Удаление обработчика xOb.Xhandler.
            Событие получено объектом W
            Событие получено объектом Y

            Попытка удалить обработчик xOb.Xhandler еще раз.
            Обработчик событий не найден.
            Событие получено объектом W
            Событие получено объектом Y

            Добавление обработчика zOb.Zhandler
            Событие получено объектом W
            Событие получено объектом Z
            Событие получено объектом Y
             */
        }
    }
}
