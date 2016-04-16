using Patterns._01_Creational.FactoryMethod._001_Game.Room;
using Patterns._01_Creational.FactoryMethod._001_Game.Wall;

namespace Patterns._01_Creational.FactoryMethod._001_Game.MazeGame
{
    class BombedMazeGame : MazeGame
    {
        // Конструктор.
        public BombedMazeGame()
        {
        }

        // Методы.

        public override Wall.Wall MakeWall()
        {
            return new BombedWall();
        }

        public override Room.Room MakeRoom(int number)
        {
            return new RoomWithBomb(number);
        }
    }
}
