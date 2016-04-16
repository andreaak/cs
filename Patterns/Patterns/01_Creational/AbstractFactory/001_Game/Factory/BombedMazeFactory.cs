using Patterns._01_Creational.AbstractFactory._001_Game.Room;
using Patterns._01_Creational.AbstractFactory._001_Game.Wall;

namespace Patterns._01_Creational.AbstractFactory._001_Game.Factory
{
    // Фабрика для создания комнат с бомбой.
    class BombedMazeFactory : MazeFactory
    {
        // Метод создает взорваные стены.
        public override Wall.Wall MakeWall()
        {
            return new BombedWall();
        }

        // Метод создает комнату с бомбой.
        public override Room.Room MakeRoom(int number)
        {
            return new RoomWithBomb(number);
        }
    }
}
