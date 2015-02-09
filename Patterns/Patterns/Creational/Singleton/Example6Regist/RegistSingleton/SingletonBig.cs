
namespace Patterns.Creational.Singleton.Example6.RegistSingleton
{
    class SingletonBig : Singleton
    {
        protected SingletonBig() { }

        public static new SingletonBig Instance()
        {
             instance = Lookup(SingletonName.Big);

            if (instance == null)
            {
                Register(SingletonName.Big, new SingletonBig());
                instance = Lookup(SingletonName.Big);
            }

            return instance as SingletonBig;
        }
    }
}
