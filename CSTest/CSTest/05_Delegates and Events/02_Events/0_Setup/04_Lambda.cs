using System;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup
{

    delegate void LambdaEventHandler(int n);
    // Объявить класс, содержащий событие, 
    class _04_Lambda
    {
        public event LambdaEventHandler SomeEvent;
        // Этот метод вызывается для запуска события, 
        public void OnSomeEvent(int n)
        {
            if (SomeEvent != null)
                SomeEvent(n);
        }
    }

    // Объявить класс, содержащий событие, 
    class _04_Lambda2
    {
        public event Action<int> SomeEvent;
        // Этот метод вызывается для запуска события, 
        public void OnSomeEvent(int n)
        {
            if (SomeEvent != null)
                SomeEvent(n);
        }
    }

    // Пример обработки событий, связанных с нажатием клавиш на клавиатуре. 
    // Создать класс, производный от класса EventArgs и 

    // хранящий символ нажатой клавиши. 
    class KeyEventArgs : EventArgs
    {
        public char ch;
    }

    // Объявить класс события, связанного с нажатием клавиш на клавиатуре, 
    class KeyEvent
    {
        public event EventHandler<KeyEventArgs> KeyPress;
        // Этот метод вызывается при нажатии клавиши, 
        public void OnKeyPress(char key)
        {
            KeyEventArgs k = new KeyEventArgs();
            if (KeyPress != null)
            {
                k.ch = key;
                KeyPress(this, k);
            }
        }
    }

}
