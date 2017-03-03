using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using NUnit.Framework;

namespace CSTest._24_Closure
{
    [TestFixture]
    public class Test
    {

        [Test]
        public void TestClosure1()
        {
            // Получить результат подсчета. 
            Func<int, int> count = Counter();
            int result;
            result = count(3);
            Debug.WriteLine("Сумма 3 равна " + result);
            result = count(5);
            Debug.WriteLine("Сумма 5 равна " + result);
            /*
            i = 0 sum = 0
            i = 1 sum = 1
            i = 2 sum = 3
            i = 3 sum = 6
            Сумма 3 равна 6
            i = 0 sum = 6
            i = 1 sum = 7
            i = 2 sum = 9
            i = 3 sum = 12
            i = 4 sum = 16
            i = 5 sum = 21
            Сумма 5 равна 21
            */
        }

        static Func<int, int> Counter()
        {
            int sum = 0;
            // Здесь подсчитанная сумма сохраняется в переменной sum. 
            Func<int, int> ctObj = delegate (int end)
            {
                for (int i = 0; i <= end; i++)
                {
                    sum += i;
                    Debug.WriteLine("i = {0} sum = {1}", i, sum);
                }
                return sum;
            };
            return ctObj;
        }

        [Test]
        public void TestClosure3()
        {
            int outerVariable = 5; //1 Внешняя переменная (незахваченная)
            string capturedVariable = "captured"; //2 Внешняя переменная, захваченная анонимным методом
            if (DateTime.Now.Hour == 23)
            {
                int normalLocalVariable = DateTime.Now.Minute; //3 Локальная переменная обычного метода
                Debug.WriteLine(normalLocalVariable);
            }

            MethodInvoker х = delegate
            {
                string anonLocal = "local to anonymous method"; //4 Локальная переменная анонимного метода
                Debug.WriteLine(capturedVariable + anonLocal); //5 Захват внешней переменной
            };
            х();
            /*
            capturedlocal to anonymous method
            */
        }

        [Test]
        public void TestClosure4()
        {
            string captured = "before х is created"; // перед созданием x
            MethodInvoker x = delegate
            {
                Debug.WriteLine(captured);
                captured = "changed by x";// изменено внутри x
            };
            captured = "directly before x is invoked";// непосредственно перед вызовом x
            x();
            Debug.WriteLine(captured);
            captured = "before second invocation";// после второго вызова
            x();
            /*
            directly before x is invoked
            changed by x
            before second invocation 
            */
        }

        [Test]
        public void TestClosure5()
        {
            List<MethodInvoker> list = new List<MethodInvoker>();
            for (int index = 0; index < 5; index++)
            {
                int counter = index * 10; //1 Создание экземпляра counter

                list.Add(delegate
                {
                    Debug.WriteLine(counter); //2 Вывод на консоль иинкрементирование захваченной переменной
                    counter++;
                });
            }

            foreach (MethodInvoker t in list)
            {
                t(); //3 Выполнение всех пяти экземпляров делегата
            }
            list[0](); //4 Трехкратное выполнение первого экземпляра делегата
            list[0]();
            list[0]();
            list[1](); //5 Однократное выполнение второго экземпляра делегата

            /*
            0
            10
            20
            30
            40
            1
            2
            3
            11 
            */
        }

        [Test]
        public void TestClosure6()
        {
            MethodInvoker[] delegates = new MethodInvoker[2];
            int outside = 0; //1 Создание экземпляра переменной один раз
            for (int i = 0; i < 2; i++)
            {
                int inside = 0; //2 Создание экземпляров переменной много раз
                delegates[i] = delegate //3 Захватывание переменных с помощью анонимного метода
                {
                    Debug.WriteLine("({0},{1})", outside, inside);
                    outside++;
                    inside++;
                };

            }

            MethodInvoker first = delegates[0];
            MethodInvoker second = delegates[1];

            first();
            first();
            first();
            second();
            second();
            /*
            (0,0)
            (1,1)
            (2,2)
            (3,0)
            (4,1) 
            */
        }

        [Test]
        public void TestClosure7()
        {
            MethodInvoker x = CreateDelegateInstance();
            x();
            x();

            /*
            5
            6
            7
            */
        }

        static MethodInvoker CreateDelegateInstance()
        {
            int counter = 5;
            MethodInvoker ret = delegate
            {
                Debug.WriteLine(counter);
                counter++;
            };

            ret();
            return ret;
        }
    }
}
