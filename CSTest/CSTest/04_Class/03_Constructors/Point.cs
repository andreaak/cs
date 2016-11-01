using System.Diagnostics;

namespace CSTest._04_Class._03_Constructors
{
    class Point
    {
        public static int staticField = InitStaticField();

        static Point()
        {
            Debug.WriteLine("Статический конструктор");
            staticField = 5;
        }
        private static int InitStaticField()
        {
            Debug.WriteLine("InitStaticField Метод");
            return 3;
        }

        // Поля.
        private int x;
        private int y;
        private string name;

        // Свойства.
        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public string Name
        {
            get { return name; }
        }

        // Конструктор по умолчанию, инициализирует поля значениями по умолчанию.
        public Point()
        {
            Debug.WriteLine("Конструктор по умолчанию");
        }

        // Пользовательский конструктор, инициализирует поля значениями заданными пользователем.
        // ВАЖНО: 
        // Если вы создали пользовательский конструктор (принимающий аргументы),
        // то конструктор по умолчанию, автоматически создаваться НЕ БУДЕТ, его придется создать явно.
        // Использование ключевого слова this в конструкторе  с одним параметром,
        // приводит к вызову этого конструктора.
        public Point(int x, int y)
        {
            Debug.WriteLine("Пользовательский конструктор");
            this.x = x;
            this.y = y;
        }

        // Конструктор может вызывать в том же самом объекте другой конструктор с помощью ключевого слова this.
        // Использование ключевого слова this в конструкторе приводит к вызову конструктора с двумя параметрами.
        public Point(string name)
            : this(300, 400)
        {
            Debug.WriteLine("Конструктор с одним параметром.");
            this.name = name;
        }
    }
}
