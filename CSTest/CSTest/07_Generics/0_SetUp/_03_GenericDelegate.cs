namespace CSTest._07_Generics._0_Setup
{
    // Универсальные шаблоны. (Универсальный делегат)

    /* Cоздаем класс-делегата с именем  MyDelegate, параметризированный двумя 
    Указателями Места Заполнения Типом - Т и R,
    метод который будет сообщен с экземпляром данного класса-делегата, 
    будет принимать один аргумент, типа Указателя Места Заполнения Типом - Т,
    и возвращать значение типа Указателя Места Заполнения Типом - R.*/
    
    delegate R GenericDelegate<T, R>(T t);

    class _03_GenericDelegate
    {

        public static int Add(int i)
        {
            return ++i;
        }

        public static string Concatenation(string s)
        {
            return "Hello " + s + "!";
        }
    }
}
