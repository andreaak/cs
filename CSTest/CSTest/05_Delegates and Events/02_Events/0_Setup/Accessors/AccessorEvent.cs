using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup.Accessors
{
    delegate void TestAccessorEventHandler();

    class AccessorEvent
    {
        TestAccessorEventHandler[] evnt = new TestAccessorEventHandler[3];
        public event TestAccessorEventHandler SomeEvent
        {
            // Добавить событие в список, 
            add
            {
                int i;
                for (i = 0; i < 3; i++)
                    if (evnt[i] == null)
                    {
                        evnt[i] = value;
                        break;
                    }
                if (i == 3)
                    Debug.WriteLine("Список событий заполнен.");
            }
            // Удалить событие из списка, 
            remove
            {
                int i;
                for (i = 0; i < 3; i++)
                    if (evnt[i] == value)
                    {
                        evnt[i] = null;
                        break;
                    }
                if (i == 3)
                    Debug.WriteLine("Обработчик событий не найден.");
            }
        }
        // Этот .метод вызывается для запуска событий, 
        public void OnSomeEvent()
        {
            for (int i = 0; i < 3; i++)
                if (evnt[i] != null)
                    evnt[i]();
        }
    }

    // Создать ряд классов, использующих делегат MyEventHandler. 
    class W
    {
        public void Whandler()
        {
            Debug.WriteLine("Событие получено объектом W");
        }
    }
    class X
    {
        public void Xhandler()
        {
            Debug.WriteLine("Событие получено объектом X");
        }
    }
    class Y
    {
        public void Yhandler()
        {
            Debug.WriteLine("Событие получено объектом Y");
        }
    }
    class Z
    {
        public void Zhandler()
        {
            Debug.WriteLine("Событие получено объектом Z");
        }
    }
}