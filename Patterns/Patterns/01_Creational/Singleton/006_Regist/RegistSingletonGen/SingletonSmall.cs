using System.Collections.Generic;

namespace Patterns._01_Creational.Singleton._006_Regist.RegistSingletonGen
{
    class SingletonSmall : Singleton
    {
        protected SingletonSmall() { }

        public static new SingletonSmall Instance()
        {
            try
            {
                return Lookup<SingletonSmall>();
            }
            catch (KeyNotFoundException)
            {
                Register(new SingletonSmall());
                return Lookup<SingletonSmall>();
            }
        }
    }
}
