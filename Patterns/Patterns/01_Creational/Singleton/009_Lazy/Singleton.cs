using System;

namespace Patterns._01_Creational.Singleton._009_Lazy
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
