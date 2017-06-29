using System;
using System.Diagnostics;
using CSTest._05_Delegates_and_Events._02_Events._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    [TestFixture]
    public class _04_LambdaTest
    {
        [Test]
        public void TestEventsLambda1()
        {
            _04_Lambda evt = new _04_Lambda();

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

        [Test]
        public void TestEventsLambda3()
        {
            _04_Lambda obj = new _04_Lambda();
            int index = 5;
            LambdaEventHandler h = (i) => SomeMethod(index, i);
            obj.SomeEvent += h;

            _04_Lambda obj2 = new _04_Lambda();
            int index2 = 4;
            LambdaEventHandler h2 = (i) => SomeMethod(index2, i);
            obj2.SomeEvent += h2;



            obj.SomeEvent -= h;
            obj2.SomeEvent -= h2;


            _04_Lambda2[] objs = new _04_Lambda2[2];
            objs[0] = new _04_Lambda2();
            objs[1] = new _04_Lambda2();

            Action<int>[] acts = new Action<int>[2];
            for (int i = 0; i < 2; i++)
            {
                int index3 = i;
                acts[i] = (j) => SomeMethod(index3, j);
                objs[i].SomeEvent += acts[i];
            }

            for (int i = 0; i < 2; i++)
            {
                objs[i].OnSomeEvent(i + 9);
            }

            for (int i = 0; i < 2; i++)
            {
                objs[i].SomeEvent -= acts[i];
            }

            /*
            Index: 0. Var: 9
            Index: 1. Var: 10
            */
        }

        private void SomeMethod(int index, int i)
        {
            Debug.WriteLine("Index: {0}. Var: {1}", index, i);
        }
    }
}
