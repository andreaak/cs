using System;
using System.Diagnostics;
using CSTest._05_Delegates_and_Events._03_Lambda._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._03_Lambda
{
    [TestFixture]
    public class _01_LambdaTest
    {
        [Test]
        public void TestLambda1()
        {
            Action action = delegate ()
            {
                Debug.WriteLine("Test delegate Without Parameter");
            };
            action();


            Action action2 = () => Debug.WriteLine("Test Lambda Without Parameter");
            action2();

            Func<int, bool> action3 = delegate (int i)
            {
                Debug.WriteLine("Test delegate With Parameter: " + i);
                return true;
            };
            Debug.WriteLine("Result delegate: " + action3(5));


            Func<int, bool> action4 = (i) =>
            {
                Debug.WriteLine("Test Lambda With Parameter: " + i);
                return false;
            };
            Debug.WriteLine("Result Lambda: " + action4(7));

            /*
            Test delegate Without Parameter
            Test Lambda Without Parameter
            Test delegate With Parameter: 5
            Result delegate: True
            Test Lambda With Parameter: 7
            Result Lambda: False
            */
        }

        //[Test]
        //public void TestLambda1Compiled()
        //{
        //    // ISSUE: method pointer
        //    (Generated.Action1 ?? (Generated.Action1 = new Action((object)Generated.GeneratedObject, __methodptr(TestLambda1_0))))();
        //    // ISSUE: method pointer
        //    (Generated.Action2 ?? (Generated.Action2 = new Action((object)Generated.GeneratedObject, __methodptr(TestLambda1_1))))();
        //    // ISSUE: method pointer
        //    Debug.WriteLine("Result delegate: " + (Generated.Func1 ??
        //        (Generated.Func1 = new Func<int, bool>((object)Generated.GeneratedObject, __methodptr(TestLambda1_2))))(5).ToString());
        //    // ISSUE: method pointer
        //    Debug.WriteLine("Result Lambda: " + (Generated.Func2 ??
        //        (Generated.Func2 = new Func<int, bool>((object)Generated.GeneratedObject, __methodptr(TestLambda1_3))))(7).ToString());
        //}

        //[CompilerGenerated]
        //[Serializable]
        //private sealed class Generated
        //{
        //    public static readonly Generated GeneratedObject;
        //    public static Action Action1;
        //    public static Action Action2;
        //    public static Func<int, bool> Func1;
        //    public static Func<int, bool> Func2;
        //    public static LambdaEventHandler LambdaEventHandler_0;
        //    public static LambdaEventHandler LambdaEventHandler_1;
        //    public static EventHandler<KeyEventArgs> EventHandler_0;

        //    static Generated()
        //    {
        //        GeneratedObject = new Generated();
        //    }

        //    internal void TestLambda1_0()
        //    {
        //        Debug.WriteLine("Test delegate Without Parameter");
        //    }

        //    internal void TestLambda1_1()
        //    {
        //        Debug.WriteLine("Test Lambda Without Parameter");
        //    }

        //    internal bool TestLambda1_2(int i)
        //    {
        //        Debug.WriteLine("Test delegate With Parameter: " + (object)i);
        //        return true;
        //    }

        //    internal bool TestLambda1_3(int i)
        //    {
        //        Debug.WriteLine("Test Lambda With Parameter: " + (object)i);
        //        return false;
        //    }

        //    internal void TestLambda2InEvents_0(int n)
        //    {
        //        Debug.WriteLine("Событие от лямбды получено. Значение равно " + (object)n);
        //    }

        //    internal void TestLambda2InEvents_1(int n)
        //    {
        //        Debug.WriteLine("Событие от анонимного метода получено. Значение равно " + (object)n);
        //    }

        //    internal void TestLambda3Events_0(object sender, KeyEventArgs e)
        //    {
        //        Debug.WriteLine("Получено сообщение о нажатии клавиши: " + e.ch.ToString());
        //    }
        //}

        [Test]
        public void TestLambda2InEvents()
        {
            _01_Lambda evt = new _01_Lambda();

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

        //[Test]
        //public void TestLambda2InEventsCompiled()
        //{
        //    _04_Lambda obj = new _04_Lambda();
        //    // ISSUE: method pointer
        //    obj.SomeEvent += Generated.LambdaEventHandler_0 ?? 
        //        (Generated.LambdaEventHandler_0 = new LambdaEventHandler((object)Generated.GeneratedObject, __methodptr(TestLambda2InEvents_0)));
        //    // ISSUE: method pointer
        //    obj.SomeEvent += Generated.LambdaEventHandler_1 ?? 
        //        (Generated.LambdaEventHandler_1 = new LambdaEventHandler((object)Generated.GeneratedObject, __methodptr(TestLambda2InEvents_1)));
        //    obj.OnSomeEvent(1);
        //    obj.OnSomeEvent(2);
        //}

        [Test]
        public void TestLambda3Events()
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
        public void TestLambda4Events()
        {
            _01_Lambda obj = new _01_Lambda();
            int index = 5;
            LambdaEventHandler h = (i) => SomeMethod(index, i);
            obj.SomeEvent += h;

            _01_Lambda obj2 = new _01_Lambda();
            int index2 = 4;
            LambdaEventHandler h2 = (i) => SomeMethod(index2, i);
            obj2.SomeEvent += h2;

            obj.OnSomeEvent(7);
            obj2.OnSomeEvent(8);

            obj.SomeEvent -= h;
            obj2.SomeEvent -= h2;

            obj.OnSomeEvent(7);
            obj2.OnSomeEvent(8);

            /*
            Index: 5. Var: 7
            Index: 4. Var: 8
            */
        }

        [Test]
        public void TestLambda5Events()
        {
            _01_Lambda2[] objs = new _01_Lambda2[2];
            objs[0] = new _01_Lambda2();
            objs[1] = new _01_Lambda2();

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
