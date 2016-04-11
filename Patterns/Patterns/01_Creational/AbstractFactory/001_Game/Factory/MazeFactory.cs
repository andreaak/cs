using System;


namespace Patterns.Creational.AbstractFactory._001_Game
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
