using System.Diagnostics;
using System.Threading;

namespace Patterns._01_Creational.FactoryMethod._008_LazyInitialization
{
    public class BigObject2
    {
        private static BigObject2 instance;

        private BigObject2()
        {
            //Большая работа.
            Thread.Sleep(3000);
            Debug.WriteLine("Экземпляр BigObject создан");
        }

        public override string ToString()
        {
            return "Обращение к BigObject";
        }

        // Метод возвращает экземпляр BigObject, при этом он создаёт его, если тот ещё не существует.
        public static BigObject2 GetInstance()
        {
            // Исключение возможности создания двух объектов в многопоточном приложении.
            if (instance == null)
            {
                lock (typeof(BigObject2))
                {
                    if (instance == null)
                        instance = new BigObject2();
                }
            }

            return instance;
        }
    }
}
