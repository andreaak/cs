using System;

namespace Patterns.Creational.Singleton._009_Lazy
{
    class Singleton
    {
        static Lazy<Singleton> instance = new Lazy<Singleton>();

        public static Singleton Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
