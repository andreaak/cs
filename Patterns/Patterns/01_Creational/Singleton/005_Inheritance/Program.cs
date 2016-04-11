using System;

namespace Patterns.Creational.Singleton._005_Inheritance
{
    class BaseSingleton
    {
        protected static BaseSingleton uniqueInstance;

        protected BaseSingleton() { }

        public static BaseSingleton Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new BaseSingleton();

            return uniqueInstance;
        }
    }

    class DerivedSingleton : BaseSingleton
    {
        protected DerivedSingleton() { }

        public new static DerivedSingleton Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DerivedSingleton();

            return uniqueInstance as DerivedSingleton;
        }
    }
}
