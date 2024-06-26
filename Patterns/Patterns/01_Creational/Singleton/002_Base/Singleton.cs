namespace Patterns._01_Creational.Singleton._002_Base
{
    class Singleton
    {
        static Singleton uniqueInstance;
        string singletonData = string.Empty;

        protected Singleton()
        {
        }

        public static Singleton Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Singleton();

            return uniqueInstance;
        }

        public void SingletonOperation()
        {
            singletonData = "SingletonData";
        }

        public string GetSingletonData()
        {
            return singletonData;
        }
    }
}
