using System;
using System.Collections.Generic;

namespace Patterns.Creational.Singleton.Example6.RegistSingletonGen
{
    class SingletonBig : Singleton
    {
        protected SingletonBig() { }

        public static new SingletonBig Instance()
        {
            try
            {
                return Lookup<SingletonBig>();
            }
            catch (KeyNotFoundException)
            {
                Register(new SingletonBig());
                return Lookup<SingletonBig>();
            }
        }
    }
}
