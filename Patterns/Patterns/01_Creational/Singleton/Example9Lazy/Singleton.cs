using System;

namespace Patterns.Creational.Singleton.Example9
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
