using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup.Accessors
{
    delegate void TestAccessorEventHandler();

    class AccessorEvent
    {
        TestAccessorEventHandler[] evnt = new TestAccessorEventHandler[3];
        public event TestAccessorEventHandler SomeEvent
        {
            // �������� ������� � ������, 
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
                    Debug.WriteLine("������ ������� ��������.");
            }
            // ������� ������� �� ������, 
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
                    Debug.WriteLine("���������� ������� �� ������.");
            }
        }
        // ���� .����� ���������� ��� ������� �������, 
        public void OnSomeEvent()
        {
            for (int i = 0; i < 3; i++)
                if (evnt[i] != null)
                    evnt[i]();
        }
    }

    // ������� ��� �������, ������������ ������� MyEventHandler. 
    class W
    {
        public void Whandler()
        {
            Debug.WriteLine("������� �������� �������� W");
        }
    }
    class X
    {
        public void Xhandler()
        {
            Debug.WriteLine("������� �������� �������� X");
        }
    }
    class Y
    {
        public void Yhandler()
        {
            Debug.WriteLine("������� �������� �������� Y");
        }
    }
    class Z
    {
        public void Zhandler()
        {
            Debug.WriteLine("������� �������� �������� Z");
        }
    }
}