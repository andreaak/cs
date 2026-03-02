using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._09_Parallel
{
#if CS5
    /*
    
    */

    [TestFixture]
    public class _01_Test
    {
        private static object syncRoot = new object();

        [Test]
        public async Task TestParallel1()
        {
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.TaskScheduler = new ParallelTaskScheduler();
            parallelOptions.MaxDegreeOfParallelism = 2;

            Action a1 = () => WriteChar('!');
            Action a2 = () => WriteChar('@');
            Action a3 = () => WriteChar('#');
            Action a4 = () => WriteChar('$');

            Parallel.Invoke(parallelOptions, a1, a2, a3, a4);
            //Parallel.Invoke(a1, a2, a3, a4);

            Debug.WriteLine("Метод Main завершен");

            /*
            Синхронное выполнение задачи 1
            Параллельное выполнение задачи 2
            Id задачи 1, id потока: 16. Поток из ThreadPool - False. 
            Id задачи 2, id потока: 10. Поток из ThreadPool - True. 
            !@!@!@!@!@@!!@!@@!!@
            Id задачи 1, id потока: 16. Поток из ThreadPool - False. 
            Id задачи 2, id потока: 10. Поток из ThreadPool - True. 
            $##$#$#$#$#$#$$#$##$
            Метод Main завершен
            */
        }

        [Test]
        public async Task TestParallel2()
        {
            Debug.WriteLine("У Вашего процессора {0} ядер.", Environment.ProcessorCount);

            Stopwatch sw = new Stopwatch();
            int[] values = new int[5] { 1, 2, 3, 4, 5 };

            double[] data = new double[10_000_000];

            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cts.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            sw.Start();
            // Параллельный вариант инициализации массива в цикле.
            //       for (int i = 0; i < data.Length; i++)
            Parallel.For(0, data.Length, parallelOptions, (i) =>
            {
                // Имитация сложных вычислений...
                double element = i * values.Sum() * 250 * 350 * 450;
                data[i] = element;
            });
            sw.Stop();

            Debug.WriteLine($"Параллельно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}.");
            sw.Reset();

            double[] data2 = new double[10_000_000];
            sw.Start();
            // Последовательный вариант инициализации массива в цикле.
            for (int i = 0; i < data2.Length; i++)
            {
                // Имитация сложных вычислений...
                double element = i * values.Sum() * 250 * 350 * 450;
                data2[i] = element;
            }
            sw.Stop();

            Debug.WriteLine($"Последовательно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}");

            // Delay.
            Thread.Sleep(1000);

            /*
            У Вашего процессора 8 ядер.
            Параллельно выполняемый цикл инициализации: 822.
            Последовательно выполняемый цикл инициализации: 1 155
            */
        }

        [Test]
        public async Task TestParallel3Break()
        {
            Random random = new Random();
            int breakIndex = random.Next(50, 80);
            int[] data = new int[100];

            Debug.WriteLine($"BreakIndex: {breakIndex}");

            Action<int, ParallelLoopState> loopBody = (i, state) =>
            {
                Thread.Sleep(random.Next(1, 150));

                if (state.ShouldExitCurrentIteration == true)
                {
                    if (state.LowestBreakIteration != null)
                    {
                        Debug.WriteLine($"Log: Был использован метод Break(). Прерываю итерацию #{i}");
                    }
                    else if (state.IsStopped == true)
                    {
                        Debug.WriteLine($"Log: Был использован метод Stop(). Прерываю итерацию #{i}");
                    }
                    else if (state.IsExceptional == true)
                    {
                        Debug.WriteLine($"Log: Было выброшено исключение. Прерываю итерацию #{i}");
                    }

                    return;
                }

                if (i == breakIndex)
                {
                    state.Break();
                    //state.Stop();
                    //throw new Exception($"Ошибка в параллельной итерации #{i}");
                }

                data[i] = i;
            };

            try
            {
                ParallelLoopResult loopResult = Parallel.For(0, data.Length, loopBody);

                if (loopResult.IsCompleted == false)
                {
                    string breakInfo = loopResult.LowestBreakIteration == null ? "" : $"Цикл прерван на {loopResult.LowestBreakIteration} итерации.";
                    Debug.WriteLine($"Параллельный цикл завершился преждевременно. {breakInfo}");
                }
                else
                {
                    Debug.WriteLine($"Параллельный цикл завершен успешно.");
                }
            }
            catch (AggregateException ex)
            {
                Debug.WriteLine("");
                Debug.WriteLine(new string('-', 80));
                Debug.WriteLine($"Произошли ошибки при выполнение цикла:\n");
                foreach (var exception in ex.InnerExceptions)
                {
                    Debug.WriteLine($"Ошибка {exception.GetType()}");
                    Debug.WriteLine($"Сообщение: {exception.Message}");
                    Debug.WriteLine(new string('-', 80));
                }
            }

            Debug.WriteLine($"\nРезультаты выполнения параллельного цикла: ");
            foreach (var item in data)
            {
                Debug.Write($"{item} ");
            }
            Thread.Sleep(2000);
            /*
            BreakIndex: 54
            Log: Был использован метод Break(). Прерываю итерацию #67
            Log: Был использован метод Break(). Прерываю итерацию #92
            Log: Был использован метод Break(). Прерываю итерацию #79
            Параллельный цикл завершился преждевременно. Цикл прерван на 54 итерации.

            Результаты выполнения параллельного цикла: 
            0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 
            36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 0 0 0 0 0 60 61 62 63 64 65 66 0 0 0 0 0 
            72 73 74 75 76 77 78 0 0 0 0 0 84 85 86 87 88 89 90 91 0 0 0 0 0 0 0 0 
            */
        }


        [Test]
        public async Task TestParallel4()
        {
            string phrase = "The quick brown fox jumps over the lazy dog";
            string[] words = phrase.ToLower().Split(' ');

            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cts.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Func<List<char>> localInit = () => new List<char>();

            Func<int, ParallelLoopState, List<char>, List<char>> loopBody = (index, loopState, localList) =>
            {
                if (loopState.ShouldExitCurrentIteration == true)
                    return localList;

                for (int j = 0; j < words[index].Length; j++)
                    if (localList.Contains(words[index][j]) == false)
                        localList.Add(words[index][j]);

                return localList;
            };

            List<char> alphabet = new List<char>();
            Action<List<char>> localFinally = (localList) =>
            {
                lock (alphabet)
                {
                    foreach (var item in localList)
                    {
                        if (alphabet.Contains(item) == false)
                            alphabet.Add(item);
                    }
                }
            };




            try
            {
                Parallel.For(0, words.Length, parallelOptions, localInit, loopBody, localFinally);

                const int englishLetterCount = 26;
                Debug.WriteLine($"Количество букв в английском алфавите - {englishLetterCount}.");
                Debug.WriteLine($"Наша коллекция нашла {alphabet.Count} уникальных символов.");

                string result = alphabet.Count == englishLetterCount ? "Фраза является панграммой. " : "Фраза не является панграммой. ";

                Debug.WriteLine($"{result}Найденные буквы:\n");
                foreach (var letter in alphabet.OrderBy(x => x))
                {
                    Debug.Write($"{letter} ");
                }
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"{ex.GetType()}");
                Debug.WriteLine($"{ex.Message}");
            }

            Thread.Sleep(2000);
            /*
            Количество букв в английском алфавите - 26.
            Наша коллекция нашла 26 уникальных символов.
            Фраза является панграммой. Найденные буквы:

            a b c d e f g h i j k l m n o p q r s t u v w x y z 
            */
        }

        private static void WriteChar(char ch)
        {
            Debug.WriteLine($"Id задачи {Task.CurrentId}, id потока: {Thread.CurrentThread.ManagedThreadId}. Поток из ThreadPool - {Thread.CurrentThread.IsThreadPoolThread}. ");
            Thread.Sleep(500);

            for (int i = 0; i < 10; i++)
            {
                lock (syncRoot)
                {
                    Debug.Write(ch);
                }
                Thread.Sleep(100);
            }
        }

    }


    class ParallelTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks() => null;

        protected override void QueueTask(Task task)
        {
            Debug.WriteLine($"Параллельное выполнение задачи {task.Id}");

            ThreadPool.QueueUserWorkItem((_) =>
            {
                base.TryExecuteTask(task);
            });
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Debug.WriteLine($"Синхронное выполнение задачи {task.Id}");
            return base.TryExecuteTask(task);
        }
    }

#endif
}