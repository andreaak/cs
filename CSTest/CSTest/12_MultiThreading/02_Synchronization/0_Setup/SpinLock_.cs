using System;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._0_Setup
{
    public class SpinLock_
    {
        // Указывает выполняется ли блок кода потоком. 0 - блок свободен. 1 - блок занят.
        private int block;

        //  Интервал через который потоки проверяют переменную block.
        private readonly int wait;

        public SpinLock_(int wait)
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
            {
                // Блокировка занята, ожидать.
                Thread.Sleep(wait);
                result = Interlocked.CompareExchange(ref block, 1, 0);
            }
        }

        // Сбросить блокировку.
        public void Exit()
        {
            Interlocked.Exchange(ref block, 0);
        }
    }
}
