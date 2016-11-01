using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User._05_Interlocked
{
    public class _05_InterlockedSpinLock
    {
        // Указывает выполняется ли блок кода потоком. 0 - блок свободен. 1 - блок занят.
        private int block;

        //  Интервал через который потоки проверяют переменную block.
        private readonly int wait;

        public _05_InterlockedSpinLock(int wait)
        {
            this.wait = wait;
        }

        // Установить блокировку.
        public void Enter()
        {
            // Метод CompareExchange() 
            // 1. Сравнивает начальное значение первого аргумента с третьим аргументом.
            // 2. Если первый аргумент равен третьему аргументу, то в первый аргумент записывается значение второго аргумента.
            // 3. Иначе, если первый аргумент не равен третьему аргументу, то первый аргумент остается без изменения.
            // 4. Возвращает начальное значение первого аргумента (в любом случае).
            int result = Interlocked.CompareExchange(ref block, 1, 0);

            while (result == 1)
            {                // Блокировка занята, ожидать.
                Thread.Sleep(wait);
                result = Interlocked.CompareExchange(ref block, 1, 0);
            }
            Debug.WriteLine("Блокировка получена. Thread = " + Thread.CurrentThread.GetHashCode());
        }

        // Сбросить блокировку.
        public void Exit()
        {
            Debug.WriteLine("Блокировка снята. Thread = " + Thread.CurrentThread.GetHashCode());
            Interlocked.Exchange(ref block, 0);
        }
    }
}
