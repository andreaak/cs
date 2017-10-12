using System;

namespace CSTest._05_Delegates_and_Events._03_Lambda._0_Setup
{
    public class TestButton
    {
        public event Action Click;

        public void OnClick()
        {
            Click?.Invoke();
        }
    }

    public class TestObj
    {
        public void Run()
        {

        }
    }
}