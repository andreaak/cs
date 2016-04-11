using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Patterns.Creational.Singleton._001_Game
{
    // Класс MazeFactory, единственный экземпляр которого создает компоненты лабиринта.
    class MazeFactory
    {
        static MazeFactory instance = null;       
        
        //Метод, который производит десериализацию свойства "MAZESTYLE" из файла окружения
        static string GetEnv(string propertyName)
        {
            // Тип для Сериализации и Десериализации.
            XmlSerializer serializer = new XmlSerializer(typeof(Properties));   
            // Тип для хранения свойств  лабиринта в файле окружения
            Properties properties = new Properties();
            string propertyValue = null;
            try
            {
                FileStream stream = new FileStream("LabirintEnvironment.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                
                // Восстанавливаем объект из XML-файла.
                properties = serializer.Deserialize(stream) as Properties;

                // Объект десериализован!
                if (propertyName == "MAZESTYLE")
                {
                    propertyValue = properties.MAZESTYLE;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return propertyValue;
        }

        public static MazeFactory Instance()
        {
            if (instance == null)
            {
                // Берем значение свойства MAZESTYLE из файла окружения
                string mazeStyle = GetEnv("MAZESTYLE");

                if (string.Compare(mazeStyle, "bombed") == 0) // 0 - совпадают, 1 - не совпадают
                {
                    Debug.WriteLine("Инстанцируем фабрику для лабиринта с бомбами");
                    instance = new BombedMazeFactory();
                }
                else if (string.Compare(mazeStyle, "enchanted") == 0)
                {
                    Debug.WriteLine("Инстанцируем фабрику для лабиринта с заклинаниями");
                    instance = new EnchantedMazeFactory();
                }
                else // По умолчанию.
                {
                    Debug.WriteLine("Инстанцируем фабрику для обычного лабиринта");
                    instance = new MazeFactory();
                }
            }
            return instance;
        }

        protected MazeFactory()
        {
        }

        // Создание лабиринта.
        public virtual Maze MakeMaze()
        {
            return new Maze();
        }

        // Создание стены.
        public virtual Wall MakeWall()
        {
            return new Wall();
        }

        // Создание комнаты.
        public virtual Room MakeRoom(int number)
        {
            return new Room(number);
        }

        // Создание двери.
        public virtual Door MakeDoor(Room room1, Room room2)
        {
            return new Door(room1, room2);
        }
    }
}
