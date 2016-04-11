using System;

namespace Patterns.Creational.Singleton._004_MultiSingleton
{
    public class Singleton2
    {
        static Singleton2 first;
        static Singleton2 second;
        static bool flag;
        public int ID { get; private set; }

        protected Singleton2(int id)
        {
            ID = id;
        }

        public static Singleton2 Instance()
        {
            if (first == null)
            {
                first = new Singleton2(1);
                return first;
            }
            else if (second == null)
            {
                second = new Singleton2(2);
                return second;
            }

            if (!flag)
            {
                flag = true;
                return first;
            }
            else
            {
                flag = false;
                return second;
            }
        }
    }
}
