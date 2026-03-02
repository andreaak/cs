using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
        
    [TestFixture]
    internal class _14_TestReflectedTaskResult
    {
        //private static void Main()
        //{
        //    int x = 3, y = 5;

        //    Task<int> additionTask = AdditionAsync("[асинхронно]", x, y);

        //    int syncSum = Addition("[синхронно]", x, y);

        //    int asyncSum = 0;

        //    // Разные способы получения результата из асинхронной задачи:
        //    asyncSum = additionTask.Result;
        //    //asyncSum = additionTask.GetAwaiter().GetResult();
        //    //asyncSum = await additionTask;
        //    Console.WriteLine($"\nРезультат асинхронного выполнения: {asyncSum}.");
        //    Console.WriteLine($"Результат синхронного выполнения: {syncSum}.");

        //    Console.WriteLine($"Метод Main завершил свою работу");
        //    Console.ReadKey();
        //}

        //private static int Addition(string operationName, int x, int y)
        //{
        //    Console.WriteLine($"Метод Addition вызван {operationName} в потоке: {Thread.CurrentThread.ManagedThreadId}");
        //    // Thread.Sleep - имитация нагруженной и тяжелой работы.
        //    Thread.Sleep(3000);
        //    return x + y;
        //}

        //private static async Task<int> AdditionAsync(string operationName, int x, int y)
        //{
        //    // Первый способ

        //    int result = await Task.Run<int>(() => Addition(operationName, x, y));
        //    return result;

        //    // ---------------------------------------------- //

        //    // Второй способ

        //    //return await Task.Run<int>(() => Addition(operationName, x, y));

        //    // ---------------------------------------------- //

        //    // Ошибочный способ
        //    //return Task.Run<int>(() => Addition(operationName, x, y));
        //}

        [Test]
        public void TestRefl1()
        {
            int x = 3;
            int y = 5;
            Task<int> task = AdditionAsync("[асинхронно]", x, y);
            int num = Addition("[синхронно]", x, y);
            Debug.WriteLine(string.Format("\nРезультат асинхронного выполнения: {0}.", (object)task.Result));
            Debug.WriteLine(string.Format("Результат синхронного выполнения: {0}.", (object)num));
            Debug.WriteLine("Метод Main завершил свою работу");
            Thread.Sleep(3000);
        }

        private static int Addition(string operationName, int x, int y)
        {
            Debug.WriteLine(string.Format("Метод Addition вызван {0} в потоке: {1}", (object)operationName, (object)Thread.CurrentThread.ManagedThreadId));
            Thread.Sleep(3000);
            return x + y;
        }

        [AsyncStateMachine(typeof(AdditionAsyncStateMachine))]
        private static Task<int> AdditionAsync(string operationName, int x, int y)
        {
            // Создание экземпляра конечного автомата
            AdditionAsyncStateMachine stateMachine = new AdditionAsyncStateMachine();
            // Заполнение полей конечного автомата
            stateMachine.operationName = operationName;
            stateMachine.x = x;
            stateMachine.y = y;
            stateMachine.builder = AsyncTaskMethodBuilder<int>.Create();
            stateMachine.state = -1;
            // Запуск конечного автомата (внутри вызов метода MoveNext())
            stateMachine.builder.Start<AdditionAsyncStateMachine>(ref stateMachine);
            return stateMachine.builder.Task;
        }

        /// <summary>
        /// Класс, который представляет лямбда-выражение () => Addition(operationName, x, y)
        /// </summary>
        [CompilerGenerated]
        private sealed class DisplayClass
        {
            /// <summary>
            /// Захваченный параметр в лямбда-выражение.
            /// </summary>
            public string operationName;
            /// <summary>
            /// Захваченный параметр в лямбда-выражение.
            /// </summary>
            public int x;
            /// <summary>
            /// Захваченный параметр в лямбда-выражение.
            /// </summary>
            public int y;

            public DisplayClass()
                    : base()
            {
            }

            /// <summary>
            /// Тело лямбда-выражения.
            /// </summary>
            internal int AdditionAsync()
            {
                return _14_TestReflectedTaskResult.Addition(this.operationName, this.x, this.y);
            }
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct AdditionAsyncStateMachine : IAsyncStateMachine
        {
            // Открытые поля конечного автомата для их заполнения при создании экземпляра.
            public int state;
            public AsyncTaskMethodBuilder<int> builder;
            public string operationName;
            public int x;
            public int y;
            // Закрытые поля конечного автомата для сохранения значений локальных переменных метода при приостановке.
            private TaskAwaiter<int> awaiter;

            /// <summary>
            /// Метод выполняет тело асинхронного метода. Изменяет состояние конечного автомата при шаге.
            /// </summary>
            void IAsyncStateMachine.MoveNext()
            {
                // Получение состояния конечного автомата из поля state.
                int num1 = this.state;
                // Создание переменной для результата асинхронной операции.
                int result;
                try
                {
                    // Создание объекта ожидания завершения асинхронной задачи
                    TaskAwaiter<int> awaiter;
                    int num2;
                    if (num1 != 0)
                    {
                        // Создание экземпляра класса, который представляет лямбда-выражение: () => Addition(operationName, x, y).
                        _14_TestReflectedTaskResult.DisplayClass displayClass = new _14_TestReflectedTaskResult.DisplayClass();
                        displayClass.operationName = this.operationName;
                        displayClass.x = this.x;
                        displayClass.y = this.y;

                        // Работа оператора await:
                        awaiter = Task.Run<int>(new Func<int>(displayClass.AdditionAsync)).GetAwaiter();
                        // Проверка: завершилась ли работа задачи.
                        if (!awaiter.IsCompleted)
                        {
                            // Перевод состояния конечного автомата в ожидающее.
                            // Любое значение отличное от "-1" и "-2" означает, что конечный автомат ожидает завершения 
                            // асинхронной операции.
                            this.state = num2 = 0;
                            this.awaiter = awaiter;
                            // Метод создаст и установит продолжение для асинхронной задачи. 
                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, _14_TestReflectedTaskResult.AdditionAsyncStateMachine>(ref awaiter, ref this);
                            // Возврат управления "вызывающему потоку".
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.awaiter;
                        this.awaiter = new TaskAwaiter<int>();
                        // Значение "-1" для состояния также означает, что конечный автомат выполняется.
                        this.state = num2 = -1;
                    }
                    // Завершения ожидания асинхронной задачи. Получение результата асинхронной операции.
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                    this.state = -2;
                    // Установка полученного исключения в задачу-марионетку
                    this.builder.SetException(ex);
                    return;
                }
                // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                this.state = -2;
                // Установка успешного выполнения задачи-марионетки
                this.builder.SetResult(result);
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.builder.SetStateMachine(stateMachine);
            }
        }
    }
}
