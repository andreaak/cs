using System.Collections.Generic;

namespace Patterns._01_Creational.Singleton._006_Regist.RegistSingletonGen
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
