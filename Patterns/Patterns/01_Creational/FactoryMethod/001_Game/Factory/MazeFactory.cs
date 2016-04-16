namespace Patterns._01_Creational.FactoryMethod._001_Game.Factory
{
    // Класс MazeFactory создает компоненты лабиринта.
    class MazeFactory
    {
        // Конструктор.
        public MazeFactory()
        {
        }

        // Создание лабиринта.
        public virtual Maze MakeMaze()
        {
            return new Maze();
        }

        // Создание стенв.
        public virtual Wall.Wall MakeWall()
        {
            return new Wall.Wall();
        }

        // Создание комнаты.
        public virtual Room.Room MakeRoom(int number)
        {
            return new Room.Room(number);
        }

        // Создание двери.
        public virtual Door.Door MakeDoor(Room.Room room1, Room.Room room2)
        {
            return new Door.Door(room1, room2);
        }
    }
}
