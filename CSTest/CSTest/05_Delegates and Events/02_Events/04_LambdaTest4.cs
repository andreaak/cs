using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    [TestFixture]
    public class _04_LambdaTest4
    {
        [Test]
        public void TestEventsLambda1()
        {
            LambdaEvent evt = new LambdaEvent();

            // Использовать лямбда-выражение в качестве обработчика событий, 
            evt.SomeEvent += (n) => Debug.WriteLine("Событие от лямбды получено. Значение равно " + n);

            // Использовать анонимный метод в качестве обработчика событий, 
            evt.SomeEvent += delegate (int n)
            {
                Debug.WriteLine("Событие от анонимного метода получено. Значение равно " + n);
            };

            // Запустить событие, 
            evt.OnSomeEvent(1);
            evt.OnSomeEvent(2);

            /*
            Событие от лямбды получено. Значение равно 1
            Событие от анонимного метода получено. Значение равно 1
            Событие от лямбды получено. Значение равно 2
            Событие от анонимного метода получено. Значение равно 2
            */
        }

[Test]
public void TestEventsLambda2()
{
    KeyEvent kevt = new KeyEvent();
    ConsoleKeyInfo key;
    int count = 0;
            
    // Использовать лямбда-выражение для отображения факта нажатия клавиши. 
    kevt.KeyPress += (sender, e) => Debug.WriteLine("Получено сообщение о нажатии клавиши: " + e.ch);
            
    // Использовать лямбда-выражение для подсчета нажатых клавиш. 
    kevt.KeyPress += (sender, e) => count++; // count — это внешняя переменная 

    Debug.WriteLine("Введите несколько символов. По завершении введите точку.");
    do
    {
        key = new ConsoleKeyInfo('.', ConsoleKey.O, false, false, false);
        kevt.OnKeyPress(key.KeyChar);
    } while (key.KeyChar != '.');
    Debug.WriteLine("Было нажато " + count + " клавиш.");

    /*
    Введите несколько символов. По завершении введите точку.
    Получено сообщение о нажатии клавиши: .
    Было нажато 1 клавиш.
    */
}

        delegate void LambdaEventHandler(int n);
        // Объявить класс, содержащий событие, 
        class LambdaEvent
        {
            public event LambdaEventHandler SomeEvent;
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
}
